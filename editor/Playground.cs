using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using EditorProject.Calculator;
using EditorProject.Calculator.Abstractions;
using EditorProject.Calculator.Operations;
using EditorProject.Graphics;
using EditorProject.Graphics.Primitives;
using EditorProject.Graphics.Primitives.Abstractions;
using EditorProject.TextEditor.Extensions;
using EditorProject.TextEditor.Persistence;
using Rectangle = EditorProject.Graphics.Primitives.Rectangle;

namespace EditorProject
{
    public partial class Playground : Form
    {
        private readonly StateMachine _stm;


        // graphics 
        private Bitmap _bitmap;
        private GraphicContext _context;
        private PointPrimitive _currentPenContext;
        private readonly IList<IDrawable> _drawablePool;
        private readonly Pen _drawPen;

        private bool _isPenDown;
        private IReadOnlyList<Operation> _symbolMapping;
        private bool _updateRequired;
        private float _currentHeight = 10;
        private Dictionary<Type, Func<int, int, PointPrimitive>> _toolsActivator;
        private Func<int, int, PointPrimitive> _currentTool;

        public Playground()
        {
            InitializeComponent();

            _stm = new StateMachine();

            CreatePlayground();

            InitializeTextEditor();
            InitializeCalculatorControls();

            Load += Playground_Load;
            Closing += Playground_Closing;
            SizeChanged += Playground_SizeChanged;
            drawingCanvas.MouseDown += DrawingCanvas_MouseDown;
            drawingCanvas.MouseUp += DrawingCanvas_MouseUp;
            drawingCanvas.MouseMove += DrawingCanvas_MouseMove;
            drawingCanvas.Paint += (_, e) =>
                e.Graphics.DrawImage(_bitmap, 0, 0);

            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.UserPaint, true);

            InitModelAndFrameTick();
            _drawablePool = new List<IDrawable>();
            _currentPenContext = new Curve();
            _drawPen = Pens.Blue;

            _toolsActivator = new Dictionary<Type, Func<int, int, PointPrimitive>>
            {
                {typeof(Circle), (x, y) => new Circle(x, y, _currentHeight)},
                {
                    typeof(Rectangle), (x, y) => new Rectangle(x, y,
                        (int) _currentHeight, (int) _currentHeight)
                },
                {typeof(Triangle), (x, y) => new Triangle(x, y, _currentHeight)},
                {typeof(Curve), (x, y) => new Curve(new List<Point> {new() {X = x, Y = y}})},
            };

            SelectDrawingTool("Curve");

            foreach (var control in drawingToolstrip.Items.OfType<ToolStripButton>())
            {
                if (control.Name.Contains("draw"))
                {
                    control.Click += (s, _) =>
                        SelectDrawingTool((s as ToolStripButton)?.Name.ToLower()
                            .Replace("draw", "")
                            .Replace("btn", ""));
                }

                if (control.Name.Contains("save"))
                    control.Click += (s, _) => SaveImageAs();
                if (control.Name.Contains("load"))
                    control.Click += (s, _) => LoadImage();
            }
        }

        private void LoadImage()
        {
            var props = typeof(ImageFormat).GetProperties(BindingFlags.Public | BindingFlags.Static);
            var formats = props.Where(f => f.Name != "MemoryBmp").Select(s => "|" + s.Name + " media file|*." + s.Name.ToLower());

            var fd = new OpenFileDialog { Filter = $@"{string.Join("", formats).Trim('|')}" };
            if (fd.ShowDialog(Owner) == DialogResult.OK)
            {
                var bitmap = Image.FromFile(fd.FileName);

                var unit = GraphicsUnit.Pixel;
                _context.Graphics.DrawImage(bitmap, _context.Graphics.VisibleClipBounds, bitmap.GetBounds(ref unit), GraphicsUnit.Pixel);
                drawingCanvas.Invalidate();
            }
        }

        private void SaveImageAs()
        {
            var props = typeof(ImageFormat).GetProperties(BindingFlags.Public | BindingFlags.Static);
            var formats = props.Where(f => f.Name != "MemoryBmp").Select(s => "|" + s.Name + " media file|*." + s.Name.ToLower());

            using var fd = new SaveFileDialog { Filter = $@"{string.Join("", formats).Trim('|')}" };

            if (fd.ShowDialog(Owner) != DialogResult.OK) return;

            var fileName = fd.FileName;
            var extension = Path.GetExtension(fd.FileName)?[1..];
            var format = (ImageFormat)props.First(x => x.Name.ToLower().Equals(extension)).GetValue(default);

            fd.Disposed += (s, e) =>
            {
                try
                {
                    var bitmap = new Bitmap(_context.Bitmap);
                    bitmap.SetResolution(1500f, 1500f);
                    using var bm = System.Drawing.Graphics.FromImage(bitmap);
                    bm.FillRegion(new SolidBrush(Color.White), bm.Clip);
                    var reg = bm.ClipBounds;
                    bm.DrawImage(_context.Bitmap, reg, reg, GraphicsUnit.Pixel);
                    bitmap.Save(fileName, format);
                }
                catch (Exception)
                {
                }
            };
        }

        private void SelectDrawingTool(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                var result = _toolsActivator
                    .Where(x => x.Key.Name.ToLower().Equals(name)).ToArray();
                if (result.Any())
                {
                    _currentTool = result.First().Value;
                }
            }
        }

        private void Playground_Closing(object sender, CancelEventArgs e)
        {
        }

        private void Playground_Load(object sender, EventArgs e)
        {
            CreatePlayground();
            _context = new GraphicContext();
            _context.Reset(_bitmap);
        }

        private void InitializeTextEditor()
        {
            editorTextBox.EnableContextMenu();
            var textEditorControls = new TextEditorControlsDiskPersistence(editorTextBox,
                e => textEditorStatus.Text = e,
                e => textEditorStatus.Text = @"Failed: " + e);
            toolStrip1.EnableControls(new Dictionary<string,
                Dictionary<string, (Action onInvoke, Func<bool> onValidation)>>
            {
                {
                    "File", new()
                    {
                        {"Open", (() => _ = textEditorControls.Open(), () => true)},
                        {"Save", (() => _ = textEditorControls.Save(), () => editorTextBox.TextLength > 0)},
                        {"Save As", (() => _ = textEditorControls.SaveAs(), () => editorTextBox.TextLength > 0)},
                        {"Create New", (() => _ = textEditorControls.Create(), () => true)}
                    }
                },
                {
                    "Edit",
                    new()
                    {
                        {"Undo", (() => editorTextBox.Undo(), () => editorTextBox.CanUndo)},
                        {"Redo", (() => editorTextBox.Redo(), () => editorTextBox.CanRedo)}
                    }
                },
                {
                    "Help",
                    new()
                    {
                        {"About", (OnAboutCall, () => true)}
                    }
                }
            });
            var cbItem = new ToolStripComboBox();
            cbItem.Items.AddRange(Enumerable.Range(5, 25).Cast<object>().ToArray());
            cbItem.SelectedIndexChanged += (s, e) =>
                editorTextBox.Font = new Font(editorTextBox.Font.FontFamily, (int)cbItem.SelectedItem);
            var cbItemFonts = new ToolStripComboBox();
            cbItemFonts.Items.AddRange(FontFamily.Families.Select(x => x.Name).Cast<object>().ToArray());
            cbItemFonts.SelectedIndexChanged += (s, e) =>
                editorTextBox.Font = new Font(new FontFamily((string)cbItemFonts.SelectedItem),
                    (int)(cbItem.SelectedItem ?? 16));
            toolStrip1.Items.Add(cbItem);
            cbItem.SelectedItem = 16;
            toolStrip1.Items.Add(cbItemFonts);
            cbItemFonts.SelectedItem = "Segoe UI";
        }

        private void OnAboutCall()
        {
            var formAbout = new About();
            formAbout.Show(this);
        }

        private void InitializeCalculatorControls()
        {
            var buttons = new List<Operation>
            {
                new RecordOperation(),
                new LoadOperation(),
                new ClearAllOperation(),
                new ClearLastOperation {ButtonMap = Keys.Back},
                new CoreOperation(".") {ButtonMap = Keys.OemPeriod},
                new CoreOperation("=") {ButtonMap = Keys.Oemplus, Modifiers = Keys.Shift},
                new CoreOperation("+") {ButtonMap = Keys.Oemplus},
                new CoreOperation("-") {ButtonMap = Keys.OemMinus},
                new CoreOperation("*") {ButtonMap = Keys.D8, Modifiers = Keys.Shift},
                new CoreOperation("/") {ButtonMap = Keys.Divide},
                new CoreOperation("(") {ButtonMap = Keys.D9, Modifiers = Keys.Shift},
                new CoreOperation(")") {ButtonMap = Keys.D0, Modifiers = Keys.Shift},
                new CoreOperation("%") {ButtonMap = Keys.D5, Modifiers = Keys.Shift},
                new CoreOperation("sqrt", "\u221A") {ButtonMap = Keys.S},
                new CoreOperation("^") {ButtonMap = Keys.D6, Modifiers = Keys.Shift},
                new CoreOperation("!") {ButtonMap = Keys.D1, Modifiers = Keys.Shift},
                new CoreOperation("exp"),
                new CoreOperation("cos"),
                new CoreOperation("sin"),
                new CoreOperation("tan"),
                new CoreOperation("log"),
                new CoreOperation("log2"),
                new CoreOperation("log10"),
                new CoreOperation("rad"),
                new CoreOperation("deg"),
                new CoreOperation("inv")
            };
            buttons.AddRange(Enumerable.Range(0, 10)
                .Select(value => new NumberOperation(value) { ButtonMap = Enum.Parse<Keys>("D" + value) }));
            _symbolMapping = buttons.ToArray();
            foreach (var operation in buttons) BindCalculatorButton(flowLayoutPanel1, operation, _stm);
        }

        private void BindCalculatorButton(Control panel, Operation operation, StateMachine stm)
        {
            var button = new Button
            {
                Size = new Size(80, 80),
                Font = new Font(FontFamily.GenericMonospace, 20),
                Text = operation.Represent
            };
            button.Click += (s, e) =>
            {
                if (stm.Execute(operation, out var value, out var err))
                {
                    resultBox.Text = stm.RepresentOperations();
                    resultBox.Text += "=" + value;
                }
                else
                {
                    resultBox.Text = err + $"[{stm.RepresentOperations()}]";
                }
            };
            operation.ActionCall += () => button.PerformClick();
            panel.Controls.Add(button);
        }

        private void Playground_KeyDown(object sender, KeyEventArgs e)
        {
            foreach (var sym in _symbolMapping)
                if (sym.Modifiers == e.Modifiers &&
                    e.KeyCode == sym.ButtonMap)
                    sym.OnActionCall();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using EditorProject.Calculator;
using EditorProject.Calculator.Abstractions;
using EditorProject.Calculator.Operations;
using EditorProject.TextEditor;
using EditorProject.TextEditor.Extensions;

namespace EditorProject
{
    public partial class Playground : Form
    {
        private readonly StateMachine _stm;
        private IReadOnlyList<Operation> _symbolMapping;

        public Playground()
        {
            _stm = new StateMachine();
            InitializeComponent();

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
                        {"Create New", (() => _ = textEditorControls.Create(), () => true)},
                    }
                },
                {
                    "Edit",
                    new()
                    {
                        {"Undo", (() => editorTextBox.Undo(), () => editorTextBox.CanUndo)},
                        {"Redo", (() => editorTextBox.Redo(), () => editorTextBox.CanRedo)},
                    }
                },
                {
                    "Help",
                    new()
                    {
                        {"About", (OnAboutCall, () => true)},
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
                editorTextBox.Font = new Font(new FontFamily((string)cbItemFonts.SelectedItem), (int)(cbItem.SelectedItem ?? 16));
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

        private void Playground_Load(object sender, EventArgs e)
        {
            InitializeCalculatorControls();
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
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace calc
{
    public partial class Playground : Form
    {
        private readonly StateMachine _stm;
        private IReadOnlyList<Operation> _symbolMapping;

        public Playground()
        {
            _stm = new StateMachine();
            InitializeComponent();
        }

        private void Playground_Load(object sender, EventArgs e)
        {
            var buttons = new List<Operation>
            {
                new RecordOperation(),
                new LoadOperation(),
                new ClearAllOperation(),
                new ClearLastOperation(){ ButtonMap = Keys.Back},
                new CoreOperation("."){ ButtonMap = Keys.OemPeriod},
                new CoreOperation("="){ ButtonMap = Keys.Oemplus,Modifiers = Keys.Shift},
                new CoreOperation("+"){ ButtonMap = Keys.Oemplus},
                new CoreOperation("-"){ ButtonMap = Keys.OemMinus},
                new CoreOperation("*"){ ButtonMap = Keys.D8,Modifiers =Keys.Shift},
                new CoreOperation("!"){ ButtonMap = Keys.D1,Modifiers =Keys.Shift},
                new CoreOperation("^"){ ButtonMap = Keys.D6,Modifiers =Keys.Shift},
                new CoreOperation("sqrt", "\u221A"){ ButtonMap = Keys.S},
                new CoreOperation("%"){ ButtonMap = Keys.D5 ,Modifiers = Keys.Shift},
                new CoreOperation("/"){ ButtonMap = Keys.Divide},
                new CoreOperation("("){ ButtonMap = Keys.D9,Modifiers =Keys.Shift},
                new CoreOperation(")"){ ButtonMap = Keys.D0,Modifiers =Keys.Shift},
            };
            buttons.AddRange(Enumerable.Range(0, 10)
                .Select(value => new NumberOperation(value)
                {
                    ButtonMap = Enum.Parse<Keys>("D" + value)
                }));
            _symbolMapping = buttons.ToArray();
            foreach (var operation in buttons)
            {
                BindButton(flowLayoutPanel1, operation, _stm);
            }
        }


        private void BindButton(Control panel, Operation operation, StateMachine stm)
        {
            var button = new Button
            {
                Size = new Size(70, 70),
                Font = new Font(FontFamily.GenericMonospace, 22),
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
            {
                if (sym.Modifiers == e.Modifiers &&
                    e.KeyCode == sym.ButtonMap)
                {
                    sym.OnActionCall();
                }
            }

        }
    }
}

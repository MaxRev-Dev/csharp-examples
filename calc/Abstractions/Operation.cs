using System;
using System.Windows.Forms;

namespace calc
{
    internal class Operation
    {
        public const string OpenBracket = "(";
        public const string CloseBracket = ")";
        public const string FloatDot = ".";
        private Keys? _buttonMapDefault;
        private Keys? _buttonModMapDefault;
        private string _represent;

        protected Operation(string value)
        {
            Value = value;
        }

        public virtual bool IsOperator =>
            !IsOperand &&
            !IsCommand &&
            Value != OpenBracket &&
            Value != CloseBracket &&
            Value != FloatDot;

        public virtual bool IsOperand { get; } = false;
        public virtual bool IsCommand { get; } = false;

        public string Value { get; protected set; }

        public string Represent
        {
            get => _represent ?? Value;
            set => _represent = value;
        }

        public Keys ButtonMap
        {
            get
            {
                if (_buttonMapDefault.HasValue)
                    return _buttonMapDefault.Value;
                if (Enum.TryParse(typeof(Keys), Represent, true, out var val))
                    if (val != null)
                        return (Keys) val;

                return Keys.None;
            }
            set => _buttonMapDefault = value;
        }

        public Keys Modifiers

        {
            get => _buttonModMapDefault ?? Keys.None;
            set => _buttonModMapDefault = value;
        }

        public float ExecuteOnStack(StateMachine stm)
        {
            if (this is IServiceOperation so)
                so.Execute(stm);
            else
                stm.Add(this);
            return stm.Evaluate();
        }

        public override string ToString()
        {
            return ">[" + Value + "]." + base.ToString();
        }

        public Operation Clone()
        {
            return (Operation) MemberwiseClone();
        }

        public void OnActionCall()
        {
            ActionCall?.Invoke();
        }

        public event Action ActionCall;
    }
}
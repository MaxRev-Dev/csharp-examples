using System;
using System.Globalization;
using EditorProject.Calculator.Abstractions;

namespace EditorProject.Calculator.Operations
{
    internal class NumberOperation : Operation
    {
        private float _fraction;
        private int _fractionCounter = 1;
        private bool _fractionTrailing;
        private float _rawValue;

        public NumberOperation(float value) : base(value.ToString(CultureInfo.InvariantCulture))
        {
            RawValue = value;
        }

        public float RawValue
        {
            get => _rawValue + _fraction;
            private set => _rawValue = value;
        }

        public override bool IsOperand => true;

        public NumberOperation Trailing(NumberOperation cx)
        {
            if (_fractionTrailing)
            {
                if (_fractionCounter == 1)
                    _fraction = cx.RawValue / 10;
                else
                    _fraction += cx.RawValue / (float) Math.Pow(10, _fractionCounter);
                _fractionCounter++;
            }
            else
            {
                RawValue = RawValue * 10f + cx.RawValue;
            }

            Value = RawValue.ToString(CultureInfo.InvariantCulture);
            return this;
        }

        public void TrailingDot()
        {
            _fractionTrailing = true;
        }
    }
}
using System;
using System.Globalization;

namespace calc
{
    internal class NumberOperation : Operation
    {
        private bool _fractionTrailing;
        private float _fraction;
        private int _fractionCounter = 1;
        private float _rawValue;

        public NumberOperation(float value) : base(value.ToString(CultureInfo.InvariantCulture))
        {
            RawValue = value;
        }

        public float RawValue
        {
            get => (float) Math.Round(_rawValue + _fraction, 5);
            private set => _rawValue = value;
        }

        public override bool IsOperand => true;

        public NumberOperation Trailing(NumberOperation cx)
        {
            if (_fractionTrailing)
            {
                if (_fractionCounter == 1)
                {
                    _fraction = cx.RawValue / 10;
                }
                else
                    _fraction = _fraction / (float)Math.Pow(10, _fractionCounter) + cx.RawValue;
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
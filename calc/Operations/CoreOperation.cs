using System;
using System.Linq;

namespace calc
{
    internal class CoreOperation : Operation
    {

        public CoreOperation(string value, string represent = default) : base(value)
        {
            Represent = represent;
        }

        public int Priority
        {
            get
            {
                return Value switch
                {
                    "=" => 1,
                    "+" => 2,
                    "-" => 2,
                    "*" => 3,
                    "/" => 3,
                    "^" => 4,
                    "sqrt" => 4,
                    "%" => 4,
                    _ => throw new ArgumentOutOfRangeException(nameof(Value))
                };
            }
        }

        public bool IsUnary => Value == "sqrt" || Value == "!";

        public float ExecuteUnaryOperation(float lhs)
        {
            return Value switch
            {
                "sqrt" => (float)Math.Sqrt(lhs),
                "!" => Enumerable.Range(1, (int)lhs).Aggregate(1, (p, item) => p * item),
                _ => throw new ArgumentOutOfRangeException(nameof(Value))
            };
        }

        public float ExecuteBinaryOperation(float lhs, float rhs)
        {
            return Value switch
            {
                "=" => lhs,
                "+" => lhs + rhs,
                "-" => lhs - rhs,
                "*" => lhs * rhs,
                "/" => lhs / rhs,
                "^" => (float)Math.Pow(lhs, rhs),
                "%" => lhs % rhs,
                _ => throw new ArgumentOutOfRangeException(nameof(Value))
            };
        }
    }
}
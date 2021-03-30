using System;
using System.Linq;
using EditorProject.Calculator.Abstractions;

namespace EditorProject.Calculator.Operations
{
    internal class CoreOperation : Operation
    {
        private const float G = 7; // g represents the precision desired, p is the values of p[i] to plug into Lanczos' formula

        private static readonly double[] _p =
        {
            0.99999999999980993d, 676.5203681218851d, -1259.1392167224028d, 771.32342877765313d, -176.61502916214059d,
            12.507343278686905d, -0.13857109526572012d, 9.9843695780195716e-6d, 1.5056327351493116e-7d
        };

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
                    "%" => 4,
                    // unary and others
                    _ => 5
                };
            }
        }

        public bool IsUnary => Value == "!" || Value.Length > 1;

        private float FactorialOfCore(float n)
        {
            if (Math.Abs(n - Math.Floor(n)) < 0.0001)
                return Enumerable.Range(1, (int)n).Aggregate(1, (p, item) => p * item);

            return FactorialOf(n);
        }

        private float FactorialOf(float n)
        {
            if (n < 0.5) return (float)(Math.PI / Math.Sin(n * Math.PI) / FactorialOf(1 - n));

            n--;
            var x = _p[0];
            for (var i = 1; i < G + 2; i++) x += _p[i] / (n + i);
            var t = n + G + 0.5;
            return (float)(Math.Sqrt(2 * Math.PI) * Math.Pow(t, n + 0.5) * Math.Exp(-t) * x);
        }

        public float ExecuteUnaryOperation(float lhs)
        {
            return Value switch
            {
                "sqrt" => (float)Math.Sqrt(lhs),
                "exp" => (float)Math.Exp(lhs),
                "sin" => (float)Math.Sin(lhs),
                "cos" => (float)Math.Cos(lhs),
                "tan" => (float)Math.Tan(lhs),
                "log" => (float)Math.Log(lhs),
                "log10" => (float)Math.Log10(lhs),
                "log2" => (float)Math.Log2(lhs),
                "rad" => (float)(Math.PI * lhs / 180),
                "deg" => (float)(180 * lhs / Math.PI),
                "inv" => 1 / lhs,
                "!" => FactorialOfCore(lhs),
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
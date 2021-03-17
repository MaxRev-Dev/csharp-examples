using System;
using System.Linq;

namespace calc
{
    internal class CoreOperation : Operation
    {
        private readonly float
            g = 7; // g represents the precision desired, p is the values of p[i] to plug into Lanczos' formula

        private readonly double[] p =
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
                    // unary
                    "sqrt" => 5,
                    "!" => 5,
                    _ => throw new ArgumentOutOfRangeException(nameof(Value))
                };
            }
        }

        public bool IsUnary => Value == "sqrt" || Value == "!";

        public float ExecuteUnaryOperation(float lhs)
        {
            return Value switch
            {
                "sqrt" => (float) Math.Sqrt(lhs),
                "!" => FactorialOfCore(lhs),
                _ => throw new ArgumentOutOfRangeException(nameof(Value))
            };
        }

        private float FactorialOfCore(float n)
        {
            if (Math.Abs(n - Math.Floor(n)) < 0.0001)
                return Enumerable.Range(1, (int) n).Aggregate(1, (p, item) => p * item);

            return FactorialOf(n);
        }

        private float FactorialOf(float n)
        {
            if (n < 0.5) return (float) (Math.PI / Math.Sin(n * Math.PI) / FactorialOf(1 - n));

            n--;
            var x = p[0];
            for (var i = 1; i < g + 2; i++) x += p[i] / (n + i);
            var t = n + g + 0.5;
            return (float) (Math.Sqrt(2 * Math.PI) * Math.Pow(t, n + 0.5) * Math.Exp(-t) * x);
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
                "^" => (float) Math.Pow(lhs, rhs),
                "%" => lhs % rhs,
                _ => throw new ArgumentOutOfRangeException(nameof(Value))
            };
        }
    }
}
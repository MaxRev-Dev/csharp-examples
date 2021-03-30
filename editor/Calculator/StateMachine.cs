using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EditorProject.Calculator.Abstractions;
using EditorProject.Calculator.Operations;

namespace EditorProject.Calculator
{
    internal class StateMachine
    {
        /// <summary>
        ///     Holds current operation stack
        /// </summary>
        private List<Operation> OperatorStack { get; } = new();

        /// <summary>
        ///     Holds MS values
        /// </summary>
        public Stack<NumberOperation> MemoryStack { get; } = new();

        public bool Execute(Operation operation, out float result, out string error)
        {
            try
            {
                result = operation.ExecuteOnStack(this);
            }
            catch (Exception ex)
            {
                result = 0;
                error = ex.Message;
                return false;
            }

            error = default;
            return true;
        }

        public void Add(Operation operation)
        {
            OperatorStack.Add(operation);
        }

        private Queue<Operation> CvtToPolishNotation(IEnumerable<Operation> commands)
        {
            var operators = new Stack<CoreOperation>();
            var final = new Queue<Operation>();

            // support several digits numbers
            var numberTrailing = false;

            void Enqueue(Operation op)
            {
                final.Enqueue(op.Clone());
            }

            foreach (var cx in commands.Where(x => x.Value != "="))
            {
                var x = cx.Value;
                if (cx.Value == "=")
                    break;
                if (x == Operation.OpenBracket)
                {
                    numberTrailing = false;
                    operators.Push(cx as CoreOperation);
                }
                else if (x == Operation.CloseBracket)
                {
                    while (operators.Count > 0 && operators.Peek().Value != Operation.OpenBracket)
                        Enqueue(operators.Pop());
                    operators.Pop();
                }
                else if (cx.IsOperand && cx is NumberOperation no1)
                {
                    if (numberTrailing)
                    {
                        var no2 = (NumberOperation) final.Last();
                        no2.Trailing(no1);
                    }
                    else
                    {
                        Enqueue(cx);
                    }

                    numberTrailing = true;
                }
                else if (cx.IsOperator && cx is CoreOperation so)
                {
                    numberTrailing = false;
                    if (operators.Count > 0 && operators.Peek().Value != Operation.OpenBracket)
                        if (so.Priority <= operators.Peek().Priority)
                            Enqueue(operators.Pop());
                    if (operators.Count > 0 && operators.Peek().Value != Operation.OpenBracket
                                            && so.Priority < operators.Peek().Priority)
                        Enqueue(operators.Pop());
                    operators.Push(so);
                }
                else if (cx.Value == ".")
                {
                    numberTrailing = true;
                    var no2 = (NumberOperation) final.Last();
                    no2.TrailingDot();
                }
                else
                {
                    if (!operators.Any())
                        continue;
                    var op = operators.Pop();
                    if (op.Value != Operation.OpenBracket) Enqueue(op);
                }
            }

            while (operators.Count > 0)
            {
                var c = operators.Pop();
                Enqueue(c);
            }

            return final;
        }

        /// <summary>
        ///     Evaluates calculation input using RPN
        /// </summary>
        /// <returns></returns>
        public float Evaluate()
        {
            var raw = CvtToPolishNotation(OperatorStack);

            float result = 0;

            var numbers = new Stack<float>();
            while (raw.Count > 0)
            {
                var current = raw.Dequeue();

                // advances caret to the next group sequence DDO
                void AdvanceCaret()
                {
                    while (raw.Count > 0 && (current == default || !current.IsOperator))
                    {
                        if (current is NumberOperation no)
                            numbers.Push(no.RawValue);
                        current = raw.Dequeue();
                    }
                }

                // fill-up before routine
                AdvanceCaret();

                // expand other
                while (numbers.Any())
                {
                    var rhs = numbers.Pop();

                    if (current != default &&
                        current.IsOperator &&
                        current is CoreOperation so)
                    {
                        var lhs = !numbers.Any() ? result : numbers.Pop();
                        result = so.IsUnary ? so.ExecuteUnaryOperation(rhs) : so.ExecuteBinaryOperation(lhs, rhs);
                        current = default;
                        numbers.Push(result);
                    }

                    AdvanceCaret();
                }

                if (current is NumberOperation nv)
                    // input is not fulfilled
                    return nv.RawValue;
            }

            return result;
        }

        public string RepresentOperations()
        {
            var str = new StringBuilder();

            foreach (var operation in OperatorStack)
                if (operation.Value != "=")
                    str.Append(operation.Represent);

            return str.ToString();
        }

        public void MemPush()
        {
            var op = Evaluate();
            MemoryStack.Push(new NumberOperation(op));
        }

        public void MemPop()
        {
            if (MemoryStack.Any())
                OperatorStack.Add(MemoryStack.Pop());
        }

        public void Clear()
        {
            OperatorStack.Clear();
        }

        public void ClearLast()
        {
            if (OperatorStack.Count > 0)
                OperatorStack.RemoveAt(OperatorStack.Count - 1);
        }
    }
}
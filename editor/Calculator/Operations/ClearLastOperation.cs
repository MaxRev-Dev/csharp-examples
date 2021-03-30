using EditorProject.Calculator.Abstractions;

namespace EditorProject.Calculator.Operations
{
    internal class ClearLastOperation : Operation, IServiceOperation
    {
        public ClearLastOperation() : base("C")
        {
        }

        public override bool IsCommand => true;

        public void Execute(StateMachine stm)
        {
            stm.ClearLast();
        }
    }
}
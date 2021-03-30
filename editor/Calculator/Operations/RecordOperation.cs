using EditorProject.Calculator.Abstractions;

namespace EditorProject.Calculator.Operations
{
    internal class RecordOperation : Operation, IServiceOperation
    {
        public RecordOperation() : base("MS")
        {
        }

        public override bool IsCommand => true;

        public void Execute(StateMachine stm)
        {
            stm.MemPush();
        }
    }
}
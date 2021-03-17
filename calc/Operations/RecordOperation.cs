namespace calc
{
    internal class RecordOperation : Operation, IServiceOperation
    {
        public RecordOperation() : base("MS")
        {
        }

        public void Execute(StateMachine stm)
        {
            stm.MemPush();
        }
        public override bool IsCommand => true;
    }
}
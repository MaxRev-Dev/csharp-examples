namespace calc
{
    internal class LoadOperation : Operation, IServiceOperation
    {
        public LoadOperation() : base("MR")
        {
        }

        public void Execute(StateMachine stm)
        {
            stm.MemPop();
        }
        public override bool IsCommand => true;
    }
}
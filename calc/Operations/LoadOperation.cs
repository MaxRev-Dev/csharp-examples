namespace calc
{
    internal class LoadOperation : Operation, IServiceOperation
    {
        public LoadOperation() : base("MR")
        {
        }

        public override bool IsCommand => true;

        public void Execute(StateMachine stm)
        {
            stm.MemPop();
        }
    }
}
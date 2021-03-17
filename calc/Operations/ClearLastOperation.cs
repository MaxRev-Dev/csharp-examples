namespace calc
{
    internal class ClearLastOperation : Operation, IServiceOperation
    {
        public ClearLastOperation() : base("C")
        {

        }

        public void Execute(StateMachine stm)
        {
            stm.ClearLast();
        }
        public override bool IsCommand => true;
    }
}
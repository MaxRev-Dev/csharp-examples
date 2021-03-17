namespace calc
{
    internal class ClearAllOperation : Operation, IServiceOperation
    {
        public ClearAllOperation() : base("MC")
        {

        }

        public void Execute(StateMachine stm)
        {
            stm.Clear();
        }

        public override bool IsCommand => true;
    }
}
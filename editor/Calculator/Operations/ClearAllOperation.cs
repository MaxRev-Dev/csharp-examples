﻿using EditorProject.Calculator.Abstractions;

namespace EditorProject.Calculator.Operations
{
    internal class ClearAllOperation : Operation, IServiceOperation
    {
        public ClearAllOperation() : base("MC")
        {
        }

        public override bool IsCommand => true;

        public void Execute(StateMachine stm)
        {
            stm.Clear();
        }
    }
}
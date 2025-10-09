using System;

namespace Game.Scripts.Battler.Module.StateMachine
{
    public class BoolCondition : ITransition
    {
        private readonly Func<bool> _getter;

        public BoolCondition(Func<bool> getter)
        {
            _getter = getter;
        }

        public bool IsMet() => _getter();
    }
}
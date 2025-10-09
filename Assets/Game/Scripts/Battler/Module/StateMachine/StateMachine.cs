using System;
using System.Collections.Generic;

namespace Game.Scripts.Battler.Module.StateMachine
{
    public class StateMachine
    {
        private IState _currentState;
        private readonly Dictionary<Type, List<Transition>> _transitions = new();
        private readonly List<Transition> _anyTransitions = new();
        private List<Transition> _currentTransitions = new();

        private static readonly List<Transition> EmptyTransitions = new(0);

        public void Update()
        {
            var transition = GetTransition();
            if (transition != null)
                ChangeState(transition.To);

            _currentState?.Update();
        }

        public void ChangeState(IState newState)
        {
            if (newState == _currentState)
                return;

            _currentState?.Exit();
            _currentState = newState;

            _transitions.TryGetValue(_currentState.GetType(), out _currentTransitions);
            _currentTransitions ??= EmptyTransitions;

            _currentState.Enter();
        }

        public void AddTransition(IState from, IState to, ITransition condition)
        {
            if (!_transitions.TryGetValue(from.GetType(), out var transitions))
            {
                transitions = new List<Transition>();
                _transitions[from.GetType()] = transitions;
            }
            transitions.Add(new Transition(to, condition));
        }

        public void AddAnyTransition(IState to, ITransition condition)
        {
            _anyTransitions.Add(new Transition(to, condition));
        }

        private Transition GetTransition()
        {
            foreach (var transition in _anyTransitions)
                if (transition.Condition.IsMet())
                    return transition;

            foreach (var transition in _currentTransitions)
                if (transition.Condition.IsMet())
                    return transition;

            return null;
        }

        private class Transition
        {
            public IState To { get; }
            public ITransition Condition { get; }

            public Transition(IState to, ITransition condition)
            {
                To = to;
                Condition = condition;
            }
        }

    }
}
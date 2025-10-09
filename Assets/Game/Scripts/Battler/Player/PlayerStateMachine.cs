using System;
using System.Collections.Generic;
using Game.Scripts.Battler.Input;
using Game.Scripts.Battler.Module.StateMachine;
using Game.Scripts.Battler.Player.States;
using UnityEngine;

namespace Game.Scripts.Battler.Player
{
    public class PlayerStateMachine
    {
        private readonly StateMachine _stateMachine = new();

        private readonly IInput _input;
        private readonly PlayerIdleState _idleState;
        private readonly PlayerMoveState _moveState;


        public PlayerStateMachine(PlayerView playerView, IInput input)
        {
            _input = input;
            
            _idleState = new PlayerIdleState(_stateMachine, playerView);
            _moveState = new PlayerMoveState(_stateMachine, playerView, _input);

            _stateMachine.AddTransition(_idleState, _moveState, new BoolCondition(()=> _input.GetInputDirection().magnitude > 0.1f));
            _stateMachine.AddTransition(_moveState, _idleState, new BoolCondition(()=> _input.GetInputDirection().magnitude <= 0.1f));

            _stateMachine.ChangeState(_idleState);
        }

        public void Update()
        {
            _stateMachine.Update();
        }
    }
}
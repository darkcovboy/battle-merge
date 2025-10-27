using Game.Scripts.Battler.Module.StateMachine;
using UnityEngine;

namespace Game.Scripts.Battler.Player.States
{
    public class PlayerDeadState : IState
    {
        private readonly StateMachine _stateMachine;
        private readonly PlayerView _playerView;

        public PlayerDeadState(StateMachine stateMachine, PlayerView playerView)
        {
            _stateMachine = stateMachine;
            _playerView = playerView;
        }
        public void Enter()
        {
            Debug.Log(GetType());

            _playerView.SetDead();
        }

        public void Update()
        {
        }

        public void Exit()
        {
            
        }
    }
}
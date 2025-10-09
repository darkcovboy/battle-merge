using Game.Scripts.Battler.Input;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Battler.Player
{
    public class PlayerCharacter : MonoBehaviour
    {
        [SerializeField] private PlayerView playerView;
        
        private IInput _input;
        private PlayerStateMachine _stateMachine;

        [Inject]
        public void Constructor(IInput input)
        {
            _input = input;
            _stateMachine = new PlayerStateMachine(playerView, _input);
        }

        private void Update()
        {
            _stateMachine.Update();
        }
    }
}
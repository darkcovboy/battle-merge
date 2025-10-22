using System;
using System.Collections;
using Game.Scripts.Battler.Battle;
using Unity.Cinemachine;
using UnityEngine;

namespace Game.Scripts.Battler.Camera
{
    public class CameraFightController : MonoBehaviour
    {
        private const float Weight = 1f;
        private const float Radius = 2f;


        [SerializeField] private CinemachineTargetGroup _targetGroup;
        
        public static CameraFightController Instance;

        private void Awake()
        {
            Instance = this;
        }

        public void StartFight(ICombat enemy)
        {
            if (IsMemberAlreadyExists(enemy.Transform))
                return;
            
            _targetGroup.AddMember(enemy.Transform, Weight, Radius);
            StartCoroutine(WaitForBattleEnd(enemy));
        }

        private IEnumerator WaitForBattleEnd(ICombat enemy)
        {
            while (enemy.IsInBattle)
            {
                yield return null;
            }
            
            _targetGroup.RemoveMember(enemy.Transform);
        }
        
        private bool IsMemberAlreadyExists(Transform target)
        {
            foreach (var member in _targetGroup.Targets)
            {
                if (member.Object == target)
                    return true;
            }

            return false;
        }
    }
}
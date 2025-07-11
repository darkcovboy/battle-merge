using System;
using UnityEngine;

namespace Game.Scripts.Useful
{
    public class CoroutineRunner : MonoBehaviour
    {
        public static CoroutineRunner Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }
    }
}
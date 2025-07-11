using System;
using UnityEngine;

namespace Game.Scripts.Useful
{
    public class DontDestroyOnLoadScript : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
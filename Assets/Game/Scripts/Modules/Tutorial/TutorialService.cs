using UnityEngine;

namespace Game.Scripts.Modules.Tutorial
{
    public class TutorialService
    {
        public bool IsCompleted { get; private set; }

        public void Setup(bool isCompleted)
        {
            IsCompleted = isCompleted;
        }

        public void MarkCompleted()
        {
            IsCompleted = true;
        }

        public void Reset()
        {
            IsCompleted = false;
        }
    }
}
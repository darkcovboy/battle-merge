using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts.Modules.LoadingTree
{
    public abstract class LoadingOperationComposite : LoadingOperation
    {
        #if ODIN_INSPECTOR
        [GUIColor(0.42f, 0.98f, 0.87f)]
#endif
        [SerializeField]
        private string name;

#if ODIN_INSPECTOR
        [ListDrawerSettings(OnBeginListElementGUI = nameof(DrawElement))]
#endif
        [SerializeField, Space]
        protected TaskInfo[] taskList;
        
#if UNITY_EDITOR
#if ODIN_INSPECTOR
        [PropertyOrder(-100)]
        [GUIColor(1, 1, 0)]
        [PropertySpace(SpaceAfter = 8, SpaceBefore = 0)]
#endif
        [SerializeField]
        protected bool showDebug = true;
#endif

        public override string Name => this.name;

        [Serializable]
        protected sealed class TaskInfo
        {
            [SerializeField]
            internal bool off;
            
            [DisableIf(nameof(off))]
            [SerializeField]
            internal int weight = 1;

            [DisableIf(nameof(off))]
            [SerializeReference]
            internal ILoadingOperation operation;

            internal float CurrentWeight
            {
                get
                {
                    float progress = this.operation?.Progress ?? 0;
                    return progress * this.weight;
                }
            }
        }

#if ODIN_INSPECTOR
        private void DrawElement(int index)
        {
            GUILayout.Space(4);
            Color prevColor = GUI.color;

            string taskName = "undefined";
            
            if (this.taskList != null && index >= 0 && index < this.taskList.Length)
            {
                TaskInfo taskInfo = this.taskList[index];
                // GUI.color = taskInfo.off ? new Color(0.76f, 0.25f, 0.2f, 1) : new Color(0.42f, 0.98f, 0.87f, 1);

                if (!taskInfo.off)
                {
                    GUI.color = taskInfo.off ? new Color(0.16f, 0.16f, 0.18f, 0.63f) : new Color(0.42f, 0.98f, 0.87f, 1);
                }
                
                taskName = taskInfo.operation?.Name;
            }

            string text = $"{index + 1}. {taskName}";
            GUILayout.Label(text);
            
            GUI.color = prevColor;
        }
#endif
    }
}
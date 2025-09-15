using UnityEditor;
using UnityEngine;

namespace Game.Scripts.App.Characters.Data
{
    [CreateAssetMenu(fileName = "Character", menuName = "Configs/CharacterConfig")]
    public class CharacterConfig : ScriptableObject
    {
        [SerializeField] private string _nameId;
        [SerializeField] private Sprite _icon;
        [SerializeField] private float _damage;
        [SerializeField] private float _health;
        [SerializeField] private GameObject _itemReference;
        [SerializeField] private CharacterWarriorType _warriorType;
        
        public string NameId => _nameId;
        public Sprite Icon => _icon;
        public float Damage => _damage;
        public float Health => _health;
        public GameObject ItemReference => _itemReference;
        public CharacterWarriorType WarriorType => _warriorType;

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (!string.IsNullOrEmpty(_nameId))
            {
                string assetPath = AssetDatabase.GetAssetPath(this);
                string newAssetPath = System.IO.Path.GetDirectoryName(assetPath) + "/CharacterConfig " + _nameId + ".asset";

                if (assetPath != newAssetPath)
                {
                    EditorApplication.delayCall += () =>
                    {
                        if (this != null)
                        {
                            AssetDatabase.RenameAsset(assetPath, $"{_nameId} CharacterConfig");
                            AssetDatabase.SaveAssets();
                        }
                    };
                }
            }
        }
#endif
    }

    public enum CharacterWarriorType
    {
        Fighter,
        Shooter
    }
}
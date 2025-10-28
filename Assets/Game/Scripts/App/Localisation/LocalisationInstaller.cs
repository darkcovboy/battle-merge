using System.Collections.Generic;
using GamePush;
using UnityEngine;
using Zenject;

namespace Game.Scripts.App.Localisation
{
    [CreateAssetMenu(
        fileName = "LocalisationInstaller",
        menuName = "Zenject/New LocalisationInstaller"
    )]
    public class LocalisationInstaller : ScriptableObjectInstaller
    {
        [field:SerializeField] public Language Language { get; private set; }
        
        private List<LocalisationDataSO> _localisationDatas = new();
        
        public override void InstallBindings()
        {
#if !UNITY_EDITOR
            Language = GP_Language.Current();
#endif
            
            _localisationDatas.AddRange(LoadLocalisationDataSO("Localisation"));
            Container.Bind<LocalisationManager>()
                .AsSingle()
                .WithArguments(Language, _localisationDatas)
                .OnInstantiated<LocalisationManager>((_,localisationManager) => localisationManager.CreateDictionary())
                .NonLazy();
        }

        private LocalisationDataSO[] LoadLocalisationDataSO(string localisation)
        {
            return Resources.LoadAll<LocalisationDataSO>(localisation);
        }
    }
}
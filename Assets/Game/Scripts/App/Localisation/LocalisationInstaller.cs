using GamePush;
using UnityEngine;
using Zenject;

namespace Game.Scripts.App.Localisation
{
    public class LocalisationInstaller : ScriptableObjectInstaller
    {
        [field:SerializeField] public Language Language { get; private set; }
        
        public override void InstallBindings()
        {
#if !UNITY_EDITOR
            Language = GP_Language.Current();
#endif
            Container.Bind<LocalisationManager>()
                .AsSingle()
                .WithArguments(Language, LoadLocalisationDataSO())
                .OnInstantiated<LocalisationManager>((_,localisationManager) => localisationManager.CreateDictionary())
                .NonLazy();
        }

        private LocalisationDataSO LoadLocalisationDataSO()
        {
            return Resources.Load<LocalisationDataSO>("Localisation");
        }
    }
}
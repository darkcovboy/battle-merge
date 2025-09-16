using Game.Scripts.App.Save;
using Game.Scripts.Menu.Field.CellScripts;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Menu.Field
{
    public class FieldInstaller : MonoInstaller
    {
        [SerializeField] private FieldView _fieldView;
        [SerializeField] private Cell cellPrefab;
        [SerializeField] private CellConfig cellConfig;
        
        public override void InstallBindings()
        {
            Container.Bind<FieldView>().FromInstance(_fieldView).AsSingle();
            Container.Bind<CellConfig>().FromInstance(cellConfig).AsSingle();
            Container.Bind<Cell>().FromInstance(cellPrefab).AsSingle();
            
            Container.Bind<CellFactory>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<FieldPresenter>().AsSingle().NonLazy();
        }
    }
}
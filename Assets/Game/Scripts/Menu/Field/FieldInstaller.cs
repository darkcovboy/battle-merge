using Game.Scripts.Menu.Field.Cell;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Menu.Field
{
    public class FieldInstaller : MonoInstaller
    {
        [SerializeField] private FieldView _fieldView;
        [SerializeField] private CellView _cellViewPrefab;
        
        public override void InstallBindings()
        {
            Container.Bind<FieldView>().FromInstance(_fieldView).AsSingle();
            Container.Bind<CellView>().FromInstance(_cellViewPrefab).AsSingle();
            
            Container.Bind<CellPresenterFactory>().AsSingle();
            Container.Bind<CellViewFactory>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<FieldPresenter>().AsSingle().NonLazy();
        }
    }
}
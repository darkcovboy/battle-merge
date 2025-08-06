using Zenject;

namespace Game.Scripts.Menu.Field.Cell
{
    public class CellPresenterFactory : IFactory<CellView, CellPresenter>
    {
        public CellPresenter Create(CellView view)
        {
            return new CellPresenter(view);
        }
    }
}
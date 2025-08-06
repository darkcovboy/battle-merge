using Game.Scripts.Menu.Field.Cell;
using Zenject;

namespace Game.Scripts.Menu.Field
{
    public class FieldPresenter : IInitializable
    {
        private readonly FieldView _fieldView;
        private readonly CellPresenterFactory _cellPresenterFactory;
        private readonly CellViewFactory _cellViewFactory;
        
        

        public FieldPresenter(FieldView fieldView,
            CellPresenterFactory cellPresenterFactory,
            CellViewFactory cellViewFactory)
        {
            _fieldView = fieldView;
            _cellPresenterFactory = cellPresenterFactory;
            _cellViewFactory = cellViewFactory;
        }

        public void Initialize()
        {
            for (int i = 0; i < 16; i++)
            {
                CellView cellView = _cellViewFactory.Create(_fieldView.Root);
                CellPresenter cellPresenter = _cellPresenterFactory.Create(cellView);
            }
        }
    }
}
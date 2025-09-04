using System.Collections.Generic;
using Game.Scripts.Menu.Field.CellScripts;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Menu.Field
{
    public class FieldPresenter : IInitializable
    {
        private readonly FieldView _fieldView;
        private readonly CellFactory _cellFactory;
        private readonly List<Cell> _cellViews = new List<Cell>();
        
        public FieldPresenter(FieldView fieldView,
            CellFactory cellFactory)
        {
            _fieldView = fieldView;
            _cellFactory = cellFactory;
        }

        public void Initialize()
        {
            for (int i = 0; i < 16; i++)
            {
                Cell cell = _cellFactory.Create(_fieldView.Root, i);
                _cellViews.Add(cell);
            }
        }
    }
}
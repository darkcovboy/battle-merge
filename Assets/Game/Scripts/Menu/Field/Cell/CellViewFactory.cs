using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Menu.Field.Cell
{
    public class CellViewFactory : IFactory<Transform, CellView>
    {
        private readonly CellView _prefab;

        public CellViewFactory(CellView prefab)
        {
            _prefab = prefab;
        }

        public CellView Create(Transform view)
        {
            return Object.Instantiate(_prefab, view);
        }
    }
}
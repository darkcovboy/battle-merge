using UnityEngine;
using Zenject;

namespace Game.Scripts.Menu.Field.CellScripts
{
    public class CellFactory : IFactory<Transform,int, Cell>
    {
        private readonly Cell _prefab;
        private readonly CellConfig _config;

        public CellFactory(Cell prefab, CellConfig config)
        {
            _prefab = prefab;
            _config = config;
        }

        public Cell Create(Transform view, int id)
        {
            Cell cell = Object.Instantiate(_prefab, view);
            cell.Setup(_config, id);
            return cell;
        }
    }
}
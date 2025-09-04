using UnityEngine;
using Zenject;

namespace Game.Scripts.Menu.Field.CellScripts
{
    public class CellFactory : IFactory<Transform,int, CellScripts.Cell>
    {
        private readonly CellScripts.Cell _prefab;
        private readonly CellConfig _config;

        public CellFactory(CellScripts.Cell prefab, CellConfig config)
        {
            _prefab = prefab;
            _config = config;
        }

        public CellScripts.Cell Create(Transform view, int id)
        {
            CellScripts.Cell cell = Object.Instantiate(_prefab, view);
            cell.Setup(_config, id);
            return cell;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;

namespace Game.Scripts.Modules.Currency
{
    public class CurrencyBank : IEnumerable<CurrencyCell>
    {
        [ShowInInspector]
        private Dictionary<CurrencyType, CurrencyCell> _cells;

        public CurrencyBank(IEnumerable<CurrencyCell> cells)
        {
            _cells = cells.ToDictionary(cell => cell.Type);
        }

        public CurrencyCell GetCell(CurrencyType type)
        {
            return _cells[type];
        }

        public IEnumerator<CurrencyCell> GetEnumerator()
        {
            return _cells.Values.GetEnumerator(); 
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
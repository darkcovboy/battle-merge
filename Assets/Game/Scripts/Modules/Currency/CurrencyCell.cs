using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts.Modules.Currency
{
    [Serializable]
    public class CurrencyCell
    {
        public event Action OnStateChanged;
        public event Action<float> OnAmountChanged;
        public event Action<float> OnAdd;
        public event Action<float> OnRemove;

        public CurrencyType Type => _type;
        public float Amount => _amount;

        [SerializeField]
        private CurrencyType _type;

        [ShowInInspector]
        private float _amount;

        public CurrencyCell(CurrencyType type)
        {
            _type = type;
        }

        [Button]
        public bool Add(float range, bool isUpdateUI = true)
        {
            if (range <= 0)
            {
                return false;
            }

            _amount += range;
            
            if (isUpdateUI)
            {
                OnAmountChanged?.Invoke(range);
                OnStateChanged?.Invoke();
            }
            
            OnAdd?.Invoke(range);
            return true;
        }

        [Button]
        public bool Add(int range, bool isUpdateUI = true)
        {
            if (range <= 0)
            {
                return false;
            }

            _amount += range;

            if (isUpdateUI)
            {
                OnAmountChanged?.Invoke(_amount);
                OnStateChanged?.Invoke();
            }

            OnAdd?.Invoke(range);
            return true;
        }

        [Button]
        public bool Spend(int range)
        {
            if (range <= 0)
            {
                return false;
            }

            if (_amount < range)
                return false;

            _amount -= range;
            OnAmountChanged?.Invoke(_amount);
            OnStateChanged?.Invoke();
            OnRemove?.Invoke(range);
            return true;
        }

        [Button]
        public bool Spend(float range)
        {
            if (range <= 0)
            {
                return false;
            }

            if (_amount < range)
                return false;

            _amount -= range;
            OnAmountChanged?.Invoke(range);
            OnStateChanged?.Invoke();
            OnRemove?.Invoke(range);
            return true;
        }

        public void Change(float amount)
        {
            if (_amount != amount)
            {
                _amount = amount;
                OnAmountChanged?.Invoke(amount);
                OnStateChanged?.Invoke();
            }
        }

        public bool Exists(int range)
        {
            return _amount >= range;
        }
    }
}
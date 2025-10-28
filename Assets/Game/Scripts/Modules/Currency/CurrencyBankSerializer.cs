using System.Collections.Generic;
using Game.Scripts.Modules.SaveLoad.Serializers;

namespace Game.Scripts.Modules.Currency
{
    
    public class CurrencyBankSerializer : GameSerializer<CurrencyBank, CurrencyDataset>
    {
        protected override CurrencyDataset Serialize(CurrencyBank service)
        {
            var currencyDataset = new CurrencyDataset
            {
                Datas = new List<CurrencyData>()
            };

            foreach (var cell in service)
            {
                currencyDataset.Datas.Add(new CurrencyData
                {
                    Type = cell.Type,
                    Value = cell.Amount
                });
            }

            return currencyDataset;
        }

        protected override void Deserialize(CurrencyBank service, CurrencyDataset data)
        {
            foreach (var currencyData in data.Datas)
            {
                service.GetCell(currencyData.Type).Change(currencyData.Value);
            }
        }

        protected override void SetupByDefault(CurrencyBank service)
        {
            foreach (var currencyData in service)
            {
                service.GetCell(currencyData.Type).Change(currencyData.Type == CurrencyType.COIN ? 200 : 0);
            }
        }
    }

    public struct CurrencyData
    {
        public float Value;
        public CurrencyType Type;
    }

    public struct CurrencyDataset
    {
        public List<CurrencyData> Datas;
    }
}
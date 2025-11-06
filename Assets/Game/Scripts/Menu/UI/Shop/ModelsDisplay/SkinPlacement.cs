using System;
using UnityEngine;

namespace Game.Scripts.Menu.UI.Shop.ModelsDisplay
{
    public class SkinPlacement : MonoBehaviour
    {
        private GameObject _currentModel;
        
        public void InstantiateModel(GameObject model)
        {
            Reset();
            
            if(_currentModel != null)
                Destroy(_currentModel);
            
            _currentModel = Instantiate(model, transform);
        }

        private void Reset()
        {
            transform.localRotation = new Quaternion(0, 180f, 0 ,0);
        }
    }
}
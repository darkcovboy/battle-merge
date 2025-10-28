using System.Collections;
using TMPro;
using UnityEngine;
using Zenject;

namespace Game.Scripts.App.Localisation
{
    public class LocalisationText : MonoBehaviour
    {
        [SerializeField] private string _key;
        
        private LocalisationManager _localisationManager;

        [Inject]
        private void Construct(LocalisationManager localisationManager)
        {
            _localisationManager = localisationManager;
        }

        private IEnumerator Start()
        {
            yield return new WaitUntil(()=>_localisationManager != null); 
            if(TryGetComponent(out TextMeshProUGUI text))
                text.text = _localisationManager.GetText(_key);
        }
    }
}
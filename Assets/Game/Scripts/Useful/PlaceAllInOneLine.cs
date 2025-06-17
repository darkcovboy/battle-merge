using Sirenix.OdinInspector;
using UnityEngine;

public class PlaceAllInOneLine : MonoBehaviour
{
    [SerializeField] private float _space = 2f;
    
    [Button]
    public void Place()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).transform.position = new Vector3(_space * i, 0f, 0f);
        }
    }
        
}

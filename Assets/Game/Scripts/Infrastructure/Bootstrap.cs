using Game.Scripts.Infrastructure;
using UnityEngine;
using Zenject;

public class Bootstrap : MonoBehaviour
{
    private IGameLoader _gameLoader;

    [Inject]
    public void Constructor(IGameLoader gameLoader)
    {
        _gameLoader = gameLoader;
    }
    
    private void Awake()
    {
        _gameLoader.LoadInitialScene();
    }
}

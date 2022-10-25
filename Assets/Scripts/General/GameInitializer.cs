using UnityEngine;
using Zenject;

public class GameInitializer : MonoBehaviour
{
    [Header("Debug settings")]
    [SerializeField] private bool _isDebug;

    [Header("Settings")]
    [Inject] private Player _player;

    private void Start()
    {
        _player.Initialize();

        EventBus.Publish<IGameStartHandler>(handler => handler.OnGameStart());
    }
}

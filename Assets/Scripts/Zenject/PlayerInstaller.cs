using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private Player _player;

    public override void InstallBindings()
    {
        _player.Initialize();

        Container.Bind<Player>().FromInstance(_player).AsSingle();
    }
}
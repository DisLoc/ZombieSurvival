using UnityEngine;
using Zenject;

public class MainMenuInstaller : MonoInstaller
{
    [SerializeField] private MainMenu _mainMenu;

    public override void InstallBindings()
    {
        Container.Bind<MainMenu>().FromInstance(_mainMenu).AsSingle();
    }
}
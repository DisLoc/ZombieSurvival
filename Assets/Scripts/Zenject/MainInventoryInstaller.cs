using UnityEngine;
using Zenject;

public class MainInventoryInstaller : MonoInstaller
{
    [SerializeField] private MainInventory _mainInventoryInstance;

    public override void InstallBindings()
    {
        Container.Bind<MainInventory>().FromInstance(_mainInventoryInstance).AsSingle();
    }
}
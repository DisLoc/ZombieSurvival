using UnityEngine;
using Zenject;

public class ZombieFactoryInstaller : MonoInstaller
{
    [SerializeField] private Zombie _zombie;

    public override void InstallBindings()
    {
        Container.BindFactory<Zombie, Zombie.Factory>().FromComponentInNewPrefab(_zombie);
    }
}
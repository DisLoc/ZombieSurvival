using UnityEngine;
using Zenject;

public class AbilityGiverInstaller : MonoInstaller
{
    [SerializeField] private AbilityGiver _abilityGiver;
    
    public override void InstallBindings()
    {
        Container.Bind<AbilityGiver>().FromInstance(_abilityGiver).AsSingle();
    }
}
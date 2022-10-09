using UnityEngine;
using Zenject;

public class CrystalFactoryInstaller : MonoInstaller
{
    [SerializeField] private ExpCrystal _crystalPrefab;

    public override void InstallBindings()
    {
        Container.BindFactory<ExpCrystal, ExpCrystal.Factory>().FromComponentInNewPrefab(_crystalPrefab);
    }
}
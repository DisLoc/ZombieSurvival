using UnityEngine;
using Zenject;

public class LevelBuilderInstaller : MonoInstaller
{
    [SerializeField] private LevelBuilder _builderPrefab;
    public override void InstallBindings()
    {
        LevelBuilder builder = Instantiate(_builderPrefab);
        builder.transform.position = Vector3.zero;
        builder.Initialize();

        Container.Bind<LevelBuilder>().FromInstance(builder).AsSingle();
    }
}
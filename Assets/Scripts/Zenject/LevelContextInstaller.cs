using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "LevelContextInstaller", menuName = "Installers/LevelContextInstaller")]
public class LevelContextInstaller : ScriptableObjectInstaller<LevelContextInstaller>
{
    [SerializeField] private LevelContext _levelContext;

    public override void InstallBindings()
    {
        _levelContext.Initialize();

        Container.Bind<LevelContext>().FromScriptableObject(_levelContext).AsSingle();
    }
}
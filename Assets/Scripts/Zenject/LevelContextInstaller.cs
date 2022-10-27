using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "LevelContextInstaller", menuName = "Installers/LevelContextInstaller")]
public class LevelContextInstaller : ScriptableObjectInstaller<LevelContextInstaller>
{
    [SerializeField] private LevelContext _levelContext;

    public override void InstallBindings()
    {
        Container.Bind<LevelContext>().FromScriptableObject(_levelContext).AsSingle();
    }
}
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "LevelContextInstaller", menuName = "Installers/LevelContextInstaller")]
public class LevelContextInstaller : ScriptableObjectInstaller<LevelContextInstaller>
{
    [SerializeField] private LevelBuilder _levelBuilderPrefab;
    [SerializeField] private LevelContext _defaultContext;
    
    private LevelContext _levelContext;

    public void SetLevel(LevelContext level)
    {
        _levelContext = level;
    }

    public override void InstallBindings()
    {
        if (_levelContext == null)
        {
            _levelContext = _defaultContext;
        }

        _levelContext.Initialize(_levelBuilderPrefab);

        Container.Bind<LevelContext>().FromScriptableObject(_levelContext).AsSingle();
    }
}

using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "LevelContextInstaller", menuName = "Installers/LevelContextInstaller")]
public class LevelContextInstaller : ScriptableObjectInstaller<LevelContextInstaller>
{
    [SerializeField] private LevelBuilder _levelBuilderPrefab;

    [SerializeField] private Player _defaultPlayer;
    [SerializeField] private LevelContext _defaultContext;
    
    private Player _player;
    private LevelContext _levelContext;

    public void SetLevel(LevelContext level)
    {
        _levelContext = level;
    }

    public void SetPlayer(Player player)
    {
        _player = player;
    }

    public override void InstallBindings()
    {
        if (_player == null)
        {
            _player = _defaultPlayer;
        }

        if (_levelContext == null)
        {
            _levelContext = _defaultContext;
        }

        _levelContext.Initialize(_levelBuilderPrefab);

        Container.Bind<LevelContext>().FromScriptableObject(_levelContext).AsSingle();
    }
}

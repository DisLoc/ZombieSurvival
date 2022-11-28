using UnityEngine;

public class ZombieChest : PickableObject
{
    [Header("Settings")]
    [SerializeField] private Canvas _canvas;
    [SerializeField] private RectTransform _abilityUIParent;
    [SerializeField] private AbilityUI _abilityUIPrefab;

    private Player _player;
    private AbilityGiver _abilityGiver;
    private int _abilitiesRewardCount;

    public void Initialize(Player player, AbilityGiver abilityGiver, int abilitiesRewardCount)
    {
        _player = player;
        _abilityGiver = abilityGiver;
        _abilitiesRewardCount = abilitiesRewardCount;

        _canvas.gameObject.SetActive(false);
    }

    protected override void OnPickUp()
    {
        base.OnPickUp();

        _canvas.gameObject.SetActive(true);

        Time.timeScale = 0;

        for(int i = 0; i < _abilitiesRewardCount; i++)
        {
            AbilityContainer ability = _abilityGiver.GetAbilitiesUpgrades(1)[0];

            if (ability != null)
            {
                AbilityUI abilityUI = Instantiate(_abilityUIPrefab, _abilityUIParent);
                abilityUI.Initialize();
                abilityUI.SetAbility(ability);

                _player.GetAbility(ability);
            }
            else if (_isDebug) Debug.Log("Missing ability!");
        }
    }

    public void OnAbilitiesGetted()
    {
        Time.timeScale = 1;

        Destroy(gameObject);
    }
}
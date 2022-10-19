using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Zombie : CharacterBase
{
    [SerializeField] protected CharacterStats _stats;
    
    private Transform _player;

    public EnemiesList _enemiesList;

 //   private GameObject test;

    [SerializeField] private GameObject crystal;

    public override CharacterStats Stats => _stats;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").gameObject.transform;
        _stats.Initialize();
        _healthBar.Initialize(_stats.HP);
        _stats.BaseWeapon.Initialize();


        //_enemiesList.enemies.Add(this);
    }

    void Update()
    {
        Move(new Vector3(0, 0, 0));
    }

    public override void Die()
    {
        base.Die();

        EventBus.Publish<IEnemyKilledHandler>(handler => handler.OnEnemyKilled(this));

        Destroy(gameObject);
    }

    public override void Move(Vector3 direction)
    {
        transform.position = Vector3.MoveTowards(transform.position,  _player.transform.position, _stats.Velocity * Time.deltaTime);
        transform.LookAt(_player);
    }

    public override void Attack()
    {
        throw new System.NotImplementedException();
    }

}

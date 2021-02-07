using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEntity : AbstractEntity
{
    public GameObject prefab;
    public float health;
    public float speed;

    protected override void Start()
    {
        base.Start();
        entity.isEnemy = true;
        entity.AddPrefab(prefab);
        entity.AddHealth(health);
        entity.AddForwardMovement(speed);
    }
}

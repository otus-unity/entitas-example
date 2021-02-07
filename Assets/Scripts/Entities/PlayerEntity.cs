using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : AbstractEntity
{
    public GameObject playerPrefab;
    public float health;

    protected override void Start()
    {
        base.Start();
        entity.isPlayer = true;
        entity.AddPrefab(playerPrefab);
        entity.AddHealth(health);
    }
}

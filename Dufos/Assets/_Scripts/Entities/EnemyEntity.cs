using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEntity : Entity
{
    protected override void Start()
    {
        base.Start();
        CombatManager.Instance.EnemyEntities.Add(this);
    }
}

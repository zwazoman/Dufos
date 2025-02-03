using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Entity", menuName = "Entities")]
public class EntityData : ScriptableObject
{
    [field : SerializeField]
    public int Initiative { get;private set; }

    [field : SerializeField]
    public int MaxHealth { get; private set; }

    [field : SerializeField]
    public int MaxMovePoints { get; private set; }

    public Spell[] Spells;

}

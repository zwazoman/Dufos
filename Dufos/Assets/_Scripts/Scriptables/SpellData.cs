using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SelectionForm
{
    Sphere,
    Ray,
    Targeted
}

public enum SpellForm
{
    Sphere,
    Ray,
    Point
}

[CreateAssetMenu(fileName = "Spell", menuName = "Spells")]
public class SpellData : ScriptableObject
{
    public Sprite UISprite;

    public int Uses;

    public SelectionForm LaunchForm;

    public int SelectionMaxRange;

    public bool BypassNearSelection;
    public float SelectionBypassSize;//seulement si BypassNearSelection est coch�

    public SpellForm TargetForm;

    public int SpellMaxRange; 

    public bool BypassNearSpell;
    public float SpellBypassSize; //seulement si BypassNearSpell est coch�

    public float Damage;

    //mettre un bouton "show" qui fait apparaitre les gizmos ou colorie la grille pour montrer la zone d'effet du spell
}

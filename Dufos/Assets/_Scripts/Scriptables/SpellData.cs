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

public enum Visuals
{
    RockProjectile,
    CatchingFire,
    MeteorProjectile
}

[CreateAssetMenu(fileName = "Spell", menuName = "Spells")]
public class SpellData : ScriptableObject
{
    public Sprite UISprite;

    public int Uses;



    public SelectionForm LaunchForm;

    public int SelectionMaxRange;

    public bool ThrowableOnWalls;

    public bool GoesThroughWalls;

    public bool BypassNearSelection;
    public float SelectionBypassSize;//seulement si BypassNearSelection est coché




    public SpellForm TargetForm;

    public int SpellMaxRange; 

    public bool BypassNearSpell;
    public float SpellBypassSize; //seulement si BypassNearSpell est coché




    public int Damage;

    public Visuals Visuals;

    //mettre un bouton "show" qui fait apparaitre les gizmos ou colorie la grille pour montrer la zone d'effet du spell
}

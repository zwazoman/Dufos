using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public SpellForm LaunchForm;

    //en fonction de la form, choisir les directions disponibles

    public float MaxRange;

    public bool BypassNear;
    public float BypassSize { get { return BypassSize; } set { BypassSize = Mathf.Clamp(value,0, MaxRange); } }

    public SpellForm DamageForm;

    public float Damage;

    //mettre un bouton "show" qui fait apparaitre les gizmos ou colorie la grille pour montrer la zone d'effet du spell
}

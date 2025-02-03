using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.Timeline;

[Serializable]
public class Spell
{
    [SerializeField] public SpellData Data;

    [HideInInspector] public Entity Caster;

    protected List<WayPoint> ReadSelectionForm()
    {
        Vector3 origin = Caster.transform.position;

        List<WayPoint> SelectionPoints = new List<WayPoint>();

        switch (Data.LaunchForm)
        {
            case SelectionForm.Ray:
                foreach(Vector3 direction in Tools.AllFlatDirections)
                {
                    foreach(RaycastHit hit in Physics.RaycastAll(origin + direction * Data.SelectionBypassSize ,direction,Data.MaxRange, LayerMask.GetMask("Ground")))
                    {
                        Debug.Log("selectionne");
                        SelectionPoints.Add(hit.collider.GetComponent<WayPoint>());
                    }
                }

                break;
            case SelectionForm.Sphere:
                Debug.Log("Sphere");
                foreach (Collider hit in Physics.OverlapSphere(origin, Data.MaxRange, LayerMask.GetMask("Ground")))
                {
                    SelectionPoints.Add(hit.GetComponent<WayPoint>());
                }
                break;
            case SelectionForm.Targeted:
                Debug.Log("targeted");
                //choper tous les ennemis ou players et selectionner leurs currentpoints
                break;
        }

        return SelectionPoints;
    }

    protected List<WayPoint> ReadSpellForm(WayPoint selectedPoint)
    {
        Vector3 origin = selectedPoint.transform.position;

        List<WayPoint> SpellPreviewPoints = new List<WayPoint>();

        switch (Data.DamageForm)
        {
            case SpellForm.Ray:
                foreach (Vector3 direction in Tools.AllFlatDirections)
                {
                    foreach (RaycastHit hit in Physics.RaycastAll(origin + direction * Data.SpellBypassSize, direction, Data.MaxRange, LayerMask.GetMask("Ground")))
                    {
                        Debug.Log("selectionne");
                        SpellPreviewPoints.Add(hit.collider.GetComponent<WayPoint>());
                    }
                }

                break;
            case SpellForm.Sphere:
                Debug.Log("Sphere");
                foreach (Collider hit in Physics.OverlapSphere(origin, Data.MaxRange, LayerMask.GetMask("Ground")))
                {
                    SpellPreviewPoints.Add(hit.GetComponent<WayPoint>());
                }
                break;
            case SpellForm.Point:
                Debug.Log("point");
                
                break;
        }

        return SpellPreviewPoints;
    }

    public void PreviewSelection()
    {
        Debug.Log("aaahhh");
        foreach(WayPoint selectedPoint in ReadSelectionForm())
        {
            Debug.Log("suu");
            selectedPoint.Select();
        }
    }

    public void PreviewSpell(WayPoint selectedPoint)
    {
        foreach(WayPoint spellPoint in ReadSpellForm(selectedPoint))
        {
            //faut rajouter le reste là
        }
    }

    public void StopPreview()
    {

    }


    public virtual async Task Execute(WayPoint target)
    {
        await ShowVisuals(target);
    }

    protected virtual async Task ShowVisuals(WayPoint target)
    {
        //faire les visuals et les await bien comme un bon toutou de merde
    }
}

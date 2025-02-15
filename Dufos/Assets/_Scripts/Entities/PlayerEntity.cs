using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerEntity : Entity
{
    Spell _currentSpell;

    protected override void Start()
    {
        base.Start();
        CombatManager.Instance.PlayerEntities.Add(this);
    }

    protected void Update()
    {
        if (!CanInteract) return;
        if (Input.GetKeyDown(KeyCode.V))
        {
            PreviewFloodField();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            StopFloodFieldPreview();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            print("ESPACE");
            Data.Spells[0].StartSelectionPreview();
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            Data.Spells[0].StopSelectionPreview();
        }
    }

    public override void StartTurn()
    {
        base.StartTurn();
        CanInteract = true;
    }

    public override void EndTurn()
    {
        base.EndTurn();
        CanInteract = false;
    }

    public override async Task TryMoveTo(WayPoint targetPoint)
    {
        if(!CanInteract) return;
        CanInteract = false;
        await base.TryMoveTo(targetPoint);
        PreviewFloodField();
        CanInteract = true;
    }

    public override void UseSpell(int spellIndex)
    {
        if(!CanInteract) return;
        base.UseSpell(spellIndex);
    }

    void PreviewFloodField()
    {
        Flood();
        foreach(WayPoint point in Walkables)
        {
            point.ApplyWalkableVisual();
        }

    }

    void StopFloodFieldPreview()
    {
        foreach(WayPoint point in Walkables)
        {
            point.ApplyDefaultVisual();
        }
    }
}

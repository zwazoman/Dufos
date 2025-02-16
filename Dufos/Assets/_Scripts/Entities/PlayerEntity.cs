using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerEntity : Entity
{
    public bool IsFree;

    Spell _currentSpell;

    protected override void Awake()
    {
        base.Awake();

        foreach(Spell spell in Data.Spells)
        {
            spell.OnPreviewStarted += Lock;

            spell.OnPreviewCanceled += Free;
        }
    }

    protected override void Start()
    {
        base.Start();
        CombatManager.Instance.PlayerEntities.Add(this);
    }

    protected void Update()
    {
        if (!IsFree) return;
        if (Input.GetKeyDown(KeyCode.V))
        {
            PreviewFloodField();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            CancelFloodFieldPreview();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            print("ESPACE");
            UseSpell(0);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            Data.Spells[0].CancelSelectionPreview();
            Free();
        }
    }

    public override void StartTurn()
    {
        base.StartTurn();
        Free();
    }

    public override void EndTurn()
    {
        base.EndTurn();
        Lock();
    }

    public override async Task TryMoveTo(WayPoint targetPoint)
    {
        if(!IsFree) return;
        Lock();
        await base.TryMoveTo(targetPoint);
        Free();
    }

    public void UseSpell(int spellIndex)
    {
        if (!IsFree) return;
        if (spellIndex < Data.Spells.Length - 1)
        {
            Data.Spells[spellIndex].StartSelectionPreview();
        }
    }

    public void CancelSpellUse(int spellIndex)
    {
        Spell spell = Data.Spells[spellIndex];

        if (spell.IsPreviewing)
        {
            spell.CancelSelectionPreview();
            Free();
        }
    }

    void Free()
    {
        IsFree = true;
        PreviewFloodField();
    }

    void Lock()
    {
        IsFree = false;
        CancelFloodFieldPreview();
    }

    #region Flood Field;
    void PreviewFloodField()
    {
        Flood();
        foreach(WayPoint point in Walkables)
        {
            point.ApplyWalkableVisual();
        }

    }
    void CancelFloodFieldPreview()
    {
        foreach(WayPoint point in Walkables)
        {
            point.ApplyDefaultVisual();
        }
    }
    #endregion
}

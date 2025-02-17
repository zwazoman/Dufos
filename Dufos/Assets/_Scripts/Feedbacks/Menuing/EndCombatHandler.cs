using System.Collections;
using UnityEngine;

public class EndCombatHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject _victoryPanel;
    [SerializeField]
    private GameObject _defeatPanel;
    private bool _end = false;

    private void Update()
    {
        if (CombatManager.Instance.Entities.Count > 0 && !_end)
        {
            if (CombatManager.Instance.EnemyEntities.Count == 0)
            {
                _end = true;
                VictoryEnd();
            }

            if (CombatManager.Instance.PlayerEntities.Count == 0)
            {
                _end = true;
                DefeatEnd();
            }
        }
    }

    public void VictoryEnd()
    {
        print(CombatManager.Instance.EnemyEntities.Count);
        print("<color=green>no more enemies, you win</color>");
        // Ajouter le combat au compteur de combats.
    }

    public void DefeatEnd()
    {
        print("<color=red>no more players, you lose</color>");
    }
}

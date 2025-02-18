using DG.Tweening;
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
        print("<color=green>no more enemies, you win</color>");
        Sequence endGameSequence = DOTween.Sequence();

        endGameSequence
          .SetDelay(1.5f).onComplete += () =>
          {
              _victoryPanel.SetActive(true);
              // Ajouter le combat au compteur de combats (++).
          };
    }

    public void DefeatEnd()
    {
        print("<color=red>no more players, you lose</color>");
        Sequence endGameSequence = DOTween.Sequence();

        endGameSequence
          .SetDelay(1.5f).onComplete += () =>
          {
              _defeatPanel.SetActive(true);
          };

    }
}

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
        Sequence endGameSequence = DOTween.Sequence();

        endGameSequence
          .SetDelay(1.5f).onComplete += () =>
          {
              _victoryPanel.SetActive(true);
              SavedDataCenter.Instance.Data.ClearedCampsCount++;
              GameManager.Instance.FightsWon = SavedDataCenter.Instance.Data.ClearedCampsCount;
              SavedDataCenter.Instance.Save();
          };
    }

    public void DefeatEnd()
    {
        Sequence endGameSequence = DOTween.Sequence();

        endGameSequence
          .SetDelay(1.5f).onComplete += () =>
          {
              _defeatPanel.SetActive(true);
              SavedDataCenter.Instance.Save();
          };

    }
}

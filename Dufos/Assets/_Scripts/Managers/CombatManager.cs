using Cinemachine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    //singleton
    private static CombatManager instance;

    public static CombatManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("Combat Manager");
                instance = go.AddComponent<CombatManager>();
            }
            return instance;
        }
    }

    [SerializeField] CinemachineVirtualCamera cam;

    [HideInInspector] public List<Entity> Entities = new List<Entity>();

    [HideInInspector] public List<PlayerEntity> PlayerEntities = new List<PlayerEntity>();

    [HideInInspector] public List<Entity> EnemyEntities = new List<Entity>();

    [HideInInspector] public Entity CurrentEntity;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Entities = Entities.OrderByDescending(entity => entity.Data.Initiative).ToList();

        CurrentEntity = Entities[0];

        cam.LookAt = CurrentEntity.transform;

        CurrentEntity.StartTurn();
    }

    public void EndCurrentEntityTurn()
    {
        CurrentEntity.EndTurn();
    }

    public void NextTurn()
    {
        int currentIndex = Entities.IndexOf(CurrentEntity);

        if (currentIndex == Entities.Count -1)
        {
            currentIndex = -1;
        }

        CurrentEntity = Entities[currentIndex + 1];

        cam.LookAt = CurrentEntity.transform;

        CurrentEntity.StartTurn();
    }

}

using Cinemachine;
using System;
using System.Collections;
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

    [HideInInspector] public List<Entity> entities = new List<Entity>();

    [HideInInspector] public Entity CurrentEntity;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        entities = entities.OrderByDescending(entity => entity.Data.Initiative).ToList();

        CurrentEntity = entities[0];

        cam.LookAt = CurrentEntity.transform;

        CurrentEntity.StartTurn();
    }

    public void NextTurn()
    {
        CurrentEntity.EndTurn();

        int currentIndex = entities.IndexOf(CurrentEntity);

        print(entities.Count);
        print(currentIndex);

        if (currentIndex == entities.Count -1)
        {
            currentIndex = -1;
        }

        CurrentEntity = entities[currentIndex + 1];

        cam.LookAt = CurrentEntity.transform;



        CurrentEntity.StartTurn();
    }

}

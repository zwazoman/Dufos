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

    List<Entity> entities = new List<Entity>();

    public Entity CurrentEntity;

    private void Awake()
    {
        instance = this;
        
        entities = entities.OrderByDescending(entity => entity.Data.Initiative).ToList();

        CurrentEntity = entities[0];
        CurrentEntity.StartTurn();
    }

    public void NextTurn()
    {
        CurrentEntity.EndTurn();

        int currentIndex = entities.IndexOf(CurrentEntity);

        CurrentEntity = entities[currentIndex + 1];
        CurrentEntity.StartTurn();


    }

}

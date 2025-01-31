using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour
{
    //singleton
    private static Click instance;

    public static Click Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("Click");
                instance = go.AddComponent<Click>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    public event Action<Vector3> OnClick;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            print("click");
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.yellow);
                OnClick?.Invoke(hit.point);
                hit.collider.gameObject.SendMessage("OnClicked");
            }
        }
    }


}

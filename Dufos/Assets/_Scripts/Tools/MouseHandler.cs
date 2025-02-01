using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHandler : MonoBehaviour
{
    //singleton
    private static MouseHandler instance;

    public static MouseHandler Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("Click");
                instance = go.AddComponent<MouseHandler>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    public event Action OnClick;
    public event Action OnHover;

    private void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.yellow);
            print("hover");
            OnHover?.Invoke();
            hit.collider.gameObject.SendMessage("OnHovered", SendMessageOptions.DontRequireReceiver);
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                print("click");
                OnClick?.Invoke();
                hit.collider.gameObject.SendMessage("OnClicked", SendMessageOptions.DontRequireReceiver);
            }
        }
    }

}

using System;
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

    GameObject _previousObject;

    private void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Ground", "Wall")))
        {
            if(_previousObject != hit.collider.gameObject)
            {
                if (_previousObject != null)
                {
                    _previousObject.SendMessage("NotHovered", SendMessageOptions.DontRequireReceiver);
                }
                hit.collider.gameObject.SendMessage("Hovered", SendMessageOptions.DontRequireReceiver);
            }
            _previousObject = hit.collider.gameObject;

            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                OnClick?.Invoke();
                hit.collider.gameObject.SendMessage("Clicked", SendMessageOptions.DontRequireReceiver);
            }
        }
    }

}

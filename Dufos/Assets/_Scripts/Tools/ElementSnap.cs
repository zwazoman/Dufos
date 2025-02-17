using Singe;
using UnityEngine;

namespace Singe
{
    public class ElementSnap : MonoBehaviour
    {
        internal bool bonjour;

        private void Awake()
        {
            transform.position.SnapOnGrid();
            transform.position = Vector3.up * 0.5f;
        }
    }



}

public class SingeController
{
    ElementSnap snap;

    void zievhze()
    {
        Debug.Log(snap.bonjour);
    }
}




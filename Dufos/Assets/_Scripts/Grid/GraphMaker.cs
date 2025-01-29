using System.Collections.Generic;
using UnityEngine;

public class GraphMaker : MonoBehaviour
{
    [SerializeField] public Vector3Int StartPos; // get set
    [SerializeField] public Vector3Int EndPos; // get set

    [SerializeField] GameObject _waypointPrefab;
    [SerializeField] LayerMask _mask;

    public Dictionary<Vector3Int, WayPoint> PointDict = new Dictionary<Vector3Int, WayPoint>();

    //singleton
    private static GraphMaker instance;

    public static GraphMaker Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("Graph Maker");
                instance = go.AddComponent<GraphMaker>();
            }
            return instance;
        }
    }

    public List<WayPoint> ActivePoints = new List<WayPoint>();

    private void Awake()
    {
        instance = this;

        int xPos = StartPos.x;
        int zPos = StartPos.z;

        for (int i = 0; i < EndPos.z * 2; i++)
        {
            for (int j = 0; j < EndPos.x * 2; j++)
            {
                Vector3Int spawnPos = new Vector3Int(xPos + j, 0, zPos + i);
                WayPoint point = Instantiate(_waypointPrefab, (Vector3)spawnPos, Quaternion.identity).GetComponent<WayPoint>();
                point.transform.parent = this.transform;
                PointDict.Add(spawnPos, point);

                Vector3Int down = new Vector3Int(spawnPos.x, 0, spawnPos.z - 1);
                Vector3Int left = new Vector3Int(spawnPos.x - 1, 0, spawnPos.z);

                if (PointDict.ContainsKey(down))
                {
                    print("y'a en bas !!");
                    point.Neighbours.Add(PointDict[down]);
                    PointDict[down].Neighbours.Add(point);
                }
                if (PointDict.ContainsKey(left))
                {
                    print("y'a à gauche");
                    point.Neighbours.Add(PointDict[left]);
                    PointDict[left].Neighbours.Add(point);
                }
            }
        }




        //foreach (WayPoint point in PointDict.Values)
        //{
        //    //if (Physics.OverlapPoint(point.transform.position, _mask.value)) point.Deactivate(); else point.Activate();
        //    point.Activate();
        //    if (point.TryGetComponent<WayPoint>(out WayPoint wayPoint))
        //    {
        //        Vector3Int actualPos = new Vector3Int((int)point.transform.position.x, 0, (int)point.transform.position.z);

        //        WayPoint rightPoint = null;
        //        WayPoint leftPoint = null;
        //        WayPoint BottomPoint = null;
        //        WayPoint TopPoint = null;
        //        if (actualPos.x != EndPos.x) rightPoint = PointDict[new Vector3Int(actualPos.x + 1, 0, actualPos.z )].GetComponent<WayPoint>();
        //        if (actualPos.x != StartPos.x) leftPoint = PointDict[new Vector3Int(actualPos.x - 1, 0, actualPos.z)].GetComponent<WayPoint>();
        //        if (actualPos.z != StartPos.z) BottomPoint = PointDict[new Vector3Int(actualPos.x, 0, actualPos.z - 1)].GetComponent<WayPoint>();
        //        if (actualPos.z != EndPos.z) TopPoint = PointDict[new Vector3Int(actualPos.x, 0, actualPos.z + 1)].GetComponent<WayPoint>();

        //        if (!wayPoint.Neighbours.Contains(rightPoint)) wayPoint.Neighbours.Add(rightPoint);
        //        if (!wayPoint.Neighbours.Contains(leftPoint)) wayPoint.Neighbours.Add(leftPoint);
        //        if (!wayPoint.Neighbours.Contains(BottomPoint)) wayPoint.Neighbours.Add(BottomPoint);
        //        if (!wayPoint.Neighbours.Contains(TopPoint)) wayPoint.Neighbours.Add(TopPoint);
        //    }
        //}
    }

    public void ActivatePoint(WayPoint point)
    {
        if (ActivePoints.Contains(point)) return;
        ActivePoints.Add(point);
    }

    public void DeactivatePoint(WayPoint point)
    {
        if (!ActivePoints.Contains(point)) return;
        ActivePoints.Remove(point);
    }
}

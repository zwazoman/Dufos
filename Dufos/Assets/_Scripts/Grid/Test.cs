using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] Grid _grid;

    // Start is called before the first frame update
    void Start()
    {
        Vector3Int cellPosition = _grid.WorldToCell(new Vector3(0,10,0));
        transform.position = _grid.CellToWorld(cellPosition);
    }

    private void Update()
    {
        RaycastHit hit;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Physics.Raycast(ray, out hit);
        }
    }
}

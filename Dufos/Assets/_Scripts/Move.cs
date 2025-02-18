using System.Threading.Tasks;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 10;

    public async Task StartMoving(Vector3 targetPos)
    {
        targetPos.y = 1f;
        Vector3 offset = targetPos - (Vector3)transform.position;
        Quaternion targetRotation = Quaternion.Euler(0, Mathf.Atan2(offset.z, offset.x) * Mathf.Rad2Deg, 0);
        transform.rotation = targetRotation;
        while ((Vector3)transform.position != targetPos)
        {
            Vector3 offset2 = targetPos - (Vector3)transform.position;
            offset2 = Vector3.ClampMagnitude(offset2, Time.deltaTime * _moveSpeed);
            transform.Translate(offset2, Space.World);
            await Task.Yield();
        }
    }
}

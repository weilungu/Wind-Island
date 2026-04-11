using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 offset = new Vector3(0, 0, -10);
    [SerializeField] float smoothTime = 0.2f;
    
    [Header("Bounds")]
    [SerializeField] PolygonCollider2D polygonBounds;
    
    Vector3 velocity;
    
    void LateUpdate()
    {
        if (target.Equals(null)) return;
        
        Vector3 targetPos = target.position + offset;
        
        Vector3 smooth = Vector3.SmoothDamp(
            transform.position,
            targetPos,
            ref velocity,
            smoothTime
        );
        
        if (!polygonBounds.Equals(null))
        {
            if (!polygonBounds.OverlapPoint(smooth))
            {
                Vector2 closest = polygonBounds.ClosestPoint(smooth);
                smooth.x = closest.x;
                smooth.y = closest.y;
            }
        }
        
        transform.position = smooth;
    }
}
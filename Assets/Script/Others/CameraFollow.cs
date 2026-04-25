using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset = new Vector3(0, 0, -10);
    [SerializeField] private float smoothTime = 0.2f;
    
    [Header("Bounds")]
    [SerializeField] private PolygonCollider2D polygonBounds;
    
    private Vector3 velocity;
    
    void LateUpdate()
    {
        if (target is null) return;
        
        Vector3 targetPos = target.position + offset;
        
        Vector3 smooth = Vector3.SmoothDamp(
            transform.position,
            targetPos,
            ref velocity,
            smoothTime
        );
        
        if (polygonBounds is not null)
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
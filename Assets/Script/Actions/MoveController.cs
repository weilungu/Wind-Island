using UnityEngine;

public class MoveController : MonoBehaviour
{
    private Rigidbody2D rb;
    [HideInInspector] public ContactFilter2D filter;
    [HideInInspector] public RaycastHit2D[] hitResults { get; private set; } = new RaycastHit2D[1];
    [HideInInspector] public float castDistance = 0.02f;

    
    [Header("Values")]
    [SerializeField] private float speed = 5;

    [SerializeField] private LayerMask obstacleLayer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        
        filter = new ContactFilter2D();
        filter.SetLayerMask(obstacleLayer);
        filter.useLayerMask = true;
        filter.useTriggers = false;
    }

    public void Move(Vector2 direction)
    {
        if (direction.Equals(Vector2.zero)) return;

        float f_dt = Time.fixedDeltaTime * speed;
        Vector2 move = direction * f_dt;
        
        int hitCount = rb.Cast(direction, filter, hitResults, move.magnitude + castDistance);

        if (hitCount == 0)
        {
            rb.MovePosition(rb.position + move);
        }
        else
        {
            TrySlideMove(move);
        }
    }
    public float CanMove(bool canMove)
    {
        float tempSpeed = speed;
        
        float result = canMove ? tempSpeed : 0;
        print(speed);
        
        return result;
    }

    void TrySlideMove(Vector2 move)
    {
        Vector2 moveX = new Vector2(move.x, 0);
        if (moveX.magnitude > 0)
        {
            int hitX = rb.Cast(moveX.normalized, filter, hitResults, Mathf.Abs(move.x) + castDistance);
            if (hitX == 0)
                rb.MovePosition(rb.position + moveX);
        }

        Vector2 moveY = new Vector2(0, move.y);
        if (moveY.magnitude > 0)
        {
            int hitY = rb.Cast(moveY.normalized, filter, hitResults, Mathf.Abs(move.y) + castDistance);
            if (hitY == 0)
                rb.MovePosition(rb.position + moveY);
        }
    }
}
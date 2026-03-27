using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Move Input")]
    [SerializeField] float speed; 
    
    [Header("Instance")]
    [SerializeField] InputController inp;

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        Vector2 move = inp.GetMoveInput();
        if (move != Vector2.zero)
        {
            float dt = Time.deltaTime;

            // print($"horizontal: {inp.horizontal}");
            // print($"vertical: {inp.vertical}");

            transform.Translate(inp.GetMoveInput() * speed * dt);
        }
    }
}

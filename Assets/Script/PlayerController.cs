using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Move Input")]
    [SerializeField] float speed; 
    
    [Header("InputController")]
    [SerializeField] InputController inp;

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        float dt = Time.deltaTime;
        
        if (inp.GetMoveInput() != Vector2.zero)
        {
            print($"horizontal: {inp.horizontal}");
            print($"vertical: {inp.vertical}");

            (float h, float v) = (inp.horizontal, inp.vertical);
            Vector2 direction = new Vector2(h, v);

            transform.Translate(direction * speed * dt);
        }
    }
}

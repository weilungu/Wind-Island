using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] InputController inp;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        print($"horizontal: {inp.horizontal}");
        print($"vertical: {inp.vertical}");
    }
}

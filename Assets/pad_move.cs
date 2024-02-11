using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pad_move : MonoBehaviour
{

    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            rb.velocity = new Vector2(0, 10);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            rb.velocity = new Vector2(0, -10);
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
        }
    }
}

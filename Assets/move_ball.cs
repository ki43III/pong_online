using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_ball : MonoBehaviour
{

    public GameObject Ball;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = Ball.GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(300,400));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

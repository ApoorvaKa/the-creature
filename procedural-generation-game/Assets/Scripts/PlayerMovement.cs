using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float horizontal, vertical;
    public float speed;
    public bool canMove = true;
    
    private Rigidbody2D rb;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Update() {
        if (canMove) {
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");

            rb.velocity = new Vector2(horizontal, vertical).normalized * speed;

        } else {
            rb.velocity = new Vector2(0, 0);
        }
    }
}

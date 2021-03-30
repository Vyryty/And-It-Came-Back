using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    [HideInInspector]public Rigidbody2D rb2d;
    public float speed = 10f;
    public Vector2 initialDirection = new Vector2(1, 1);

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = initialDirection.normalized * speed;
    }
}

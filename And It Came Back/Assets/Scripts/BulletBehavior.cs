using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    Rigidbody2D rb2d;
    public bool infiniteBounces = true; //Whether the bullet should bounce infinitely or not
    public int bounceCount = 0;         //The number of walls the bullet has bounced off of
    public int maxBounces = 5;          //The max number of bounces of a bullet

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    public void setSpeed(float speed)
    {
        rb2d.velocity = rb2d.velocity.normalized * speed;
    }

    public void multSpeed(float speed)
    {
        rb2d.velocity *= speed;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        // Event when a bullet hits a wall and infinte bounces is turned off.
        if (!infiniteBounces && other.gameObject.tag == "Walls")
        {
            bounceCount += 1;
            if (bounceCount == maxBounces)
            {
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Event when a bullet hits a powerup
        if (other.gameObject.tag == "Powerup")
            other.GetComponent<Powerup>().activate(this.gameObject);
    }
}
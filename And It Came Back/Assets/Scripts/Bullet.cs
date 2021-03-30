using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int bounceCount = 0;

    void OnCollisionEnter2D(Collision2D other)
    {
        // Event when a bullet hits the player.
        if (other.gameObject.tag == "Player")
            Destroy(gameObject);
        if (other.gameObject.tag == "Powerup") {
            //On collision with a powerup, destroys both objects
            Destroy(other.gameObject);
            Destroy(gameObject);
        }

        // Event when a bullet hits a wall.
        if (other.gameObject.tag == "Walls")
        {
            bounceCount += 1;
            if (bounceCount == 5)
            {
                Destroy(gameObject);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int bounceCount = 0;
    public int maxBounces = 5;

    void OnCollisionEnter2D(Collision2D other)
    {
        // Event when a bullet hits the player.
        if (other.gameObject.tag == "Player")
        Destroy(gameObject);

        // Event when a bullet hits a wall.
        if (other.gameObject.tag == "Walls")
        {
            bounceCount += 1;
            if (bounceCount == maxBounces)
            {
                Destroy(gameObject);
            }
        }
    }
}

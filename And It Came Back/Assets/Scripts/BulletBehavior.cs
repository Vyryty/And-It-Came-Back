using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float defaultSpeed = 10f;
    public int maxBounces = 5;          //The max number of bounces of a bullet
    private int bounceCount = 0;         //The number of walls the bullet has bounced off of
    
    [HideInInspector]public PowerAttribute powerScript;
    [HideInInspector]public Rigidbody2D rb2d;
    [HideInInspector]public float actualSpeed;

    private float tripleSpread = 30f;    //How much the triple spreads in degrees



    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        powerScript = GetComponent<PowerAttribute>();
        actualSpeed = defaultSpeed;
    }



    private void FixedUpdate()
    {
        if (powerScript.currentPower != PowerupType.NA)
        {
            activatePowerup();
        }
    }



    void OnCollisionEnter2D(Collision2D other)
    {
        // Event when a bullet hits a wall and infinte bounces is turned off.
        if (maxBounces > 0 && other.gameObject.tag == "Walls")
        {
            bounceCount++;

            if (bounceCount >= maxBounces)
            {
                Destroy(gameObject);
            }
        }
    }



    void activatePowerup()
    {
        switch (powerScript.currentPower)
        {
            case PowerupType.Triple:

                //Find the vector perpendicular to the bullet's movement
                Vector3 velocity = rb2d.velocity;
                Vector3 perpendicular = Vector3.Cross(velocity, Vector3.back).normalized * .5f;

                //Create a new bullet angling out from the right side
                GameObject newBullet = Instantiate(bulletPrefab);
                BulletBehavior newBulletScript = newBullet.transform.GetComponent<BulletBehavior>();
                newBulletScript.powerScript.currentPower = PowerupType.NA;
                newBullet.transform.position = transform.position + perpendicular;
                newBulletScript.rb2d.AddForce(
                    Quaternion.Euler(0f, 0f, tripleSpread) * velocity,
                    ForceMode2D.Impulse);

                //Repeat for the left side
                newBullet = Instantiate(bulletPrefab);
                newBulletScript = newBullet.GetComponent<BulletBehavior>();
                newBulletScript.powerScript.currentPower = PowerupType.NA;
                newBullet.transform.position = transform.position - perpendicular;
                newBulletScript.rb2d.AddForce(
                    Quaternion.Euler(0f, 0f, -tripleSpread) * velocity,
                    ForceMode2D.Impulse);

                break;
            case PowerupType.Auto:
                rb2d.velocity *= 2;
                powerScript.powerupTimer = 5f;
                break;
            case PowerupType.Bomb:
                // Blow up bullet
                break;
            case PowerupType.Laser:
                //activate...
                break;
            case PowerupType.Shield:
                //activate...
                break;
            case PowerupType.Life:
                //activate...
                break;
            default:
                Debug.Log("Powerup type not handled/set");
                break;
        }

        powerScript.currentPower = PowerupType.NA;
    }
}
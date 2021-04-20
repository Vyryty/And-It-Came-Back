using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Gun { Revolver }

public class PlayerScript : MonoBehaviour
{
    public GameObject firingArm;    //The firing arm of the player
    public Camera cam;              //The camera for the scene
    public float moveSpeed = 5f;    //How fast the player moves
    public int health = 3;          //The health of the player
    public float turnSpeed = .1f;
    private float turned = 0f;
    private bool swap = false;
    public Gun weapon = Gun.Revolver;
    private float bulletVelocity;
    private float autoFireRate;
    private float shootingSpeed;

    [HideInInspector]public PowerAttribute powerupScript;
    [HideInInspector]Rigidbody2D rb2d;               //The rigidbody component of the player
    [HideInInspector]Vector2 movement;               //The direction the player is moving
    [HideInInspector]Vector2 mousePos;               //The position of the mouse in the camera

    public Transform firePoint;
    public GameObject bulletPrefab;


    void Start()
    {
        //Get the Rigidbody2D component from the script's gameobject
        rb2d = GetComponent<Rigidbody2D>();
        powerupScript = GetComponent<PowerAttribute>();

        switch (weapon)
        {
            default:
                bulletVelocity = 20f;
                autoFireRate = .1f;
                shootingSpeed = autoFireRate;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Get axes position input.
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Get mouse input.
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        float left = mousePos.x - transform.position.x;

        if (left < 0)
        {
            Vector2 temp = Vector2.Lerp(transform.localScale, new Vector2(-1, transform.localScale.y), Mathf.Abs(turned / turnSpeed));
            transform.localScale = temp;
            if (swap == false)
            {
                turned = 0;
                swap = true;
            }
        }
        else if (left > 0)
        {
            Vector2 temp = Vector2.Lerp(transform.localScale, new Vector2(1, transform.localScale.y), Mathf.Abs(turned / turnSpeed));
            transform.localScale = temp;
            if (swap == true)
            {
                turned = 0;
                swap = false;
            }
        }

        if (turned < turnSpeed)
            turned += Time.deltaTime;
        else
            turned = turnSpeed;

        if (powerupScript.currentPower == PowerupType.Auto && Input.GetButton("Fire1"))
        {
            if (shootingSpeed <= 0)
            {
                Shoot();
                shootingSpeed = autoFireRate;
            }
            else
            {
                shootingSpeed -= Time.deltaTime;
            }
        }
        else if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    // FixedUpdate is called once per physics frame
    void FixedUpdate()
    {
        // Move the player according to the input gathered.
        rb2d.MovePosition(rb2d.position + movement * moveSpeed * Time.fixedDeltaTime);
        
        // Allow the player to rotate in the z axis to follow the position of the mouse.
        Vector2 lookDir = mousePos - (Vector2)transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        firingArm.transform.rotation = Quaternion.RotateTowards(
                firingArm.transform.rotation,
                Quaternion.AngleAxis(angle, Vector3.forward),
                float.PositiveInfinity);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Bullet")
        {
            Destroy(other.gameObject);
            health--;
            if (health == 0)
                Destroy(this.gameObject);
        } 
    }

    void Shoot()
    {
        //Create a bullet at the player's firepoint and a forward force
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        BulletBehavior newBulletScript = bullet.GetComponent<BulletBehavior>();
        newBulletScript.rb2d.velocity = firePoint.up * bulletVelocity;
        newBulletScript.powerScript.currentPower = powerupScript.currentPower;
    }
}
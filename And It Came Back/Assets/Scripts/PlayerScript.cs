using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public GameObject firingArm;    //The firing arm of the player
    public Camera cam;              //The camera for the scene
    public float moveSpeed = 5f;    //How fast the player moves
    public int health = 3;          //The health of the player

    Rigidbody2D rb2d;               //The rigidbody component of the player
    Vector2 movement;               //The direction the player is moving
    Vector2 mousePos;               //The position of the mouse in the camera

    void Start()
    {
        //Get the Rigidbody2D component from the script's gameobject
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get axes position input.
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        
        // Get mouse input.
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
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

}
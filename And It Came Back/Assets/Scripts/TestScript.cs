using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public GameObject bulletPrefab;

    private Vector2 bulletSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKey(KeyCode.Space))
        {
            GameObject bullet = Instantiate<GameObject>(bulletPrefab);
            bullet.GetComponent<BulletBehavior>().initialDirection = new Vector2(Random.value, Random.value);
        }*/
    }
}

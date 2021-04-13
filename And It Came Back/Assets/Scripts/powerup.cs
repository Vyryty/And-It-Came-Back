using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    //The types that a powerup can be; NA is 'not applicable' and should only be used outside of the powerup itself
    public enum PowerupType { Triple, Auto, Bomb, Laser, Shield, Life, NA }
    public GameObject bulletPrefab;

    public PowerupType type;
    public float tripleSpread = 30f;    //How much the triple spreads in degrees

    //Activate the powerup on the given object
    public void activate(GameObject obj)
    {
        Debug.Log("Powerup hit (type=" + type.ToString() + ")");
        switch (type)
        {
            case PowerupType.Triple:
                activateTriple(obj);
                break;
            case PowerupType.Auto:
                activateAuto(obj);
                break;
            case PowerupType.Bomb:
                //activate...
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
        Destroy(this.gameObject);
    }

    private void activateTriple(GameObject obj)
    {
        //Activate the powerup on a player
        if (obj.tag == "Player") {
            
        }
        //Activate the powerup on a bullet
        else {
            //Find the vector perpendicular to the bullet's movement
            Vector3 velocity = obj.GetComponent<Rigidbody2D>().velocity;
            Vector3 perpendicular = Vector3.Cross(velocity, Vector3.back).normalized * .5f;

            //Create a new bullet angling out from the right side
            GameObject rightBullet = Instantiate(bulletPrefab);
            rightBullet.transform.position = obj.transform.position + perpendicular;
            rightBullet.GetComponent<Rigidbody2D>().AddForce(
                Quaternion.Euler(0f, 0f, tripleSpread) * velocity,
                ForceMode2D.Impulse);

            //Repeat for the left side
            GameObject leftBullet = Instantiate(bulletPrefab);
            leftBullet.transform.position = obj.transform.position - perpendicular;
            leftBullet.GetComponent<Rigidbody2D>().AddForce(
                Quaternion.Euler(0f, 0f, -tripleSpread) * velocity,
                ForceMode2D.Impulse);
        }
    }

    private void activateAuto(GameObject obj)
    {
        //Activate the powerup on a player
        if (obj.tag == "Player") {

        }
        //Activate the powerup on a bullet
        else {
            obj.GetComponent<BulletBehavior>().multSpeed(2f);
        }
    }
}

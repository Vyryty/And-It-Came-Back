using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    //The types that a powerup can be; NA is 'not applicable' and should only be used outside of the powerup itself
    public enum PowerupType { Triple, Auto, Bomb, Laser, Shield, Life, NA }
    public PowerupType type;                    //The type of the powerup
    public GameObject bulletPrefab;

    //Activate the powerup on a bullet
    public void activate(GameObject obj)
    {
        Debug.Log("Powerup hit (type=" + type.ToString() + ")");
        switch (type)
        {
            case PowerupType.Triple:
                activateTripleBullet(obj, bulletPrefab);
                break;
            case PowerupType.Auto:
                activateAutoBullet(obj);
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

    private static void activateTripleBullet(GameObject obj, GameObject bulletPrefab)
    {
        //Find the vector perpendicular to the bullet's movement
        Vector3 velocity = obj.GetComponent<Rigidbody2D>().velocity;
        Vector3 perpendicular = Vector3.Cross(velocity, Vector3.back).normalized * .5f;

        //Create a new bullet angling out from the right side
        GameObject rightBullet = Instantiate(bulletPrefab);
        rightBullet.transform.position = obj.transform.position + perpendicular;
        rightBullet.GetComponent<Rigidbody2D>().AddForce(
            Quaternion.Euler(0f, 0f, 30f) * velocity,
            ForceMode2D.Impulse);

        //Repeat for the left side
        GameObject leftBullet = Instantiate(bulletPrefab);
        leftBullet.transform.position = obj.transform.position - perpendicular;
        leftBullet.GetComponent<Rigidbody2D>().AddForce(
            Quaternion.Euler(0f, 0f, -30f) * velocity,
            ForceMode2D.Impulse);
    }

    private void activateAutoBullet(GameObject obj)
    {
        obj.GetComponent<BulletBehavior>().multSpeed(2f);
    }

    public static void activateTriplePlayer(GameObject player, GameObject bulletPrefab)
    {
        //Shoots a normal bullet, then activate a triple shot on it
        activateTripleBullet(player.GetComponent<Shooting>().ShootNormal(), bulletPrefab);
    }
}

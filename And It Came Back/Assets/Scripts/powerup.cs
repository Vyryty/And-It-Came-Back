using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerup : MonoBehaviour
{
    public enum PowerupType { Triple, Auto, Bomb, Laser, Shield, Life }
    public GameObject bulletPrefab;

    public PowerupType type;
    public float tripleSpread = 30f;    //How much the triple spreads in degrees

    public void activate(GameObject bullet)
    {
        Debug.Log("Powerup hit (type=" + type.ToString() + ")");
        switch (type)
        {
            case PowerupType.Triple:
                activateTriple(bullet);
                break;
            case PowerupType.Auto:
                activateAuto(bullet);
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

    private void activateTriple(GameObject bullet)
    {
        //Find the vector perpendicular to the bullet's movement
        Vector3 velocity = bullet.GetComponent<Rigidbody2D>().velocity;
        Vector3 perpendicular = Vector3.Cross(velocity, Vector3.back).normalized * .5f;

        //Create a new bullet angling out from the right side
        GameObject rightBullet = Instantiate(bulletPrefab);
        rightBullet.transform.position = bullet.transform.position + perpendicular;
        rightBullet.GetComponent<Rigidbody2D>().AddForce(
            Quaternion.Euler(0f, 0f, tripleSpread) * velocity,
            ForceMode2D.Impulse);

        //Repeat for the left side
        GameObject leftBullet = Instantiate(bulletPrefab);
        leftBullet.transform.position = bullet.transform.position - perpendicular;
        leftBullet.GetComponent<Rigidbody2D>().AddForce(
            Quaternion.Euler(0f, 0f, -tripleSpread) * velocity,
            ForceMode2D.Impulse);
    }

    private void activateAuto(GameObject bullet)
    {
        bullet.GetComponent<Rigidbody2D>().velocity *= 2f;
    }
}

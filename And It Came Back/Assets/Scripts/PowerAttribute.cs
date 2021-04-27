using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The types that a powerup can be; NA is 'not applicable' and should only be used outside of the powerup itself
public enum PowerupType { Triple, Auto, Bomb, Laser, Shield, Life, NA } //Clone: if the timer is up, destroy itself

public class PowerAttribute : MonoBehaviour
{
    public PowerupType currentPower = PowerupType.NA;
    public float powerupTimer = 0f; //A timer for when the powerup runs out
    public bool useTimer = true;    //Whether to use the timer or counter

    private void FixedUpdate()
    {
        if (powerupTimer <= 0)
            currentPower = PowerupType.NA;
        else
            if (useTimer)
                powerupTimer -= Time.deltaTime;
    }
}

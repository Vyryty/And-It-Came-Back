using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{
    public GameObject powerupPrefab;
    public float spawnCooldown = 5f;    //Time (in seconds) until the next powerup spawns
    public Transform[] locations;      //The transforms of each powerup spawner; index 0 is the powerup spawner controller
    private float timeUntilSpawn = 0;     //Time until the next powerup spawns
    private int numLocations;           //The number of powerup spawners



    // Start is called before the first frame update
    void Start()
    {
        locations = GetComponentsInChildren<Transform>();
        numLocations = locations.Length;
    }



    // FixedUpdate is called once per physics frame
    void FixedUpdate()
    {
        timeUntilSpawn += Time.deltaTime;



        //When it is time for a powerup to spawn
        if (timeUntilSpawn >= spawnCooldown) {
            //Tries to spawn a powerup at an empty random powerup spawner
            //If all locations already have powerups, doesn't spawn a powerup
            int locIndex = Random.Range(1, numLocations), checks = 0;
            bool spawned = false;
            while (!spawned && checks < numLocations) {
                //If the location is free
                if (locations[locIndex].childCount == 0) {
                    //Spawn a powerup at this location
                    GameObject powerup = Instantiate(powerupPrefab, locations[locIndex].position, Quaternion.identity, locations[locIndex]);
                    //Set the powerup to have a random type
                    powerup.GetComponent<Powerup>().type = (PowerupType)Random.Range(0, (int)PowerupType.NA);
                    //Set the powerup's name for easy identifiability
                    powerup.name = "powerup (" + powerup.GetComponent<Powerup>().type + ")";
                    spawned = true;
                } else {
                    //Check the next location
                    locIndex = (locIndex + 1) % numLocations;
                    checks++;
                }
            }
            timeUntilSpawn = 0;
        }
    }
}

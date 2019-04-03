﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManagerScript : MonoBehaviour
{
    public List<GameObject> cores;
    public float test;

    public List<GameObject> enemyList;

    public int waveNumber;
    public int enemiesRemaining;
    public int numEnemiesToSpawn;

    int spawnDirection;
    float spawnDirection2;
    public GameObject enemyReference;
    Vector2 spawnLocation;
    public int spawnTimer;
    
    // Use this for initialization
    void Awake()
    {
        waveNumber = 0;
        spawnLocation = Vector2.zero;
        spawnTimer = 0;
        //this method call will be moved later once we have a game loop that continually generates waves once the previous is beaten
        setupWave();
        
        enemyList = new List<GameObject>();
    }


    // Update is called once per frame
    void Update()
    {
        spawnEnemies();
    }

    void setupWave()
    {
        //increases wave number, sets number of enemies to be spawned and syncs enemies remaining with that number, resets spawn timer to zero
        waveNumber++;
        numEnemiesToSpawn = waveNumber * 2 + 2;
        enemiesRemaining = numEnemiesToSpawn;
        spawnTimer = 0;
    }

    //method to spawn enemies on outer ring on random tiles (outer ring is ((-3,-2) (-3,2) (3,-2) (3,2)) only spawn on tenths aka (-3,.6) or (.3,2)
    void spawnEnemies()
    {
        spawnTimer++;
        //all of this only happens if the timer reaches a certain point AND there are enemies left to spawn in the wave
        if (numEnemiesToSpawn > 0&&spawnTimer==120)
        {
            //select random number to see if spawn on top, left, bottom, or right of screen
            spawnDirection = Random.Range(0, 4);

            //depending on selection, enemy is placed on one of the sides of the outer spawning rectangle
            if (spawnDirection == 0)
            {
                spawnLocation.y = 2;
                spawnDirection2 = Random.Range(-3, 3);
            }
            else if (spawnDirection == 1)
            {
                spawnLocation.x = -3;
                spawnDirection2 = Random.Range(-2, 2);
            }
            else if (spawnDirection == 2)
            {
                spawnLocation.y = -2;
                spawnDirection2 = Random.Range(-3, 3);
            }
            else if (spawnDirection == 3)
            {
                spawnLocation.x = 3;
                spawnDirection2 = Random.Range(-2, 2);
            }

            //rounds spawn direction 2 to an even tenth of a number
            spawnDirection2 = spawnDirection2 * 10;
            spawnDirection2 = Mathf.Round(spawnDirection2);
            spawnDirection2 = spawnDirection2 / 10;

            //set final spawn location
            if (spawnDirection == 0 || spawnDirection == 2)
                spawnLocation.x = spawnDirection2;
            else if (spawnDirection == 2 || spawnDirection == 3)
                spawnLocation.y = spawnDirection2;

            //spawn enemy using this location
            enemyList.Add(Instantiate(enemyReference, spawnLocation, Quaternion.identity));

            //reduce enemies left to spawn by one
            numEnemiesToSpawn--;

            //reset spawnTimer
            spawnTimer = 0;
        }

        if (enemiesRemaining == 0)
        {
            //sets up next wave's numbers
            setupWave();
            //call method that is while loop containing the shop
        }
            

    }
}

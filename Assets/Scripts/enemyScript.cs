﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour
{
    public gameManagerScript mScript;
    Vector2 targetPosition;
    Vector2 distanceFromTowers;
    Vector2 position;
    bool isWalking;
    int direction;
    bool hasMovedVertical;
    bool hasMovedHorizontal;
    public int health;
    // Use this for initialization
    void Start()
    {
        mScript = GameObject.Find("gameManager").GetComponent<gameManagerScript>();
        targetPosition = Vector2.zero;
        distanceFromTowers = new Vector2(0, 1000);
        position = transform.position;
        isWalking = false;
        direction = -1;
        calculateTargetTower();
        hasMovedVertical = false;
        hasMovedHorizontal = false;
        health = 5;
    }

    void calculateTargetTower()
    {
        foreach (var t in mScript.GetComponent<gameManagerScript>().towers)
        {
            if ((transform.position - t.transform.position).magnitude < distanceFromTowers.magnitude)
            {
                distanceFromTowers = transform.position - t.transform.position;
                targetPosition = t.transform.position;
            }
        }
        if (targetPosition.y > position.y)
            targetPosition.y -= .1f;
        if (targetPosition.x < position.x)
            targetPosition.x += .1f;
        if (targetPosition.y < position.y)
            targetPosition.y += .1f;
        if (targetPosition.x > position.x)
            targetPosition.x -= .1f;
    }

    // Update is called once per frame
    void Update()
    {
        movement();
        transform.position = position;
    }

    void movement()
    {
        if (!hasMovedVertical)
        {
            if (targetPosition.y > position.y)
                direction = 0;
            else if (targetPosition.y < position.y)
                direction = 2;
        }
        else if (!hasMovedHorizontal)
        {
            if (targetPosition.x < position.x)
                direction = 1;
            else if (targetPosition.x > position.x)
                direction = 3;
        }

        if (!hasMovedVertical || !hasMovedHorizontal)
        {
            if (direction == 0)
                position.y += .01f;
            if (direction == 1)
                position.x -= .01f;
            if (direction == 2)
                position.y -= .01f;
            if (direction == 3)
                position.x += .01f;

            if (Mathf.Abs((targetPosition.y - position.y)) <= .01f|| Mathf.Abs((targetPosition.y - position.y)) <= -.01f)
            {
                position.y = position.y * 10;
                position.y = Mathf.Round(position.y);
                position.y = position.y / 10;
                isWalking = false;
                hasMovedVertical = true;
            }
            if (Mathf.Abs((targetPosition.x - position.x)) <= .01f || Mathf.Abs((targetPosition.x - position.x)) <= -.01f)
            {
                position.x = position.x * 10;
                position.x = Mathf.Round(position.x);
                position.x = position.x / 10;
                isWalking = false;
                hasMovedHorizontal = true;
            }
        }
    }

    //method reduces enemy health when hit by projectile
    void reduceHealth(int dmg)
    {
        health-=dmg;
        if (health <= 0)
        {
            destroyEnemy();
        }
    }

    //method to destroy enemy and call methods in manager to add score/money
    void destroyEnemy()
    {
        //destroy enemy
        Destroy(gameObject);
        //call manager to reduce enemy count for wave
        //call manager to add money to player's bank etc
    }
    //method to attack when reached target tower

}
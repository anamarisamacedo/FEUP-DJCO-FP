using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static utils.Configs;

public class Monster : MonoBehaviour
{
    private Rigidbody rigidBody;
    private int hp;

    private void Start()
    {
        hp = 100;
    }

    private void Update()
    {
        if (!FollowPlayer())
        {
            MoveRandomly();
        }
    }

    public bool FollowPlayer()
    {
        Transform character = GameObject.Find("GirlCharacter").transform;

        float distanceFromPlayer = Vector3.Distance(transform.position, character.position);

        if (distanceFromPlayer < MonsterFollowRadius)
        {
            transform.LookAt(character);
            transform.position += transform.forward * MonsterSpeed * Time.deltaTime;
            return true;
        }
        else
        {
            return false;
        }
    }

    private void MoveRandomly()
    {
        System.Random rand = new System.Random();
        int randomAngle = rand.Next(-22500, 22500);
        if (Math.Abs(randomAngle) < 90)
        {
            transform.Rotate(0, randomAngle, 0);
        }
        else
        {
            transform.position += transform.forward * Time.deltaTime * MonsterSpeed;
        }
    }

    public void TakeDamage(int value)
    {
        hp -= value;
        if (hp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static utils.Configs;

public class Monster : MonoBehaviour
{
    private void Start()
    {
    }

    private void Update()
    {
        Move();
    }

    private void Move()
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
}
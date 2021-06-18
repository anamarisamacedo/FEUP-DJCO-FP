using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static utils.Configs;
using utils;


public class NPC : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        MoveRandomly();
    }

    private void MoveRandomly()
    {
        System.Random rand = new System.Random();
        int randomAngle = rand.Next(-1400, 1400);
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

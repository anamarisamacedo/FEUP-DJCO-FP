using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static utils.Configs;
using utils;


public class Monster : MonoBehaviour
{
    private Rigidbody rigidBody;
    private int hp;
    private FMOD.Studio.EventInstance instance;
    private TerrainUtils tu;
    public GameObject drop;
    public LayerMask playersMask;
    private float nextAttackTime = 0f;
    private float nextAttackCheckTime = 0f;

    private void Start()
    {
        hp = 100;
        instance = FMODUnity.RuntimeManager.CreateInstance("event:/Monster/Walking");
        tu = new TerrainUtils();
        instance.setParameterByName("Terrain", tu.SelectFootstep(this.transform.position));
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(instance, this.transform, GetComponent<Rigidbody>());
        instance.start();
    }

    private void Update()
    {
        if (!FollowPlayer())
        {
            MoveRandomly();
        }
        instance.setParameterByName("Terrain", tu.SelectFootstep(this.transform.position));
        Attack();
    }

    public bool FollowPlayer()
    {
        GameObject character = GameObject.Find("GirlCharacter");
        if (!character.GetComponent<Character>().enabled)
        {
            character = GameObject.Find("BoyCharacter");
        }

        Transform characterTransf = character.transform;

        float distanceFromPlayer = Vector3.Distance(transform.position, characterTransf.position);

        if (distanceFromPlayer < MonsterFollowRadius)
        {
            transform.LookAt(characterTransf);
            transform.position += transform.forward * MonsterSpeed * 3 * Time.deltaTime;
            return true;
        }
        else
        {
            return false;
        }
    }

    private void MoveRandomly()
    {
        if (gameObject.name != "GuardMonster")
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

    private void Attack()
    {
        if (Time.time >= nextAttackCheckTime && Time.time >= nextAttackTime)
        {
            nextAttackCheckTime = Time.time + 1f / 5f;

            Collider[] hitPlayers = Physics.OverlapSphere(transform.position, MonsterAttackRadius, playersMask);

            foreach (Collider player in hitPlayers)
            {
                nextAttackTime = Time.time + 1f / MonsterAttackRate;
                FMODUnity.RuntimeManager.PlayOneShot("event:/Monster/MeleeAttack");
                gameObject.GetComponent<Animator>().SetBool("IsAttacking", true);
                StartCoroutine(StopAttackAnimation(player));
            }
        }
    }

    private IEnumerator StopAttackAnimation(Collider player)
    {
        yield return new WaitForSeconds(1f);
        player.gameObject.GetComponent<Character>().TakeDamage(MonsterDamage);
        yield return new WaitForSeconds(0.94f);
        gameObject.GetComponent<Animator>().SetBool("IsAttacking", false);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Arrow"))
        {
            TakeDamage(ArrowDamage);
        }
    }

    public void TakeDamage(int value)
    {
        hp -= value;
        if (hp <= 0)
        {
            Die();
        }
        FMODUnity.RuntimeManager.PlayOneShot("event:/Monster/TakeDamage");
    }

    private void Die()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Monster/Die");
        Instantiate(drop, gameObject.transform.position + new Vector3(0, 2, 0), Quaternion.identity);
        Destroy(gameObject);
    }
}
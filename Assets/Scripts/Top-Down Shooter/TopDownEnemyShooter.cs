using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownEnemyShooter : MonoBehaviour
{
    public int health = 100;
    public GameObject deathEffect;
    public float deathEffectTimer;

    public GameObject player;

    public float waitTime;
    private float currentTime;
    private bool shot;

    public GameObject enemyBullet;
    public Transform bulletSpawnpoint;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(player.transform);

        if(currentTime == 0)
        {
            Shoot();
        }
        if (shot && currentTime < waitTime)
        {
            currentTime += 1 * Time.deltaTime;
        }
        if (currentTime >= waitTime)
        {
            currentTime = 0;
        }
    }


    public void FacingPlayer()
    {

    }
    public void Shoot()
    {
        shot = true;
        Instantiate(enemyBullet, bulletSpawnpoint.position, bulletSpawnpoint.rotation);
    }



    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }


    void Die()
    {
        GameObject deatheffect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(deatheffect, deathEffectTimer);
        Debug.Log("Enemy is dead");
        Destroy(gameObject);
    }
}

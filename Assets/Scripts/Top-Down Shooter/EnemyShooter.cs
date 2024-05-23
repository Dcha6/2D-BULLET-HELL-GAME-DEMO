using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyShooter : MonoBehaviour
{
    public float speed;
    public float stoppingDistance;
    public float retreatDistance;

    public Transform player;

    //shooting variables
    private float timeBtwnShots;
    public float startTimeBtwnShots;

    public GameObject projectile;

    public int health = 100;
    public GameObject deathEffect;
    public float deathEffectTimer;

    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player").transform;

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        timeBtwnShots = startTimeBtwnShots;
    }

    void Update()
    {
        if (player == null)
        {
            return; // Exit the Update method if player is not set
        }

        if (Vector2.Distance(transform.position, player.position)> stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
        else if (Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position)>retreatDistance)
        {
            transform.position = this.transform.position;
        }
        else if (Vector2.Distance(transform.position, player.position) < retreatDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
        }
        if (timeBtwnShots <= 0)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);

            timeBtwnShots = startTimeBtwnShots;
        }
        else
        {
            timeBtwnShots -= Time.deltaTime;
        }
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
        GameObject playerEffect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(playerEffect, deathEffectTimer);
        Destroy(this.gameObject);
    }
}

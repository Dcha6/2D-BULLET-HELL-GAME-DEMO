using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownBUllet : MonoBehaviour
{
    public GameObject hiteffect;
    public float timer;

    public float effectTimer = 5f;
    public int damage = 10;
    void Start()
    {
        
    }
    void Update()
    {
        timer += 1.0F * Time.deltaTime;
        if (timer >= 1)
        {
            GameObject.Destroy(this.gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        TopDownEnemy enemy = other.GetComponent<TopDownEnemy>();
        if(enemy != null)
        {
            enemy.TakeDamage(damage);
        }
        EnemyShooter shooter = other.GetComponent<EnemyShooter>();
        if (shooter != null)
        {
            shooter.TakeDamage(damage);
        }

        Spawner spawner = other.GetComponent<Spawner>();
        if(spawner != null)
        {
            spawner.TakeDamage(damage);
        }
        CircleTowerShooter towerboss = other.GetComponent<CircleTowerShooter>();
        if(towerboss != null)
        {
            towerboss.TakeDamage(damage);
        }
        TopDownBoss boss = other.GetComponent<TopDownBoss>();
        if (boss != null)
        {
            boss.TakeDamage(damage);
        }
        Destroy(gameObject);


    }
    void OnCollisionEnter2D(Collision2D other)
    {
        GameObject effect = Instantiate(hiteffect, transform.position, Quaternion.identity);
        Destroy(effect, effectTimer);
        Destroy(gameObject);
    }



}

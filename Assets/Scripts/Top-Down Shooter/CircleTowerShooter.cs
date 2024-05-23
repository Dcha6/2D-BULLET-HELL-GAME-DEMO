using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CircleTowerShooter : MonoBehaviour
{
    public int health;
    public GameObject deathEffect;
    public float deathEffectTimer;

    public GameObject bullet;
    public Transform firepoint;
    public float fireRate;
    public float nextFireRate;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextFireRate)
        {
            nextFireRate = Time.time + fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(bullet, firepoint.position, firepoint.rotation);
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
        SceneManager.LoadScene("RightRoom1_1");
        Destroy(gameObject);
    }
    
}

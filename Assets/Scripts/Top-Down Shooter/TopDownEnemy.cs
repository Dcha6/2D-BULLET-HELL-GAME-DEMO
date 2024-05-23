using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TopDownEnemy : MonoBehaviour
{
    
    public int health = 100;
    public GameObject deathEffect;
    public float deathEffectTimer;

    //Follow function
    public Rigidbody2D rb;
    public GameObject target;
    public Transform player;
    public float moveSpeed;
    public Vector3 directionToTarget;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
        moveSpeed = Random.Range(1f, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        MoveMonster();
        RotateMonster(player.position);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Player":
                MonsterSpawnerControl.spawnAllowed = false;
                GameObject playerEffect = Instantiate(deathEffect, other.gameObject.transform.position, Quaternion.identity);
                Destroy(playerEffect, deathEffectTimer);
                Destroy(other.gameObject);
                SceneManager.LoadScene("GameOverL");
                target = null;
                break;
           
        }
    }

    void MoveMonster()
    {
        if(target != null)
        {
            directionToTarget = (target.transform.position - transform.position).normalized;
            rb.velocity = new Vector2(directionToTarget.x * moveSpeed, directionToTarget.y * moveSpeed);
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }
    
    void RotateMonster(Vector2 player)
    {
        var offset = 90f;
        Vector2 direction = player - (Vector2)transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
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
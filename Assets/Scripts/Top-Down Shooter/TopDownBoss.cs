using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TopDownBoss : MonoBehaviour
{
    //stats & variables
    public GameObject deathEffect;
    public float deathEffectTimer;

    //health
    public const int maxHealth = 100;
    public int currentHealth = maxHealth;
    public Slider healthBar;
    public RectTransform HealthBar;

    //Follow function
    public Rigidbody2D rb;
    public GameObject target;
    public Transform player;
    public float moveSpeed;
    public Vector3 directionToTarget;

    //Shooting
    private float timeBtwnShots;
    public float startTimeBtwnShots;
    public GameObject projectile;

    public enum battleStates
    {
        STAGEONE,
        WALKING,
        IDLE,
        SHOOTING,
        STAGETWO,
        CIRCLESHOOTING,
        SHOOTING2,
        MISSLE,
        DEAD
    }

    public battleStates currentState;
    bool dead;
    bool stageTwo;
    private bool actionStarted = false;
    private int rand;
    void Start()
    {
        target = GameObject.Find("Player");
        player = target.transform;
        rb = GetComponent<Rigidbody2D>();
        moveSpeed = Random.Range(1f, 3f);
        currentState = battleStates.STAGEONE;
        timeBtwnShots = startTimeBtwnShots;
    }

    void Update()
    {
        MoveMonster();
        RotateMonster(player.position);
        healthBar.value = currentHealth;
        if (currentHealth <= 50 && !stageTwo)
        {
            stageTwo = true;
            currentState = battleStates.STAGETWO;
        }
        if (currentHealth <= 0 && !dead)
        {
            dead = true;
            currentState = battleStates.DEAD;
        }
        switch (currentState)
        {
            case battleStates.STAGEONE:
                Debug.Log("Intro");
                StartCoroutine(ExecuteAfterTime(5));
                break;

            case battleStates.WALKING:
                Debug.Log("Walking");
                MoveMonster();
                RotateMonster(player.position);
                StartCoroutine(ExecuteAfterTime(20));
                break;

            case battleStates.IDLE:
                Debug.Log("Idle");
                StartCoroutine(ExecuteAfterTime(20));
                break;

            case battleStates.SHOOTING:
                Debug.Log("Shooting");
                Shooting();
                StartCoroutine(ExecuteAfterTime(20));
                break;

            case battleStates.STAGETWO:
                Debug.Log("StageTwo");
                break;

            case battleStates.SHOOTING2:
                Debug.Log("Shooting2");
                break;

            case battleStates.CIRCLESHOOTING:
                Debug.Log("CircleShooting");
                break;

            case battleStates.MISSLE:
                Debug.Log("Missle");
                break;

            case battleStates.DEAD:
                Debug.Log("Dead");
                Die();
                break;
        }
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

    IEnumerator ExecuteAfterTime(float time)
    {
        rand = Random.Range(0, 2);
        if (rand == 0)
        {
            currentState = battleStates.IDLE;
        }
        else if (rand == 1)
        {
            currentState = battleStates.SHOOTING;
        }
        else if (rand == 2)
        {
            currentState = battleStates.WALKING;
        }
        yield return new WaitForSeconds(time);

    }
    void Shooting()
    {
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
    void MoveMonster()
    {
        if (target != null)
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
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
        HealthBar.sizeDelta = new Vector2(currentHealth * 5, HealthBar.sizeDelta.y);
    }
    void Die()
    {
        GameObject deatheffect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(deatheffect, deathEffectTimer);
        Debug.Log("Boss is dead");
        Destroy(gameObject);
    }
}

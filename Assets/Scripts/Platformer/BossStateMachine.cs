using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossStateMachine : MonoBehaviour
{
    public Enemy enemy;
    public EnemyShooting enemyShooting;
    public float delayTime = 5f;
    

   
    public enum battleStates
    {
        STAGEONE,
        IDLE,
        SHOOTING,
        STAGETWO,
        BULLETSPREAD,
        LASER,
        IDLE2,
        SHOOTING2,
        DEAD
    }
    public battleStates currentState;

    public const int maxHealth = 100;
    public int currentHealth = maxHealth;
    public Slider healthBar;
    public RectTransform HealthBar;
    public GameObject deathEffect;

    //Shooting variables
    public GameObject shot;
    public Transform bulletSpawn;
    public Transform bulletSpawn1;
    public float fireRate;
    private float nextFire;

    public float timeBtwnShots;
    public float startTimeBtwnShots;
    public GameObject shots;

    public Animator animator;

    bool dead;
    bool stageTwo;
    private bool actionStarted = false;
    void Start()
    {
        currentState = battleStates.STAGEONE;
        enemy = FindObjectOfType<Enemy>();
        timeBtwnShots = startTimeBtwnShots;
        enemyShooting = FindObjectOfType<EnemyShooting>();


    }

    private int rand;
    // Update is called once per frame
    void Update()
    {
        
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

            case battleStates.IDLE:
                Debug.Log("Idle");
                animator.SetTrigger("idle");
                StartCoroutine(ExecuteAfterTime(15));
                break;

            case battleStates.SHOOTING:
                Debug.Log("Shooting");
                animator.SetTrigger("shooting");
                Shooting();
                StartCoroutine(ExecuteAfterTime(15));
                break;

            case battleStates.STAGETWO:
                Debug.Log("StageTwo");

                //StartCoroutine(ExecuteAfterTime2(10));
                if (stageTwo == true)
                {
                    animator.SetTrigger("stageTwo");
                }
                currentState = battleStates.IDLE2;
                break;

            case battleStates.IDLE2:
                Debug.Log("idle2");
                animator.SetTrigger("idle2");
                StartCoroutine(TimeForAction());
                break;

            case battleStates.BULLETSPREAD:
                Debug.Log("bullet spread");
                animator.SetTrigger("bulletspread");
                BulletSpread();
                break;

            case battleStates.LASER:
                Debug.Log("laser");
                animator.SetTrigger("laser");
                Lasers();
                
                break;

            case battleStates.SHOOTING2:
                Debug.Log("shooting2");
                animator.SetTrigger("shooting2");
                Shooting();
                break;

            case battleStates.DEAD:
                Debug.Log("Dead");
                animator.SetTrigger("dead");
                Die();
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
        else
        {
            currentState = battleStates.SHOOTING;
        }
        yield return new WaitForSeconds(time);
        
    }

    IEnumerator ExecuteAfterTime2(float time)
    {
        yield return new WaitForSeconds(time);
        rand = Random.Range(1, 3);
         if (rand == 1)
        {
            currentState = battleStates.SHOOTING2;
        }
        else if (rand == 2)
        {
            currentState = battleStates.LASER;
        }
        else if (rand == 3)
        {
            currentState = battleStates.BULLETSPREAD;
        }
    }

    IEnumerator TimeForAction()
    {
        if (actionStarted)
        {
            yield break;
        }
        actionStarted = true;

        //wait
        yield return new WaitForSeconds(0.5f);

        //dmg
        BulletSpread();
        Lasers();

        //end coroutine
        actionStarted = false;

        //reset this enemy state
        currentState = battleStates.STAGETWO;
    }
    void Shooting()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, bulletSpawn.position, bulletSpawn.rotation);
        }
    }
    public void BulletSpread()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, bulletSpawn1.position, bulletSpawn1.rotation);
        }
    }
    void Lasers()
    {
        if (timeBtwnShots <= 0)
        {
            Instantiate(shots, transform.position, Quaternion.identity);
            timeBtwnShots = startTimeBtwnShots;
        }
        else
        {
            timeBtwnShots -= Time.deltaTime;
        }
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
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
        SceneManager.LoadScene("2Stage1");
    }
}

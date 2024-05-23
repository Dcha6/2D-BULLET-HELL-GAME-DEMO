using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TopDownPlayer : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    Vector2 mousePos;
    Vector2 movement;
    public Camera cam;

    public int health;

    public GameObject dashEffect;
    public GameObject deathEffect;
    public float deathEffectTimer;


    //dash
    public bool dashing = false;
    private Vector3 mouseLocation;

    //gem
    //public static bool gemL = false;
    //public static bool gemR = false;
    //public static bool gemB = false;

    //gameover
    public bool bulletHitR = false;
    public bool bulletHitB = false;
    public bool bossHit = false;
    




    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }
    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

    //    if (Input.GetButtonDown("Dash"))
    //    {
    //        StartCoroutine("DashMove");
    //    }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        Vector2 lookDirection = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;

        if (Input.GetButtonDown("Dash") && !dashing)
        {
            dashing = true;
            Invoke("endDash", 0.5f);
            mouseLocation = mousePos;
        }
        if (!dashing)
        {

        }
        else
        {
            float step = moveSpeed * 2 * Time.deltaTime;
            this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(mouseLocation.x, mouseLocation.y, 0), step);
        }
    }
    void endDash()
    {
        dashing = false;
    }
    //IEnumerator DashMove()
    //{
    //    moveSpeed += 5;
    //    yield return new WaitForSeconds(5f);
    //    moveSpeed -= 5;
    //    Instantiate(dashEffect, transform.position, Quaternion.identity);
    //}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "teleporter")
        {
            CollisionHandler col = other.gameObject.GetComponent<CollisionHandler>();
            GameManager.instance.nextSpawnPoint = col.spawnPointName;
            GameManager.instance.sceneToLoad = col.sceneToLoad;
            GameManager.instance.LoadNextScene();
        }
        if(other.tag == "EnemyBullet")
        {
            Debug.Log("bullet hit");
            bulletHitR = true;
        }
        if(other.tag == "FollowingBullet")
        {
            bulletHitB = true;
        }
        if(other.tag == "Boss")
        {
            bossHit = true;
        }
        
        switch (other.name)
        {
            case "GemB":
                GameManager.instance.gemB = true;
                GemScore.scoreValue ++;
                Destroy(other.gameObject);
                LoadSceneBasedOnGemScore();
                //if (GemScore.scoreValue == 1)
                //{
                //    SceneManager.LoadScene("MainlandB");
                //}
                //else if ((GemScore.scoreValue == 2) && (GameManager.instance.gemL = true))
                //{
                //    SceneManager.LoadScene("MainlandLB");
                //}
                //else if ((GemScore.scoreValue == 2) && (GameManager.instance.gemR = true))
                //{
                //    SceneManager.LoadScene("MainlandRB");
                //}
                //else if (GemScore.scoreValue == 3)
                //{
                //    SceneManager.LoadScene("MainlandFinal");
                //}
                break;

            case "GemL":
                GameManager.instance.gemL = true;
                GemScore.scoreValue++;
                Destroy(other.gameObject);
                LoadSceneBasedOnGemScore();
                //if (GemScore.scoreValue == 1)
                //{
                //    SceneManager.LoadScene("MainlandL");
                //}
                //else if ((GemScore.scoreValue == 2) && (GameManager.instance.gemB = true))
                //{
                //    SceneManager.LoadScene("MainlandLB");
                //}
                //else if ((GemScore.scoreValue == 2) && (GameManager.instance.gemR = true))
                //{
                //    SceneManager.LoadScene("MainlandLR");
                //}
                //else if (GemScore.scoreValue == 3)
                //{
                //    SceneManager.LoadScene("MainlandFinal");
                //}
                break;

            case "GemR":
                GameManager.instance.gemR = true;
                GemScore.scoreValue+=1;
                Destroy(other.gameObject);
                LoadSceneBasedOnGemScore();
                //if (GemScore.scoreValue == 1)
                //{
                //    SceneManager.LoadScene("MainlandR");
                //}
                //else if ((GemScore.scoreValue == 2) && (GameManager.instance.gemB = true))
                //{
                //    SceneManager.LoadScene("MainlandRB");
                //}
                //else if ((GemScore.scoreValue == 2) && (GameManager.instance.gemL = true))
                //{
                //    SceneManager.LoadScene("MainlandLR");
                //}
                //else if (GemScore.scoreValue == 3)
                //{
                //    SceneManager.LoadScene("MainlandFinal");
                //}
                break;

        }

    }

    void LoadSceneBasedOnGemScore()
    {
        if (GemScore.scoreValue == 1)
        {
            if (GameManager.instance.gemB)
            {
                SceneManager.LoadScene("MainlandB");
            }
            else if (GameManager.instance.gemL)
            {
                SceneManager.LoadScene("MainlandL");
            }
            else if (GameManager.instance.gemR)
            {
                SceneManager.LoadScene("MainlandR");
            }
        }
        else if (GemScore.scoreValue == 2)
        {
            if (GameManager.instance.gemL && GameManager.instance.gemB)
            {
                SceneManager.LoadScene("MainlandLB");
            }
            else if (GameManager.instance.gemR && GameManager.instance.gemB)
            {
                SceneManager.LoadScene("MainlandRB");
            }
            else if (GameManager.instance.gemR && GameManager.instance.gemL)
            {
                SceneManager.LoadScene("MainlandLR");
            }
        }
        else if (GemScore.scoreValue == 3)
        {
            SceneManager.LoadScene("MainlandFinal");
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
        
        if (bulletHitR)
        {
            GameObject playerEffect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(playerEffect, deathEffectTimer);
            Destroy(this.gameObject);
            Debug.Log("Dead");
            SceneManager.LoadScene("GameOverR");
        }
        else if (bulletHitB)
        {
            GameObject playerEffect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(playerEffect, deathEffectTimer);
            Destroy(this.gameObject);
            Debug.Log("Dead");
            SceneManager.LoadScene("GameOverB");
        }
        else if (bossHit)
        {
            GameObject playerEffect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(playerEffect, deathEffectTimer);
            Destroy(this.gameObject);
            Debug.Log("Dead");
            SceneManager.LoadScene("GameOverBoss");
        }
        else
        {
            GameObject playerEffect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(playerEffect, deathEffectTimer);
            Destroy(this.gameObject);
        }
    }
}

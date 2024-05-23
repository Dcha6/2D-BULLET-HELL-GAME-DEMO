using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownBUllet1 : MonoBehaviour
{
    public GameObject hiteffect;
    public float timer;

    public float effectTimer = 5f;
    public int damage = 10;

    public Rigidbody2D rb;
    public float speed = 20f;

    public bool bulletHit = false;
    void Start()
    {
        rb.velocity = transform.up * speed;

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

        TopDownPlayer player = other.GetComponent<TopDownPlayer>();
        if (player != null)
        {
            player.TakeDamage(damage);
           
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

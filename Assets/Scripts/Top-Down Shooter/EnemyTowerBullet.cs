using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTowerBullet : MonoBehaviour
{
    public GameObject hitEffect;
    public float timer;

    public float effectTimer = 5f;
    public int damage = 10;

    public Rigidbody2D rb;
    public float speed = 20f;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.up * speed;
    }

    // Update is called once per frame
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
        if(player != null)
        {
            player.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}

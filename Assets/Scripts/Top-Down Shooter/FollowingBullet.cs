using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingBullet : MonoBehaviour
{
    public float speed;
    public int damage;

    public float timer;
    public float effectTimer = 5f;

    public GameObject hiteffect;

    private Transform player;
    private Vector2 target;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        timer += 1.0F * Time.deltaTime;
        if (timer >= 1)
        {
            GameObject.Destroy(this.gameObject);
        }
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if(transform.position.x == target.x && transform.position.y == target.y)
        {
            DestroyProjectile();
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
        if (other.tag == "collider")
        {
            GameObject effect = Instantiate(hiteffect, transform.position, Quaternion.identity);
            Destroy(effect, effectTimer);
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.collider.tag == "collider")
        {
            GameObject effect = Instantiate(hiteffect, transform.position, Quaternion.identity);
            Destroy(effect, effectTimer);
            Destroy(gameObject);
        }
       
    }

    void DestroyProjectile()
    {
        
        GameObject effect = Instantiate(hiteffect, transform.position, Quaternion.identity);
        Destroy(effect, effectTimer);
        Destroy(gameObject);
    }
}

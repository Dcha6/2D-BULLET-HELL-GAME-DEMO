using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemytest : MonoBehaviour
{

    public int health = 100;
    public GameObject enemyBullet;
    public Transform bulletSpawnpoint;
    public float fireRate;
    public float nextFire;
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(enemyBullet, bulletSpawnpoint.position, bulletSpawnpoint.rotation);
        }
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TowerShooter : MonoBehaviour
{
    

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

    
}

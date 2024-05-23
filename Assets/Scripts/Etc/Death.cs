using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    public Transform respawnPoint;
    void OnTriggerEnter2D(Collision2D other)
    {
        if(other.collider.CompareTag("Player"))
        {
            other.collider.GetComponent<Rigidbody2D>().position = respawnPoint.position;
        }
    }
}


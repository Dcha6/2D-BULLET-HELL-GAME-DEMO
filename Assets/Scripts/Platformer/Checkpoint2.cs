using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint2 : MonoBehaviour
{
    public static Respawner RespawnPlayer;

    void Start()
    {
        if (RespawnPlayer == null)
        {
            RespawnPlayer = FindObjectOfType<Respawner>();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //if the object that entered has a Player component
        if (other.GetComponent<Player>() != null)
        {
            // update the current checkpoint
            RespawnPlayer.currentCheckpoint = this;
            Debug.Log("Activated Checkpoint " + transform.position);
        }
    }
}

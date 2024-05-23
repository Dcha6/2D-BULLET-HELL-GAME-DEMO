using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour
{
    public Checkpoint2 currentCheckpoint;
    public PlayerDeath player;

    public void Start()
    {
        player = FindObjectOfType<PlayerDeath>();
    }

    public void Respawn()
    {
        player.transform.position = currentCheckpoint.transform.position;
    }
}

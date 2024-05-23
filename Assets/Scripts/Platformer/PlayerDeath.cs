using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    private Respawner respawner;

    private void Start()
    {
        respawner = FindObjectOfType<Respawner>();
    }

    // called when the player dies
    public void OnDeath()
    {
        respawner.Respawn();
    }
}

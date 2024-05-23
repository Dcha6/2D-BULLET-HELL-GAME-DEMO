using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCheckpoint : MonoBehaviour
{
    private GameMaster gm;

    public Animator animator;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player") || other.CompareTag("bullet"))
        {
            gm.lastCheckPoint = transform.position;
            Debug.Log("Activated Checkpoint " + transform.position);
            animator.SetBool("isTouched", true);

        }
        else
        {
            animator.SetBool("isTouched", false);
        }
    }
}

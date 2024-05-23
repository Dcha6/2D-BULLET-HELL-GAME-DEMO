using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gem1 : MonoBehaviour
{
    public TopDownPlayer TPD;

    void Start()
    {
        TPD = GetComponent<TopDownPlayer>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            GemScore.scoreValue += 1;
            Destroy(this.gameObject);
        }
    }

}

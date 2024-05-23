using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalManager : MonoBehaviour
{
    public Goal[] goals;


    void Awake()
    {
        goals = GetComponents<Goal>();
    }

    void OnGUI()
    {
        foreach (var goal in goals)
        {
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(var goal in goals)
        {
            if (goal.IsAchieved())
            {
                Destroy(goal);
            }
        }
    }
}

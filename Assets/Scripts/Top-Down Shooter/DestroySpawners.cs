using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

    
    
public class DestroySpawners : MonoBehaviour
{

    public int goal = 6;
    private int SpawnersDestroyedGoal = 0;
    public string nextScene;

    public void SpawnersDestroyed()
    {
        SpawnersDestroyedGoal++;
        if(SpawnersDestroyedGoal >= goal)
        {
            SceneManager.LoadScene(nextScene);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterSpawnerControl : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] monsters;
    public int randomSpawnPoint;
    public int randomMonster;
    public static bool spawnAllowed;
    public float spawnRate = 1f;

    public string nextScene;


    // Start is called before the first frame update
    void Start()
    {
        spawnAllowed = true;
        InvokeRepeating("SpawnAMonster", 0f, spawnRate);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnAMonster()
    {
        if (spawnAllowed)
        {
            // Filter out null spawn points
            var validSpawnPoints = spawnPoints.Where(sp => sp != null).ToArray();

            // Check if there are no valid spawn points left
            if (validSpawnPoints.Length == 0)
            {
                SceneManager.LoadScene(nextScene);
            }

            randomSpawnPoint = Random.Range(0, validSpawnPoints.Length);
            randomMonster = Random.Range(0, monsters.Length);
            if (spawnPoints[randomSpawnPoint] != null)
            {
                Instantiate(monsters[randomMonster], validSpawnPoints[randomSpawnPoint].position, Quaternion.identity);
            }
            
        }
    }
    //void OnDestroy()
    //{
    //    FindObjectOfType<DestroySpawners>().SpawnersDestroyed();
    //}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject player;
    public string sceneToLoad;
    public string lastScene;
    public Vector3 playerPosition;

    public string nextSpawnPoint;

    public bool gemL = false;
    public bool gemR = false;
    public bool gemB = false;


    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        if (!GameObject.Find("Player"))
        {
            GameObject Player = Instantiate(player, playerPosition, Quaternion.identity) as GameObject;
            Player.name = "Player";
        }
    }

    
    public void LoadNextScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}

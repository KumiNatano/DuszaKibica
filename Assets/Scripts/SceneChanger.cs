using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string LevelName = "scene_0"; //domyslnie portal jest ustawiony na poziom scene_0 (do zmiany w przyszlosci);
    //public GameObject player;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name != "Player")
        {
            return;
        }
        else
            //DontDestroyOnLoad(player);
            //DontDestroyOnLoad(this);
            SceneManager.LoadScene(LevelName);
    }

}

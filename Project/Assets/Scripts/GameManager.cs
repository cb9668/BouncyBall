using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    static GameManager _instance = null;
    public GameObject playerPrefab;
    public Text mainText;

    GameObject _player;


    public float highscore;

    public PlayerData saveData;

    // Use this for initialization
    void Start () {
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }

        saveData = DataManager.instance.LoadPlayerData();

        highscore = saveData.PDhighscore;

        Debug.Log(highscore);
    }


    // Update is called once per frame
    void Update () {
		
	}


    public static GameManager instance
    {
        get { return _instance; }
        set { _instance = value; }
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
   

    public void StartGame()
    {
        SceneManager.LoadScene("main");

    }


    public void LoadStart()
    {
        SceneManager.LoadScene("Start");
    }


    public void Load(string name)
    {
        SceneManager.LoadScene(name);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour {


    private Rigidbody rb;
    public float force;

    private Vector3 _currentMousePosition;

    public static int playerScore;          
    public GameObject scoreTextDynamic;


    public GameObject scoreText;                    //gameobject which shows the score on screen
    public GameObject bestScoreText;                //gameobject which shows the best saved score on screen

    public AudioClip menuTap;						//touch sound
    public AudioClip eat;

    void Awake()
    {
        playerScore = 0;
        bestScoreText.GetComponent<TextMesh>().text = PlayerPrefs.GetInt("bestScore").ToString();
    }
    // Use this for initialization
    void Start () {

        rb = GetComponent<Rigidbody>();
        force = 5;
        
	}
	
	// Update is called once per frame
	void Update () {

        calculateScore();

        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                
                if (hit.collider.tag == "Player")
                {
                    _currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    var mousePos_xy = new Vector3(_currentMousePosition.x,0, _currentMousePosition.z);
                    var center_xy = new Vector3(gameObject.transform.position.x, 0,gameObject.transform.position.z);

                    var vector1 = center_xy - mousePos_xy; // VectorToMoveTo

                    rb.AddForce(vector1.normalized* force, ForceMode.Impulse);
                }

                else if (hit.collider.name == "retryButton")
                    {
                        playSfx(menuTap);
                    
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    }	

            }
        }
    }

    void calculateScore()
    {
            playerScore += (int)(GameController.currentLevel * Mathf.Log(GameController.currentLevel + 1, 2));
            scoreTextDynamic.GetComponent<TextMesh>().text = playerScore.ToString();
    }



    void OnCollisionEnter(Collision c)
    {

        if (c.gameObject.tag == "Maze" || c.gameObject.tag == "KillZone")
        {
            GameController.gameOver = true;
            
        }
        else if (c.gameObject.tag =="Collectible")
        {
            Destroy(c.gameObject);
            playSfx(eat);
            playerScore += 500;
        }
    }

    void playSfx(AudioClip _sfx)
    {
        GetComponent<AudioSource>().clip = _sfx;
        if (!GetComponent<AudioSource>().isPlaying)
            GetComponent<AudioSource>().Play();
    }
}

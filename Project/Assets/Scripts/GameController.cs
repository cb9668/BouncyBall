using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {



	public static float moveSpeed; 			//Global speed of moving items (mazes)
	public static float cloneInterval; 		//clone maze and enenmyball every N seconds
	

	public static int currentLevel = 1;		//Start from easy settings (1 = very easy ---> 10 = very hard)	
	private float levelJump = 10.0f; 		//increase the level every N seconds

	private Vector3 startPoint;				//starting point of the clones object
	private float levelPassedTime;			//passed time since we started the game
	private float levelStartTime;			//time of starting the game
	

	public static bool gameOver;			//Gameover plane object
	private bool gameOverFlag;				//Run the gameover sequence just once
	

	public AudioClip levelAdvanceSfx;
	public AudioClip gameoverSfx;
	
	private bool createMaze;				

	public GameObject[] maze;				
	
	public GameObject gameOverPlane;		
	public GameObject mainBackground;		
	public GameObject player;


    public GameObject scoreText;                    //gameobject which shows the score on screen
    public GameObject bestScoreText;                //gameobject which shows the best saved score on screen

    void Awake() {

		gameOverPlane.SetActive(false);				
		mainBackground.GetComponent<Renderer>().material.color = new Color(1, 1, 1);	

		createMaze = true;			

		currentLevel = 1;				
		levelPassedTime = 0;
		levelStartTime = 0;
		moveSpeed = 1.2f;
		cloneInterval = 5.0f;
		gameOver = false;
		gameOverFlag = false;
	}
	

	void Update() {


		if(gameOver) {

			if(!gameOverFlag) {
				gameOverFlag = true;
				playSfx(gameoverSfx);

				processGameover();
			}

			return;
		}

		if(createMaze) 
			cloneMaze(); 

		modifyLevelDifficulty();
		
	}
	
	void cloneMaze() {
		createMaze = false;
		startPoint = new Vector3( Random.Range(-1.0f, 1.0f) , 0.5f, 7);
		Instantiate(maze[Random.Range(0, currentLevel+1)], startPoint, Quaternion.Euler( new Vector3(0, 0, 0)));	
		StartCoroutine(reactiveMazeCreation());
	}


	IEnumerator reactiveMazeCreation() {
		yield return new WaitForSeconds(cloneInterval);
		createMaze = true;
	}
	
	

	void modifyLevelDifficulty() {

		levelPassedTime = Time.timeSinceLevelLoad;
		if(levelPassedTime > levelStartTime + levelJump) {


			if(currentLevel < 10) {

				currentLevel += 1;
				playSfx(levelAdvanceSfx);
				moveSpeed += 0.6f;
				cloneInterval -= 0.5f; 
				if(cloneInterval < 0.3f) cloneInterval = 0.3f;
				levelStartTime += levelJump;

				//Background color correction (fade to red)
				float colorCorrection = currentLevel / 10.0f;
				mainBackground.GetComponent<Renderer>().material.color = new Color(1, 
								                                                   1 - colorCorrection, 
								                                                   1 - colorCorrection);
			}
		}
	}
	

	void processGameover() {
        saveScore();
        gameOverPlane.SetActive(true);       
        scoreText.GetComponent<TextMesh>().text = Controller.playerScore.ToString();
        bestScoreText.GetComponent<TextMesh>().text = PlayerPrefs.GetInt("bestScore").ToString();
    }


	void playSfx(AudioClip _sfx) {
		GetComponent<AudioSource>().clip = _sfx;
		if(!GetComponent<AudioSource>().isPlaying)
			GetComponent<AudioSource>().Play();
	}

    void saveScore()
    {

        PlayerPrefs.SetInt("lastScore", Controller.playerScore);

        int lastBestScore;
        lastBestScore = PlayerPrefs.GetInt("bestScore");

        if (Controller.playerScore > lastBestScore)
            PlayerPrefs.SetInt("bestScore", Controller.playerScore);
    }

}
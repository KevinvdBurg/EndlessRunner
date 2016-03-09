using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameControler : MonoBehaviour {

	// Use this for initialization
	public GUIText scoreText;
	public GUIText livesText;
	public Canvas GameOverScreen;
	public Canvas StartScreen;
	public PlayerController player;
	private int score;	
	private int lives;
	private Text text;
	private bool died = false;


	//private HighScoreScript highScoreScript;
	public GameObject startPlatform;
	public Vector3 platformNext;
	public Vector3 platformSize;
	public List<GameObject> prefabs = new List<GameObject>();

	// Use this for initialization
	void Start () {

		Screen.orientation = ScreenOrientation.LandscapeLeft;
		score = 0;
		lives = 5;

		UpdateScore ();
		UpdateLives ();
		GameOverScreen.enabled = false;
		platformSize = startPlatform.transform.FindChild("Floor").GetComponent<Renderer>().bounds.size;
		platformNext = startPlatform.transform.position;

		for (int i = 0; i < 5; i++) {
			GenerateNextTrack();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("g")) {
			GenerateNextTrack();
		}
	}

	public void AddScore(int ScoreValue){
		score += ScoreValue;
		UpdateScore();
	}

	public void ChangeLives(int live){
		lives = lives + live;

		UpdateLives ();
	}

	void UpdateScore(){
		scoreText.text = "Score " + score;

		if (score <= 0) {

		}
		else if (score % 50 == 0) {
			player.ChangeSpeed (1.005f);
		}
	}

	void UpdateLives(){

		if (lives <= 0) {
			GameOver();
		} else {
			livesText.text = "Lives " + lives;
		}
	}

	public void GameOver(){
		GameOverScreen.enabled = true;
		scoreText.enabled = false;
		Text[] textValue = GameOverScreen.GetComponentsInChildren<Text>();
		textValue [1].text = "Finalscore " + score;
		player.speed = 0;
		died = true;
	}

	public void StartGame(){
		StartScreen.enabled = false;
	}


	public int GetScore(){
		return score;	
	}

	public int GetLives(){
		return lives;
	}

	public bool GetDeath(){
		return died;
	}

	public float GetSpeed(){
		return player.GetSpeed ();
	}

	public void GenerateNextTrack(){
		platformNext = new Vector3 (platformNext.x, platformNext.y, (platformNext.z + platformSize.z));
		int randomint = Random.Range (0,prefabs.Count);
		Instantiate (prefabs[randomint], platformNext, Quaternion.identity);
	}


}

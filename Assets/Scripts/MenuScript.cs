using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Text.RegularExpressions;

public class MenuScript : MonoBehaviour {
	private bool writeHighscore;
	public Canvas GameOverScreen;
	// Use this for initialization
	void Start () {
		writeHighscore = true;

		Screen.orientation = ScreenOrientation.LandscapeLeft;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void onClick(string action){
		switch (action) {
		case "start":
			StartButton();
			break;
		case "highscore":
			HighScoreButton();
			break;
		case "exit":
			ExitButton();
			break;
		case "back":
			BackButton();
			break;
		case "writehighscore":
			WriteHighScoreButton();
			break;
		default:
			break;
		}

	}

	public void StartButton(){
		Application.LoadLevel("Runner");
	}

	public void HighScoreButton(){
		Application.LoadLevel("Highscore");
	}

	public void ExitButton(){
		Application.Quit();
	}

	public void BackButton(){
		Application.LoadLevel("Menu");

	}

	public void WriteHighScoreButton(){
		GameObject inputFieldGo = GameObject.Find("NameHighScore");
		InputField inputFieldCo = inputFieldGo.GetComponent<InputField>();
		string name = Regex.Replace (inputFieldCo.text, @"\s+", "");
		if (writeHighscore) {
			HighScoreScript hScore = gameObject.AddComponent<HighScoreScript> ();
			Text[] textValue = GameOverScreen.GetComponentsInChildren<Text>();
			int highScore = Int32.Parse(textValue [1].text.Split(' ')[1]);

			hScore.WriteHighScore (name, highScore);
			writeHighscore = false;
			Debug.Log("HighScore Written");
		}

	}
}

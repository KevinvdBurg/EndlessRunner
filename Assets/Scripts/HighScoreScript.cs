using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine.UI;

public class HighScoreScript : MonoBehaviour {


	private string highscore = "Assets/Resource/highscore.txt";
//	private TextAsset mytxtData= (TextAsset)Resources.Load("highscore.txt");
//	private string highscore = mytxtData.text;

//	TextAsset mytxtData=(TextAsset)Resources.Load("highscore.txt");
//	string txt=mytxtData.text;
//	private TextAsset highscoreLocation;
//	private string highscore;

	//private Path highscore = Path.Combine(Application.streamingAssetsPath, "highscore.txt");
	public Text scoreboard;

	// Use this for initialization
	void Start () {

//		highscoreLocation = (TextAsset)Resources.Load("highscore.txt", typeof(TextAsset));
//		highscore = highscoreLocation.text;

		Debug.Log (highscore);
		if (File.Exists (highscore)) {
			Debug.Log (highscore + " already exists.");

			if (scoreboard != null) {
				
				List<Score> NewHighscores = new List<Score> ();
				NewHighscores = GetHighscore ();
				
				string board = "";
				
				foreach (Score s in NewHighscores) {
					board = board + s.name + " " + s.score + "\n";
				}
				scoreboard.text = board;
			} else {
			}

		} else {
			StreamWriter sr = File.CreateText(highscore);
			sr.Close();
		}
	}
	
	// Update is called once per frame
	void Update () {

	}

	public bool WriteHighScore(string name, int score){
		bool result = false;
		using (StreamWriter sr = new StreamWriter(highscore, true))
		{
			sr.WriteLine ("Name {0} Score {1}", name, score);
			result = true;
		}

		return result;

	}

	public List<Score> GetHighscore(){
		List<Score> highscores = new List<Score>();

		using (StreamReader sr = new StreamReader(highscore, true))
		{
			string score;
			while ((score = sr.ReadLine()) != null)
			{
				string[] scoreArray = score.Split(' ');
				highscores.Add(new Score(scoreArray[1], int.Parse(scoreArray[3])));
			}


		}

		List<Score> SortHighScore = highscores.OrderByDescending (s => s.score).ToList ();
		return SortHighScore;
	}
}

public class Score {

	public string name { get;set; }

	public int score { get;set; }

	public Score(string Name, int Score){
		this.name = Name;
		this.score = Score;
	}
}










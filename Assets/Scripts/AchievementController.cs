using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AchievementController : MonoBehaviour {

	private GameControler gameControler;
	public Canvas AchievementCanvas;
	private bool newAchievement = false;
	public Text AchievementText;
	public AudioSource  AchievementSound;
	public string lastswitch;

	private bool s1 = true;
	private bool s2 = true;
	private bool s3 = true;
	private bool s4 = true;
	private bool s5 = true;
	private bool s6 = true;
	private bool s7 = true;
	private bool s8 = true;


	private bool l1 = true;
	private bool l2 = true;
	private bool l3 = true;
	private bool l4 = true;

	private bool d1 = true;
	// Use this for initialization
	void Start () {

		AchievementCanvas.enabled = false;
		GameObject gameControlerObject = GameObject.FindGameObjectWithTag ("gameControler");
		if (gameControlerObject != null) {
			gameControler = gameControlerObject.GetComponent <GameControler>();
		}
		if (gameControler == null) {
			Debug.Log ("Cannot find GameControler");
		}
	}
	
	// Update is called once per frame
	void Update () {

		Achievement a = null;
		switch (gameControler.GetScore ()) {
			case 50:
			if (s1) {
				a = new Achievement("Score 50", 10);
				StartCoroutine(ShowAchievement(a));
				s1 = false;
			}
			break;
		case 1000:
			if (s2) {
				a = new Achievement("Score 1000", 20);
				StartCoroutine(ShowAchievement(a));
				s2 = false;
			}
			break;
		case 2000:
			if (s3) {
				a = new Achievement("Score 2000", 50);
				StartCoroutine(ShowAchievement(a));
				s3 = false;
			}
			break;
		case 3000:
			if (s4) {
				a = new Achievement("Score 3000", 100);
				StartCoroutine(ShowAchievement(a));
				s4 = false;
			}
			break;
		case 6000:
			if (s5) {
				a = new Achievement("Score 6000", 150);
				StartCoroutine(ShowAchievement(a));
				s5 = false;
			}
			break;
		case 12000:
			if (s6) {
				a = new Achievement("Score 12000", 400);
				StartCoroutine(ShowAchievement(a));
				s6 = false;
			}
			break;
		case 20000:
			if (s7) {
				a = new Achievement("Score 20000", 500);
				StartCoroutine(ShowAchievement(a));
				s7 = false;
			}
			break;
		case 50000:
			if (s8) {
				a = new Achievement("Score 50000", 9001);
				StartCoroutine(ShowAchievement(a));
				s8 = false;
			}
			break;
		default:
			break;
		}

		switch (gameControler.GetLives ()) {
		case 6:
			if(l1){
				a = new Achievement("One More Life!", 10);
				StartCoroutine(ShowAchievement(a));
				l1 = false;
			}
			break;
		case 10:
			if(l2){
				a = new Achievement("10 Lives", 100);
				StartCoroutine(ShowAchievement(a));
				l2 = false;
			}
			break;
		case 20:
			if(l3){
				a = new Achievement("20 Lives?", 200);
				StartCoroutine(ShowAchievement(a));
				l3 = false;
			}
			break;
		case 50:
			if(l4){
				a = new Achievement("50 Lives!", 1000);
				StartCoroutine(ShowAchievement(a));
				l3 = false;
			}
			break;
		}

		switch (gameControler.GetDeath()) {
		case true:
			if(d1){
				a = new Achievement("You Died!", -9001);
				StartCoroutine(ShowAchievement(a));
				d1= false;
			}
			break;
		}

	}

	IEnumerator ShowAchievement(Achievement a){
		if (!AchievementSound.isPlaying) {
			AchievementSound.Play();
		}

		AchievementText.text = a.name + " / " + a.score;
		AchievementCanvas.enabled = true;
		yield return (new WaitForSeconds(1f));
		newAchievement = false;
		AchievementCanvas.enabled = false;
		AchievementText.text = "";
	}
}

public class Achievement {
	
	public string name { get;set; }
	
	public int score { get;set; }

	public Achievement(string Name, int Score){
		this.name = Name;
		this.score = Score;
	}
}

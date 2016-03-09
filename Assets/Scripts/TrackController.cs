using UnityEngine;
using System.Collections;

public class TrackController : MonoBehaviour {

	private GameControler gameControler;
	private PlayerController player;
	// Use this for initialization
	void Start () {
		GameObject gameControlerObject = GameObject.FindGameObjectWithTag ("gameControler");
		if (gameControlerObject != null) {
			gameControler = gameControlerObject.GetComponent <GameControler>();
		}
		if (gameControler == null) {
			Debug.Log ("Cannot find GameControler");
		}

		GameObject playerControlerObject = GameObject.FindGameObjectWithTag ("Player");
		if (playerControlerObject != null) {
			player = playerControlerObject.GetComponent <PlayerController>();
		}
		if (player == null) {
			Debug.Log ("Cannot find Player");
		}
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter(Collider other) {
		gameControler.GenerateNextTrack ();
	}

	void OnTriggerExit(Collider other) {
		StartCoroutine(DestroyTrack());
	}

	IEnumerator DestroyTrack(){
		float timer = 0;
		while (timer < 1) {
			float destroyTimer = (player.GetSpeed() / 10);
			timer += Time.deltaTime * destroyTimer;
			yield return null;
		}
		if (timer >= 1) {
			Destroy(this.gameObject);
		}
		
	}


}

using UnityEngine;
using System.Collections;

public class CoinController : MonoBehaviour {



	private GameControler gameControler;
	public int scoreValue = 10;
	public AudioSource coinSound;	
	public Renderer rend;
	// Use this for initialization
	void Start () {
		GameObject gameControlerObject = GameObject.FindGameObjectWithTag ("gameControler");
		coinSound = GetComponent<AudioSource>();
		rend = GetComponent<MeshRenderer>();


		if (gameControlerObject != null) {
			gameControler = gameControlerObject.GetComponent <GameControler>();
		}
		if (gameControler == null) {
			Debug.Log ("Cannot find GameControler");
		}
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(new Vector3(90,0,0) * Time.deltaTime);
	}


	void OnTriggerEnter(Collider other) {
		StartCoroutine(DestroyCoin(this.gameObject));
		gameControler.AddScore (scoreValue);

	}

	IEnumerator DestroyCoin(GameObject coin){
		coinSound.Play();
		yield return (new WaitForSeconds(0.1f));
		rend.enabled = false;
		yield return (new WaitForSeconds(1f));
	}
}

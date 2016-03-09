using UnityEngine;
using System.Collections;

public class HeartScript : MonoBehaviour {
	private GameControler gameControler;
	public AudioSource heartSound;	
	public float t=1f; // speed
	private float l= 3.5f; // length from 0 to endpoint.
	private float posX; // Offset
	private int count = 0;

	// Use this for initialization
	void Start () {
		posX = this.gameObject.transform.position.x;
		GameObject gameControlerObject = GameObject.FindGameObjectWithTag ("gameControler");
		heartSound = GetComponent<AudioSource>();

		if (gameControlerObject != null) {
			gameControler = gameControlerObject.GetComponent <GameControler>();
		}
		if (gameControler == null) {
			Debug.Log ("Cannot find GameControler");
		}

	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = new Vector3 ( posX+Mathf.PingPong (t * Time.time, l),this.gameObject.transform.position.y, this.gameObject.transform.position.z);
		transform.position = pos;
	}

	void OnTriggerEnter(Collider other) {
		if (count == 0) {
			StartCoroutine(DestroyHeart(this.gameObject));
			gameControler.ChangeLives (1);
			count++;
		}
	}
	
	IEnumerator DestroyHeart(GameObject coin){
		heartSound.Play();
		yield return (new WaitForSeconds(0.2f));
		this.gameObject.SetActive (false);
	}

}

using UnityEngine;
using System.Collections;

public class CanScript : MonoBehaviour {
	
	[SerializeField] private AudioClip[] m_CanSounds;  
	private AudioSource m_AudioSource;
	private PlayerController playerControler;
	// Use this for initialization
	void Start () {
		GameObject playerControlerObject = GameObject.FindGameObjectWithTag ("Player");
		m_AudioSource = GetComponent<AudioSource>();
		if (playerControlerObject != null) {
			playerControler = playerControlerObject.GetComponent <PlayerController>();
		}
		if (playerControler == null) {
			Debug.Log ("Cannot find PlayerControler");
		}
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(new Vector3(0,90,0) * Time.deltaTime);
	}
	
	void OnTriggerEnter(Collider other) {
		Debug.Log ("Speed");

		if (playerControler.GetSpeed () <= 2f) {
			SpeedUp();
		} else {
			if (Random.Range (1, 4) <= 2) {
				SpeedUp();
			} else {
				SpeedDown();
			}
		}

		m_AudioSource.Play ();
	}

	public void SpeedUp(){
		m_AudioSource.clip = m_CanSounds [0];
		playerControler.ChangeSpeed (1.010f);
	}

	public void SpeedDown(){
		m_AudioSource.clip = m_CanSounds [1];
		playerControler.ChangeSpeed (-1);
	}
}


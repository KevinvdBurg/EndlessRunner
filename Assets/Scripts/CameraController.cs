using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject player;
	
	private Vector3 offset;


	// Use this for initialization
	void Start () {
		offset = transform.position - player.transform.position;
	}

	void LateUpdate ()
	{
		//transform.position = player.transform.position + offset;
		if (Input.GetKeyDown ("space")) {
			float speed = 2.5f;
			float smooth = 1.0f - Mathf.Pow (0.5f, Time.deltaTime * speed);
			transform.position = Vector3.Lerp (transform.position, player.transform.position, smooth);
		} 
		else {
			transform.position = player.transform.position + offset;
		}

	}
}

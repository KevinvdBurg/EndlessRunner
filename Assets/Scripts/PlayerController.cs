using UnityEngine;
using System.Collections;
using System.Timers;


public class PlayerController : MonoBehaviour {

	public float speed;
	private float maxSpeed = 0f;
	public Vector2 startPos;
	public Vector2 direction;
	public string swipe;
	public int swipeMargin = 50;
	public bool directionChosen;
	public Touch touch;
	public Collision col;
	public bool canJump = true;
	public AudioClip[] audioClip;
	private AudioSource audio;
	private GameControler gameControler;
	private bool hitObject = false;
	public Animator anim;

	int jumpHash = Animator.StringToHash("Jump");
	private bool iswalking = true;
	private bool gameover = false;
	private bool tilt = true;

	//int duckHash = Animator.StringToHash("Duck");




	// Use this for initialization
	void Start () {

		anim.SetInteger (jumpHash, 0);
		//anim.SetInteger (duckHash, -1);
		audio = GetComponent<AudioSource>();
		GameObject gameControlerObject = GameObject.FindGameObjectWithTag ("gameControler");
		if (gameControlerObject != null) {
			gameControler = gameControlerObject.GetComponent <GameControler>();
		}
		if (gameControler == null) {
			Debug.Log ("Cannot find GameControler");
		}

		PlaySound (0);


	}
	
	// Update is called once per frame
	void Update () {
		gameover = gameControler.GetDeath();
		if (!gameover) {
			MoveForward ();
			if (OutOffBound ()) {
				gameControler.GameOver ();

			}
		

			if (!hitObject) {
				//Debug.Log("Niet Hit");
				ChecForWalls ();
			} else {
				StartCoroutine (Hit ());
				//Debug.Log("Hit!");
			}

			int fingerCount = 0;
			foreach (Touch touch in Input.touches) {
				if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
					fingerCount++;
			}

			if (tilt){
				MoveTilt();
			}
			if (fingerCount > 0) {		
				if(!tilt){
					Touch touch = Input.touches [0];
					if (touch.position.x < Screen.width / 2) {
						MoveLeft ();
					} else if (touch.position.x > Screen.width / 2) {
						MoveRight ();
					}
				}

			} else {
				if (Input.GetKeyDown ("w")) {
					Jump ();
				}
			
				if (Input.GetKeyDown ("s")) {
					DuckDown ();
				}

				if (Input.GetKeyDown ("q")) {
					RotateLeft ();
				}
			
				if (Input.GetKeyDown ("e")) {
					RotateRight ();
				}
			
				if (Input.GetKey ("a")) {
					MoveLeft ();
				}
			
				if (Input.GetKey ("d")) {
					MoveRight ();
				}
			
				if (Input.GetKeyDown ("space")) {
//				Debug.Log("Space is pressed");
					Jump ();
				}
			
				if (Input.GetKeyDown ("s")) {
					DuckDown ();
				}
			
				if (Input.GetKeyDown ("q")) {
					RotateLeft ();
				}
			
				if (Input.GetKeyDown ("e")) {
					RotateRight ();
				}
			}
		} else {
			audio.Stop();
			anim.Stop();
		}

	}

	private Touch iniTouch = new Touch();
	private float distance = 0;
	private bool hadSwiped = false;
	void FixedUpdate(){
		foreach (Touch t in Input.touches) {
			if(t.phase == TouchPhase.Began){
				iniTouch = t;
			}
			else if(t.phase == TouchPhase.Moved && !hadSwiped){
				float deltaX = iniTouch.position.x - t.position.x;
				float deltaY = iniTouch.position.y - t.position.y;
				distance = Mathf.Sqrt((deltaX * deltaX) + (deltaY * deltaY));
				bool swipedSideways = Mathf.Abs(deltaX) > Mathf.Abs(deltaY);
				if(distance > 100f){
					//SwipedLeft
					if(swipedSideways && deltaX > 0){
						//RotateLeft();
					}
					//SwipedRight
					else if(swipedSideways && deltaX <= 0){
						//RotateRight();
					}
					//SwipedDown
					else if(!swipedSideways && deltaY > 0){
						//DduckDown ();
					}
					//SwipedUp
					else if(!swipedSideways && deltaY <= 0){
						Jump ();
					}
					hadSwiped = true;
				}

			}
			else if(t.phase == TouchPhase.Ended){
				iniTouch = new Touch();
				hadSwiped = false;
			}
		}
	}
//	void OnGUI() {
//		Rect button = new Rect (5, 5, 50, 50);
//
//		if (GUI.Button (button, "Jump")) {
//			Jump();
//		}
//	}


	void OnCollisionEnter(Collision other) {
		//Debug.Log (other.gameObject.tag);
		if (other.gameObject.tag == "Floor") {
			canJump = true;
			anim.SetInteger (jumpHash, 2);

			if (!iswalking) {
				PlaySound (0);
				iswalking = true;
			}
		} else {
			//Debug.Log(other.gameObject.tag);
		}
	}

	public void ChecForWalls(){
		Ray ray = new Ray (transform.position, Vector3.forward);

		Debug.DrawRay(transform.position, Vector3.forward, Color.blue);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, 1f)){
			if (hit.collider.tag == "Wall"){
				//Debug.Log("Wall Hit!");
				ChangeSpeed(-1);
				gameControler.ChangeLives(-1);
				//Debug.Log("Hit!");

				hitObject = true;

				//StartCoroutine(Hit());

				if(speed <= 0){
					gameControler.GameOver();

				}
				else{
					Jump();
				}
			}

			if (hit.collider.tag == "Hole") {
				//ChangeSpeed(-1);
				//Debug.Log("Hole");
			}
		}
	}


	public void MoveForward(){
		transform.Translate(new Vector3(0,0,1) * Time.deltaTime * (speed * 2));
	}

	public void MoveLeft (){
		//Debug.Log ("Left");
		transform.Translate (new Vector3 (-1, 0, 1) * Time.deltaTime * speed);
	}

	public void MoveRight (){
		transform.Translate (new Vector3 (1, 0, 1) * Time.deltaTime * speed);
	}

	public void MoveTilt (){
		transform.Translate (new Vector3 (Input.acceleration.x, 0, 1) * Time.deltaTime * speed);
	}


	public void Jump (){
		if (canJump) {
			canJump = false;
			anim.SetInteger (jumpHash, 1);
			StartCoroutine(SmoothJump());
		} 
		else {
			Debug.Log("kan niet springen");
		}

	}

	IEnumerator SmoothJump(){
		Vector3 targetPositionNew = new Vector3 (transform.position.x, (transform.position.y + 5f), (transform.position.z + 4f));
		Vector3 targetPositionNewBottom = new Vector3 (transform.position.x, transform.position.y, (transform.position.z + 4f));
		iswalking = false;
		float timer = 0;
		float timer2 = 0;
		bool jumpArc = false;

		PlaySound (1);

		if (jumpArc == false) {
			while (timer < 1) {

				timer += Time.deltaTime * 3f;
				transform.position = Vector3.Lerp (transform.position, targetPositionNew, timer);
				yield return jumpArc = true;
			}
		} 
		else 
		{
			while (timer2 < 1) {
				timer += Time.deltaTime * 3f;
				transform.position = Vector3.Lerp (targetPositionNew, targetPositionNewBottom, timer);
				yield return null;
			}
		}
	}

	public void DuckDown (){
//		System.Timers.Timer aTimer = new System.Timers.Timer();
//		aTimer.Elapsed+=new ElapsedEventHandler(OnTimedEvent);
//		aTimer.Interval=5000;
//		aTimer.Enabled=true;

		//transform.localScale -= new Vector3(0, 0.5f, 0);
	}

	private void OnTimedEvent(object source, ElapsedEventArgs e)
	{
		transform.localScale += new Vector3(0, 0.5f, 0);
	}

	public void RotateLeft(){
		transform.Rotate(0, -45, 0);
	}

	public void RotateRight(){
		transform.Rotate(0, 45, 0);
	}

	public void PlaySound(int clip){
		audio.clip = audioClip [clip];

		if (clip == 1) {
			audio.PlayOneShot(audioClip[clip], 0.7F);

		} else {
			audio.loop = true;
			audio.Play ();
		}


	}

	public void ChangeSpeed(float changeInt){
		if (changeInt > 0) {
			speed = Mathf.Pow(speed, changeInt);
		} 
		else {
			if(maxSpeed < speed){
				maxSpeed = speed;
			}
			speed = speed + changeInt;
			StartCoroutine(UpToSpeed(maxSpeed));
		}
	}

	IEnumerator UpToSpeed(float maxSpeed){
		while (speed < maxSpeed) {
			speed += Time.deltaTime * 0.3f;
			yield return new WaitForSeconds(.08f);
		}
	}

	IEnumerator Hit(){
		float timer = 0;
		while (timer < 1) {
			timer += Time.deltaTime * 0.5f;
		}
		hitObject = false;
		yield return null;
	}

	public float GetSpeed(){
		return speed;
	}

	public bool OutOffBound(){
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		if (player.transform.position.y <= -0.1) {
			return true;
		}
		return false;
	}
}

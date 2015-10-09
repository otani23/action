using UnityEngine;
using System.Collections;

public class PlayerControll : MonoBehaviour {


	float flont = 400f;
	float back = -200f;
	float sideStep = 200f;
	float side = 100f;
	float jumpUp = 30f;
	Vector3 rotation = new Vector3(0,0.1f,0);

	bool isJump = false;
	bool isEnemyLook = false;
	bool isTurbo = false;

	GameObject model;
	public GameObject camera;
	Animator animation;
	Transform bulletKeeper;

	Vector3 defaultCameraPos = new Vector3(0f,2f,-5f);
	Vector3 oldCameraPos;

	Vector3 enemyPosition;
	Transform enemyLook;

	int missileCount = 0;
	int missileHaveCount = 0;

	// Use this for initialization
	void Start () {
		model = GameObject.Find ("unitychan");
		bulletKeeper = GameObject.Find (itemConst.missilePositioner).transform;
		camera = GameObject.Find("Main Camera");
		animation = model.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {

		RotationContoroll();
		MoveControll();

		sendFlontAnimation ();
		PersonControll ();
		actionControll ();

		if (this.rigidbody.velocity != Vector3.zero) {
			this.rigidbody.AddForce (-this.rigidbody.velocity / 10, ForceMode.Impulse);
		}
	}

	void sendFlontAnimation()
	{
		animation.SetBool ("Jump",isJump);
		if (!isJump) {
			float movingVec = Mathf.Sqrt (Mathf.Pow (this.rigidbody.velocity.x, 2) + Mathf.Pow (this.rigidbody.velocity.z, 2));
			animation.SetFloat ("Speed", movingVec);
		}
	}

	//Charactor Move

	void RotationContoroll()
	{
		if (Input.GetKey (KeyCode.A) && !isEnemyLook) {
			this.rigidbody.AddTorque (-rotation, ForceMode.Impulse);
		}
		if (Input.GetKey (KeyCode.D) && !isEnemyLook) {
			this.rigidbody.AddTorque (rotation, ForceMode.Impulse);
		}
	}

	void MoveControll()
	{
		if (Input.GetKey (KeyCode.Space)) {
			this.rigidbody.AddForce (this.transform.up * jumpUp, ForceMode.Force);
			isJump = true;
//			StartCoroutine(jumpAd());
		} else {
			this.rigidbody.AddForce ( this.transform.up * -jumpUp , ForceMode.Force);
		}

		if (Input.GetKey (KeyCode.A) && isEnemyLook) {
			this.rigidbody.AddForce (-side * this.transform.right, ForceMode.Force);
		}
		if (Input.GetKey (KeyCode.D) && isEnemyLook) {
			this.rigidbody.AddForce (side * this.transform.right, ForceMode.Force);
		}

		if (Input.GetKeyDown (KeyCode.Q)) {
			Debug.Log("Q");
			this.rigidbody.AddForce(-this.rigidbody.velocity/10 , ForceMode.Impulse);
			this.rigidbody.AddForce (-sideStep * this.transform.right, ForceMode.Impulse);
			camera.GetComponent<Animator> ().SetInteger ("isBoost", 1);
			StartCoroutine(turboCameraFollow());
		}
		if (Input.GetKeyDown (KeyCode.E)) {
			Debug.Log("E");
			this.rigidbody.AddForce(-this.rigidbody.velocity/10 , ForceMode.Impulse);
			this.rigidbody.AddForce (sideStep * this.transform.right, ForceMode.Impulse);
			camera.GetComponent<Animator> ().SetInteger ("isBoost", 2);
			StartCoroutine(turboCameraFollow());
		}

		if(this.rigidbody.velocity.x > 100f){
			return;
		}
		if(this.rigidbody.velocity.z > 100f){
			return;
		}

		if (Input.GetKey (KeyCode.W)) {
			this.rigidbody.AddForce (flont * this.transform.forward, ForceMode.Force);
//			model.GetComponent<Animator>().Play("RUN00_F");
		}
		if (Input.GetKeyDown (KeyCode.S)) {
			this.rigidbody.velocity = new Vector3 (0, 0, 0);
			this.rigidbody.AddForce (back * this.transform.forward, ForceMode.Impulse);
		}
	}

	IEnumerator jumpAd()
	{

		yield return new WaitForSeconds  (0.3f);
		this.rigidbody.AddForce (this.transform.up * jumpUp*100f, ForceMode.Force);
		isJump = true;
	}

	void OnCollisionStay(Collision col) {
		
		if (col.gameObject.CompareTag("Terrain")) {
			isJump = false;
		}
	}

	void actionControll()
	{
		if (Input.GetKeyDown (KeyCode.I)) {
			shootBullet();
		}

		if (Input.GetKey(KeyCode.O)) {
			if(missileHaveCount < 10){
				missileCount ++;
				if(missileCount == 10)
				{
					setMissile();
					missileHaveCount ++;
					missileCount = 0;
				}
			}
		} else if (Input.GetKeyUp (KeyCode.O)) {
			shootMissile ();
		}
	}

	void shootBullet()
	{
		GameObject bullet = Instantiate (Resources.Load (itemConst.normalBullet)) as GameObject;
		bullet.transform.parent = bulletKeeper;
	}

	void setMissile()
	{
		GameObject bullet = Instantiate (Resources.Load (itemConst.missle)) as GameObject;
		bullet.name = "pos" + missileHaveCount.ToString();
	}

	void shootMissile()
	{

		bulletKeeper.BroadcastMessage("Shoot");
		missileHaveCount = 0;
	}

	////////////////// CAMERA ////////////////////

	public void TargetLock(Vector3 enemyVec)
	{
		enemyLook = this.transform;
		enemyLook.LookAt (enemyVec);
//		this.transform.LookAt (enemyVec);
		isEnemyLook = true;
	}

	public void TargetOut()
	{
		isEnemyLook = false;
	}

	public void PersonControll()
	{

		if (!isEnemyLook && this.transform.rotation.x != 0f && this.transform.rotation.z != 0f) {
			Quaternion quat = this.transform.rotation;
			quat.x = rotation.x - (rotation.x / 200f);
			quat.z = rotation.z - (rotation.z / 200f);
//			this.transform.rotation = quat;
			this.transform.rotation = Quaternion.Slerp (this.transform.rotation, quat, 0.1f);
		} else if (isEnemyLook) {
			Quaternion quat = enemyLook.transform.rotation;
			quat.x = rotation.x - (rotation.x / 200f);
			quat.z = rotation.z - (rotation.z / 200f);
			//			this.transform.rotation = quat;
			this.transform.rotation = Quaternion.Slerp (this.transform.rotation, quat, 0.1f);
		}
	}

	IEnumerator turboCameraFollow()
	{

		oldCameraPos = camera.transform.position;
		
		yield return new WaitForSeconds (0.1f);

		camera.GetComponent<Animator> ().SetInteger ("isBoost", 0);

	}
}

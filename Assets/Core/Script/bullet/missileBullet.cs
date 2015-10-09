using UnityEngine;
using System.Collections;

public class missileBullet : MonoBehaviour {
	FindEnemy findEnemy;
	PlayerControll charaCont;
	Vector3 bulletSpeed;
	Vector3 bulletDefaultPosition;
	public GameObject TargetEnemy;

	float speed = 1f;
	
	bool input = false;
	bool alreadyLock = false;
	
	// Use this for initialization
	void Start () {
		
		if(findEnemy == null)
			findEnemy = GameObject.Find (itemConst.Finder).GetComponent<FindEnemy>();
		
		if(charaCont == null)
			charaCont = GameObject.Find (itemConst.player).GetComponent<PlayerControll> ();
		
		bulletDefaultPosition = GameObject.Find (itemConst.playerRightHand).transform.position;
		GetEnemyAndShootingInit ();
	}
	
	// Update is called once per frame
	void Update () {

		if (input) {

			if (TargetEnemy != null) {
				transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(TargetEnemy.transform.position - transform.position), 3f);
			}
			
			transform.position += transform.forward * speed;
			if(TargetEnemy != null){
				if( Vector3.Distance( transform.position , TargetEnemy.transform.position )<=0.1f)
				{
					GameObject.Destroy(this.gameObject);
				}
			}
		} else {
			GetEnemyAndShooting ();
		}
	}
	
	public void GetEnemyAndShootingInit()
	{
		this.transform.position = bulletDefaultPosition;
		if (findEnemy.enemy != null && !alreadyLock) {
			TargetEnemy = findEnemy.enemy;
			Vector3 enemyPos = findEnemy.enemy.transform.position;
			bulletSpeed = (enemyPos - bulletDefaultPosition).normalized;	
		} else {
			bulletSpeed = GameObject.Find(itemConst.player).transform.forward * 4f;
		}
		alreadyLock = true;
	}

	
	public void GetEnemyAndShooting()
	{
		this.transform.position = bulletDefaultPosition;
		if (TargetEnemy != null) {
			Vector3 enemyPos = TargetEnemy.transform.position;
			this.transform.LookAt(TargetEnemy.transform.position);
			bulletSpeed = this.transform.forward;
			Debug.Log(	this.TargetEnemy.transform.position);
		}
	}
	
	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.CompareTag("Enemy")) {
			GameObject eff = Instantiate(Resources.Load("DamageEffect")) as GameObject;
			eff.transform.position = this.transform.position;
			Destroy(col.gameObject);
			Destroy(this.gameObject);
			charaCont.TargetOut();
		}
	}

	public void Shoot()
	{
		input = true;
		Destroy (this.gameObject, 8);
	}
}

	/*
	 * 
	FindEnemy findEnemy;
	PlayerControll charaCont;
	Vector3 bulletSpeed;
	Vector3 bulletDefaultPosition;
	bool input = false;
	public GameObject TargetEnemy;
	
	// Use this for initialization
	void Start () {
		Debug.Log ("Init");
		
		if(findEnemy == null)
			findEnemy = GameObject.Find (itemConst.Finder).GetComponent<FindEnemy>();
		
		if(charaCont == null)
			charaCont = GameObject.Find (itemConst.player).GetComponent<PlayerControll> ();
		
		bulletDefaultPosition = GameObject.Find (itemConst.playerRightHand).transform.position;
		GetEnemyAndShootingInit ();
		Destroy (this.gameObject, 10);
	}
	
	// Update is called once per frame
	void Update () {
		GetEnemyAndShooting ();
		this.transform.Translate (this.transform.forward * 10f);
		if (TargetEnemy == null) {
			Destroy (this.gameObject);
		}
	}

	
	public void GetEnemyAndShootingInit()
	{
		this.transform.position = bulletDefaultPosition;
		if (findEnemy.enemy != null) {
			Debug.Log("target::" + TargetEnemy);
			TargetEnemy = findEnemy.enemy;
		} else {
			Destroy(this.gameObject);
		}
	}
	
	public void GetEnemyAndShooting()
	{
		this.transform.position = bulletDefaultPosition;
		if (TargetEnemy != null) {
//			Vector3 enemyPos = TargetEnemy.transform.position;
//			bulletSpeed = (enemyPos - this.transform.position).normalized * 50;	
			this.transform.LookAt(TargetEnemy.transform.position);
			bulletSpeed = this.transform.forward;
			Debug.Log("bulletSpeed :" + bulletSpeed);
		}
	}
	
	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.CompareTag("Enemy")) {
			GameObject eff = Instantiate(Resources.Load("DamageEffect")) as GameObject;
			eff.transform.position = this.transform.position;
			Destroy(col.gameObject);
			Destroy(this.gameObject);
			charaCont.TargetOut();
		}


	}

	public void Shoot()
	{
		input = true;

	}

}
*/
using UnityEngine;
using System.Collections;

public class normalBullet : MonoBehaviour {

	FindEnemy findEnemy;
	PlayerControll charaCont;
	Vector3 bulletSpeed;
	Vector3 bulletDefaultPosition;

	// Use this for initialization
	void Start () {

		if(findEnemy == null)
			findEnemy = GameObject.Find (itemConst.Finder).GetComponent<FindEnemy>();

		if(charaCont == null)
			charaCont = GameObject.Find (itemConst.player).GetComponent<PlayerControll> ();

		bulletDefaultPosition = GameObject.Find (itemConst.playerRightHand).transform.position;
		GetEnemyAndShooting ();
		Destroy (this.gameObject, 1);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (bulletSpeed);
	}

	public void GetEnemyAndShooting()
	{
		this.transform.position = bulletDefaultPosition;
		if (findEnemy.enemy != null) {
			Vector3 enemyPos = findEnemy.enemy.transform.position;
			bulletSpeed = (enemyPos - bulletDefaultPosition).normalized;	
		} else {
			bulletSpeed = GameObject.Find(itemConst.player).transform.forward * 4f;
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
}

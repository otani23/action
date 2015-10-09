using UnityEngine;
using System.Collections;

public class FindEnemy : MonoBehaviour {

	public PlayerControll charaCont ;
	public GameObject enemy = null;

	// Use this for initialization
	void Start () {
		if(charaCont == null)
			charaCont = this.transform.parent.GetComponent<PlayerControll> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (enemy != null) {
			charaCont.TargetLock(enemy.transform.position);
		}
	}



	void OnTriggerStay(Collider col)
	{
		if (enemy == null && col.gameObject.CompareTag ("Enemy")) {
			enemy = col.gameObject;
		}
	}
	void  OnTriggerExit(Collider col)
	{
		if(col.gameObject.CompareTag("Enemy")){
			enemy = null;
			charaCont.TargetOut();
		}
	}
}

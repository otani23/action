using UnityEngine;
using System.Collections;

public class wallTrap : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.CompareTag ("Player")) {
			col.gameObject.transform.parent.transform.position = new Vector3(1000f,0,1000f);
		}
	}
}

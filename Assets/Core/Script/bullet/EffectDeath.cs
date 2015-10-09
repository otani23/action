using UnityEngine;
using System.Collections;

public class EffectDeath : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine (death ());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator death()
	{
		yield return new WaitForSeconds (1f);
		Destroy (this.gameObject);
	}
}

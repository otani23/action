using UnityEngine;
using System.Collections;

public class EnemyCreater : MonoBehaviour {

	int waitCount = 0;
	int count = 0;
	GameObject[] enemy = new GameObject[30];
	GameObject obstacleMaster;

	// Use this for initialization
	void Start () {
		obstacleMaster = GameObject.Find ("Obstacle");
	}
	
	// Update is called once per frame
	void Update () {
		waitCount++;
		if (waitCount == 1 && count != 30) {
			GameObject go = Instantiate(Resources.Load("Enemy")) as GameObject;

			float x = Random.Range(0,1000);
			float z = Random.Range(0,1000);
			go.transform.position = new Vector3 (x, 2, z);
			go.transform.parent = obstacleMaster.transform;
			go.name += count.ToString();
			enemy[count] = go;
			count++;
			waitCount = 0;
		}
	}
}

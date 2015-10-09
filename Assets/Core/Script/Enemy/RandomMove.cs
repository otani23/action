using UnityEngine;
using System.Collections;

public class RandomMove : MonoBehaviour {

	bool moving = false;
	float waitTime = 0;
	float moveSpeed = 20;
	enum moveVec
	{
		leftFlont = 7,	flont = 8,		rightFlont = 9,
		left = 4,		dontMove = 5,	right = 6,
		leftBack = 1,	back = 2,		rightBack = 3,
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if(!moving){
			moving = true;
			this.rigidbody.velocity = new Vector3 (0, 0, 0);

			waitTime = Random.Range(1,50);
			waitTime = waitTime/10;

			switch (Random.Range (1, 9)) {
			case (int)moveVec.flont:
				StartCoroutine(FlontMove());
				break;

			case (int)moveVec.back:
				StartCoroutine(BackMove());
				break;

			case (int)moveVec.right:
				StartCoroutine(RightMove());
				break;

			case (int)moveVec.left:
				StartCoroutine(LeftMove());
				break;

			case (int)moveVec.rightBack:
				StartCoroutine(RightBackMove());
				break;

			case (int)moveVec.rightFlont:
				StartCoroutine(RightFlontMove());
				break;

			case (int)moveVec.leftFlont:
				StartCoroutine(LeftFlontMove());
				break;

			case (int)moveVec.leftBack:
				StartCoroutine(LeftBackMove());
				break;

			case (int)moveVec.dontMove:
				StartCoroutine(DontMove());
				break;

			default:
				break;
			}
		}


	}

	IEnumerator FlontMove()
	{
		this.rigidbody.AddForce (Vector3.forward*moveSpeed, ForceMode.Impulse);
		yield return new WaitForSeconds(waitTime);
		moving = false;
	}

	IEnumerator BackMove()
	{
		this.rigidbody.AddForce (Vector3.forward*-moveSpeed, ForceMode.Impulse);
		yield return new WaitForSeconds(waitTime);
		moving = false;
	}
	
	IEnumerator RightMove()
	{
		this.rigidbody.AddForce (Vector3.right*moveSpeed, ForceMode.Impulse);
		yield return new WaitForSeconds(waitTime);
		moving = false;
	}

	IEnumerator LeftMove()
	{
		this.rigidbody.AddForce (Vector3.right*-moveSpeed, ForceMode.Impulse);
		yield return new WaitForSeconds(waitTime);
		moving = false;
	}
	
	IEnumerator RightFlontMove()
	{
		this.rigidbody.AddForce (Vector3.forward*moveSpeed, ForceMode.Impulse);
		this.rigidbody.AddForce (Vector3.right*moveSpeed, ForceMode.Impulse);
		yield return new WaitForSeconds(waitTime);
		moving = false;
	}
	
	IEnumerator RightBackMove()
	{
		this.rigidbody.AddForce (Vector3.forward*-moveSpeed, ForceMode.Impulse);
		this.rigidbody.AddForce (Vector3.right*moveSpeed, ForceMode.Impulse);
		yield return new WaitForSeconds(waitTime);
		moving = false;
	}

	IEnumerator LeftFlontMove()
	{
		this.rigidbody.AddForce (Vector3.forward*moveSpeed, ForceMode.Impulse);
		this.rigidbody.AddForce (Vector3.right*-moveSpeed, ForceMode.Impulse);
		yield return new WaitForSeconds(waitTime);
		moving = false;
	}
	
	IEnumerator LeftBackMove()
	{
		this.rigidbody.AddForce (Vector3.forward*-moveSpeed, ForceMode.Impulse);
		this.rigidbody.AddForce (Vector3.right*-moveSpeed, ForceMode.Impulse);
		yield return new WaitForSeconds(waitTime);
		moving = false;
	}

	IEnumerator DontMove()
	{
		yield return new WaitForSeconds (waitTime);
		moving = false;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	private Animator a;

	private Rigidbody2D rb;

	private Vector2 force;

	private bool facingRight;

	private GameObject tempFighter;

	// Use this for initialization
	void Start () {
		a = GetComponent<Animator> ();

		rb = GetComponent <Rigidbody2D> ();
		if (this.tag == "Fighter1") {
			tempFighter = GameObject.FindGameObjectWithTag ("Fighter1");
			facingRight = tempFighter.GetComponent<Fighter1> ().getFacingRight ();
		} else {
			tempFighter = GameObject.FindGameObjectWithTag ("Fighter2");
			facingRight = !tempFighter.GetComponent<Fighter2> ().getFacingRight ();
		}

		if (facingRight) {
			force = new Vector2 (1, 0);
		} else {
			force = new Vector2 (-1, 0);
		}

		rb.AddRelativeForce (force, ForceMode2D.Impulse);
	}
	
	// Update is called once per frame
	void Update () {
		Destroy (gameObject, 2f);
	}

	void OnTriggerEnter2D (Collider2D c){
		if (c.tag != this.tag) {
			a.SetBool ("Collision", true);
			rb.velocity = Vector3.zero;
			if (gameObject.name == "Batarang(Clone)") {
				Destroy (gameObject, .6f);
			} else {
				Destroy (gameObject, .3f);
			}
		}
	}

	public void batarangCrash(){
		if (facingRight) {
			force = new Vector2 (-0.2f, 0.1f);
			rb.constraints = RigidbodyConstraints2D.None;
			rb.constraints = RigidbodyConstraints2D.FreezeRotation;
			rb.AddRelativeForce (force, ForceMode2D.Impulse);
		} else {
			force = new Vector2 (0.2f, 0.1f);
			rb.constraints = RigidbodyConstraints2D.None;
			rb.constraints = RigidbodyConstraints2D.FreezeRotation;
			rb.AddRelativeForce (force, ForceMode2D.Impulse);
		}
	}
}

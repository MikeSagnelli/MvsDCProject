using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Fighter2 : MonoBehaviour {

	public GameObject pPreFav; //Used to continously instantiate projectiles
	private GameObject projectile; 
	private Animator a, winnerA;
	private Transform pSpawn, character, enemy;
	private Rigidbody2D rb;
	private Slider health;
	private new string name;
	private Text fighter, winner;
	private bool canControl, isHit, facingRight;
	private float special, b1, b2, b3, b4, h, v, //Inputs
	oldSp, oldV, old1, old2, old3, old4, // Used for only one action per button press
	healthVal; // Used for sliders

	// Use this for initialization
	void Start () {
		character = GetComponent<Transform> ();
		enemy = GameObject.FindGameObjectWithTag ("Fighter1").GetComponent<Transform> ();

		//No animation flag
		canControl = true;
		isHit = false;
		facingRight = true;

		//Set slider
		name = gameObject.name;
		this.tag = "Fighter2";
		health = GameObject.Find ("P2Health").GetComponent<Slider> ();
		healthVal = health.value;
		fighter = GameObject.Find ("P2Name").GetComponent<Text> ();
		fighter.text = name;
		winner = GameObject.Find ("Winner").GetComponent<Text> ();

		//Set tags
		this.tag = "Fighter2";
		for (int i = 0; i < character.childCount; i++) {
			transform.GetChild (i).gameObject.tag = "Fighter2";
		}

		//Animator
		a = GetComponent <Animator>();
		winnerA = GameObject.Find ("Canvas").GetComponent<Animator> ();

		//Projectile spawn
		pSpawn = transform.GetChild (0);

		// Only 1 action per button press
		oldSp = 0;
		oldV = 0;
		old1 = 0;
		old2 = 0;
		old3 = 0;
		old4 = 0;

		//Rigidbody 2D
		rb = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update () {	
		if (enemy.gameObject.GetComponent<Fighter1> ().getHealth () > 0) {
			if (!facingRight) {
				if (enemy.position.x - 0.5f < transform.position.x + 0.5f) {
					transform.Rotate (0, 180, 0, Space.World);
					transform.Translate (2.5f, 0, 0);
					facingRight = true;
				}
			} else if (facingRight) {
				if (enemy.position.x + 0.5f > transform.position.x - 0.5f) {
					transform.Rotate (0, 180, 0, Space.World);
					transform.Translate (2.5f, 0, 0);
					facingRight = false;
				}
			}
		} else {
			CannotControl ();
			winner.text = name + " WINS!";
			winnerA.SetTrigger ("GameOver");
		}

		//Update health with slider
		healthVal = health.value;

		//Death
		if (healthVal <= 0) {
			CannotControl ();
		}

		// Get inputs
		if (canControl == true) {
			h = Input.GetAxis ("P2Horizontal");
			v = Input.GetAxis ("P2Vertical");
			special = Input.GetAxis ("P2Projectile");
			b1 = Input.GetAxis ("P2_1");
			b2 = Input.GetAxis ("P2_2");
			b3 = Input.GetAxis ("P2_3");
			b4 = Input.GetAxis ("P2_4");
		} else {
			h = 0;
			v = 0;
			b1 = 0;
			b2 = 0;
			b3 = 0;
			b4 = 0;
			special = 0;
		}

		//Move horizontally
		if (oldSp == 0 && old1 == 0 && old2 == 0 && old3 == 0 && old4 == 0) {
			if (facingRight) {
				transform.Translate (h * Time.deltaTime * 5, 0, 0);
			} else if (!facingRight) {
				transform.Translate (-h * Time.deltaTime * 5, 0, 0);
			}
			if (oldV == 1) {
				a.SetFloat ("Movement", -h);
			}
		}

		// Jump
		if (v == -1 && oldV == 1) {
			Vector2 jumpForce = new Vector2 (0, 10f);
			rb.AddForce(jumpForce, ForceMode2D.Impulse);
			oldV = 0;
			a.SetTrigger ("Jump");
		}

		//Button 1
		if (b1 == 1 && old1 == 0) {
			a.SetTrigger ("1");
		}

		//Button 2
		if (b2 == 1 && old2 == 0) {
			a.SetTrigger ("2");
		}

		//Button 3
		if (b3 == 1 && old3 == 0) {
			a.SetTrigger ("3");
		}

		//Button 1
		if (b4 == 1 && old4 == 0) {
			a.SetTrigger ("4");
		}

		//Throw projectile
		if (special == 1 && oldSp == 0) {
			a.SetTrigger ("Special");
			throwProjectile ();
		}	

		oldSp = special;
		old1 = b1;
		old2 = b2;
		old3 = b3;
		old4 = b4;
	}

	void OnCollisionEnter2D (Collision2D c){
		// Fighter is not jumping
		if (c.gameObject.tag == "Stage") {
			oldV = 1;
		}

	}

	void OnTriggerEnter2D (Collider2D c){
		if (c.gameObject.tag == "Fighter1" && canControl) {
			isHit = true;
		} else if (c.gameObject.tag == "Fighter1" && isHit) {
			isHit = true;
		} else {
			isHit = false;
		}

		if (isHit) {
			if (c.gameObject.name == "Web(Clone)") {
				a.SetTrigger ("HitWeb");
			} else {
				a.SetTrigger ("NormalHit");
			}
			health.value -= 5;
			Vector2 hit = new Vector2 (1f, 0);
			rb.AddForce (hit, ForceMode2D.Impulse);
		}

		isHit = false;
	}

	void throwProjectile (){
		projectile = Instantiate (pPreFav, pSpawn.position, pSpawn.rotation);
		projectile.tag = "Fighter2";
	}

	public void CanControl(){
		canControl = true;
		oldSp = 1;
		old1 = 1;
		old2 = 1;
		old3 = 1;
		old4 = 1;
	}

	public void CannotControl(){
		canControl = false;
	}

	public bool getFacingRight(){
		return facingRight;
	}

	public void detectWhenLanding(){
		if (oldV == 1) {
			a.SetTrigger ("Landing");
		}
	}

	public float getHealth(){
		return healthVal;
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour {

	private Text clock;

	private int initialTime;

	private float timeCounter;

	// Use this for initialization
	void Start () {
		clock = GetComponent<Text> ();
		initialTime = 99;
		timeCounter = 0;
	}
	
	// Update is called once per frame
	void Update () {
		timeCounter += Time.deltaTime;
		if (timeCounter > 1) {
			initialTime -= 1;
			timeCounter = 0;
		}
		clock.text = initialTime.ToString();

		if (initialTime <= 0) {
			Destroy (GameObject.FindGameObjectWithTag("Fighter1"));
			Destroy (GameObject.FindGameObjectWithTag("Fighter2"));
			Destroy (gameObject);
		}
	}
}

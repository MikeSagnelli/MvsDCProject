using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour {
	float oldO, timer;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (gameObject);
		oldO = 0;
		timer = 30;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp (KeyCode.Escape)) {
			Application.Quit ();
		}
		if (SceneManager.GetActiveScene ().buildIndex == 3) {
			timer -= Time.deltaTime;
			if (Input.GetAxis ("Options") == 1 && oldO == 0) {
				nextScene ();
			}
			if (timer <= 0) {
				SceneManager.LoadScene (0);
				Destroy (gameObject);
			}
			oldO = Input.GetAxis ("Options");
		}
		if (SceneManager.GetActiveScene ().buildIndex == 4) {
			if (Input.GetKeyUp (KeyCode.B) && oldO == 0) {
				SceneManager.LoadScene (0);
				Destroy (gameObject);
				oldO = 1;
			}
			oldO = 0;
		}
		if (SceneManager.GetActiveScene ().buildIndex == 5) {
			Destroy (gameObject);
		}
	}

	public void nextScene(){
		SceneManager.LoadScene ((SceneManager.GetActiveScene ().buildIndex + 1));
	}

	public void exit(){
		Application.Quit ();
	}
}

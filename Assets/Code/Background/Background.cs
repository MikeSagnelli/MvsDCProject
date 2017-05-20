using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour {

	GameObject a;
	NextScene nS;

	// Use this for initialization
	void Start () {
		a = GameObject.Find ("SceneChanger");
		nS = a.GetComponent<NextScene> ();
	}

	// Update is called once per frame
	void Update () {
		
	}

	void NextScene(){
		nS.nextScene ();
	}
}

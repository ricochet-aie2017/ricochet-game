using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadScore : MonoBehaviour {

    public Text score;

	// Use this for initialization
	void Start () {
        score.text = (GlobalVariables.score).ToString();
	}
	
	// Update is called once per frame
	void Update () {
        score.text = (GlobalVariables.score).ToString();
    }
}

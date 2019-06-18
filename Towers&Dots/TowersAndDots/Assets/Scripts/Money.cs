using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour {

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        int money = GameController.zlato;
        GetComponent<TextMesh>().text = money + "";
    }
}

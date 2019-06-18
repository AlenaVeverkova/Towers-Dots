using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class koloVypis : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        string vypis = "Kolo: " + GameController.kolo + System.Environment.NewLine + "Zbývá nepřátel: " + (20 - LevelManager.dead);
        GetComponent<TextMesh>().text = vypis;

    }
}

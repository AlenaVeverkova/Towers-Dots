using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBar : MonoBehaviour {
    private int hp=20;
	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        if(hp != GameController.zivoty)
        {
            float change = (float)(hp - GameController.zivoty)/ 20;
            transform.localScale -= new Vector3(change, 0, 0);
            Debug.Log("chande HP " + change);
            hp = GameController.zivoty;
        }
    }
}

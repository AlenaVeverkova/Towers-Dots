using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAnimator : MonoBehaviour {
    // Use this for initialization
    private int ID;
    private int typ;

    private Animator anim;
    void Start () {

        string objectName = gameObject.name;
        Debug.Log(objectName);
        string number = objectName.Replace("Tower", string.Empty);
        int id = int.Parse(number);
        Debug.Log("ID" + id);
        anim = GetComponent<Animator>();

        if (id / 800 > 1)
        {
            typ = 8;
            ID = id % 800;

        }
        else if (id / 700 > 1)
        {
            typ = 7;
            ID = id % 700;

        }
        else if (id / 600 > 1)
        {
            typ = 6;
            ID = id % 600;

        }
        else if (id / 500 > 1)
        {
            typ = 5;
            ID = id % 500;

        }
        else if (id / 400 > 1)
        {
            typ = 4;
            ID = id % 400;

        }
        else if (id / 300 > 1)
        {
            typ = 3;
            ID = id % 300;

        }
        else if (id/200 > 1 )
        {
            typ = 2;
            ID = id % 200;
            
        }
        else 
        {
            typ = 1;
            ID = id % 100;
            Debug.Log("ID" + ID);
        }

       
    }
	
    void animateClose()
    {
        anim.SetBool("attack", false);

    }
	// Update is called once per frame
	void Update () {


        /* if(LevelManager.towers[ID].animate == false)
         {
             anim.SetBool("attack", false);
             //   anim.SetBool("attack", false);
         }*/
        if(ID > 100) {
            ID -= 100;
                }
      if (LevelManager.towers[ID].animate == true)
        {
            Invoke("animateClose", 1f);
            //Debug.Log("atttaaaaack");
            LevelManager.towers[ID].animate = false;
            anim.SetBool("attack", true);
            //anim.Play("spin");
        }
    }
}

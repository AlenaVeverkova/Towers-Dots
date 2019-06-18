using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {
    public float x;
    public GameObject ja;
    public float y;
    public int typ = 0;
    public int id;
    private int kolo;
    public int update;
    public bool loadBool = true;
    public bool animate = true;
    public bool attacking = false;
    public int attack = 2;
    int i = 0;
    private Animation anim;
			public int lvl;

			public Tower(int lvl){
				this.lvl = lvl;
			}

	    void Awake ()
	    {
         
    }
      void Start()
    {
        anim = GetComponent<Animation>();
    }
    void bar()
    {
        Debug.Log("waiting tower");
        loadBool = false;
    }

    void wake()
    {
        
    }

    public int getAttack()
    {
        return attack;
    }

    void Update ()
        
	    {
        if((typ == 4) && (GameController.kolo != kolo))
        {
            kolo++;
            if (GameController.zivoty < 20)
            {
                GameController.zivoty++;
            }
        }
        for (int i = 0; i < 20; i++) {
            
        }
        //Debug.Log(id+" "+y+" "+x);
	    }



}
//{ { 0, 0, 0, 0, 0, 0, 0, 0 }, { 0, 1,2,2,2,2,0,0}, { 0,0,0,0,0,2,2,0}, { 0,0,1,0,0,1,2,0}, {2,2,2,2,2,2,2,0 }, { 0, 0, 2, 1, 0, 0, 0, 0 } };
//{ 0, 0, 2, 1, 0, 0, 0, 0 }, {2,2,2,2,2,2,2,0 }, { 0,0,1,0,0,1,2,0}, { 0,0,0,0,0,2,2,0}, { 0, 1,2,2,2,2,0,0}, { 0, 0, 0, 0, 0, 0, 0, 0 }

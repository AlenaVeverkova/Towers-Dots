using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour {
    public int id;
    public int hp;
    public float x=0;
    public GameObject ja;
    public float y=0;
    public float xs =0;
    private bool s;
    public float ys=0;
    public float speed = 0.1f;
    private int position =0;
    public Vector3 pos;
    private bool posi = false;
    private int  a = 0;
    private bool stop;
    private float test = 0f;
    private double yLast;
    private double xLast;
    public double yFirst;
    public double xFirst;
    private bool isPaused = false;
    public bool active = false;
    private bool dead = false;
    public int zlato = 15;
    
    public enemy(int x, int y)
    {
        this.x = x;
        this.y = y;

    }
    public void setX(float x)
    {
        this.x = x;
    }
    public void setY(float y)
    {
        this.y = y;
    }
    public float getX()
    {
        return x;
    }
    public float getY()
    {
        return y;
    }
    public void findWay()
    {
        
    }

    public void destroy()
    {
        active = true;
        Debug.Log("Destroy enemy " + id);
        GameController.zlato += (int)((double)zlato*LevelManager.nasobek);
        LevelManager.dead += 1;
    }
    public void attacked(int hpp)
    {
        Debug.Log("attack " + hp);
        hp -= hpp;
        if (hp <= 0)
        {
            destroy();
        }
    }


    public void posFinder()
    {
        if ((LevelManager.mista[(int)ys + 1, (int)xs] == 2) && (ys + 1 != yLast || xs != xLast))
        {
            xLast = xs;
            yLast = ys;
            ys++;
            y++;

        }
        else if ((LevelManager.mista[(int)ys, (int)xs - 1] == 2) && (ys != yLast || xs - 1 != xLast))
        {
            xLast = xs;
            yLast = ys;
            xs--;
            x--;

        }
        else if ((LevelManager.mista[(int)ys, (int)xs + 1] == 2) && (ys != yLast || xs + 1 != xLast))
        {
            xLast = xs;
            yLast = ys;
            xs++;
            x++;

        }
        else if ((LevelManager.mista[(int)ys - 1, (int)xs] == 2) && (ys - 1 != yLast || xs != xLast))
        {
            xLast = xs;
            yLast = ys;
            ys--;
            y--;

        }
        else if ((ys == yFirst || xs == xFirst))
        {
            GameController.zivoty -= 1;
            Debug.Log(GameController.zivoty);
            active = true;
            LevelManager.dead += 1;
        }
        if (id == 0)
        {
            Debug.Log("enemy  " + xs + " " + ys);
        }
      
    }



     void LateUpdate () {
        if (active & !dead)
        {
            Vector3 newPos = new Vector3(x, y, 0);
            if ((Vector3.Distance(ja.transform.position, newPos) < 0.05f))
            {
                dead = true;
                Debug.Log("dead!");
                Destroy(ja);
            }
        }
        if (Input.GetKeyDown("space"))
        {

            if (isPaused)
            {
                isPaused = false;
            }
            else
            {
                isPaused = true;
            }
        }
        if (GameController.isPaused == false & ja!=null)
        {
            Vector3 newPos = new Vector3(x, y, 0);
            ja.transform.position = Vector3.Lerp(ja.transform.position, newPos, Time.deltaTime*(speed/10)*LevelManager.nasobekRychlosti);
            test = test + Time.deltaTime;

            if ((Vector3.Distance(ja.transform.position, newPos) < 0.05f && isPaused == false) && active == false)
            {
                posFinder();

            }
        }
           
     }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
     bool pause = false;
    public static bool isPaused = false;
    public static bool lost = false;
    [SerializeField]
    private GameObject pauza;
    [SerializeField]
    private GameObject prohra;
    [SerializeField]
    private GameObject noveKoloTile;
    public static GameObject nt;
    public static int zivoty = 20;
   public static int zlato;
    public static int kolo = 1;
    public static bool noveKolo;
    public static bool isOpen = false;
    public static bool isUpdate = false;
    public static int choice=-1;
    public static bool hit = false;
    public static int hitID;
    public Texture2D cursorTexture;
    private Vector2 hotSpot = Vector2.zero;
    public Texture[] btn = new Texture [9];

    void OnGUI()
    {
       
      
    }

    void Start()
    {

    }

    
    void Update()
    {
        if(noveKolo == true &&  zivoty>0)
        {
            nt = Instantiate(noveKoloTile);
            noveKolo = false;
        }
        if (zivoty < 1 & !lost)
        {
            lost = true;
            isPaused = true;
            nt = Instantiate(prohra);
        }
        if (Input.GetKeyDown("space") && lost == false)
        {

            if (isPaused)
            {
                Destroy(nt);
                isPaused = false;
            }
            else
            {
                isPaused = true;
                nt = Instantiate(pauza);
            }
        }
    }
}

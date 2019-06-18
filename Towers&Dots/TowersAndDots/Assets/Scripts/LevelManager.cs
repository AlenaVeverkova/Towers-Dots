using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    public GameObject tile;
    public GameObject towerTile;
    public GameObject roadTile;
    public GameObject enemy;
    public GameObject finishTile;
    public GameObject tower1tile;
    public GameObject bombTowerTile;
    public GameObject eleTowerTile;
    public GameObject medicTowerTile;
    public GameObject poisonTowerTile;
    public GameObject moneyTowerTile;
    public GameObject coldTowerTile;
    public GameObject pauseTile;
    public GameObject startTile;
    public GameObject[] rychlost = new GameObject[3];
    public GameObject[] sandTiles = new GameObject[3];
    public GameObject[] stoneTiles = new GameObject[3];
    public GameObject chosen;
    public GameObject[] lavaTiles = new GameObject[3];
    public GameObject[] iceTiles = new GameObject[3];
    public GameObject[] snowTiles = new GameObject[3];
    private GameObject rychlostScena;
    public GameObject[] towerButtons = new GameObject[4]; 

    public static double nasobek = 1;
    public static int nasobekRychlosti = 1;
    public int idHit;
    public int zivoty;
    private float xFirst;
    private float yFirst;
    private float xLast;
    private float yLast;
    private float xsFirst;
    private float ysFirst;
    private GameObject nt;
    private int spawned = 0;
    private bool spawn = true;
    public static float tileSize;
    private float xsLast;
    private float ysLast;
    private int counter = 0;
    private bool first;
    public static int dead = 0;
    public static float tiles;
    public static Tower[] towers = new Tower[10];
    public enemy[] enemies = new enemy[20];
    public GameObject[] enemyTiles = new GameObject[13];
    public int[,] enemyInfo = new int[,] { { 2, 4, 8, 10, 15,18,21,24,27,30,33,34,38,42,45,49,52 }, { 20, 22, 24, 26,28,30,31,32,33,34,36,38,40,42,44,46,47 }, { 2, 3, 5, 6, 8,10,11,12,14,16,18,20,22,24,26,28,30 } }; //zivoty, rychlost,penize
    public bool isPaused = false;
    private int t = 0;
    public static bool[] towerAttack = new bool[]{ false, false, false, false, false, false, false, false, false, false };
    private int[] spawnTypes = new int[13];
    public GameObject[] spawnTiles = new GameObject[4];
    private int[] spawnInfo = new int[13];

    int[,] mapa;
    public GameObject[] btnAll = new GameObject[20];
    public GameObject close;
    public static string readText;
    private GameObject btnClose;
    private GameObject[] btnChosen = new GameObject[10];
    public static GameObject chosenTile;
    public static float[,] way = new float[50, 50];
    public static int waycounter = 0;
    private object[,] pole = new object[10, 12];
    public static int[,] mista = new int[,] { { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 }, {3,0,0,0,0,0,0,1,0,2,0,3}, { 3, 0,2,2,2,1,2,2,2,2,0, 3 }, { 3, 0,2,1,2,2,2,1,0,1,0, 3 }, { 3, 0,2,0,0,0,1,0,0,0,0, 3 }, { 3, 0,2,2,2,2,2,2,2,2,0, 3 }, { 3, 0,1,0,0,1,0,0,1,2,0, 3 }, { 3, 0,2,2,2,2,2,2,2,2,0, 3 }, { 3, 0,2,0,1,0,0,0,0,0,0, 3 }, { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 } };
   
    public float GetEnemyX(int counter) {
        return enemies[counter].xs;
    }
    public float GetEnemyY(int counter)
    {
        return enemies[counter].ys;
    }


        public void buttonLoader()
    {
        buttonsDestroyer(false);
        btnClose = Instantiate(close);
        btnClose.transform.position = new Vector3((tileSize * 14.2f) - 7, (tileSize * 8.3f) - 5, 0);
        int index = 0;
        for(int i = 0; i < 5; i++)
        {
            for(int a = 0; a < 3; a++)
            {
                btnChosen[index] = Instantiate(btnAll[index]);
                btnChosen[index].transform.position = new Vector3((tileSize * 13.5f-a) - 7, (tileSize * 7.5f-i) - 5, 0);
                index++;
            }

        }
        
        btnChosen[0] = Instantiate(btnAll[0]);
        btnChosen[0].transform.position = new Vector3((tileSize * 13.5f) - 7, (tileSize * 7.5f) - 5, 0);
        btnChosen[1] = Instantiate(btnAll[1]);
        btnChosen[1].transform.position = new Vector3((tileSize * 12.5f) - 7, (tileSize * 7.5f) - 5, 0);
        btnChosen[2] = Instantiate(btnAll[2]);
        btnChosen[1].transform.position = new Vector3((tileSize * 12.5f) - 7, (tileSize * 7.5f) - 5, 0);
    }

        public void buttonsDestroyer(bool c)
    {
        for(int i = 0; i < btnChosen.Length; i++)
        {
            if (c)
            {

                Destroy(chosenTile);
            }
            Destroy(btnClose);
            Destroy(btnChosen[i]);
        }

    }
    

    private void towersController() {
        for (int i = 0; i <10; i++)
        {
            if ((towers[i].loadBool == false) && (towers[i].typ == 1 || towers[i].typ == 2|| towers[i].typ == 6))
            {
                for (int a = 0; a < spawned; a++)
                {
                    if (enemies[a].active == false) {
                        if (((towers[i].x == enemies[a].xs - 1) && (towers[i].y == enemies[a].ys)) || ((towers[i].x == enemies[a].xs + 1) && (towers[i].y == enemies[a].ys)) || ((towers[i].x == enemies[a].xs) && (towers[i].y == enemies[a].ys + 1)) || ((towers[i].x == enemies[a].xs) && (towers[i].y == enemies[a].ys - 1)))
                        {
                            towers[i].loadBool = true;
                            towerAttack[i] = true;
                            towers[i].Invoke("bar", 3f/ nasobekRychlosti);
                            towers[i].animate = true;
                            int power = towers[i].getAttack();
                            enemies[a].attacked(power);
                            towerAttack[i] = true;

                        }

                        if ((towers[i].typ == 2)&& ((towers[i].x == enemies[a].xs - 1) && (towers[i].y == enemies[a].ys-1) || (towers[i].x == enemies[a].xs + 1) && (towers[i].y == enemies[a].ys + 1) || (towers[i].x  == enemies[a].xs-1) && (towers[i].y == enemies[a].ys + 1) || (towers[i].x == enemies[a].xs + 1) && (towers[i].y == enemies[a].ys - 1)))
                        {
                            towers[i].animate = true;
                            towers[i].loadBool = true;
                            towers[i].Invoke("bar", 3f/ nasobekRychlosti);
                            int power = towers[i].getAttack();
                            enemies[a].attacked(power);
                        }
                    }
                }
            }
            else if ((towers[i].loadBool == false) && (towers[i].typ == 7))
            {
                for (int a = 0; a < spawned; a++)
                {
                    if (enemies[a].active == false)
                    {
                        if (((towers[i].x == enemies[a].xs - 1) && (towers[i].y == enemies[a].ys)) || ((towers[i].x == enemies[a].xs + 1) && (towers[i].y == enemies[a].ys)) || ((towers[i].x == enemies[a].xs) && (towers[i].y == enemies[a].ys + 1)) || ((towers[i].x == enemies[a].xs) && (towers[i].y == enemies[a].ys - 1)))
                        {
                            towers[i].loadBool = true;
                            towerAttack[i] = true;
                            towers[i].Invoke("bar", 2f/ nasobekRychlosti);
                            towers[i].animate = true;
                            int power = towers[i].getAttack();
                            enemies[a].attacked(power);
                            towerAttack[i] = true;

                        }

                        
                    }
                }
            }
            else if ((towers[i].loadBool == false) && (towers[i].typ == 3))
            {
                for (int a = 0; a < spawned; a++)
                {
                    if (enemies[a].active == false)
                    {
                        if (((towers[i].x == enemies[a].xs - 1) && (towers[i].y == enemies[a].ys)) || ((towers[i].x == enemies[a].xs + 1) && (towers[i].y == enemies[a].ys)) || ((towers[i].x == enemies[a].xs) && (towers[i].y == enemies[a].ys + 1)) || ((towers[i].x == enemies[a].xs) && (towers[i].y == enemies[a].ys - 1)))
                        {
                            towers[i].loadBool = true;
                            towerAttack[i] = true;
                            towers[i].Invoke("bar", 1f / nasobekRychlosti);
                            towers[i].animate = true;
                            int power = towers[i].getAttack();
                            enemies[a].attacked(power);
                            towerAttack[i] = true;

                        }


                    }
                }
            }
        }
    }

    void Start() {
        mapLoader();
        GameController.noveKolo = false;
        GameController.isPaused = false;
        for (int i = 0; i < 20; i++)
        {
            enemies[i] = null;
        }
        GameController.zivoty = 20;
        GameController.kolo = 1;
        GameController.zlato = 150;
        nasobekRychlosti = 1;
        rychlostScena = Instantiate(rychlost[0]);
        rychlostScena.transform.position = new Vector3(-5.8f, 3.9f, 0f);
       
        Random rnd = new Random();
        int typMapy = Random.Range(1, 7);
        switch (typMapy)
        {
            case 1:
                tile = sandTiles[0];
                roadTile = sandTiles[1];
                towerTile = sandTiles[2];
                
                break;
            case 2:
                tile = stoneTiles[0];
                roadTile = stoneTiles[1];
                towerTile = stoneTiles[2];

                break;
            case 3:
                tile = lavaTiles[0];
                roadTile = lavaTiles[1];
                towerTile = lavaTiles[2];

                break;
            case 4:
                tile = snowTiles[0];
                roadTile = snowTiles[1];
                towerTile = snowTiles[2];

                break;
            case 5:
                tile = iceTiles[0];
                roadTile = iceTiles[1];
                towerTile = iceTiles[2];

                break;


        }
        mista =  mapa;
      
        recounter();
        for (int y = 1; y < 10; y++)
        {
            for (int x = 1; x < 11; x++)
            {
                if (mista[y, x] == 0) {
                    pole[y, x] = gameObject.AddComponent<Grass>();
                }
                else if (mista[y, x] == 1)
                {
                    pole[y, x] = gameObject.AddComponent<Tower>();

                }
                else if (mista[y, x] == 2)
                {

                    pole[y, x] = gameObject.AddComponent<Cesta>();

                }
            }
        }


        CreateLevel();
    }
    void mapLoader()
    {
          char spl = '&';
        string toRead = readText + "0;1;0;0;0;0;0;0;0;0;!0;1;2;0;0;2;1;1;1;0;!0;1;1;1;1;1;1;2;1;0;!0;0;0;2;0;2;0;0;1;0;!0;1;1;1;1;1;1;0;1;0;!2;1;0;2;0;2;1;2;1;0;!0;1;1;1;1;0;1;1;1;0;!0;0;0;2;1;0;0;0;0;0;!&" + "0;0;0;0;0;0;0;2;1;0;!0;1;1;1;0;2;0;0;1;0;!0;1;2;1;1;0;1;1;1;0;!0;1;1;2;1;0;1;0;2;0;!0;2;1;0;1;0;1;1;1;0;!0;1;1;0;1;0;2;0;1;0;!0;1;2;0;1;1;1;1;1;0;!0;1;0;0;0;2;0;2;0;0;!&" + "0;1;0;0;0;0;0;0;0;0;!2;1;0;0;0;2;1;1;1;0;!0;1;0;1;1;1;1;2;1;0;!0;1;2;1;2;0;0;0;1;0;!0;1;0;1;0;1;1;1;1;0;!0;1;2;1;2;1;2;0;2;0;!0;1;1;1;0;1;1;1;1;0;!0;0;0;0;0;0;0;2;1;0;!&" + "0;0;0;0;0;0;0;0;1;0;!0;1;1;1;0;0;0;2;1;0;!2;1;2;1;0;1;1;1;1;0;!0;1;0;1;0;1;2;0;2;0;!0;1;2;1;0;1;1;1;1;0;!2;1;0;1;0;2;0;2;1;0;!0;1;2;1;1;1;1;1;1;0;!0;1;0;0;0;0;0;0;0;0;!&" + "0;0;0;0;0;0;0;0;1;0;!0;1;1;1;1;1;1;2;1;0;!0;1;0;2;0;2;1;0;1;0;!0;1;1;1;0;0;1;2;1;0;!0;0;2;1;1;2;1;0;1;0;!0;0;0;2;1;0;1;2;1;0;!0;1;1;1;1;2;1;1;1;0;!0;1;2;0;0;0;0;0;0;0;!&" + "0;0;0;0;0;0;1;0;0;0;!0;1;1;1;2;0;1;2;0;0;!0;1;2;1;0;0;1;1;1;0;!0;1;0;1;1;2;0;2;1;0;!0;1;2;0;1;0;1;1;1;0;!0;1;1;2;1;2;1;2;0;0;!0;0;1;0;1;1;1;0;0;0;!0;0;1;2;0;0;0;0;0;0;!&";
        string[] maps = toRead.Split(spl);
        Random rnd = new Random();
        string chosen = maps[Random.Range(0, maps.Length-1)];
        mapa = new int[10, 12];
        string radek= "";
        string[] radky = chosen.Split('!');
        for(int i =0; i <radky.Length;i++)
        {
            string[] policka = radky[i].Split(';');
            mapa[i,0] = 3;
            for (int a = 0; a < policka.Length; a++)
            {
                if (policka[a].Contains("0"))
                {
                    mapa[i+1, a+1] = 0;
                }
                if (policka[a].Contains("1"))
                {
                    mapa[i+1, a+1] = 2;
                }
                if (policka[a].Contains("2"))
                {
                    mapa[i+1, a+1] = 1;
                }
            }
             mapa[i, 11] = 3;
        }

    }
    void recounter()
    {
        int max = (GameController.kolo + 2) / 3;
        int zbytek = GameController.kolo % 3;
        for (int i = 0; i < 4; i++)
        {
            if (max == 0)
            {
                spawnTiles[i] = enemyTiles[0];
                spawnInfo[i] = 0;
            }
            else
            {
                spawnTiles[i] = enemyTiles[(int)max];
                spawnInfo[i] = (int)max;
                max--;
            }
        }

        switch (zbytek)
        {
            case 0:
                spawnTypes[0] = 5;
                spawnTypes[1] = 5;
                spawnTypes[2] = 5;
                spawnTypes[3] = 5;
                break;
            case 1:
                spawnTypes[0] = 3;
                spawnTypes[1] = 4;
                spawnTypes[2] = 6;
                spawnTypes[3] = 7;
                break;
            case 2:
                spawnTypes[0] = 1;
                spawnTypes[1] = 5;
                spawnTypes[2] = 6;
                spawnTypes[3] = 8;
                break;
        }
        dead = 0;

    }
    void noveKolo()
    {
        GameController.kolo++;
        Destroy(GameController.nt);
        GameController.noveKolo = false;
        spawned = 0;
    }

    void Update() {
       
        if(GameController.hit == true)
        {
            changeTower(idHit);
            GameController.hit = false;
        }
        if (dead == 20)
        {
            recounter();
            GameController.noveKolo = true;
            Invoke("noveKolo", 3f);
        }
            if (Input.GetMouseButton(0))
            { 
                if (Input.GetMouseButtonDown(0))
                {
                    
                    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

                    RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
                    if (hit.collider != null)
                    {
                        string hitname = hit.collider.gameObject.name;
                        if (hitname.Contains("Tower") && GameController.isPaused == false)
                        {
                            Destroy(chosenTile);
                            string number = hitname.Replace("Tower", string.Empty);
                            idHit = int.Parse(number);
                            chosenTile = Instantiate(chosen);
                            chosenTile.transform.position = new Vector3((tileSize * towers[idHit].x) - 7, (tileSize * towers[idHit].y) - 5, 0);
                        
                            if (towers[idHit].typ == 0)
                            {
                                buttonLoader();
                            }
                            else
                            {
                                GameController.isUpdate = true;
                            }
                            GameController.hitID = idHit;
                        }
                        
                        else if (hitname.Contains("rychlost"))
                        {
                            if (nasobekRychlosti == 1)
                            {
                                nasobekRychlosti = 2;
                                Destroy(rychlostScena);
                                rychlostScena = Instantiate(rychlost[1]);
                                rychlostScena.transform.position = new Vector3(-5.8f, 3.9f, 0f);
                            }

                            else if (nasobekRychlosti == 2)
                            {
                                nasobekRychlosti = 3;
                                Destroy(rychlostScena);
                                rychlostScena = Instantiate(rychlost[2]);
                                rychlostScena.transform.position = new Vector3(-5.8f, 3.9f, 0f);
                            }

                            else if (nasobekRychlosti == 3)
                            {
                                nasobekRychlosti = 1;
                                Destroy(rychlostScena);
                                rychlostScena = Instantiate(rychlost[0]);
                                rychlostScena.transform.position = new Vector3(-5.8f, 3.9f, 0f);
                            }
                        }
                    

                
                        else if (hitname.Contains("Button"))
                        {
                            if (hitname.Contains("flame") && GameController.isPaused == false)
                            {
                                GameController.choice = 2;
                                GameController.hit = true;
                            }
                            else if (hitname.Contains("cold") && GameController.isPaused == false)
                            {
                                GameController.choice = 7;
                                GameController.hit = true;
                            }
                            else if(hitname.Contains("poison") && GameController.isPaused == false)
                            {
                                GameController.choice = 6;
                                GameController.hit = true;
                            }
                            else if(hitname.Contains("light") && GameController.isPaused == false)
                            {
                                GameController.choice = 3;
                                GameController.hit = true;
                            }
                            else if (hitname.Contains("cross") && GameController.isPaused == false)
                            {
                                GameController.choice = 1;
                                GameController.hit = true;
                            }
                            else if(hitname.Contains("money") && GameController.isPaused == false)
                            {
                                GameController.choice = 5;
                                GameController.hit = true;
                            }
                            else if (hitname.Contains("medic") && GameController.isPaused == false)
                            {
                                GameController.choice = 4;
                                GameController.hit = true;
                            }
                            else if (hitname.Contains("close") && GameController.isPaused == false)
                            {
                                buttonsDestroyer(true);
                            }
                             if (hitname.Contains("menu"))
                            {
                             mapLoader();
                                for (int i = 0; i < 10; i++) {
                                    Destroy(towers[i].ja);
                                }
                                for (int i = 0; i < 20; i++)
                                {
                                   
                                }
                                foreach (GameObject o in Object.FindObjectsOfType<GameObject>())
                                {
                                    Destroy(o);
                                }
                                waycounter = 0;
                                SceneManager.LoadScene("MenuScene", LoadSceneMode.Additive); 
                            }
                        }
                    }
                }
            }

        if (GameController.isPaused == false)
        {
            controlEnemy();
            towersController();
            
        }
    }
  
    
  public void changeTower(int id){

        if (towers[id].typ == 0) { 
        if (GameController.choice == 1 && GameController.zlato > 74)
            {
                Destroy(chosenTile);
                towers[id].ja = Instantiate(tower1tile);
                towers[id].typ = 1;
                towers[id].attack = 2;
                towers[id].ja.transform.position = new Vector3((tileSize * towers[id].x) - 7, (tileSize * towers[id].y) - 5, 0);
            GameController.zlato -= 75;
            GameController.choice = -1;

                buttonsDestroyer(true);
                towers[id].ja.name = "Tower" + "10"+ id;

        }
        else if (GameController.choice == 2 && GameController.zlato > 199)
            {
                Destroy(chosenTile);
                towers[id].ja = Instantiate(bombTowerTile);
                towers[id].typ = 2;
                towers[id].attack = 2;
                towers[id].ja.transform.position = new Vector3((tileSize * towers[id].x) - 7, (tileSize * towers[id].y) - 5, 0);
                GameController.zlato -= 200;

                buttonsDestroyer(true);
                GameController.choice = -1;
            towers[id].ja.name = "Tower" + "20"+id;

        }
        else if (GameController.choice == 3 && GameController.zlato > 299)
        {
                Destroy(chosenTile);
            towers[id].ja = Instantiate(eleTowerTile);
            towers[id].typ = 3;
            towers[id].attack = 1;
            towers[id].ja.transform.position = new Vector3((tileSize * towers[id].x) - 7, (tileSize * towers[id].y) - 5, 0);
            GameController.zlato -= 300;
                buttonsDestroyer(true);
                GameController.choice = -1;
            towers[id].ja.name = "Tower" + "30" + id;

        }
        else if (GameController.choice == 4 && GameController.zlato > 449)
        {
                Destroy(chosenTile);
            towers[id].ja = Instantiate(medicTowerTile);
            towers[id].typ = 4;
            towers[id].attack = 0;
            towers[id].ja.transform.position = new Vector3((tileSize * towers[id].x) - 7, (tileSize * towers[id].y) - 5, 0);
            GameController.zlato -= 450;

                buttonsDestroyer(true);
                GameController.choice = -1;
            towers[id].ja.name = "Tower" + "40" + id;

        }
            else if (GameController.choice == 5 && GameController.zlato > 499)
            {
                Destroy(chosenTile);
                towers[id].ja = Instantiate(moneyTowerTile);
                towers[id].typ = 5;
                towers[id].attack = 0; ;
                nasobek += 0.1;
                towers[id].ja.transform.position = new Vector3((tileSize * towers[id].x) - 7, (tileSize * towers[id].y) - 5, 0);
                GameController.zlato -= 1000;

                buttonsDestroyer(true);
                GameController.choice = -1;
                towers[id].ja.name = "Tower" + "50" + id;

            }
            else if (GameController.choice == 6 && GameController.zlato > 749)
            {
                Destroy(chosenTile);
                towers[id].ja = Instantiate(poisonTowerTile);
                towers[id].typ = 6;
                towers[id].attack = 3;
                towers[id].ja.transform.position = new Vector3((tileSize * towers[id].x) - 7, (tileSize * towers[id].y) - 5, 0);
                GameController.zlato -= 750;

                buttonsDestroyer(true);
                GameController.choice = -1;
                towers[id].ja.name = "Tower" + "60" + id;

            }
            else if (GameController.choice == 7 && GameController.zlato > 699)
            {
                Destroy(chosenTile);
                towers[id].ja = Instantiate(coldTowerTile);
                towers[id].typ = 7;
                towers[id].attack = 5;
                towers[id].ja.transform.position = new Vector3((tileSize * towers[id].x) - 7, (tileSize * towers[id].y) - 5, 0);
                GameController.zlato -= 700;

                buttonsDestroyer(true);
                GameController.choice = -1;
                towers[id].ja.name = "Tower" + "70" + id;

            }
        }
    }

    private void CreateLevel() {
        
        tileSize = this.tile.GetComponent<SpriteRenderer>().sprite.bounds.size.x;

        for (int y = 1; y < 9; y++) {
            for (int x = 1; x < 11; x++)
            {
                if (pole[y, x].GetType() == typeof(Tower))
                {
                    towers[t] = gameObject.AddComponent<Tower>();
                    towers[t].ja = Instantiate(towerTile);
                    towers[t].ja.name = "Tower"+counter;
                    counter++;
                    towers[t].ja.transform.position = new Vector3((tileSize * x)-7, (tileSize * y)-5, 0);
                    towers[t].x = x;
                    towers[t].y = y;
                    towers[t].id = t;
                    t++;
                }
                else if (pole[y, x].GetType() == typeof(Cesta))
                {
                    GameObject newTile = Instantiate(roadTile);
                    newTile.transform.position = new Vector3((tileSize * x)-7, (tileSize * y)-5, 0);
                    way[0,waycounter]=x;
                    way[1,waycounter]=y;
                    waycounter++;

                    if (first == false)
                    {
                        xsFirst = x;
                        ysFirst = y;
                        yFirst = (tileSize * y) - 5;
                        xFirst = (tileSize * x) - 7;
                        first = true;
                        tiles = tileSize;
                    }
                    yLast = (tileSize * y) - 5;
                    xLast = (tileSize * x) - 7;
                    xsLast = x;
                    ysLast = y;

                }
                else {
                    GameObject newTile = Instantiate(tile);
                    newTile.transform.position = new Vector3((tileSize * x)-7, (tileSize * y)-5, 0);

                }

            }
        }
        GameObject start = Instantiate(startTile);
        start.transform.position = new Vector3((tileSize * xsLast) - 7, (tileSize * ysLast) - 5, 0);
        GameObject final = Instantiate(finishTile);
        final.transform.position = new Vector3((tileSize * xsFirst) - 7, (tileSize * ysFirst) - 5, 0);

    }
    private void controlEnemy() {
        
        if (spawned < 20 && spawn == true && GameController.isPaused == false) {
            if (spawnTypes[0] > 0)
            {
                spawnTypes[0]--;
                Debug.Log("spawn " + spawned);
                enemies[spawned] = gameObject.AddComponent<enemy>();
                enemies[spawned].setX(xLast);
                enemies[spawned].setY(yLast);
                enemies[spawned].ys = ysLast;
                enemies[spawned].xs = xsLast;
                enemies[spawned].yFirst = ysFirst;
                enemies[spawned].xFirst = xsFirst;
                enemies[spawned].ja = Instantiate(spawnTiles[0]);
                enemies[spawned].ja.transform.position = new Vector3(enemies[spawned].x, enemies[spawned].y , 0);
                enemies[spawned].id = spawned;
                enemies[spawned].hp = enemyInfo[0, spawnInfo[0]];
                enemies[spawned].zlato = enemyInfo[2, spawnInfo[0]];
                enemies[spawned].speed = enemyInfo[1, spawnInfo[0]];
                spawned++;
                spawn = false;
                Invoke("bar", 3f/ nasobekRychlosti);
            }
            else if (spawnTypes[1] > 0)
            {
                spawnTypes[1]--;
                enemies[spawned] = gameObject.AddComponent<enemy>();
                enemies[spawned].setX(xLast);
                enemies[spawned].setY(yLast);
                enemies[spawned].ys = ysLast;
                enemies[spawned].xs = xsLast;
                enemies[spawned].yFirst = ysFirst;
                enemies[spawned].xFirst = xsFirst;
                enemies[spawned].ja = Instantiate(spawnTiles[1]);
                enemies[spawned].ja.transform.position = new Vector3(enemies[spawned].x, enemies[spawned].y , 0);
                enemies[spawned].id = spawned;
                enemies[spawned].hp = enemyInfo[0, spawnInfo[1]];
                enemies[spawned].zlato = enemyInfo[2, spawnInfo[1]];
                enemies[spawned].speed = enemyInfo[1, spawnInfo[1]];
                spawned++;
                spawn = false;
                Invoke("bar", 3f/ nasobekRychlosti);
            }
            else if (spawnTypes[2] > 0)
            {
                spawnTypes[2]--;
                enemies[spawned] = gameObject.AddComponent<enemy>();
                enemies[spawned].setX(xLast);
                enemies[spawned].setY(yLast);
                enemies[spawned].ys = ysLast;
                enemies[spawned].xs = xsLast;
                enemies[spawned].yFirst = ysFirst;
                enemies[spawned].xFirst = xsFirst;
                enemies[spawned].ja = Instantiate(spawnTiles[2]);
                enemies[spawned].ja.transform.position = new Vector3(enemies[spawned].x, enemies[spawned].y , 0);
                enemies[spawned].id = spawned;
                enemies[spawned].hp = enemyInfo[0, spawnInfo[2]];
                enemies[spawned].zlato = enemyInfo[2, spawnInfo[2]];
                enemies[spawned].speed = enemyInfo[1, spawnInfo[2]];
                spawned++;
                spawn = false;
                Invoke("bar", 3f/ nasobekRychlosti);
            }
            else if (spawnTypes[3] > 0)
            {
                spawnTypes[3]--;
                enemies[spawned] = gameObject.AddComponent<enemy>();
                enemies[spawned].setX(xLast);
                enemies[spawned].setY(yLast);
                enemies[spawned].ys = ysLast;
                enemies[spawned].xs = xsLast;
                enemies[spawned].yFirst = ysFirst;
                enemies[spawned].xFirst = xsFirst;
                enemies[spawned].ja = Instantiate(spawnTiles[3]);
                enemies[spawned].ja.transform.position = new Vector3(enemies[spawned].x, enemies[spawned].y , 0);
                enemies[spawned].id = spawned;
                enemies[spawned].hp = enemyInfo[0, spawnInfo[3]];
                enemies[spawned].zlato = enemyInfo[2, spawnInfo[3]];
                enemies[spawned].speed = enemyInfo[1, spawnInfo[3]];
                spawned++;
                spawn = false;
                Invoke("bar", 3f/ nasobekRychlosti);
            }
        }
    }

    void bar() {
        spawn = true;
    }
    IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        spawn = true;
    }

 
    }

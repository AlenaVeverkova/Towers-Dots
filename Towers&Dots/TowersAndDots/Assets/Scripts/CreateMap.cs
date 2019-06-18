//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

using System.Text;

public class CreateMap : MonoBehaviour
{
    public GameObject[] toSpawnTiles = new GameObject[3];
    public GameObject[] sandTiles = new GameObject[3];
    public GameObject[] stoneTiles = new GameObject[3];
    public GameObject[] lavaTiles = new GameObject[3];
    public GameObject[] iceTiles = new GameObject[3];
    public GameObject[] snowTiles = new GameObject[3];
    public GameObject[] grassTiles = new GameObject[3];
    private int[,] pole = new int[10, 12];
    private GameObject[,] tiles = new GameObject[10, 12];
    public GameObject chosen;
    public GameObject chosenTile;
    public int chosenX;
    public int chosenY;
    public bool chosenBool = false;
    public bool all = false;
    public GameObject toSpawn;
    public GameObject empty;
    public static float tileSize;
    public int spawnID;
    public int chosenTop;
    public GameObject chosenTopTile;
    public bool delete = false;
    private int counter = 1;
    private GameObject btn1;
    private GameObject btn2;
    private GameObject btn3;
    private GameObject bomb;
    private GameObject del;
    // Use this for initialization
    void change()
    {

        for (int ys = 1; ys < 9; ys++)
        {
            for (int xs = 1; xs < 11; xs++)
            {
                
                if(pole[ys, xs] == 0)
                {
                    Destroy(tiles[ys, xs]);
                    tiles[ys, xs] = Instantiate(toSpawnTiles[0]);
                    tiles[ys, xs].name = "way;" + xs + ";" + ys;
                }
                if (pole[ys, xs] == 1)
                {
                    Destroy(tiles[ys, xs]);
                    tiles[ys, xs] = Instantiate(toSpawnTiles[1]);
                    tiles[ys, xs].name = "way;" + xs + ";" + ys;
                }
                if (pole[ys, xs] == 2)
                {
                    Destroy(tiles[ys, xs]);
                    tiles[ys, xs] = Instantiate(toSpawnTiles[2]);
                    tiles[ys, xs].name = "way;" + xs + ";" + ys;
                }
                //GameObject newTile = Instantiate(towerTile);
                tiles[ys, xs].transform.position = new Vector3((tileSize * xs) - 7, (tileSize * ys) - 5, 0);

            }
        }


    }
    void Start()
    {
        toSpawnTiles = iceTiles;
        tileSize = this.chosen.GetComponent<SpriteRenderer>().sprite.bounds.size.x;
        load();
        //chosen = Instantiate(toSpawnTiles[0]);
        btn1.transform.position = new Vector3((tileSize * 13.5f) - 7, (tileSize * 7.5f) - 5, 0);
        toSpawn = toSpawnTiles[spawnID];
        float x = -5.5f;
        float y = 4.3f;
        chosenTopTile = Instantiate(chosen);
        chosenTopTile.transform.position = new Vector3(x, (y), 0);
        for (int ys = 1; ys < 9; ys++)
        {
            for (int xs = 1; xs < 11; xs++)
            {
                pole[ys, xs] =-1;

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        hit();
    }
    void leftBtns()
    {
         btn1 = Instantiate(toSpawnTiles[0]);
        btn1.transform.position = new Vector3((tileSize * 13.5f) - 7, (tileSize * 7.5f) - 5, 0);
        btn1.name = "tileBtn";
         btn2 = Instantiate(toSpawnTiles[1]);
        btn2.transform.position = new Vector3((tileSize * 13.5f) - 7, (tileSize * 6.4f) - 5, 0);
        btn2.name = "wayBtn";
         btn3 = Instantiate(toSpawnTiles[2]);
        btn3.transform.position = new Vector3((tileSize * 13.5f) - 7, (tileSize * 5.3f) - 5, 0);
        btn3.name = "towerBtn";
    }
    void load()
    {
        for (int y = 1; y < 9; y++)
        {
            for (int x = 1; x < 11; x++)
            {
                tiles[y, x] = Instantiate(empty);
                tiles[y, x].name = "way;" + x + ";" + y;
                counter++;
                //GameObject newTile = Instantiate(towerTile);
                tiles[y, x].transform.position = new Vector3((tileSize * x) - 7, (tileSize * y) - 5, 0);

            }
        }
        leftBtns();
        
    }

    void hit()
    {
        if (Input.GetMouseButton(0))
        { // << use GetMouseButton instead of GetMouseButtonDown
            if (Input.GetMouseButtonDown(0))
            {

                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

                RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
                if (hit.collider != null)
                {
                   // Debug.Log(hit.collider.gameObject.name);
                    string hitname = hit.collider.gameObject.name;

                    if (hitname.Contains("Btn"))
                    {
                        
                        if (chosenBool)
                        {
                            Destroy(chosenTile);
                        }
                        chosenBool = true;

                        chosenTile = Instantiate(chosen);

                        if (hitname.Contains("tile"))
                        {
                            delete = false;

                            Destroy(del);
                            toSpawn = toSpawnTiles[0];
                            spawnID = 0;
                            chosenTile.transform.position = new Vector3((tileSize * 13.5f) - 7, (tileSize * 7.5f) - 5, 0);
                        }
                        if (hitname.Contains("way"))
                        {
                            delete = false;

                            Destroy(del);
                            toSpawn = toSpawnTiles[1];
                            spawnID = 1;
                            chosenTile.transform.position = new Vector3((tileSize * 13.5f) - 7, (tileSize * 6.4f) - 5, 0);
                        }
                        if (hitname.Contains("tower"))
                        {
                            delete = false;

                            Destroy(del);
                            toSpawn = toSpawnTiles[2];
                            spawnID = 2;
                            chosenTile.transform.position = new Vector3((tileSize * 13.5f) - 7, (tileSize * 5.3f) - 5, 0);
                        }
                    }
                    else if (hitname.Contains("menu"))
                    {

                        foreach (GameObject o in Object.FindObjectsOfType<GameObject>())
                        {
                            Destroy(o);
                        }
                        SceneManager.LoadScene("MenuScene", LoadSceneMode.Single);
                    }

                    else if (hitname.Contains("way"))
                    {
                        if (all == true)
                        {
                            for (int ys = 1; ys < 9; ys++)
                            {
                                for (int xs = 1; xs < 11; xs++)
                                {
                                    Destroy(tiles[ys, xs]);
                                    tiles[ys, xs] = Instantiate(toSpawn);
                                    tiles[ys, xs].name = "way;" + xs + ";" + ys;
                                    pole[ys, xs] = spawnID;
                                    counter++;
                                    //GameObject newTile = Instantiate(towerTile);
                                    tiles[ys, xs].transform.position = new Vector3((tileSize * xs) - 7, (tileSize * ys) - 5, 0);

                                }
                            }
                        }
                        string[] info = hitname.Split(';');
                        int x = int.Parse(info[1]);
                        int y = int.Parse(info[2]);
                        Debug.Log(x + "  " + y);

                        if (delete == true)
                        {
                            Destroy(tiles[y, x]);
                            pole[y, x] = -1;
                            tiles[y, x] = Instantiate(empty);
                            tiles[y, x].name = "way;" + x + ";" + y;
                            tiles[y, x].transform.position = new Vector3((tileSize * x) - 7, (tileSize * y) - 5, 0);
                        }
                        else
                        {
                            pole[y, x] = spawnID;
                            Destroy(tiles[y, x]);
                            tiles[y, x] = Instantiate(toSpawn);
                            tiles[y, x].name = "way;" + x + ";" + y;
                            tiles[y, x].transform.position = new Vector3((tileSize * x) - 7, (tileSize * y) - 5, 0);
                        }
                    }

                    else if (hitname.Contains("stone"))
                    {
                        toSpawnTiles = stoneTiles;
                        toSpawn = toSpawnTiles[spawnID];

                        Destroy(del);
                        delete = false;
                        Destroy(btn1);
                        Destroy(btn2);
                        Destroy(btn3);
                        leftBtns();
                        Destroy(chosenTopTile);
                        float x = -2.5f;
                        float y = 4.3f;
                        chosenTopTile = Instantiate(chosen);
                        chosenTopTile.transform.position = new Vector3(x, (y), 0);
                        change();

                    }
                    else if (hitname.Contains("ice"))
                    {
                        toSpawnTiles = iceTiles;
                        toSpawn = toSpawnTiles[spawnID];
                        delete = false;

                        Destroy(del);
                        Destroy(btn1);
                        Destroy(btn2);
                        Destroy(btn3);
                        leftBtns();
                        Destroy(chosenTopTile);
                        float x = -5.5f;
                        float y = 4.3f;
                        chosenTopTile = Instantiate(chosen);
                        chosenTopTile.transform.position = new Vector3(x, (y), 0);
                        change();
                    }
                    else if (hitname.Contains("sand"))
                    {
                        toSpawnTiles = sandTiles;
                        toSpawn = toSpawnTiles[spawnID];
                        delete = false;

                        Destroy(del);
                        Destroy(btn1);
                        Destroy(btn2);
                        Destroy(btn3);
                        leftBtns();
                        Destroy(chosenTopTile);
                        float x = -4f;
                        float y = 4.3f;
                        chosenTopTile = Instantiate(chosen);
                        chosenTopTile.transform.position = new Vector3(x, (y), 0);
                        change();
                    }
                    else if (hitname.Contains("snow"))
                    {
                        toSpawnTiles = snowTiles;
                        toSpawn = toSpawnTiles[spawnID];
                        delete = false;

                        Destroy(del);
                        Destroy(btn1);
                        Destroy(btn2);
                        Destroy(btn3);
                        leftBtns();
                        Destroy(chosenTopTile);
                        float x = -1f;
                        float y = 4.3f;
                        chosenTopTile = Instantiate(chosen);
                        chosenTopTile.transform.position = new Vector3(x, (y), 0);
                        change();
                    }
                    else if (hitname.Contains("grass"))
                    {
                        toSpawnTiles = grassTiles;
                        toSpawn = toSpawnTiles[spawnID];
                        delete = false;
                        Destroy(del);
                        Destroy(btn1);
                        Destroy(btn2);
                        Destroy(btn3);
                        leftBtns();
                        Destroy(chosenTopTile);
                        float x = 0.5f;
                        float y = 4.3f;
                        chosenTopTile = Instantiate(chosen);
                        chosenTopTile.transform.position = new Vector3(x, (y), 0);
                        change();
                    }
                    if (hitname.Contains("delete"))
                    {
                        all = false;
                       
                        if (delete == true)
                        {
                            delete = false;
                            Destroy(del);
                        }
                        else
                        {
                            float x = 2f;
                            float y = 4.3f;
                            del = Instantiate(chosen);
                            del.transform.position = new Vector3(x, (y), 0);
                            delete = true;
                        }
                        if (chosenBool)
                        {
                            Destroy(chosenTile);
                        }
                        chosenBool = false;
                        Destroy(bomb);

                    }
                    else if (hitname.Contains("all"))
                    {
                        Destroy(del);
                        delete = false;
                        if (all == false)
                        {  
                            float x = 3.2f;
                            float y = 4.3f;
                            bomb = Instantiate(chosen);
                            bomb.transform.position = new Vector3(x, (y), 0);
                            all = true;
                        }
                        else
                        {
                            Destroy(bomb);
                            all = false;
                        }

                    }
                    else if (hitname.Contains("save"))
                    {
                        string toWrite = "";
                        for (int ys = 1; ys < 9; ys++)
                        {
                            
                            for (int xs = 1; xs < 11; xs++)
                            {
                                Destroy(tiles[ys, xs]);
                                toWrite += pole[ys, xs];
                                toWrite += ";"; 
                            }
                            toWrite += "!";
                        }
                        Debug.Log(toWrite);
                        File.AppendAllText("text.txt", toWrite +"&");

                        LevelManager.readText += toWrite + "&";
                        Start();
                    }

                }
            }
        }
    }
}

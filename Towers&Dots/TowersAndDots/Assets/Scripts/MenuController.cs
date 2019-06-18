using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0))
        { // << use GetMouseButton instead of GetMouseButtonDown
            if (Input.GetMouseButtonDown(0))
            {

                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

                RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
                if (hit.collider != null)
                {
                    Debug.Log(hit.collider.gameObject.name);
                    string hitname = hit.collider.gameObject.name;
                  
                        if (hitname.Contains("map"))
                        {
                        foreach (GameObject o in Object.FindObjectsOfType<GameObject>())
                        {
                            Destroy(o);
                        }
                        SceneManager.LoadScene("mapMaker");
                        }
                        else if (hitname.Contains("hra"))
                        {
                        foreach (GameObject o in Object.FindObjectsOfType<GameObject>())
                        {
                            Destroy(o);
                        }
                        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
                        }
                        else if (hitname.Contains("exit"))
                        {
                         Application.Quit();
                    }
                }
                
            }
        }
    }
}

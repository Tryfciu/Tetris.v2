using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBlockSpawner : MonoBehaviour {

    GameObject prefab;
    public bool isMainBlockAlive = false;
    public bool[,] blocksArray = new bool[9, 12];

	// Use this for initialization
	void Start () {
        prefab = (GameObject)Resources.Load("MainBlock");

    }
	
	// Update is called once per frame
	void Update () {
        if (blocksArray[4, 11] == true)
        {
            Application.Quit();
            //GetComponent<MainBlockSpawner>().enabled = false;
        }
        else if (isMainBlockAlive == false)
        {
            Instantiate(prefab);
            isMainBlockAlive = true;
        }
	}
}

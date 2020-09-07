using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLibrary : MonoBehaviour
{

    public GameObject flappyBirdsObject;
    public GameObject rowGame;
    public GameObject boardGame;
    public GameObject spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateGame(GameObject game)
    {
        Instantiate(game, spawnPoint.transform.position, Quaternion.identity);
    }
}

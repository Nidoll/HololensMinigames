using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerGameManager : MonoBehaviour
{
    // 0 - Cube, 1 - Piramyd, 2 - Sphere
    public GameObject[] spawners = new GameObject[3];
    public GameObject[] spawnerPoints = new GameObject[3];
    public EnemySpawner[] enemySpawner = new EnemySpawner[3];
    public List<int> positions = new List<int>(); 
    public float timeSurvived;
    public int lives;
    
    // Wave Variables
    public int startShots;
    public float startPause;
    public float startShotDelay;
    public int numberOfShots;
    public float shotDelay;
    public float currentPause;
    public int shotsIncrease;
    public float shotDelayDecrease;
    public float pauseDecrease;

    void Start()
    {
        numberOfShots = startShots;
        currentPause = startPause;
        shotDelay = startShotDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.J)){
            startGame();
        }   
        if(Input.GetKeyDown(KeyCode.K)){
            restartGame();
        }
    }

    public void startGame()
    {
        StopAllCoroutines();
        StartCoroutine(spawnWaves());
    }

    public void restartGame()
    {
        StopAllCoroutines();
        numberOfShots = startShots;
        currentPause = startPause;
        shotDelay = startShotDelay;
        StartCoroutine(spawnWaves());
    }

    public void placeSpawnerRandom()
    {
        for(int i = 0; i < 3; i++){
            positions.Add(i);
        }

        for(int i = 0; i < 3; i++){
            int randomNumber = (int)Random.Range(1,4-i);
            if(randomNumber == 4-i){
                randomNumber -= 1;
            }

            spawners[i].transform.position = spawnerPoints[positions[randomNumber-1]].transform.position;
            positions.RemoveAt(randomNumber-1);
        }

        positions.Clear();
    }

    public void doRandomShot()
    {
        int randomNumber = (int) Random.Range(1,4);
        if(randomNumber == 4){
            randomNumber = 3;
        }
        spawners[randomNumber-1].GetComponent<EnemySpawner>().fire();
    }

    IEnumerator spawnWaves()
    {
        while(true){
            currentPause -= pauseDecrease;
            for(int i = 0; i < numberOfShots; i++){
                doRandomShot();
                yield return new WaitForSeconds(shotDelay);
            }
            shotDelay -= shotDelayDecrease;
            numberOfShots += shotsIncrease;
            placeSpawnerRandom();
            yield return new WaitForSeconds(currentPause);
        }
    }
}

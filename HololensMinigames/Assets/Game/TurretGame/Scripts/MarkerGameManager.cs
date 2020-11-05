using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

public class MarkerGameManager : MonoBehaviour
{
    // 0 - Cube, 1 - Piramyd, 2 - Sphere
    public GameObject[] spawners = new GameObject[3];
    public GameObject[] spawnerPoints = new GameObject[3];
    public EnemySpawner[] enemySpawner = new EnemySpawner[3];
    public GameObject[] turrets = new GameObject[3];
    public GameObject[] turretPoints = new GameObject[3];
    public List<int> positions = new List<int>(); 
    public float timeSurvived;
    public GameObject marker;
    
    // Wave Variables
    public int startShots;
    public float startPause;
    public float startShotDelay;
    private int numberOfShots;
    private float shotDelay;
    private float currentPause;
    public int shotsIncrease;
    public float shotDelayDecrease;
    public float pauseDecrease;
    public float shotDelayMin;
    public float pauseMin;
    public int shotMax;

    //Health
    public int lives;
    public GameObject[] heartObjects = new GameObject[3];

    // Projectiles
    public GameObject projectileParent;

    // Buttons
    public GameObject playButton;
    public GameObject stopButton;

    public bool gameRunning;

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
        if(Input.GetKeyDown(KeyCode.N)){
            switchPosition(true);
        }
        if(Input.GetKeyDown(KeyCode.M)){
            switchPosition(false);
        }

        if(Input.GetKeyDown(KeyCode.U)){
            turrets[0].GetComponent<Turret>().fire();
        }
        if(Input.GetKeyDown(KeyCode.I)){
            turrets[1].GetComponent<Turret>().fire();
        }
         if(Input.GetKeyDown(KeyCode.O)){
            turrets[2].GetComponent<Turret>().fire();
        }
        
    }

    public void startGame()
    {
        StopAllCoroutines();
        StartCoroutine(spawnWaves());
        GetComponent<ObjectManipulator>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        switchButtons();
        enableHearts();
        switchButtons();
        lives = 3;
        gameRunning = true;
    }

    private void switchButtons()
    {
        playButton.SetActive(!playButton.activeSelf);
        stopButton.SetActive(!stopButton.activeSelf);
    }

    public void stopGame()
    {
        StopAllCoroutines();
        deleteProjectiles();
        GetComponent<ObjectManipulator>().enabled = true;
        GetComponent<BoxCollider>().enabled = true;
        switchButtons();
        gameRunning = false;
    }

    private void enableHearts()
    {
        foreach(GameObject obj in heartObjects){
            obj.SetActive(true);
        }
    }

    public void restartGame()
    {
        if(!gameRunning){
            startGame();
        }else{
            StopAllCoroutines();
            numberOfShots = startShots;
            currentPause = startPause;
            shotDelay = startShotDelay;
            lives = 3;
            deleteProjectiles();
            enableHearts(); 
            StartCoroutine(spawnWaves());
        }
    }

    public void placeBoard()
    {
        if(transform.parent != null){
            transform.SetParent(null);
        }else{
            transform.SetParent(marker.transform);
        }
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
            if(currentPause <= pauseMin){
                currentPause = pauseMin;
            }
            for(int i = 0; i < numberOfShots; i++){
                doRandomShot();
                yield return new WaitForSeconds(shotDelay);
            }
            shotDelay -= shotDelayDecrease;
            if(shotDelay <= shotDelayMin){
                shotDelay = shotDelayMin;
            }
            numberOfShots += shotsIncrease;
            if(numberOfShots >= shotMax){
                numberOfShots = shotMax;
            }
            Coroutine shuffleRoutine = StartCoroutine(shuffleSpawner());
            yield return new WaitForSeconds(currentPause/2);
            StopCoroutine(shuffleRoutine);
            yield return new WaitForSeconds(currentPause/2);
        }
    }

    IEnumerator shuffleSpawner()
    {
        while(true){
            placeSpawnerRandom();
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void switchPosition(bool leftButton)
    {
        GameObject turret;
        if(leftButton){
            turrets[0].transform.position = turretPoints[1].transform.position;
            turrets[1].transform.position = turretPoints[0].transform.position;
            turret = turrets[0];
            turrets[0] = turrets[1];
            turrets[1] = turret;
        }else{
            turrets[1].transform.position = turretPoints[2].transform.position;
            turrets[2].transform.position = turretPoints[1].transform.position;
            turret = turrets[1];
            turrets[1] = turrets[2];
            turrets[2] = turret;
        }
    }

    public void loseHP()
    {
        lives -= 1;
        heartObjects[lives].SetActive(false);
        if(lives == 0){
            gameOver();
        }
    }

    public void gameOver()
    {
        StopAllCoroutines();
        deleteProjectiles();
    }

    private void deleteProjectiles()
    {
        foreach(Transform child in projectileParent.transform){
            Destroy(child.gameObject);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FlappyBirds : MonoBehaviour, InterfacePlayable
{

    public Transform parentTransform;
    public GameObject gameArea;
    public GameObject playButoon;
    public GameObject deletButton;
    public GameObject pipeStartTop;
    public GameObject pipeStartBot;
    public GameObject pipeEnd;
    public List<Pipe> pipes;
    public GameObject pipe;
    public GameObject dragon;
    public float emptySize;
    public float topSize;
    public float botSize;
    public float emptyOffset;
    public float score = 0;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameAreaText;
    public GameObject restartButton;

    public GameObject cloudTop;

    public GameObject cloudBot;

    public int time = 4;


    private float emptySizeHolder;

    public float scaleFactor;

    public ScoreManager scoreManager;
    public TimerManager timerManager;

    void Awake()
    {
        emptySizeHolder = emptySize;

        parentTransform = transform.parent.transform;
        scaleFactor = parentTransform.localScale.y;
        time = 4;
        emptySize = emptySizeHolder * scaleFactor;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.O)){
            startGame();
        }

        if(Input.GetKeyDown(KeyCode.R)){
            restartGame();
        }
    }

    public void startGame()
    {
        parentTransform = transform.parent.transform;
        scaleFactor = parentTransform.localScale.y;
        emptySize = emptySizeHolder * scaleFactor;

        gameArea.SetActive(false);
        playButoon.SetActive(false);
        dragon.SetActive(true);
        //dragon.GetComponent<DragonController>().enableGravity = true;
        InvokeRepeating("spawnPipes", 3f, 2f);
        InvokeRepeating("startTimer", 0f, 1f);

    }

    public void startTimer()
    {
        time--;
        timerManager.setTimer(time);
        //gameAreaText.text = time.ToString();

        if(time == 0){
            //gameAreaText.text = "";
            dragon.GetComponent<DragonController>().enableGravity = true;
            CancelInvoke("startTimer");
        }

    }

    public void spawnPipes()
    {
        generateOffset();

        GameObject newPipe = Instantiate(pipe, pipeStartTop.transform.position, Quaternion.identity) as GameObject;
        Pipe pipeScript = newPipe.GetComponent<Pipe>();

        newPipe.transform.parent = transform;

        pipeScript.top = true;
        pipeScript.setSize(topSize, parentTransform.localScale.y);

        pipes.Add(pipeScript);

        newPipe = Instantiate(pipe, pipeStartBot.transform.position, Quaternion.identity) as GameObject;
        pipeScript = newPipe.GetComponent<Pipe>();

        newPipe.transform.parent = transform;

        pipeScript.top = false;
        pipeScript.setSize(botSize, parentTransform.localScale.y);

        pipes.Add(pipeScript);
        
    }

    public void generateOffset()
    {
        emptySize = Random.Range(0.45f, 0.6f) * scaleFactor;

        emptyOffset = Random.Range(-0.5f * scaleFactor, 0.5f * scaleFactor);

        topSize = (1.5f * scaleFactor - emptySize - emptyOffset);
        botSize = (1.5f * scaleFactor - emptySize + emptyOffset);
    }

    public void stopGame()
    {
        CancelInvoke();

        foreach(Pipe pipe in pipes){
            if(pipe != null){
                pipe.move = false;
            }
        }

        restartButton.SetActive(true);
    }

    public void restartGame()
    {
        parentTransform = transform.parent.transform;
        scaleFactor = parentTransform.localScale.y;
        emptySize = emptySizeHolder * scaleFactor;


        time = 4;
        restartButton.SetActive(false);
        score = 0;
        setScore();
        //scoreText.text = "Score:";

        foreach(Pipe pipe in pipes){
            if(pipe != null){
                GameObject.Destroy(pipe.gameObject);
            }
        }

        pipes.Clear();

        dragon.transform.localPosition = new Vector3(-0.815f, 0,0);

        InvokeRepeating("spawnPipes", 3f, 2f);
        InvokeRepeating("startTimer", 0f, 1f);

    }

    public void setScore()
    {
        score += 0.5f;
        //scoreText.text = "Score: " + score;
        scoreManager.UpdateScore((int)score);
    
    }
}

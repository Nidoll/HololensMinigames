using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour
{

    public Vector3 cursorPositionW;
    public Vector3 cursorPositionL;

    private float size = 0.25f;
    public float xPercent;
    public float zPercent;

    public Vector3 newRotation;
    private float xRotation;
    private float zRotation;
    private float maxRotation = 30f;

    public WallGenerator wallGenerator;
    public GameObject ball;
    private Vector3 ballStartPosition;
    private bool gameStarted;

    // Start is called before the first frame update
    void Start()
    {
        ballStartPosition = ball.transform.localPosition;
        wallGenerator.GenerateNodes();
    }

    // Update is called once per frame
    void Update()
    {
       if(Input.GetKeyDown(KeyCode.U)){
           StartGame();
       } 

       if(Input.GetKeyDown(KeyCode.I)){
           RestartGame();
       }
    }

    void FixedUpdate()
    {
        if(gameStarted){
            RaycastHit hitInfo;
            if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hitInfo, 15f, LayerMask.GetMask("Game"))){
                if(hitInfo.collider.tag == "Board"){
                    cursorPositionW = hitInfo.point;
                    cursorPositionL = transform.InverseTransformPoint(cursorPositionW); 

                    zPercent = (-1) * (cursorPositionL.x / size);
                    xPercent = (cursorPositionL.z / size);

                    zRotation = zPercent * maxRotation;
                    xRotation = xPercent * maxRotation;
                    newRotation = new Vector3(xRotation, transform.eulerAngles.y, zRotation);

                    transform.eulerAngles = newRotation;
                }else{
                    ResetBoard();
                }
            }  
        }      
    }

    void ResetBoard()
    {
        transform.eulerAngles = new Vector3(0,transform.eulerAngles.y,0);
    }

    void ResetBall()
    {
        ball.transform.localPosition = ballStartPosition;
    }

    public void StartGame()
    {
        wallGenerator.TraverseNodes();
        gameStarted = true;
    }

    public void RestartGame()
    {
        ResetBall();
        wallGenerator.ResetWalls();
        wallGenerator.TraverseNodes();
    }

    public void Win()
    {
        RestartGame();
    }

}

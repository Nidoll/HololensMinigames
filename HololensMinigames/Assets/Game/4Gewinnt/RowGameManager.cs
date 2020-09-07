using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RowGameManager : MonoBehaviour
{
    // 0 empty, 1 red(x), 2 yellow(o)
    public int[,] gameBoard = new int[7,7];
    public GameObject[] stoneSpawns = new GameObject[7];
    public bool gamePaused = false;

    public int smartTurn = -1;
    public GameObject winTexts;
    public GameObject loseTexts;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 7; i++){
            gameBoard[i,6] = 3;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P)){
            printBoard();
        }

        if(Input.GetKeyDown(KeyCode.Alpha1)){
            placeStone(1, true);
        }else if(Input.GetKeyDown(KeyCode.Alpha2)){
            placeStone(2, true);
        }else if(Input.GetKeyDown(KeyCode.Alpha3)){
            placeStone(3, true);
        }else if(Input.GetKeyDown(KeyCode.Alpha4)){
            placeStone(4, true);
        }else if(Input.GetKeyDown(KeyCode.Alpha5)){
            placeStone(5, true);
        }else if(Input.GetKeyDown(KeyCode.Alpha6)){
            placeStone(6, true);
        }else if(Input.GetKeyDown(KeyCode.Alpha7)){
            placeStone(7, true);
        }else if(Input.GetKeyDown(KeyCode.R)){
            resetGame();
        }
    }

    void printBoard()
    {
        string boardString = "";
        for(int y = 0; y < 6; y++){
            for(int x = 0; x < 7; x++){
                switch(gameBoard[x, y]){
                    case 1:
                        boardString += "x ";
                        break;
                    case 2:
                        boardString += "o ";
                        break;
                    case 0:
                        boardString += "? ";
                        break;
                }        
            }
            boardString += "\n";
        }
        Debug.Log(boardString);
    }

    public bool placeStone(int position, bool redStone)
    {
        if(!gamePaused){
            bool success = stoneSpawns[position - 1].GetComponent<ColumSpawnS>().placeNewStone(redStone);
            int arrayPositionY = 6 - stoneSpawns[position - 1].GetComponent<ColumSpawnS>().stoneAmount;
        
            if(redStone && success){
                gameBoard[position - 1, arrayPositionY] = 1;
                checkForWin();
                StartCoroutine(kiTurnafterDelay());
            }else if(!redStone && success){
                gameBoard[position - 1, arrayPositionY] = 2;
                checkForWin();
            }
            
            return success;
        }else{
            return false;
        }

               
    }

    IEnumerator kiTurnafterDelay()
    {
        yield return new WaitForSeconds(0.5f);

        kiTurn();
    }

    public void kiTurn()
    {
        if(!gamePaused){
            if(smartTurn == -1){
                bool success = false;
                int cancel = 0;

                 do{
                    if(cancel > 100){
                        break;
                    }
                
                    cancel++;
                    int randomNumber = (int) UnityEngine.Random.Range(1, 8);
                    success = placeStone(randomNumber, false);
                }while(success == false);
            }else{
                bool succes = placeStone(smartTurn+1, false);
                smartTurn = -1;
                if(!succes){
                    StartCoroutine(kiTurnafterDelay());
                }
            }
        }else{
            
        }
    
    }

    public void checkForWin()
    {
        int count = 0;

        //horizontal
        for(int y = 0; y < 6; y++){
            for(int x = 0; x < 6; x++){
                if(gameBoard[x,y] != 0 && gameBoard[x,y] == gameBoard[x+1,y]){
                    count++;
                }else{
                    count = 0;
                }

                if(count >= 1){
                    try{
                        if((gameBoard[x-1,y] == 0 && gameBoard[x-1,y+1] != 0) && (gameBoard[x+2,y] == 0 && gameBoard[x+2,y+1] != 0)){
                            smartTurn = x-1;
                        }
                    }catch(Exception e){

                    }

                    try{
                        if(gameBoard[x+2,y] == 0 && gameBoard[x+3,y] == 1 && gameBoard[x+2,y+1] != 0){
                            smartTurn = x+2;
                        }
                    }catch(Exception e){

                    }

                    try{
                        if(gameBoard[x-1,y] == 0 && gameBoard[x-2,y] == 1 && gameBoard[x-1,y+1] != 0){
                            smartTurn = x-1;
                        }
                    }catch(Exception e){

                    }
                }

                if(count >= 2){
                    try{
                        if(gameBoard[x-2,y] == 0 && gameBoard[x-2,y+1] != 0){
                            smartTurn = x-2;
                        }
                    }catch(Exception e){
                    }

                    try{
                        if(gameBoard[x+2,y] == 0 && gameBoard[x+2,y+1] != 0){
                            smartTurn = x+2;
                        }
                    }catch(Exception e){

                    }

                    
                }

                if(count >= 3){
                    if(gameBoard[x,y] == 1){
                        winMessage(true);
                    }else{
                        winMessage(false);
                    }
                }
            }
            count = 0;
        }

        count = 0;

        // vertical
        for(int x = 0; x < 7; x++){
                for(int y = 0; y < 5; y++){
                    if(gameBoard[x,y] != 0 && gameBoard[x,y] == gameBoard[x,y+1]){
                        count++;
                    }else{
                        count = 0;
                    }

                    if(count >= 2){
                        try{
                            if(gameBoard[x,y-2] == 0){
                                smartTurn = x;
                            }
                        }catch(Exception e){
                        }
                    
                    }

                    if(count >= 3){
                        if(gameBoard[x,y] == 1){
                            winMessage(true);
                        }else{
                            winMessage(false);
                        }
                    }
                }
                count = 0;
        }

        count = 0;
        
        //diagonal TL->BR
        int yStart = 3;
        int xStart = 0;
        int[] xMaxA = {2,3,4,5,5,5};
        int[] xStartA = {0,0,0,1,2,3};
        for(int i = 0; i < 6; i++){
            if(i < 3){
                yStart--;
            }
            int  y = yStart;
            int xMax = xMaxA[i];
            xStart = xStartA[i];
            
            for(int x = xStart; x <= xMax; x++, y++){
                if(gameBoard[x,y] != 0 && gameBoard[x,y] == gameBoard[x+1,y+1]){
                    count++;
                }else{
                    count = 0;
                }

                if(count >= 2){
                    try{
                       if(gameBoard[x+2,y+2] == 0 && gameBoard[x+2,y+3] != 0){
                           smartTurn = x+2;
                       }
                    }catch(Exception e){
                    }

                    try{
                        if(gameBoard[x-2,y-2] == 0 && gameBoard[x-2,y-1] != 0){
                           smartTurn = x-2;
                       } 
                    }catch(Exception e){

                    }
                }

                if(count >= 3){
                    if(gameBoard[x,y] == 1){
                        winMessage(true);
                    }else{
                        winMessage(false);
                    }
                }
            }
            count = 0;
        }
        
        //diagonal TR->BL
        yStart = 3;
        xStart = 0;
        int[] xMinA = {4, 3, 2, 1, 1, 1};
        xStartA = new int[] {6, 6, 6, 5, 4, 3};

        for(int i = 0; i < 6;i++){
            if(i < 3){
                yStart--;
            }

            int y = yStart;
            int xMin = xMinA[i];
            xStart = xStartA[i];

            for(int x = xStart; x >= xMin; x--, y++){
                if(gameBoard[x,y] != 0 && gameBoard[x,y] == gameBoard[x-1,y+1]){
                    count++;
                }else{
                    count = 0;
                }

                if(count >= 2){
                    try{
                       if(gameBoard[x+2,y-2] == 0 && gameBoard[x+2,y-1] != 0){
                           smartTurn = x+2;
                       }
                       
                    }catch(Exception e){
                    }

                    try{
                        if(gameBoard[x-2,y+2] == 0 && gameBoard[x-2,y+3] != 0){
                           smartTurn = x-2;
                       } 
                    }catch(Exception e){

                    }
                }

                if(count >= 3){
                    if(gameBoard[x,y] == 1){
                        winMessage(true);
                    }else{
                        winMessage(false);
                    }
                }
            }
            count = 0;
        }
        
        count = 0;

    }

    public void winMessage(bool redStone)
    {
        gamePaused = true;
        if(redStone){
            winTexts.SetActive(true);
        }else{
            loseTexts.SetActive(true);
        }
    }

    public void resetGame()
    {
        for(int y = 0; y < 6; y++){
            for(int x = 0;x < 7; x++){
                gameBoard[x,y] = 0;
            }
        }

        for(int i = 0; i < 7; i++){
            stoneSpawns[i].GetComponent<ColumSpawnS>().deleteStones();
        }

        winTexts.SetActive(false);
        loseTexts.SetActive(false);
        gamePaused = false;
    }
}

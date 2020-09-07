using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public GameObject activeFirstDigit;
    public GameObject activeSecondDigit;
    public GameObject[] firstDigits;
    public GameObject[] secondDigits;

    public int firstDigit;
    public int secondDigit;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore(int score)
    {
        firstDigit = score / 10;
        secondDigit = score % 10;

        if(firstDigits[firstDigit] == activeFirstDigit){

        }else{
            activeFirstDigit.SetActive(false);
            activeFirstDigit = firstDigits[firstDigit];
            activeFirstDigit.SetActive(true);
        }

        if(secondDigits[secondDigit] == activeSecondDigit){

        }else{
            activeSecondDigit.SetActive(false);
            activeSecondDigit = secondDigits[secondDigit];
            activeSecondDigit.SetActive(true);
        }

    }
}

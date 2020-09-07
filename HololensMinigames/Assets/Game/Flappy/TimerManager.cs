using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{

    public GameObject[] digits;
    public GameObject activeDigit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setTimer(int digit)
    {
        if(digit == 0){
            activeDigit.SetActive(false);
            activeDigit = null;
            return;
        }

        if(activeDigit == null){
            activeDigit = digits[digit - 1];
            activeDigit.SetActive(true);    
        }else{
            activeDigit.SetActive(false);
            activeDigit = digits[digit - 1];
            activeDigit.SetActive(true);
        }

    }
}

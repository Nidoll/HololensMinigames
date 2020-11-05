using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireButton : MonoBehaviour
{
    public GameObject buttonButton;
    private Animator ani;
    public MarkerGameManager gm;
    public int turretNumber; 
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R)){
          buttonPressed();  
        }  
    }

    public void buttonPressed()
    {
        ani.SetTrigger("Pressed");
        gm.turrets[turretNumber].GetComponent<Turret>().fire();
    }

    public void buttonReleased()
    {
        ani.SetTrigger("Released");   
    }
}

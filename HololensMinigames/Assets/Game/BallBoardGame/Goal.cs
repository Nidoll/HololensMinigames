using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{

    public BoardController boardController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider coll)
    {   
        if(coll.tag == "Player"){
            boardController.Win();
            Debug.Log("coll");
        }
    }
}

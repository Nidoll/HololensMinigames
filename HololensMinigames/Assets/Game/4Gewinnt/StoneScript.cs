using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneScript : MonoBehaviour
{

    public float stopPosition;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if(transform.localPosition.y > stopPosition){
            transform.localPosition = new Vector3(0, transform.localPosition.y - speed * Time.fixedDeltaTime, 0);
        }else{
            transform.localPosition = new Vector3(0,stopPosition,0);
        }
    }
}

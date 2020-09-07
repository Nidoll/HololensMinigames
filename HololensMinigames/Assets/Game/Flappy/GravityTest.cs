using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityTest : MonoBehaviour
{

    Rigidbody rig;
    public float force;
    public float jumpForce;
    private bool start = false;
    private bool jump = false;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.O)){
            start = true;
        }

        if(Input.GetKeyDown(KeyCode.I)){
            jump = true;
        }
    }

    void FixedUpdate()
    {
        if(start){
            rig.AddForce(Vector3.down * force, ForceMode.Acceleration);
        }

        if(jump){
            rig.AddForce(Vector3.up * jumpForce, ForceMode.Acceleration);
            jump = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonController : MonoBehaviour
{

    private Transform modelTransform;
    private Rigidbody rig;
    public float jumpForce;
    public float gravityForce;
    private bool jump = false;
    public bool enableGravity = false;
    public FlappyBirds flappyBirds;

    private float gravityForceHolder;
    private float jumpForceHolder;

    // Start is called before the first frame update
    void Start()
    {
        gravityForceHolder = gravityForce;
        jumpForceHolder = jumpForce;

        rig = GetComponent<Rigidbody>();
        flappyBirds = transform.parent.GetComponent<FlappyBirds>();
        jumpForce = jumpForceHolder * flappyBirds.parentTransform.localScale.y;
        gravityForce = gravityForceHolder * flappyBirds.parentTransform.localScale.y;

        modelTransform = GetComponentInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        jumpForce = jumpForceHolder * flappyBirds.parentTransform.localScale.y;
        gravityForce = gravityForceHolder * flappyBirds.parentTransform.localScale.y;

        if(Input.GetKeyDown(KeyCode.Space)){
            jump = true;
        }

        if(rig.velocity.y < 0){
            modelTransform.localEulerAngles = new Vector3(30, 90, 0);
        }else if(rig.velocity.y > 0){
            modelTransform.localEulerAngles = new Vector3(-30, 90, 0);
        }else if(rig.velocity.y == 0){
            modelTransform.localEulerAngles = new Vector3(0, 90, 0);
        }

    }

    public void jumpAction()
    {
        jump = true;
    }

    void FixedUpdate()
    {
        if(jump && enableGravity){
            rig.velocity = Vector3.zero;
            rig.AddForce(Vector3.up * jumpForce, ForceMode.Acceleration);
            jump = false; 
        }

        if(enableGravity){
            rig.AddForce(Vector3.down * gravityForce, ForceMode.Acceleration);
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        if(coll.tag == "GameOver"){
            enableGravity = false;
            rig.velocity = Vector3.zero;
            transform.parent.GetComponent<FlappyBirds>().stopGame();
        }
    }
}

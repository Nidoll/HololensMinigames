using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    public bool top;
    public float speed;
    public Rigidbody rig;
    public Vector3 pipeEnd;
    public bool move = true;
    public GameObject pipeTopPart;
    public GameObject pipeTopObject;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody>();
        if(top){
            transform.localEulerAngles = new Vector3(-90, 0, 0);
        }else{
            transform.localEulerAngles = new Vector3(90, 0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if(move){
            rig.MovePosition(transform.position - transform.right * speed * Time.fixedDeltaTime);
        }

    }

    public void setSize(float size, float scale)
    {
        speed = speed * scale;
        size = size / scale;
        transform.localScale = new Vector3(0.3f, 0.3f, size);

        if(top){
           transform.position = new Vector3(transform.position.x, transform.position.y - size / 2f * scale, transform.position.z); 
        }else{
            transform.position = new Vector3(transform.position.x, transform.position.y + size / 2f * scale, transform.position.z);
        }

        pipeTopPart.transform.localScale = new Vector3(1f, 1f, 1f / transform.localScale.z / 2f);
        pipeTopPart.transform.localPosition = new Vector3(0f, 0f, - 1f * transform.localScale.z * pipeTopPart.transform.localScale.z);

    }

    void OnTriggerEnter(Collider coll)
    {
        if(coll.tag == "pipeEnd"){
            GameObject.Destroy(transform.gameObject);
        }
    }

}

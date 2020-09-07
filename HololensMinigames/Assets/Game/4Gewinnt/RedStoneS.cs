using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedStoneS : MonoBehaviour
{
    Rigidbody rig;
    bool enableGravity = false;
    public float force;
    public GameObject[] stoneSpawns = new GameObject[7];
    public float[] distanceToSpawns = new float[7]; 
    public float placeDistanceHolder = 0.1f;
    public float placeDistance;


    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody>();
        force *= transform.localScale.y;
        placeDistance = placeDistanceHolder * transform.localScale.y;                 
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.G)){
            turnOnGravity();
        }

        if(Input.GetKeyDown(KeyCode.P)){
            loseParent();
        }

        if(Input.GetKeyDown(KeyCode.D)){
            placeStone();
        }
    }

    void FixedUpdate()
    {
        if(enableGravity){
            rig.AddForce(Vector3.down, ForceMode.Acceleration);
        }
    }

    public void turnOnGravity()
    {
        enableGravity = true;
    }

    public void placeStone()
    {

        int closest = 0;

        for(int i = 0; i < 7; i++){
            distanceToSpawns[i] = Vector3.Distance(transform.position, stoneSpawns[i].transform.position);
            if(i > 0){
                if(distanceToSpawns[i] < distanceToSpawns[i - 1]){
                    closest = i;
                }
            }
        }

        if(distanceToSpawns[closest] <= placeDistance){
            if(stoneSpawns[closest].transform.parent.GetComponent<RowGameManager>().placeStone(closest + 1, true)){
                Destroy(gameObject);
            }
        }
        
    }

    public void loseParent()
    {
        transform.SetParent(null);
    }

}

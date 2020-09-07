using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumSpawnS : MonoBehaviour
{
    public GameObject simpleRedStone;
    public GameObject simpleYellowStone;
    public int stoneAmount = 0;
    public float[] stopPoints = new float[6];
    public GameObject activeStone;
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
        
    }

    public bool placeNewStone(bool redStone)
    {
        if(stoneAmount < 6){

            stoneAmount++;
            if(redStone){
                activeStone = Instantiate(simpleRedStone, transform.position, Quaternion.identity);
                activeStone.transform.parent = gameObject.transform;
                activeStone.transform.localRotation = Quaternion.identity;
                activeStone.transform.Rotate(new Vector3(90f, 0f, 0f), Space.Self);
                activeStone.transform.localScale = new Vector3(1, 1, 1);
                activeStone.GetComponent<StoneScript>().speed = speed;
                activeStone.GetComponent<StoneScript>().stopPosition = stopPoints[stoneAmount - 1];
            }else{
                activeStone = Instantiate(simpleYellowStone, transform.position, Quaternion.identity);
                activeStone.transform.parent = gameObject.transform;
                activeStone.transform.localRotation = Quaternion.identity;
                activeStone.transform.Rotate(new Vector3(90f, 0f, 0f), Space.Self);
                activeStone.transform.localScale = new Vector3(1, 1, 1);
                activeStone.GetComponent<StoneScript>().speed = speed;
                activeStone.GetComponent<StoneScript>().stopPosition = stopPoints[stoneAmount - 1];
            }

            return true;
        }else{
            return false;
        }
        
    }

    

    public void deleteStones()
    {
        foreach(Transform child in transform){
            Destroy(child.gameObject);
        }

        stoneAmount = 0;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneDropper : MonoBehaviour
{

    public GameObject redStonePrefab;
    public LinkedList<GameObject> stones = new LinkedList<GameObject>();
    public GameObject newStone;
    public float distanceToNewStone;
    public float disToRespawn;
    private float disToRespawnHolder = 0.1f;
    public float scale;
    public Vector3 scaleV;

    // Start is called before the first frame update
    void Start()
    {
       spawnRedStone();
    }

    // Update is called once per frame
    void Update()
    {
        scale = transform.parent.transform.localScale.y;
        distanceToNewStone = calcDisToNewStone();

        disToRespawn = disToRespawnHolder * transform.parent.transform.localScale.y;
        if(distanceToNewStone >= disToRespawn){
            spawnRedStone();
        }

    }

    void spawnRedStone()
    {
        newStone = Instantiate(redStonePrefab, gameObject.transform);
        newStone.transform.localScale.Set(1, 1, 1);
        newStone.transform.eulerAngles.Set(0, 0, 0);
        //newStone.transform.SetParent(gameObject.transform);
        newStone.GetComponent<RedStoneS>().stoneSpawns = transform.parent.GetComponent<RowGameManager>().stoneSpawns;
        stones.AddFirst(newStone);
    }

    float calcDisToNewStone()
    {
        return Vector3.Distance(transform.position, newStone.transform.position);
        
    }

}

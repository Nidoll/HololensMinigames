using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject projectile;
    public GameObject origin;
    private GameObject parent;


    void Start()
    {
        parent = GameObject.Find("Projectiles");
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q)){
            fire();
        }
    }

    public void fire()
    {
        GameObject projec = Instantiate(projectile, origin.transform.position, origin.transform.rotation, parent.transform);
        projec.transform.eulerAngles = new Vector3(0,projec.transform.eulerAngles.y,0);
    }
}

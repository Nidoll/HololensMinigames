using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

    public bool gameStart = false;
    bool shooting = false;
    public GameObject projectile;
    public GameObject projectileOriginO;
    public static float delay = 1f;
    private GameObject projec;

    // Start is called before the first frame update
    void Start()
    {
        gameStart = false; 
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)){
            fire();
        }
    }

    public void fire()
    {
        projec = Instantiate(projectile, projectileOriginO.transform.position, projectileOriginO.transform.rotation);
        projec.transform.eulerAngles = new Vector3(0, projec.transform.eulerAngles.y, 0);
    }
}

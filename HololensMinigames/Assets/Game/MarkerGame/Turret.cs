using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

    public bool gameStart = false;
    bool shooting = false;
    public GameObject projectile;
    public GameObject projectileOriginO;
    private Vector3 projectileOrigin;
    public static float delay = 1f;

    // Start is called before the first frame update
    void Start()
    {
        gameStart = false; 
    }

    // Update is called once per frame
    void Update()
    {
        if(gameStart && !shooting){
           StartCoroutine(shoot());
           shooting = true;
        }
        projectileOrigin = projectileOriginO.transform.position;
    }

    IEnumerator shoot()
    {
        while(true){
            Instantiate(projectile, projectileOrigin, projectileOriginO.transform.rotation);
            yield return new WaitForSeconds(delay);
        }
    }

    public void fire()
    {
        Instantiate(projectile, projectileOrigin, projectileOriginO.transform.localRotation);
    }
}

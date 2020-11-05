using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    private Rigidbody rig;
    public static float speed = 0.1f;
    public string type;
    private float rotation = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody>();
        StartCoroutine(die());
    }

    // Update is called once per frame
    void Update()
    {
        transform.GetChild(0).Rotate(new Vector3(rotation,rotation,rotation), Space.Self);
    }

    void FixedUpdate()
    {
        Vector3 newPosition = transform.position + transform.TransformVector(new Vector3(0,0,1)) * speed * Time.fixedDeltaTime;
        rig.MovePosition(newPosition);
    }

    IEnumerator die()
    {
        yield return new WaitForSeconds(15);
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider coll)
    {
        if(coll.tag == "Enemy"){
            if(coll.GetComponent<EnemyProjectile>().type.Equals(type)){
                Destroy(coll.gameObject);
                Destroy(gameObject);
            }else{
                Destroy(gameObject);
            }
        }else if(coll.tag == "Board"){
            transform.parent.parent.GetComponent<MarkerGameManager>().loseHP();
            GameObject.Destroy(gameObject);
        }
    }
}

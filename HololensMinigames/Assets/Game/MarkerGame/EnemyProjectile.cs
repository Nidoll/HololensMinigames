using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
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

    void Update()
    {
        transform.GetChild(0).Rotate(new Vector3(rotation,rotation,rotation), Space.Self);
    }

    void FixedUpdate()
    {
        rig.MovePosition(transform.position + transform.forward * Time.fixedDeltaTime * speed);
    }

    IEnumerator die()
    {
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }

}

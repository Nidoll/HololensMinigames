using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGravityManager : MonoBehaviour
{
    private float scale = 0;
    public float force = 3.14f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scale = transform.parent.transform.localScale.x;
        
    }

    void FixedUpdate()
    {
        GetComponent<Rigidbody>().AddForce(Vector3.down * force * scale, ForceMode.Acceleration);
    }
}

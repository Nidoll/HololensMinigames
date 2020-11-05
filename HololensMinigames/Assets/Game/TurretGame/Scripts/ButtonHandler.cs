using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class ButtonHandler : MonoBehaviour
{

    public Turret turret;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<VirtualButtonBehaviour>().RegisterOnButtonPressed(OnButtonPressed);
        gameObject.GetComponent<VirtualButtonBehaviour>().RegisterOnButtonReleased(OnButtonReleased);
    }

    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        turret.fire();
    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {

    }
}

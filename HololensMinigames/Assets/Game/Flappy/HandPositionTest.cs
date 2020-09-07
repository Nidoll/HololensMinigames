using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Tools;
using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality;

public class HandPositionTest : MonoBehaviour
{

    public Vector3 handPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSourceDetected(SourceStateEventData eventData)
    {
        var hand = eventData.Controller as IMixedRealityHand;

        if(hand != null){
            if(hand.TryGetJoint(TrackedHandJoint.IndexTip, out MixedRealityPose pose)){
                handPosition = pose.Position;
            }
        }
    }
}

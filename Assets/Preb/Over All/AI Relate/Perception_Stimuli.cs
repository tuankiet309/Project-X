using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perception_Stimuli : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Sense_Component.RegisterStimuli(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDestroy()
    {
        Sense_Component.UnRegisterStimuli(this);
    }
}

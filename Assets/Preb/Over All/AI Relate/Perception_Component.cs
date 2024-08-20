using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perception_Component : MonoBehaviour
{
    [SerializeField] List<Sense_Component> senses = new List<Sense_Component>();
    LinkedList<Perception_Stimuli> currentlyPerceivedStimuli = new LinkedList<Perception_Stimuli>();

    Perception_Stimuli targetStimuli;

    public delegate void OnPerceptionTargetChanged(GameObject target, bool sense);
    public event OnPerceptionTargetChanged onPerceptionTargetChanged;
    void Start()
    {
        foreach (Sense_Component s in senses)
        {
            s.onPerceptionUpdate += SenseUpdate;
        }
    }

    private void SenseUpdate(Perception_Stimuli perception_Stimuli, bool sensedSuccessful)
    {
        var nodeFound = currentlyPerceivedStimuli.Find(perception_Stimuli);
        if (sensedSuccessful)
        {
            if (nodeFound != null) 
            {
                currentlyPerceivedStimuli.AddAfter(nodeFound,perception_Stimuli);
            }
            else
            {
                currentlyPerceivedStimuli.AddLast(perception_Stimuli);
            }
        }
        else
        {
                currentlyPerceivedStimuli.Remove(nodeFound);
            //BUGGGGGG
        }

        if(currentlyPerceivedStimuli.Count !=0)
        {
            Perception_Stimuli highest_Stimuli = currentlyPerceivedStimuli.First.Value;
            if(targetStimuli==null||targetStimuli!=highest_Stimuli)
            {
                targetStimuli = highest_Stimuli;
                onPerceptionTargetChanged?.Invoke(targetStimuli.gameObject,true);
            }
        }
        else
        {
            if (targetStimuli != null)
            {
                onPerceptionTargetChanged?.Invoke(targetStimuli.gameObject, false);
                targetStimuli = null;

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

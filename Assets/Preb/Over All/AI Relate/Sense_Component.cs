using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Sense_Component : MonoBehaviour
{
    [SerializeField] protected float forgetTime = 3f;

    static List<Perception_Stimuli> registeredStimuli = new List<Perception_Stimuli>();

    List<Perception_Stimuli> percivedStimuli = new List<Perception_Stimuli>();

    Dictionary<Perception_Stimuli, Coroutine> ForgettingRoutine = new Dictionary<Perception_Stimuli, Coroutine>();

    public delegate void OnPerceptionUpdate(Perception_Stimuli perception,bool successfulSensed);
    public event OnPerceptionUpdate onPerceptionUpdate;

    static public void RegisterStimuli(Perception_Stimuli perception_Stimuli)
    {
        if (registeredStimuli.Contains(perception_Stimuli))
            return;
        registeredStimuli.Add(perception_Stimuli);
        
    }
    static public void UnRegisterStimuli(Perception_Stimuli perception_Stimuli)
    {
        registeredStimuli.Remove(perception_Stimuli);
    }

    protected abstract bool IsStimuliSensable(Perception_Stimuli perception_Stimuli);
   
    
    void Update()
    {
        foreach(var perception_Stimuli in registeredStimuli)
        {
            if (IsStimuliSensable(perception_Stimuli))
            {
                if (!percivedStimuli.Contains(perception_Stimuli))
                {
                    percivedStimuli.Add(perception_Stimuli);
                    if (ForgettingRoutine.TryGetValue(perception_Stimuli, out Coroutine coroutine))
                    {
                        StopCoroutine(coroutine);
                        ForgettingRoutine.Remove(perception_Stimuli);
                    }
                    else 
                    {
                        onPerceptionUpdate?.Invoke(perception_Stimuli,true);
                        Debug.Log("I sense the player");
                    }

                }

            }
            else
            {
                if (percivedStimuli.Contains(perception_Stimuli))
                {
                    percivedStimuli.Remove(perception_Stimuli);
                    ForgettingRoutine.Add(perception_Stimuli,StartCoroutine(ForgetPlayer(perception_Stimuli)));
                }
            }
        }
    }

    IEnumerator ForgetPlayer(Perception_Stimuli perception_Stimuli)
    {
        yield return new WaitForSeconds(forgetTime);
        ForgettingRoutine.Remove(perception_Stimuli);
        onPerceptionUpdate?.Invoke(perception_Stimuli, false);
        Debug.Log("I cant sense the player");
        
    }

    protected virtual void DrawDebug()
    {
        
    }
    private void OnDrawGizmos()
    {
        DrawDebug();
    }
}

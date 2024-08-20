using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit_Sense : Sense_Component
{
    HealthComponent healthComp;


    Dictionary<Perception_Stimuli,Coroutine> hit_Record = new Dictionary<Perception_Stimuli, Coroutine> ();
    protected override bool IsStimuliSensable(Perception_Stimuli perception_Stimuli)
    {
        return hit_Record.ContainsKey(perception_Stimuli);
    }

    IEnumerator ForgetStimuli(Perception_Stimuli perception_Stimuli)
    {
        yield return new WaitForSeconds (forgetTime);
        hit_Record.Remove (perception_Stimuli);
    }
    private void Start()
    {
        healthComp = GetComponent<HealthComponent>();
        healthComp.onTakeDamamge += TookDamage;
    }

    private void TookDamage(float health, float delta, float maxHealth, GameObject Instigator)
    {
        Perception_Stimuli perception_Stimuli = Instigator.GetComponent<Perception_Stimuli>();
        if(perception_Stimuli != null)
        {
            Coroutine newForgettingCoroutine = StartCoroutine(ForgetStimuli(perception_Stimuli));
            if(hit_Record.TryGetValue(perception_Stimuli,out Coroutine coroutine))
            {
                StopCoroutine(coroutine);
                hit_Record[perception_Stimuli] = newForgettingCoroutine;
            }
            else
            {
                hit_Record.Add(perception_Stimuli, newForgettingCoroutine);
            }
        }
    }

}

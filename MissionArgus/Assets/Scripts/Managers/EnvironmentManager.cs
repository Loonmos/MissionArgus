using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour, IActivatable
{
    public int calledEvents;

    public MarkDia1 mark1;
    public MarkDia2 mark2;

    void IActivatable.Activate()
    {
        switch (calledEvents)
        {
            case 0:
                FirstLeverActivation();
                break;
        }
        calledEvents++;
    }

    void FirstLeverActivation()
    {
        //Debug.Log("Activate Called");
        //this is where we would activate the tilesets/gameobjects
        //also where you would call your event for mark's dialogue. magic
        //also probably where we would play SFX for the lights turning on

        mark1.RemoveMark1Trigger();
        mark2.TriggerMarkMove1();
        Debug.Log("gen event happens");
    }

}

//  ^ ^
// =0.0=
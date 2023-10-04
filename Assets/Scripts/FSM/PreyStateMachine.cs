using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreyStateMachine : MonoBehaviour
{
    private IPreyModel PreyModel;

    public void SetModel(IPreyModel preyModel)
    {
        PreyModel = preyModel;
    }

    private void OnEnable()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClimbableEnums;

public class Climbable : MonoBehaviour
{
    public ClimbableType climbableType;
    public ClimbableModifier climbableModifier;


    public ClimbableModifier getClimbableData()
    {
        return climbableModifier;
    }

}


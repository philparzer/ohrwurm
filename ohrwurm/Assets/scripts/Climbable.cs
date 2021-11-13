using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClimbableEnums;

public class Climbable : MonoBehaviour
{
    public ClimbableType climbableType;
    public ClimbableModifier climbableModifier;
    [SerializeField] float skinSpeed = 0.5f;
    [SerializeField] float clothSpeed = 0.7f;
    [SerializeField] float leatherSpeed = 0.9f;


    public float getClimbableData()
    {
        switch (climbableType)
        {
            case ClimbableType.Skin:
                return skinSpeed;
            case ClimbableType.Cloth:
                return clothSpeed;
            case ClimbableType.Leather:
                return leatherSpeed;
            default:
                return 0;
        }
    }

}



//TODO: pull player
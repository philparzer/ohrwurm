using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClimbableEnums;

public class PlayerController : MonoBehaviour
{

    //expose a public variable to adjust the speed of the player
    [SerializeField] [Range(10, 15)] float baseSpeed = 10.0f;
    public ClimbableModifier climbableModifier;
    public bool isClimbing = false;
    public bool isRunning = false;
    private UnityEngine.Rigidbody rigidBody;
    private Animator earwigAnimator;
    private float speed;


    // Start is called before the first frame update
    void Start()
    {
        earwigAnimator = GetComponent<Animator>();
        speed = baseSpeed;
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.freezeRotation = true;
    }

    void Move(float horizontalIn, float verticalIn)
    {   
        
        if (!isClimbing) {transform.Translate(horizontalIn, 0, verticalIn);}
        else {transform.Translate(horizontalIn, verticalIn, 0);}
        

    }

    // Update is called once per frame
    void Update()
    {
        float horizontalIn = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        float verticalIn = Input.GetAxis("Vertical") * Time.deltaTime * speed;

        if (verticalIn != 0)
        {
            Move(horizontalIn, verticalIn);
        }

        else 
        {
            rigidBody.useGravity = true;
        }
        
    }


    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.GetComponent<Climbable>() == null) {return;}

        speed = other.gameObject.GetComponent<Climbable>().getClimbableData() * baseSpeed;
        climbableModifier = other.gameObject.GetComponent<Climbable>().climbableModifier;
        
        isClimbing = true;
        rigidBody.useGravity = false;

        Transform child = transform.GetChild(0);
        child.rotation = Quaternion.Euler(0, -90, 90);

    }


    private void OnCollisionExit(Collision other) {
        if (other.gameObject.GetComponent<Climbable>() == null) {return;}
        isClimbing = false;
        speed = baseSpeed;
    }
}

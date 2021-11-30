using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClimbableEnums;


namespace Player
{
    public class PlayerController : MonoBehaviour
{

    [SerializeField] [Range(10, 15)] float baseSpeed = 10.0f;
    [SerializeField] float climbOffset = .8f;
    public ClimbableModifier climbableModifier;
    public bool isClimbing = false;
    public bool isRunning = false;
    private float speed;

    private UnityEngine.Rigidbody rigidBody;
    private Animator earwigAnimator;

    private Transform castObjR;
    private Transform castObjL;
    private Transform castObjB;
    private Transform castObjF;

    private RaycastHit hitForward;
    private RaycastHit hitF;
    private RaycastHit hitB;
    private RaycastHit hitL;
    private RaycastHit hitR;
    

    void Start()
    {
        earwigAnimator = transform.Find("earwig").GetComponent<Animator>();
        
        rigidBody = transform.Find("earwig").GetComponent<Rigidbody>();
        rigidBody.freezeRotation = true;
        rigidBody.useGravity = false;

        castObjR = transform.GetChild(1).Find("rightCast");
        castObjL = transform.GetChild(1).Find("leftCast");
        castObjF = transform.GetChild(1).Find("frontCast");
        castObjB = transform.GetChild(1).Find("backCast");
        
        speed = baseSpeed;

    }

    void Update()
    {
        RotationCasts(); 
        Move();
    }

    private void Move()
    {
        float horizontalIn = Input.GetAxis("Horizontal");
        float verticalIn = Input.GetAxis("Vertical");

        if (horizontalIn == 0 && verticalIn == 0)
        {
            earwigAnimator.SetBool("running", false);
        }

        else 
        {
            earwigAnimator.SetBool("running", true);
            float horizontal = horizontalIn * speed * Time.deltaTime;
            float vertical = verticalIn * speed * Time.deltaTime;
            transform.Translate(horizontal, 0, vertical); 
        }
        
    }

    private void RotationCasts()
    {

            Physics.Raycast(castObjR.position, -castObjR.up, out hitR);
            Debug.DrawRay(castObjR.position, -castObjR.up * hitR.distance, Color.red);


            Physics.Raycast(castObjL.position, -castObjL.up, out hitL);
            Debug.DrawRay(castObjL.position, -castObjL.up * hitL.distance, Color.blue);


            Physics.Raycast(castObjF.position, -castObjF.up, out hitF);
            Debug.DrawRay(castObjF.position, -castObjF.up * hitF.distance, Color.green);

            

            Physics.Raycast(castObjB.position, -castObjB.up, out hitB);
            Debug.DrawRay(castObjB.position, -castObjB.up * hitB.distance, Color.black);

            //TODO: check climbableModifier of hit surface

            float delta = hitF.distance - hitB.distance;

            //revert clipping 
            //TODO: remove magic numbers
            if (hitF.distance < 1f || hitB.distance < 1f)
            {
                Debug.Log("NOT clipping");
                transform.Translate(0, 5 * Time.deltaTime, 0);
            }

            else if (hitF.distance > 1.5f || hitB.distance > 1.5f)
            {
                Debug.Log("clipping"); //FIXME: buggy?
                transform.Translate(0, -30 * Time.deltaTime, 0);
            }


            if (delta > 0)
            {   
                Debug.Log("delta: " + delta);
                Debug.Log("FRONT");
                transform.Rotate(90 * Time.deltaTime, 0, 0);
            }

            else if (delta < 0)
            {
                Debug.Log("delta: " + delta);
                Debug.Log("BACK");
                transform.Rotate(-90 * Time.deltaTime, 0, 0);
            }

            Physics.Raycast(castObjF.position, castObjF.forward, out hitForward);
            Debug.DrawRay(castObjF.position, castObjF.forward * hitForward.distance, Color.yellow);
            
            //TODO: y rotation
    
    }



}
}


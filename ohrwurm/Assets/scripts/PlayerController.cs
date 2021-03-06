using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClimbableEnums;
using Cinemachine;


namespace Player
{
    public class PlayerController : MonoBehaviour
{
    public bool isDead = false;
    public GameMeta gameManager;
    [SerializeField] [Range(10, 100)] float baseSpeed = 10.0f;
    [SerializeField] float climbOffset = .8f;
    public ClimbableModifier climbableModifier;
    public float dryMod = 1.0f;
    public float stickyMod = 0.3f;
    public float slipperyMod = 5.0f;

    public bool isClimbing = false;
    public bool isDying = false;

    public float frozenTime = 0; //TODO: set this on impact w particle effect
    public float slipperyTime = 0;
    public float stickyTime = 0;
    private float speed;

    private float modifier = 1.0f;
    

    public Cinemachine.CinemachineVirtualCamera cam1;
    public Cinemachine.CinemachineVirtualCamera cam2;

    public Cinemachine.CinemachineVirtualCamera loseCam;
    public ParticleSystem freezeEffect;
    public ParticleSystem deathEffect;

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
    
    private void Awake() {
        cam1.Priority = 11;    
    }

    void Start()
    {
        deathEffect.Stop();
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
        if (isDead)
        {
            return;
        }

        if (isDying) 
        {
            InitDeath();
            return;
        }

        RotationCasts(); 
        if (stickyTime > 0){stickyTime -= Time.deltaTime;}
        if (slipperyTime > 0){slipperyTime -= Time.deltaTime;}
        
        if (frozenTime <= 0)
        {
            if (freezeEffect.isPlaying)
            {
                freezeEffect.Stop();
            }

            Move();
            if (transform.rotation.eulerAngles.x < 300) 
            {   
                cam1.Priority = 10;
                cam2.Priority = 11;
            }

            else {
                cam1.Priority = 11;
                cam2.Priority = 10;
            }
        }

        else 
        {
            if (freezeEffect.isPlaying == false)
            {
                freezeEffect.Play();
            }
            frozenTime -= Time.deltaTime;
        }
        
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
            float horizontal = horizontalIn * speed * modifier * Time.deltaTime;
            float vertical = verticalIn * speed * modifier * Time.deltaTime;
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

            CheckSurface(hitF);

            float delta = hitF.distance - hitB.distance;
            float deltaY = hitL.distance - hitR.distance;

            //revert clipping 
            if (hitF.distance < 0.5f || hitB.distance < 0.5f)
            {
                transform.Translate(0, 30 * Time.deltaTime, 0);
            }

            else if (hitF.distance > 1.2f || hitB.distance > 1.2f)
            {
                transform.Translate(0, -30 * Time.deltaTime, 0);
            }


            if ((Mathf.Abs(delta) - 1) > (Mathf.Abs(deltaY) - 1))
            {
                //rotate up down
                if (delta > 0)
                {   
                    transform.Rotate(90 * Time.deltaTime, 0, 0);
                }

                else if (delta < 0)
                {

                    transform.Rotate(-90 * Time.deltaTime, 0, 0);
                }
            }

            else 
            {
                //rotate left right
                if (deltaY > .5f)
                {   
                    transform.Rotate(0, 0, 90 * Time.deltaTime);
                }

                else if (deltaY < -.5f)
                {
                    transform.Rotate(0, 0, -90 * Time.deltaTime);
                }
            }
    }


    private void CheckSurface(RaycastHit hit)
    {
        try {climbableModifier = hit.collider.gameObject.GetComponent<Climbable>().getClimbableData();}
        catch{climbableModifier = ClimbableEnums.ClimbableModifier.Dry;}

        switch (climbableModifier)
        {
            case ClimbableEnums.ClimbableModifier.Dry:
                if (slipperyTime <= 0 && stickyTime <= 0) {modifier = dryMod;};
                break;

            case ClimbableEnums.ClimbableModifier.Sticky:
                modifier = stickyMod;
                stickyTime = 10f;
                break;

            case ClimbableEnums.ClimbableModifier.Slippery:
                modifier = slipperyMod;
                slipperyTime = 10f;
                break;
        }
    }


    public void InitDeath(){
        //TODO: implement third virtual cam and change priority
        rigidBody.useGravity = true;
        rigidBody.isKinematic = false;
        rigidBody.constraints = RigidbodyConstraints.None;
        rigidBody.AddForce(Vector3.down * 10);
        deathEffect.Play();
        StartCoroutine(Die());
    }


    private IEnumerator Die()
    {
        yield return new WaitForSeconds(2.3f);
        //TODO: some UI stuff

        isDead = true;
        gameManager.GameOver();
        loseCam.Priority = 12;
    }

    




}
}


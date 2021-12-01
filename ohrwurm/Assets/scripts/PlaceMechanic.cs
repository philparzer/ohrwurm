using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaceMechanic : MonoBehaviour
{
    [SerializeField] private Camera cineCamera;
    [SerializeField] private GameObject camera1;
    [SerializeField] private GameObject camera2;
    [SerializeField] private GameObject camera3;
    [SerializeField] private GameObject camera4;    

    [SerializeField] private GameObject prefab;
    private GameObject previewMesh;
    [SerializeField] private float rotationSpeed = .5f;

    private float y = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            camera1.SetActive(true);
            camera2.SetActive(false);
            camera3.SetActive(false);
            camera4.SetActive(false);
        }
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            camera1.SetActive(false);
            camera2.SetActive(true);
            camera3.SetActive(false);
            camera4.SetActive(false);
        }
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            camera1.SetActive(false);
            camera2.SetActive(false);
            camera3.SetActive(true);
            camera4.SetActive(false);
        }
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            camera1.SetActive(false);
            camera2.SetActive(false);
            camera3.SetActive(false);
            camera4.SetActive(true);
        }



        if(previewMesh != null){
            Ray ray = cineCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            //instanciate a cube at the hit position
            if (Physics.Raycast(ray, out hit))
            {
                //check if hit gameobject has tag Climbable
                if (hit.collider.gameObject.tag == "Climbable")
                {
                    previewMesh.transform.position = hit.point;

                    Quaternion normalVectorRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
                    previewMesh.transform.rotation = normalVectorRotation;
                    previewMesh.transform.Rotate(0, y, 0);

                }
            }

            if (Input.GetKey(KeyCode.A))
            {
                y += rotationSpeed;
                // previewMesh.transform.Rotate(0, 0, 1*rotationSpeed);
            }
            if (Input.GetKey(KeyCode.D))
            {
                y -= rotationSpeed;
                // previewMesh.transform.Rotate(0, 0, -1*rotationSpeed);
            }

            //if left mouse key is pressed
            if(Input.GetMouseButtonDown(0))
            {
                DontDestroyOnLoad(previewMesh);
                previewMesh = null;
            }
        }
    }

    // set preview mesh
    public void SetPreviewMesh(GameObject mesh)
    {
        previewMesh = mesh;
    }

    public bool previewMeshGotPlaced()
    {
        return previewMesh == null;
    }

    public void instanciatePrefab(GameObject prefab){
        previewMesh = Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
    }


    public void StartGame (){
        SceneManager.LoadScene(2);
    }

}

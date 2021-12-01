using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceMechanic : MonoBehaviour
{

    [SerializeField] private GameObject prefab;
    private GameObject previewMesh;
    [SerializeField] private float rotationSpeed = .5f;

    private float y = 0;

    // Start is called before the first frame update
    void Start()
    {
        previewMesh = Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {   if(previewMesh != null){

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
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
                previewMesh = null;
            }
        }
    }

    // set preview mesh
    public void SetPreviewMesh(GameObject mesh)
    {
        previewMesh = mesh;
    }
}

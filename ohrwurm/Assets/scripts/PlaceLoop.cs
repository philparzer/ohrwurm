using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceLoop : MonoBehaviour
{
    [SerializeField] public GameObject[] obstaclePrefabs;
    private PlaceMechanic placeMechanic;
    
    // Start is called before the first frame update
    void Start()
    {
        //get "PlaceMechanic" component of this game object
        placeMechanic = GetComponent<PlaceMechanic>();
    }

    // Update is called once per frame
    void Update()
    {
        if(placeMechanic.previewMeshGotPlaced()){
            if(Input.GetKeyDown(KeyCode.Q)){
                placeMechanic.instanciatePrefab(obstaclePrefabs[0]);
            } else if(Input.GetKeyDown(KeyCode.W)){
                placeMechanic.instanciatePrefab(obstaclePrefabs[1]);
            } else if(Input.GetKeyDown(KeyCode.E)){
                placeMechanic.instanciatePrefab(obstaclePrefabs[2]);
            } else if(Input.GetKeyDown(KeyCode.R)){
                placeMechanic.instanciatePrefab(obstaclePrefabs[3]);
            }
            
        }

    }
}

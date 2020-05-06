using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructurePlacement : MonoBehaviour
{            
    [SerializeField] private GameObject structurePrefab = null;
    [SerializeField] private GameObject[] structurePlaceholderModel = null;
    private GameObject player = null;
    private GameObject tempStructureHolder;
    private int structureToPlace = 0;
    private float mouseX = 0;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        mouseX = Input.GetAxis("Mouse X");
        if (Input.GetKeyDown(KeyCode.Alpha1) && structureToPlace.Equals(0)) 
        {
            structureToPlace = 1;
            SpawnPlaceholder(structurePlaceholderModel[0]);
        }

        switch(structureToPlace)
        {
            case 1:
                PreviewPlacement(tempStructureHolder);
                break;

            default:
                break;
        }
    }

    private void PreviewPlacement(GameObject previewObject)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var objectIsPlaced = false;
        var stop = false;

        if(Input.GetMouseButtonDown(0)) 
            objectIsPlaced = true;
        if (Input.GetMouseButtonDown(1))
            stop = true;

        if(Physics.Raycast(ray, out RaycastHit hitInfo, 5f))
        {
            previewObject.SetActive(true);
            previewObject.transform.position = hitInfo.point;
            previewObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);

        }
        else
            previewObject.SetActive(false);
               
        if (stop.Equals(true))
        {
            structureToPlace = 0;
            Destroy(previewObject);
            return;
        }
        
        if (objectIsPlaced.Equals(true))
        {
            previewObject.transform.position = previewObject.transform.position;
            //previewObject.transform.rotation = Quaternion.Euler(0, mouseX * 5, 0);
            structureToPlace = 0;
            return;              
        }
    }

    GameObject SpawnPlaceholder(GameObject placeholderStructureToSpawn)
    {
        tempStructureHolder = Instantiate(placeholderStructureToSpawn);
        tempStructureHolder.SetActive(false);

        return tempStructureHolder;
    }
}

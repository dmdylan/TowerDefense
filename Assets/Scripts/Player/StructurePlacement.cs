using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Maybe set this up to be triggered as an event?
//Coroutine?
//Player has owns states?
public class StructurePlacement : MonoBehaviour
{            
    [SerializeField] private GameObject[] structurePrefabs = null;
    [SerializeField] private GameObject[] structurePlaceholderModel = null;
    private GameObject structureHolder;
    private GameObject tempStructureHolder;
    private int structureToPlace = 0;
    private float mouseX = 0;

    // Update is called once per frame
    void Update()
    {
        mouseX = Input.GetAxis("Mouse X");
        if (Input.GetKeyDown(KeyCode.Alpha1) && structureToPlace.Equals(0)) 
        {
            structureToPlace = 1;
            structureHolder = structurePrefabs[0];
            SpawnPlaceholder(structurePlaceholderModel[0]);
        }

        switch(structureToPlace)
        {
            case 1:
                PreviewPlacement(tempStructureHolder);             
                break;
            case 10:
                PreviewRotation(tempStructureHolder);
                break;
            default:
                break;
        }
    }

    private void PreviewRotation(GameObject previewObject)
    {
        previewObject.transform.RotateAround(previewObject.transform.position, previewObject.transform.up, mouseX * 20);

        if (Input.GetMouseButtonDown(0))
        {
            SpawnStructure(structureHolder, previewObject);
            //Calls event that structure was built
            GameEvents.Instance.BuiltAStructure();
            Destroy(previewObject);
            structureToPlace = 0;
        }
        
        if (Input.GetMouseButtonDown(1))
        {
            structureToPlace = 0;
            Destroy(previewObject);
        }        
    }

    private void PreviewPlacement(GameObject previewObject)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var objectIsPlaced = false;
        var stop = false;

        if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, 5f)) { objectIsPlaced = true; }
        if (Input.GetMouseButtonDown(1)) { stop = true; }
            
        if (objectIsPlaced.Equals(true))
        {
            previewObject.transform.position = previewObject.transform.position;
            structureToPlace = 10;
        }
        else
        {
            if(Physics.Raycast(ray, out RaycastHit hitInfo, 5f))
            {
                previewObject.SetActive(true);
                previewObject.transform.position = hitInfo.point;
                previewObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
            }
            else
                previewObject.SetActive(false);      
        }

        if (stop.Equals(true))
        {
            structureToPlace = 0;
            Destroy(previewObject);
        }
    }

    GameObject SpawnPlaceholder(GameObject placeholderStructureToSpawn)
    {
        tempStructureHolder = Instantiate(placeholderStructureToSpawn);
        tempStructureHolder.SetActive(false);

        return tempStructureHolder;
    }

    void SpawnStructure(GameObject structureToSpawn, GameObject structurePlaceholder)
    {
        Instantiate(structureToSpawn, structurePlaceholder.transform.position, structurePlaceholder.transform.rotation);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructurePlacement : MonoBehaviour
{
    [SerializeField] private GameObject structurePrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
            SpawnStructure();
    }

    public void SpawnStructure()
    {
        Debug.Log("Placing barricade");
        //if (Input.GetMouseButtonDown(1))
        //    return;

        GameObject barricade = Instantiate(structurePrefab);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if(Physics.Raycast(ray, out hitInfo))
        {
            barricade.transform.position = hitInfo.point;
            barricade.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
        }
    }
}

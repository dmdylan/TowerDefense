using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructurePlacement : MonoBehaviour
{            
    [SerializeField] private GameObject structurePrefab;
    // Generate a ray from the cursor position

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SpawnStructure();
        }
    }

    private void SpawnStructure()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        GameObject barricade = Instantiate(structurePrefab);

        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            barricade.transform.position = hitInfo.point;
            barricade.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
        }

        barricade.transform.rotation = Quaternion.Slerp(Quaternion.Euler(0, barricade.transform.rotation.y,0), Quaternion.Euler(0,ray.origin.y,0), .5f);
    }
}

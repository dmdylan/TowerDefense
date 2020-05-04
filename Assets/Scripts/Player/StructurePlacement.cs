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

    }

    public void SpawnStructure()
    {
        Vector3 middleScreenPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2));

        GameObject barricade = Instantiate(structurePrefab, middleScreenPosition, Quaternion.Euler(middleScreenPosition.x, middleScreenPosition.y, middleScreenPosition.z));
    }
}

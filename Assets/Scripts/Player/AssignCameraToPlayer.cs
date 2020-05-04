using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignCameraToPlayer : MonoBehaviour
{
    CinemachineVirtualCamera cinemachineCamera;
    GameObject player;

    // Start is called before the first frame update
    void Awake()
    {
        cinemachineCamera = GetComponent<CinemachineVirtualCamera>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    {
        cinemachineCamera.Follow = player.transform;
        cinemachineCamera.LookAt = player.GetComponentInChildren<Transform>().Find("Target");
    }
}

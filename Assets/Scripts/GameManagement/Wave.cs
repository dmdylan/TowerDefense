using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;

public class Wave
{
    private GameObject[] enemiesToSpawn;

    //Wave should only include the enemies that are going to spawn
    //all other info should be calculated outside and passed in
    public Wave(GameObject[] enemiesInWave)
    {
        enemiesToSpawn = enemiesInWave;
    }
}

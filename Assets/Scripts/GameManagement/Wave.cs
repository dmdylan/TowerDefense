using UnityEngine;

public class Wave
{
    public GameObject[] EnemiesToSpawn { get; }

    //Wave should only include the enemies that are going to spawn
    //all other info should be calculated outside and passed in
    public Wave(GameObject[] enemiesInWave)
    {
        EnemiesToSpawn = enemiesInWave;
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;

//[System.Serializable]
public class Wave
{
    private GameObject[] enemiesToSpawn;

    //Wave should only include the enemies that are going to spawn
    //all other info should be calculated outside and passed in
    public Wave(GameObject[] enemiesInWave)
    {
        enemiesToSpawn = enemiesInWave;
    }

    public Wave GenerateNewWave(GameObject[] enemyPool, float currentThreatLevel)
    {
        List<GameObject> possibleEnemiesThatCanSpawn = new List<GameObject>();
        List<GameObject> enemiesThatWillSpawn = new List<GameObject>();

        int totalEnemyCountValue = Mathf.RoundToInt(100 * currentThreatLevel * (int)GameManager.Instance.LevelDifficulty.Difficulty);

        //Goes through pool of all enemies in level and adds them to potential spawn list based on current wave threat level
        foreach(GameObject enemy in enemyPool)
        {
            if(enemy.GetComponent<Enemy>().baseEnemyStats.ThreatLevel <= currentThreatLevel)
            {
                possibleEnemiesThatCanSpawn.Add(enemy);
            }
        }

        //Add random enemies to list of things to spawn
        for(int i = 0; i <= totalEnemyCountValue;)
        {
            int enemyToAddPosition = Random.Range(0, possibleEnemiesThatCanSpawn.Count);
            GameObject enemyToAdd = possibleEnemiesThatCanSpawn[enemyToAddPosition];

            //Check if the list has the enemy type limit
            //Will probably have to refactor this, has to iterate through the list everytime
            if (CheckEnemyListCount(enemyToAdd, enemiesThatWillSpawn) < enemyToAdd.GetComponent<Enemy>().baseEnemyStats.AllowedPerWave)
                enemiesThatWillSpawn.Add(enemyToAdd);
            else
                possibleEnemiesThatCanSpawn.Remove(enemyToAdd);            
        }

        var newWave = new Wave(enemiesThatWillSpawn.ToArray());
        return newWave;
    }

    //Takes the enemy game object and the current enemy spawn list
    //Adds up how many times it is in that list and returns it
    int CheckEnemyListCount(GameObject enemyToAdd , List<GameObject> enemiesThatWillSpawn)
    {
        int i = 0;

        foreach(GameObject enemy in enemiesThatWillSpawn)
        {
            if (enemy.Equals(enemyToAdd))
                i++;
        }

        return i;
    }
}

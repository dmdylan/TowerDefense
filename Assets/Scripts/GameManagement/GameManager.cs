using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private static LevelDifficulty levelDifficulty = null;
    public static LevelDifficulty LevelDifficulty => levelDifficulty;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {
        
    }
}

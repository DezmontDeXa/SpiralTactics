using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private const float PLAYER_DISTANCE_SPAWN_LEVEL_PART = 250f;

    [SerializeField] private Transform levelPart_Start;
    [SerializeField] private Transform levelPart_End;
    [SerializeField] private List <Transform> levelPartList;
    [SerializeField] private GameObject player;
    [Tooltip("Количество платформ до победы")] [SerializeField]  private int maxPlatformsCount = 20;
    [SerializeField] private float winDistance;
    [SerializeField] private GameObject winText;


    private int currentPlatformsCount = 1;
    private Vector3 lastEndPosition;
    private Transform lastEndPoint;
    private Transform winPlatform;
    private bool win;


    private void Awake()
    {
        lastEndPosition = levelPart_Start.Find("EndPosition_L").position;

        int startingSpawnLevelParts = 2;
        for (int i = 0; i < startingSpawnLevelParts; i++)
        {
            SpawnLevelPart();
        }
    }

    private void Update()
    {
        if (win)
        {
            if (Vector3.Distance(player.transform.position, winPlatform.position) < winDistance)
            {
                player.GetComponent<DraftPigMovement>().isDead = true;
                winText.SetActive(true);
            }
            
            
            return;
        } 
        
        if (Vector3.Distance(player.transform.position, lastEndPosition) < PLAYER_DISTANCE_SPAWN_LEVEL_PART)
        {
            SpawnLevelPart();
        }
    }

    private void SpawnLevelPart()
    {
      
        if (currentPlatformsCount >= maxPlatformsCount)
        {
             winPlatform =  Instantiate(levelPart_End, lastEndPosition, Quaternion.identity);
            win = true;
        }
        else
        {
            Transform chosenLevelPart = levelPartList[Random.Range(0, levelPartList.Count)];
            Transform lastLevelPartTransform = SpawnLevelPart(chosenLevelPart, lastEndPosition);
            lastEndPosition = lastLevelPartTransform.Find("EndPosition_L").position; 
            currentPlatformsCount++;
        }
    }

    private Transform SpawnLevelPart(Transform levelPart, Vector3 spawnPosition)
    {
        Transform levelPartTransform = Instantiate(levelPart, spawnPosition, Quaternion.identity);
        return levelPartTransform;
    }
}
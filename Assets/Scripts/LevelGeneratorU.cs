using UnityEngine;
using System.Collections.Generic;

public class LevelGeneratorU : MonoBehaviour
{
    private const float PLAYER_DISTANCE_SPAWN_LEVEL_PART = 250f;

    [SerializeField] private Transform _levelPart_Start;
    [SerializeField] private Transform _levelPart_End;
    [SerializeField] private List <Transform> _levelPartList;
    [SerializeField] private GameObject _player;

    private int _currentPlatformsCount = 1;
    private Vector3 _lastEndPosition;
    private bool _win;


    private void Awake()
    {
        _lastEndPosition = _levelPart_Start.Find("EndPosition_U").position;

        var startingSpawnLevelParts = 2;
        for (var i = 0; i < startingSpawnLevelParts; i++)
        {
            SpawnLevelPart();
        }
    }

    private void Update()
    {
        if (_win) return;
        
        if (Vector3.Distance(_player.transform.position, _lastEndPosition) < PLAYER_DISTANCE_SPAWN_LEVEL_PART)
            SpawnLevelPart();
    }

    private void SpawnLevelPart()
    {
      
        if (_currentPlatformsCount >= 20)
        {
            Instantiate(_levelPart_End, _lastEndPosition, Quaternion.identity);
            _win = true;
        }
        else
        {
            Transform chosenLevelPart = _levelPartList[Random.Range(0, _levelPartList.Count)];
            Transform lastLevelPartTransform = SpawnLevelPart(chosenLevelPart, _lastEndPosition);
            _lastEndPosition = lastLevelPartTransform.Find("EndPosition_U").position; 
            _currentPlatformsCount++;
        }
    }

    private Transform SpawnLevelPart(Transform levelPart, Vector3 spawnPosition)
    {
        Transform levelPartTransform = Instantiate(levelPart, spawnPosition, Quaternion.identity);
        return levelPartTransform;
    }
}
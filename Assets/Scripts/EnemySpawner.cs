using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private Tilemap _groundTiles;
    [SerializeField] private float _spawnCooldown;
    private List<Vector3> _spawnPositions = new();
    private float _currentCooldown;

    private void Start()
    {
        SetEnemySpawnPositions();
        _currentCooldown = _spawnCooldown;
    }

    private void Update()
    {
        HandleEnemySpawning();
    }

    private void SetEnemySpawnPositions()
    {
        foreach (Vector3Int position in _groundTiles.cellBounds.allPositionsWithin)
        {
            if (_groundTiles.HasTile(position))
            { 
                _spawnPositions.Add(_groundTiles.GetCellCenterWorld(position));
            }
        }
    }

    private void HandleEnemySpawning()
    {
        if (Time.time < _currentCooldown) return;

        SpawnEnemyToRandomLocation();

        _currentCooldown = Time.time + _spawnCooldown;
    }

    private void SpawnEnemyToRandomLocation()
    {
        Instantiate(_enemyPrefab, GetRandomPosition(), Quaternion.identity);
    }

    private Vector3 GetRandomPosition()
    {
        return _spawnPositions[Random.Range(0, _spawnPositions.Count)];
    }
}

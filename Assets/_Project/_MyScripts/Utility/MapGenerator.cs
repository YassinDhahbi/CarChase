using System.Collections;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _prefabToSpawn;
    [SerializeField] private Transform _startPosition;
    [SerializeField] private float _numberOfSpawnedPrefabs;
    [SerializeField] private Transform _parentTransform;
    [SerializeField] private int _maxNumberOfSpawnedPrefabs;

    [Header("Generation Parameters")]
    [SerializeField] private float _minY;
    [SerializeField] private float _maxY;
    [SerializeField] private float _minX;
    [SerializeField] private float _maxX;
    private void Start()
    {
        for (int i = 0; i < _maxNumberOfSpawnedPrefabs; i++)
        {
            GetRandomizedYPosition();
        }
    }

    void GetRandomizedYPosition()
    {
        var yPosition = Random.Range(_minY, _maxX);
        var xPosition = Random.Range(_minX, _maxX);
        _numberOfSpawnedPrefabs++;
        var newGO = Instantiate(_prefabToSpawn, _startPosition.position + new Vector3(xPosition, yPosition + _numberOfSpawnedPrefabs, 0), Quaternion.identity);
        newGO.transform.parent = _parentTransform;
    }

    IEnumerator DelayedSPawn()
    {
        for (int i = 0; i < _maxNumberOfSpawnedPrefabs; i++)
        {
            yield return new WaitForSeconds(0.1f);
            GetRandomizedYPosition();
        }
    }
}

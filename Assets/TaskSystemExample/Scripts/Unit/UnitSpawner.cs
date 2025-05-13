using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField] private List<SpawnedObjectData> _objectToSpawn;
    [SerializeField] private float _initialSpeed = 1f;
    [SerializeField] private float _spawnRadius = 1f;


    private void Start()
    {
        SpawnObjects();
    }

    private void SpawnObjects()
    {
        foreach (var obj in _objectToSpawn)
        {
            for (int i = 0; i < obj.count; i++)
            {
                Vector2 circle = UnityEngine.Random.insideUnitCircle * _spawnRadius;
                Instantiate(obj.go, obj.go.transform.position + new Vector3(circle.x, 0, circle.y), Quaternion.identity, this.transform).
                    GetComponent<IPushable>().Push(circle, _initialSpeed);
            }
        }
    }
}

[Serializable]
public struct SpawnedObjectData
{
    public GameObject go;
    public int count;
}

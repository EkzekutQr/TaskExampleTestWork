using System;
using System.Collections.Generic;
using UnityEngine;

public class GameplayEntryPoint : MonoBehaviour
{
    [SerializeField] private GameObject _sceneRootBinder;
    [SerializeField] private List<GameObject> _prefabsToSpawn;

    public void Run()
    {
        Debug.Log("Gameplay Scene Loaded");

        SpawnPrefabs();
    }

    public void SpawnPrefabs()
    {
        foreach (var item in _prefabsToSpawn)
        {
            Instantiate(item);
        }
    }
}

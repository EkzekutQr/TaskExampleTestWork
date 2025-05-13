using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TaskKillAny : TaskBase
{
    private bool _isInited;

    [SerializeField] private ClickToDestroy _clickToDestroy;
    [SerializeField] private Unit _specificUnitPrefab;

    private int unitCount = 0;
    private int unitMaxCount = 0;

    public override void CompleteClause()
    {
        if (!_isInited)
            Init();
        if (_isComleted)
            onTaskCompleted.Invoke(this);
    }

    private void Init()
    {
        if (!_isInited)
        {
            _isInited = true;
            _clickToDestroy = FindFirstObjectByType<ClickToDestroy>();
            unitMaxCount = GameObject.FindObjectsOfType(_specificUnitPrefab.GetType()).Length;

            _clickToDestroy.OnObjectDestroyed += CheckDestroyedObject;
        }
    }
    private void CheckDestroyedObject(GameObject go)
    {
        if (go.TryGetComponent<Unit>(out Unit unit))
        {
                unitCount++;

            if (unitCount >= unitMaxCount)
                _isComleted = true;
        }
    }

    public override float GetProgress()
    {
        float progress = 0;
        if (unitMaxCount > 0)
            progress = (float)unitCount / (float)unitMaxCount;
        return progress;
    }
}

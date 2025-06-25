using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TaskKillAny : TaskBase
{
    [SerializeField] private ClickToDestroy _clickToDestroy;
    [SerializeField] private Unit _specificUnitPrefab;

    private int _unitCurrentCount = 0;
    private int _unitMaxCount = 0;


    public override void MissionStart()
    {
        base.MissionStart();

        _clickToDestroy = GameObject.FindFirstObjectByType<ClickToDestroy>();
        _unitMaxCount = GameObject.FindObjectsOfType(_specificUnitPrefab.GetType()).Length;

        _clickToDestroy.OnObjectDestroyed += CheckDestroyedObject;
        OnFinished += _ => _clickToDestroy.OnObjectDestroyed -= CheckDestroyedObject;
        RaiseOnStarted(this);
    }

    public override void MissionUpdate()
    {

    }

    private void CheckDestroyedObject(GameObject go)
    {
        if (go.TryGetComponent<Unit>(out Unit unit))
        {
                _unitCurrentCount++;

            if (_unitCurrentCount >= _unitMaxCount)
            {
                _isComleted = true;
                RaiseOnMissionPointReached(this);
                RaiseOnTaskFinished(this);
            }
        }
    }

    public override float GetProgress()
    {
        float progress = 0;
        if (_unitMaxCount > 0)
            progress = (float)_unitCurrentCount / (float)_unitMaxCount;
        return progress;
    }
}

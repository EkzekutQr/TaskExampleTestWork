using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskColliderTrigger : MonoBehaviour
{
    [SerializeField] private bool triggerEnter;

    [SerializeField] GameObject triggerMeshGameObject;

    public bool TriggerEnter { get => triggerEnter; set => triggerEnter = value; }
    public GameObject TriggerMeshGameObject { get => triggerMeshGameObject; set => triggerMeshGameObject = value; }

    private void OnTriggerEnter(Collider other)
    {
        TriggerMeshGameObject.SetActive(false);
        Debug.Log("TriggerEnter");
        Debug.Log(other.gameObject.name);
        triggerEnter = true;
    }
    private void OnTriggerStay(Collider other)
    {
        TriggerMeshGameObject.SetActive(false);
    }
}

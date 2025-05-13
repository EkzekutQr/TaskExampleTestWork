using System;
using UnityEngine;

public class ClickToDestroy : MonoBehaviour, IDestroyer
{
    [SerializeField] private int damagePerClick = 1;
    [SerializeField] private LayerMask layerMask;

    public event Action<GameObject> OnObjectDestroyed;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            TryToDestroy();

    }

    private void TryToDestroy()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, float.PositiveInfinity, layerMask))
        {
            if (hit.transform.TryGetComponent<HP>(out HP hp))
            {
                hp.TakeDamage(damagePerClick);
                if (hp.HPCount <= 0)
                    OnObjectDestroyed?.Invoke(hp.gameObject);
            }
        }
    }
}

public interface IDestroyer
{
    public event Action<GameObject> OnObjectDestroyed;
}
using UnityEngine;

public class ClickToDestroy : MonoBehaviour
{
    [SerializeField] private int damagePerClick = 1;
    [SerializeField] private LayerMask layerMask;

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
            if (hit.transform.TryGetComponent<IHP>(out IHP hp))
                hp.TakeDamage(damagePerClick);
        }
    }
}
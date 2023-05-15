using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This script checks what the player selected and returns that data
/// </summary>
public class SelectionManager : MonoBehaviour
{
    private Camera _mainCamera;
    private readonly float _rayDistance = 200;
    
    public LayerMask SelectionMask;

    public UnityEvent<GameObject> OnShipSelected;
    public UnityEvent<GameObject> OnTerrainSelected;
    public UnityEvent<GameObject> OnEnemySelected;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    /// <summary>
    /// Checks if the player selected a Ship or Something else
    /// </summary>
    /// <param name="mousePosition"></param>
    public void HandleClick(Vector3 mousePosition)
    {
        if (!FindTarget(mousePosition, out GameObject result)) return;
        if (ShipSelected(result))
        {
            if (result.layer == 7)
            {
                OnEnemySelected.Invoke(result);
                return;
            }
            OnShipSelected?.Invoke(result);
            return;
        }

        OnTerrainSelected.Invoke(result);
    }

    /// <summary>
    /// Checks if the Selected Ship has the 'Ship.cs' script
    /// </summary>
    /// <param name="result"></param>
    /// <returns></returns>
    private bool ShipSelected(GameObject result)
    {
        return result.GetComponent<Ship>() != null;
    }
    
    /// <summary>
    /// Returns the object the player touched with a specific LayerMask
    /// </summary>
    /// <param name="mousePosition"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    private bool FindTarget(Vector3 mousePosition, out GameObject result)
    {
        Ray ray = _mainCamera.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, _rayDistance,SelectionMask))
        {
            result = hit.collider.gameObject;
            return true;
        }

        result = null;
        return false;
    }
}

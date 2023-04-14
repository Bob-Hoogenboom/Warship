using UnityEngine;

public class TurnManager : MonoBehaviour
{
    [SerializeField] private bool playerTurn;
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;

        if (_camera == null) return;
        
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity)) return;

        
        
        transform.GetComponent<HealthManager>().ChangeHealth(hit.transform.GetComponent<Stats>().Damage, hit.transform.GetComponent<Stats>().HealthBar);
        playerTurn = !playerTurn;
        
        if (!playerTurn)
        {
            Debug.Log("Enemy turn");
            return;
        }
        
        Debug.Log("PlayerTurn");
    }
}

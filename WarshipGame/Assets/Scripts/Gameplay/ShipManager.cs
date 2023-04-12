using UnityEngine;

public class ShipManager : MonoBehaviour
{
    [SerializeField] private HexGrid HexGridScript;
    [SerializeField] private Movement MovementScript;

    public bool PlayersTurn { get; private set; } = true;

    [SerializeField] private Ship SelectedShip;
    private HexData PreviouslySelectedHex;

    public void HandleShipSelected(GameObject ship)
    {
        if (!PlayersTurn) return;

        Ship shipReference = ship.GetComponent<Ship>();

        if (CheckIfTheSameShipIsSelected(shipReference)) return;
        
        PrepareShipForMovement(shipReference);
    }

    private bool CheckIfTheSameShipIsSelected(Ship shipReference)
    {
        if (this.SelectedShip == shipReference)
        {
            ClearOldSelection();
            return true;
        }
        return false;
    }

    public void HandleTerrainSelected(GameObject hexGameObject)
    {
        if (SelectedShip == null || !PlayersTurn) return;

        HexData selectedHex = hexGameObject.GetComponent<HexData>();

        if (HandelHexOutOfRange(selectedHex.Grid) || HandleSelectedHexIsShipHex(selectedHex.Grid)) return;

        HandleTargetHexSelected(selectedHex);
    }
    

    private void PrepareShipForMovement(Ship shipReference)
    {
        if (this.SelectedShip != null)
        {
            ClearOldSelection();
        }

        this.SelectedShip = shipReference;
        this.SelectedShip.Select();
        MovementScript.ShowRange(this.SelectedShip, this.HexGridScript);
    }

    private void ClearOldSelection()
    {
        PreviouslySelectedHex = null;
        this.SelectedShip.Deselect();
        MovementScript.HideRange(this.HexGridScript);
        this.SelectedShip = null;
    }

    private void HandleTargetHexSelected(HexData selectedHex)
    {
        if (PreviouslySelectedHex == null || PreviouslySelectedHex != selectedHex)
        {
            PreviouslySelectedHex = selectedHex;
            MovementScript.ShowPath(selectedHex.Grid, this.HexGridScript);
        }
        else
        {
            MovementScript.MoveShip(SelectedShip,this.HexGridScript);
            PlayersTurn = false;
            SelectedShip.MovementFinished += ResetTurn;
            ClearOldSelection();
        }
    }
    
    private bool HandleSelectedHexIsShipHex(Vector2Int hexPosition)
    {
        if (hexPosition != HexGridScript.GetClosestHex(SelectedShip.transform.position)) return false;
        
            SelectedShip.Deselect();
            ClearOldSelection();
            
            return true;
    }
    
    private bool HandelHexOutOfRange(Vector2Int hexPosition)
    {
        if (MovementScript.IsHexInRange(hexPosition)) return false;
        Debug.Log("Hex Out Of Range!");
        return true;
    }

    private void ResetTurn(Ship selectedShip)
    {
        selectedShip.MovementFinished -= ResetTurn;
        PlayersTurn = true;
    }
}

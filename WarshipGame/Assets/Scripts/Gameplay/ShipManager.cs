using System;
using UnityEngine;

/// <summary>
/// This script handles manages all the Ships and parses data to the Ships
/// like movement data and grid data.
/// </summary>
public class ShipManager : MonoBehaviour
{
    [SerializeField] private HexGrid hexGridScript;
    [SerializeField] private Movement movementScript;
    [SerializeField] private Ship selectedShip;

    private HexData _previouslySelectedHex;

    private bool PlayersTurn { get; set; } = true;
    

    /// <summary>
    /// Checks if its the players turn
    /// And fills in the reference to the current Ship's ship class
    /// </summary>
    /// <param name="ship"></param>
    public void HandleShipSelected(GameObject ship)
    {
        if (!PlayersTurn) return;

        Ship shipReference = ship.GetComponent<Ship>();

        if (CheckIfTheSameShipIsSelected(shipReference)) return;
        
        PrepareShipForMovement(shipReference);
    }

    /// <summary>
    /// Checks if the player tried to select the same ship twice
    /// </summary>
    /// <param name="shipReference"></param>
    /// <returns></returns>
    private bool CheckIfTheSameShipIsSelected(Ship shipReference)
    {
        if (selectedShip != shipReference) return false;
        ClearOldSelection();
        return true;
    }
    
    /// <summary>
    /// Checks if a ship is selected and if its the players turn
    /// And fills in the reference of the selected Tile
    /// </summary>
    /// <param name="hexGameObject"></param>
    public void HandleTerrainSelected(GameObject hexGameObject)
    {
        if (selectedShip == null || !PlayersTurn) return;

        HexData selectedHex = hexGameObject.GetComponent<HexData>();

        if (HandelHexOutOfRange(selectedHex.Grid) || HandleSelectedHexIsShipHex(selectedHex.Grid)) return;

        HandleTargetHexSelected(selectedHex);
    }
    
    /// <summary>
    /// Clears the Range of the ships if its not selected
    /// And draws the range if the ship is selected
    /// </summary>
    /// <param name="shipReference"></param>
    private void PrepareShipForMovement(Ship shipReference)
    {
        if (selectedShip != null)
        {
            ClearOldSelection();
        }

        selectedShip = shipReference;
        selectedShip.Select();
        movementScript.ShowRange(selectedShip, hexGridScript);
    }

    /// <summary>
    /// Hides the range of the previous turn
    /// And empties the previouslySelectedHex variable for next turn
    /// </summary>
    private void ClearOldSelection()
    {
        _previouslySelectedHex = null;
        selectedShip.Deselect();
        movementScript.HideRange(hexGridScript);
        selectedShip = null;
    }

    /// <summary>
    /// Shows the path of the selected hex to Ship
    /// If the hex is pressed again invoke the Ship movement
    /// </summary>
    /// <param name="selectedHex"></param>
    private void HandleTargetHexSelected(HexData selectedHex)
    {
        if (_previouslySelectedHex == null || _previouslySelectedHex != selectedHex)
        {
            _previouslySelectedHex = selectedHex;
            movementScript.ShowPath(selectedHex.Grid, hexGridScript);
            return;
        }

        movementScript.MoveShip(selectedShip,hexGridScript); 
        PlayersTurn = false;
        selectedShip.MovementFinished += ResetTurn;
        ClearOldSelection();
    }
    
    /// <summary>
    /// If you select a ship and you then click the hex the ship is standing on
    /// this function will prevent an error and just deselect the ship
    /// </summary>
    /// <param name="hexPosition"></param>
    /// <returns></returns>
    private bool HandleSelectedHexIsShipHex(Vector2Int hexPosition)
    {
        if (hexPosition != hexGridScript.GetClosestHex(selectedShip.transform.position)) return false;
        
        ClearOldSelection();
            
        return true;
    }
    
    /// <summary>
    /// If the selected hex is not inside the movement range of the currently selected ship it will
    /// deselect the selected Ship.
    /// </summary>
    /// <param name="hexPosition"></param>
    /// <returns></returns>
    private bool HandelHexOutOfRange(Vector2Int hexPosition)
    {
        if (movementScript.IsHexInRange(hexPosition)) return false;

        selectedShip.Deselect();
        ClearOldSelection();
        return true;
    }

    /// <summary>
    /// Resets the players Turn
    /// [should be handled later by a separate turn handler for the enemies ships ;)]
    /// </summary>
    /// <param name="selectedShip"></param>
    private void ResetTurn(Ship selectedShip)
    {
        selectedShip.MovementFinished -= ResetTurn;
        PlayersTurn = true;
    }
}

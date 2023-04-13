using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Calculates the range of the character, the cost of the path and the possibility of the range and path
/// </summary>
public class GraphSearch
{
    //Breadth First Search Result*
    public static BFSResult BFSGetRange(HexGrid hexGrid, Vector2Int startPoint, int movementPoints)
    {
        Dictionary<Vector2Int, Vector2Int?> visitedHexes = new Dictionary<Vector2Int, Vector2Int?>();
        Dictionary<Vector2Int, int> rangeCost = new Dictionary<Vector2Int, int>();
        Queue<Vector2Int> hexesToVisitQueue = new Queue<Vector2Int>();

        hexesToVisitQueue.Enqueue(startPoint);
        rangeCost.Add(startPoint, 0);
        visitedHexes.Add(startPoint, null);

        //as long as there is a queue, this will work trough the queue
        while (hexesToVisitQueue.Count > 0)
        {
            Vector2Int currentHex = hexesToVisitQueue.Dequeue();

            //Gets the neighbour hexes position from the HexGrid script
            foreach (Vector2Int neighbourPosition in hexGrid.GetNeighboursFor(currentHex))
            {
                if (hexGrid.GetTileAt(neighbourPosition).IsObstacle()) continue;

                int hexCost = hexGrid.GetTileAt(neighbourPosition).GetType();
                int currentCost = rangeCost[currentHex];
                int newCost = currentCost + hexCost;
                
                if (newCost > movementPoints) continue;
                
                if (!visitedHexes.ContainsKey(neighbourPosition))
                {
                    visitedHexes[neighbourPosition] = currentHex;
                    rangeCost[neighbourPosition] = newCost;
                    hexesToVisitQueue.Enqueue(neighbourPosition);
                }
                else if (rangeCost[neighbourPosition] > newCost)
                {
                    rangeCost[neighbourPosition] = newCost;
                    visitedHexes[neighbourPosition] = currentHex;
                }
            }
        }

        return new BFSResult {VisitedHexesDict = visitedHexes};
    }
    
    /// <summary>
    /// Makes a list of all hexes for the route but skips the first hex tile as you are standing on that so it does not have to be
    /// calculated with the route.
    /// </summary>
    /// <param name="current"></param>
    /// <param name="visitedHexesDict"></param>
    /// <returns></returns>
    public static List<Vector2Int> GeneratePathBFS(Vector2Int current, Dictionary<Vector2Int, Vector2Int?> visitedHexesDict)
    {
        List<Vector2Int> path = new List<Vector2Int>();
        path.Add(current);
        while (visitedHexesDict[current] != null)
        {
            path.Add(visitedHexesDict[current].Value);
            current = visitedHexesDict[current].Value;
        }
        path.Reverse();
        return path.Skip(1).ToList();
    }
}

/// <summary>
/// Holds hexes the Ship has already visited
/// </summary>
public struct BFSResult
{
    public Dictionary<Vector2Int, Vector2Int?> VisitedHexesDict;

    public List<Vector2Int> GetPathTo(Vector2Int destination)
    {
        return !VisitedHexesDict.ContainsKey(destination) ? new List<Vector2Int>() : GraphSearch.GeneratePathBFS(destination, VisitedHexesDict);
    }

    /// <summary>
    /// Returns the key of the dictionary if the hex tile is in range of the Ship
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public bool IsHexPositionInRange(Vector2Int position)
    {
        return VisitedHexesDict.ContainsKey(position);
    }

    public IEnumerable<Vector2Int> GetRangePositions() => VisitedHexesDict.Keys;
}



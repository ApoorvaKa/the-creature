using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SimpleRandomWalkDungeonGenerator : AbstractDungeonGenerator
{   

    [SerializeField]
    private int iterations = 10;

    public int walkLength = 10;
    public bool startRandomlyEachIteration = true;

    protected override void RunProceduralGeneration(){
        HashSet<Vector2Int> floorPositions = RunRandomWalk();
        tileMapVisualizer.Clear();
        tileMapVisualizer.VisualizeFloor(floorPositions);
        // foreach (var position in floorPositions)
        // {
        //     Debug.Log(position);
        // }
    }

    protected HashSet<Vector2Int> RunRandomWalk(){
        var currentPosition = startPosition;
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        
        for (int i = 0; i < iterations; i++)
        {
            var path = ProceduralDungeonAlgorithms.SimpleRandomWalk(currentPosition, walkLength);
            floorPositions.UnionWith(path);
            
        }
        return floorPositions;
    }
}

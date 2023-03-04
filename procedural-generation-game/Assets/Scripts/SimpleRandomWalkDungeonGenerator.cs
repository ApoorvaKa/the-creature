using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SimpleRandomWalkDungeonGenerator : MonoBehaviour
{   
    [SerializeField]
    protected Vector2Int startPosition = Vector2Int.zero;

    [SerializeField]
    private int iterations = 10;

    public int walkLength = 10;
    public bool startRandomlyEachIteration = true;

    public TilemapVisualizer tilemapVisualizer;

    public void RunProceduralGeneration(){
        HashSet<Vector2Int> floorPositions = RunRandomWalk();
        tilemapVisualizer.Clear();
        tilemapVisualizer.VisualizeFloor(floorPositions);
        foreach (var position in floorPositions)
        {
            Debug.Log(position);
        }
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

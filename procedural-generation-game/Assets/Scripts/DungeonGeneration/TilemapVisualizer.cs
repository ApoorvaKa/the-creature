using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapVisualizer : MonoBehaviour
{
    public Tilemap floorTileMap;
    public TileBase floorTile;

    public Tilemap wallTileMap;
    public TileBase wallTile;

    public void VisualizeFloor(IEnumerable<Vector2Int> floorPositions){
        PaintTiles(floorPositions, floorTileMap, floorTile);
    }

    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile){
        
        foreach (var position in positions)
        {
            PaintSingleTile(tilemap, tile, position);
        }
    }

    private void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position){
        var tilePosition = tilemap.WorldToCell((Vector3Int)position);
        tilemap.SetTile(tilePosition, tile);
    }

    public void PaintSingleBasicWall(Vector2Int position){
        PaintSingleTile(wallTileMap, wallTile, position);
    }


    public void Clear(){
        floorTileMap.ClearAllTiles();
        wallTileMap.ClearAllTiles();
    }
}

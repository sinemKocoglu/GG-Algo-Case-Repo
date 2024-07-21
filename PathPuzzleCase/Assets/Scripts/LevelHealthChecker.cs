using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class LevelHealthChecker : MonoBehaviour{

    [SerializeField] private Level level;

    public bool CheckHealth(){

        if(level.TilesList.Count==1){
            return SingleTileLevelChecker(level.StartingTile, level.StartingPoint);
        }

        HashSet<Tile> visited = new HashSet<Tile>();  // visited tiles

        Queue<(Tile, int)> queue = new Queue<(Tile, int)>();
        queue.Enqueue((level.StartingTile, level.StartingPoint));

        while (queue.Count > 0){

            (Tile currentTile, int currentPoint) = queue.Dequeue();

            if (IsWinArea(currentTile, currentPoint)){
                Debug.Log(level.levelName+" is SOLVABLE.");
                return true;
            }

            visited.Add(currentTile);
            //Debug.Log("Tile "+currentTile.tileIndex +" is visited");

            for (int i = 0; i < 4; i++){ // 3 rotations (90, 180, 270)

                RotateTile(currentTile);

                List<(Tile Tile, int EntryPoint)> connectedTiles = GetConnectedTiles(currentTile, currentPoint);

                if(connectedTiles.Count==0){
                    continue;
                }
                
                foreach (var neighbor in connectedTiles){

                    if (!visited.Contains(neighbor.Tile)){
                        queue.Enqueue((neighbor.Tile, neighbor.EntryPoint));
                    }
                }
            }
        }

        Debug.Log(level.levelName + " is UNSOLVABLE!");
        return false;
    }

    private void RotateTile(Tile tile){

        // point indexes of paths are updated according to the rotation.
        // for example, (1,3) becomes (3,5)
        foreach (var path in tile.TilePathDataList){ 
            path.PointX = (path.PointX + 2) % 8;
            path.PointY = (path.PointY + 2) % 8;
        }
        //Debug.Log("Tile " + tile.tileIndex + " is rotated.");
    }

    // checks and get the neigbouring tiles connected to the current tile with a path.
    // Because the paths(edges) are not directed, it finds the tiles in both ends (point x and y)
    private List<(Tile Tile, int EntryPoint)> GetConnectedTiles(Tile tile, int entryPoint){

        List<(Tile, int)> connectedTiles = new List<(Tile, int)>();

        foreach (var path in tile.TilePathDataList){

            if (path.PointX == entryPoint){

                Tile neighbor= GetNeigbourAt(tile, path.PointY);
                if(neighbor!=null){
                    connectedTiles.Add((neighbor, path.PointY));
                }

            }else if (path.PointY == entryPoint){

                Tile neighbor= GetNeigbourAt(tile, path.PointX);
                if(neighbor!=null){
                    connectedTiles.Add((neighbor, path.PointX));
                }
            }
        }
        
        return connectedTiles;
    }

    // getting neighboring tile based on the point index of current tile
    private Tile GetNeigbourAt(Tile tile, int point){

        int tileColumn = tile.tileIndex % level.ColumnCount;

        if(!IsWinArea(tile,point) && (point==0 || point==1)){ // Upper neighbor

            int neigborIndex = tile.tileIndex+level.ColumnCount;
            return level.TilesList.FirstOrDefault(t => t.tileIndex == neigborIndex);
        
        }
        if(!IsLoseArea(tile, point)){

            List<Tile> tileList = level.TilesList;

            if(point==2 || point==3){ // Right neighbor

                int neigborIndex = tile.tileIndex+1;
                return tileList.FirstOrDefault(t => t.tileIndex == neigborIndex);

            }else if(point==4 || point==5){ // Bottom neighbor

                int neigborIndex = tile.tileIndex-level.ColumnCount;
                return tileList.FirstOrDefault(t => t.tileIndex == neigborIndex);

            }else if(point==6 || point==7){ // Left neighbour

                int neigborIndex = tile.tileIndex-1;
                return tileList.FirstOrDefault(t => t.tileIndex == neigborIndex);

            }else{
                return null;
            }

        }else{
            return null;
        }
    }

    private bool IsWinArea(Tile tile, int point){

        int tileRow;
        if(level.RowCount==1){
            tileRow=1;
        }else{
            tileRow = (tile.tileIndex / level.ColumnCount)+1; // row indexes starts from 1.
        }

        if(tileRow==level.RowCount && (point==0 || point==1)){
            return true;
        }

        return false;
    }

    private bool IsLoseArea(Tile tile, int point){
        int tileRow;
        int tileColumn;

        if(level.RowCount==1){
            tileRow=1;
            tileColumn = tile.tileIndex;
        }else{
            tileRow = (tile.tileIndex / level.ColumnCount)+1; // row and column indexes starts from 1.
            tileColumn = tile.tileIndex % level.ColumnCount;
        }

        if(tileColumn==level.ColumnCount && (point==2 || point==3)){ // right lose area
            return true;
        }
        if(tileColumn == 1 && (point==6 || point==7)){ // left lose area
            return true;
        }
        if(tileRow==1 && (point==4 || point==5)){ // bottom lose area
            return true;
        }

        return false;
    }

    private bool SingleTileLevelChecker(Tile tile, int entryPoint){
        for (int i = 0; i < 4; i++){ // 3 rotations (90, 180, 270)
            
            RotateTile(tile);
            foreach (var path in tile.TilePathDataList){

                if (path.PointX == entryPoint && (path.PointY==0 || path.PointY==1)){
                    Debug.Log(level.levelName + " is SOLVABLE!");
                    return true;

                }
                if (path.PointY == entryPoint && (path.PointX==0 || path.PointX==1)){
                    Debug.Log(level.levelName + " is SOLVABLE!");
                    return true;
                }
            }
        }
        Debug.Log(level.levelName + " is UNSOLVABLE!");
        return false;
    }

    // used in testing.
    public Level Level{ 
        get{return level;}
        set{level = value;}
     }

}
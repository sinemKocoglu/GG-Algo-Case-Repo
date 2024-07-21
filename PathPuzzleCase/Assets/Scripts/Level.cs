using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    /// <summary>
    /// starting tile of the level and its starting point
    /// </summary>
    [SerializeField] private Tile _startingTile;
    [SerializeField] private int _startingPoint;
    [SerializeField] private int _rowCount;
    [SerializeField] private int _columnCount;
    [SerializeField] private List<Tile> _tilesList;
    [SerializeField] public string levelName;

    public Tile StartingTile{
        get{return _startingTile;}
        set{_startingTile=value;}
    } 

    public int StartingPoint{ 
        get{return _startingPoint;}
        set{_startingPoint=value;}
     }

    public int RowCount{ 
        get{return _rowCount;}
        set{_rowCount=value;}
     }
    
    public int ColumnCount{ 
        get{return _columnCount;}
        set{_columnCount=value;}
    }

    public List<Tile> TilesList{ 
        get{return _tilesList;}
        set{_tilesList=value;}
    }

}
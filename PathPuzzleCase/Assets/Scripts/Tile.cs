using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Each tile in level.
/// </summary>
public class Tile : MonoBehaviour
{
    [SerializeField] private List<TilePathData> _tilePathDataList;
    [SerializeField] public int tileIndex; // tile index and tile name should be compatible!

    public List<TilePathData> TilePathDataList{ 
        get{return _tilePathDataList;}
        set{_tilePathDataList=value;}
    }

}
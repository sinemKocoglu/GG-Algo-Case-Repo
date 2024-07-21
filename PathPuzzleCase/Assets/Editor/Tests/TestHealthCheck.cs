using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;
using System.Collections.Generic;

[TestFixture]
public class TestHealthCheck{

    private GameObject testGameObject;
    private LevelHealthChecker levelHealthChecker;
    private GameObject testLevelObject;
    private Level testLevel;

    [SetUp]
    public void SetUp(){
        testGameObject = new GameObject("Test GameObject");
        levelHealthChecker = testGameObject.AddComponent<LevelHealthChecker>();
        
        testLevelObject = new GameObject("Level Object");
        testLevel = testLevelObject.AddComponent<Level>();   
    }

    [TearDown]
    public void TearDown(){
        // Clean up after each test
        if (testGameObject != null){
            Object.DestroyImmediate(testGameObject);
        }
        if (testLevel != null && testLevel.gameObject != null){
            Object.DestroyImmediate(testLevel.gameObject); 
        }
    }

    [Test]
    public void TestCase1(){  //dead-end
        List<Tile> tileList = new List<Tile>();

        tileList.Add(CreateTile(1, new List<TilePathData> { CreateTilePathData(4, 1) }));
        tileList.Add(CreateTile(2, new List<TilePathData> { }));
       
        levelHealthChecker.Level = CreateLevel("Test Level 1",tileList[1], 4, 2, 1, tileList);

        bool result = levelHealthChecker.CheckHealth();

        Assert.IsFalse(levelHealthChecker.CheckHealth());
        
    }

    [Test]
    public void TestCase2(){   // lose area
        List<Tile> tileList = new List<Tile>();

        tileList.Add(CreateTile(1, new List<TilePathData> { CreateTilePathData(4, 1)}));
        tileList.Add(CreateTile(2, new List<TilePathData> { CreateTilePathData(4, 3)}));
        
        levelHealthChecker.Level = CreateLevel("Test Level 2",tileList[0], 4, 2, 1, tileList);
        bool result = levelHealthChecker.CheckHealth();
        Assert.IsFalse(levelHealthChecker.CheckHealth());
    
    }

    [Test]
    public void TestCase3(){   //win area
        List<Tile> tileList = new List<Tile>();

        tileList.Add(CreateTile(1, new List<TilePathData> { CreateTilePathData(4, 7), CreateTilePathData(5, 6) }));
        tileList.Add(CreateTile(2, new List<TilePathData> { CreateTilePathData(5, 6), CreateTilePathData(1, 2) }));
        tileList.Add(CreateTile(3, new List<TilePathData> { CreateTilePathData(5, 6), CreateTilePathData(3, 1) }));
        tileList.Add(CreateTile(4, new List<TilePathData> { CreateTilePathData(4, 6), CreateTilePathData(2, 3) }));
        
        levelHealthChecker.Level = CreateLevel("Test Level 3",tileList[0], 6, 2, 2, tileList);

        bool result = levelHealthChecker.CheckHealth();

        Assert.IsTrue(levelHealthChecker.CheckHealth());
    }

    [Test]
    public void TestCase4(){ // 1 tile with all dead-ends
        List<Tile> tileList = new List<Tile>();

        tileList.Add(CreateTile(1, new List<TilePathData> { }));
        
        levelHealthChecker.Level = CreateLevel("Test Level 4",tileList[0], 7, 1, 1, tileList);

        bool result = levelHealthChecker.CheckHealth();

        Assert.IsFalse(levelHealthChecker.CheckHealth());
    }

    [Test]
    public void TestCase5(){  
        List<Tile> tileList = new List<Tile>();

        tileList.Add(CreateTile(1, new List<TilePathData> { CreateTilePathData(0, 2), CreateTilePathData(1,3) }));
        tileList.Add(CreateTile(2, new List<TilePathData> { CreateTilePathData(0, 1), CreateTilePathData(2,3) }));
        tileList.Add(CreateTile(3, new List<TilePathData> {  CreateTilePathData(0, 1), CreateTilePathData(2,3)  }));
        tileList.Add(CreateTile(4, new List<TilePathData> {  CreateTilePathData(0, 1), CreateTilePathData(2,3)  }));
        tileList.Add(CreateTile(5, new List<TilePathData> {  CreateTilePathData(0, 1), CreateTilePathData(2,3)  }));
        tileList.Add(CreateTile(6, new List<TilePathData> {  CreateTilePathData(0, 1), CreateTilePathData(2,3)  }));
        tileList.Add(CreateTile(7, new List<TilePathData> {  CreateTilePathData(0, 1), CreateTilePathData(2,3)  }));
        tileList.Add(CreateTile(8, new List<TilePathData> {  CreateTilePathData(0, 1), CreateTilePathData(2,3)  }));
        tileList.Add(CreateTile(9, new List<TilePathData> {  CreateTilePathData(0, 1), CreateTilePathData(2,3)  }));
        tileList.Add(CreateTile(10, new List<TilePathData> {  CreateTilePathData(0, 1), CreateTilePathData(2,3)  }));
        tileList.Add(CreateTile(11, new List<TilePathData> {  CreateTilePathData(0, 1), CreateTilePathData(2,3)  }));
        tileList.Add(CreateTile(12, new List<TilePathData> {  CreateTilePathData(0, 1), CreateTilePathData(2,3)  }));
        
        levelHealthChecker.Level = CreateLevel("Test Level 5",tileList[0], 0, 4, 3, tileList);

        bool result = levelHealthChecker.CheckHealth();

        Assert.IsFalse(levelHealthChecker.CheckHealth());
    }

    [Test]
    public void TestCase6(){  //level with a single tile
        List<Tile> tileList = new List<Tile>();

        tileList.Add(CreateTile(1, new List<TilePathData> { CreateTilePathData(6, 3)}));
        
        levelHealthChecker.Level = CreateLevel("Test Level 6",tileList[0], 5, 1, 1, tileList);

        bool result = levelHealthChecker.CheckHealth();

        Assert.IsTrue(levelHealthChecker.CheckHealth());
    }

    private Tile CreateTile(int index, List<TilePathData> list){
        Tile tile = new GameObject("Tile " + index).AddComponent<Tile>();
        tile.tileIndex=index;
        tile.TilePathDataList=list;
        return tile;
    }

    private Level CreateLevel(string name, Tile startingTile, int startingPoint, int rowCount, int columnCount, List<Tile> tileList){
        testLevel.levelName = name;
        testLevel.StartingTile = startingTile;
        testLevel.StartingPoint = startingPoint;
        testLevel.RowCount = rowCount;
        testLevel.ColumnCount = columnCount;
        testLevel.TilesList = tileList;
        return testLevel;
    }

    
    private TilePathData CreateTilePathData(int x, int y){
        TilePathData path = new TilePathData();
        path.PointX=x;
        path.PointY=y;
        return path;
    }
}
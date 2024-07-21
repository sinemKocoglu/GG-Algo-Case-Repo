# PathPuzzleCase
  Sinem Kocoglu

* CheckHealth method works with the Case Level by running the game on unity. 
  I created HealthCheckCaller.cs and attached to LevelHealtChecker object as a component to do it.

* CheckHealthMethod returns true, if the level is solvable. False, if unsolvable.
  Result is also printed on console.

* CheckHealth is implemented by using BFS (Breadth-First Search) algorithm.
  It uses a queue and explores all neighbors before moving to the next tile.

* To test the edge cases, TestHealthCheck.cs is implemented. 
  It is in the Assets/Editor/Tests.
  It has 6 test cases including solvable and unsolvable levels.
  TestCase1: Unsolvable level ending up with dead-end
  TestCase2: Unsolvable level ending up in lose area
  TestCase3: Solvable level 
  TestCase4: Unsolvable level with a single tile having all dead-ends
  TestCase5: Random unsolvable level  
  TestCase6: Solvable level with a single tile

* Properties in Level.cs, Tile.cs, and TilePathData.cs modified accordingly. 

* The logic is that it starts with given starting tile and starting point.
  After checking if the point opens to win area, it finds the tiles connected by a path on the current tile with the GetConnectedTiles method.
  It goes over each points(1..7) and finds neighbor tile indexes according the their placement to the current tile.
  Total count of rows and columns are kept in Level.cs. The orders are decided according to the tile index order:
<br>
    row4                            
    row3                            
    row2                          
  row1/col1 col2 col3              

* The code can check for levels having different number of tiles as long as row count and column count are provided in Level.
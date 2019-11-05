using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding {
    [HideInInspector]
    public GameObject GameController;
    public Grid grid;
    public Transform StartPosition;
    public Transform TargetPosition;

    /*private void Awake()
    {
        if (gameObject.name == "GameController")
        {
            grid = GetComponent<Grid>();
        }
        else grid = GameController.GetComponent<Grid>();
        //grid = GetComponent<Grid>();
    }*/

    /*private void Start()
    {
        if (gameObject.name == "GameController")
        {
            grid = GetComponent<Grid>();
        }
        else grid = GameController.GetComponent<Grid>();
    }*/

    private void Update()
    {/*
        if (GameController == null)
        {
            if (gameObject.name == "GameController")
            {
                grid = GetComponent<Grid>();
            }
            //else grid = owner.grid;
        }

        if (TargetPosition != null)
        {
            FindPath(StartPosition.position, TargetPosition.position);
        }
*/
    }

    public void FindPath(Transform a_StartPos ,Transform a_TargetPos)
    {
        FindPath(a_StartPos.position, a_TargetPos.position);
    }


    public List<Tile> FindPath(Vector3 a_StartPos, Vector3 a_TargetPos)
    {
        Tile StartTile = TileManager.Instance.GetTileFromWorldPosition(a_StartPos);
        Tile TargetTile = TileManager.Instance.GetTileFromWorldPosition(a_TargetPos);

        List<Tile> OpenList = new List<Tile>();
        HashSet<Tile> ClosedList = new HashSet<Tile>();

        OpenList.Add(StartTile);

        while (OpenList.Count > 0)
        {
            //Debug.Log("Loop Started");
            Tile CurrentTile = OpenList[0];
            //Debug.Log("Loop Length: " + OpenList.Count);

            

            for (int i = 0; i < OpenList.Count; i++)
            {
                if (OpenList[i].FCost < CurrentTile.FCost || (OpenList[i].FCost == CurrentTile.FCost && OpenList[i].hCost < CurrentTile.hCost))
                {
                    CurrentTile = OpenList[i];
                }
            }
            //Debug.Log("Set Current Tile");
            //Debug.Log("Current Tile: " + CurrentTile.name);

            OpenList.Remove(CurrentTile);
            ClosedList.Add(CurrentTile);

            if (CurrentTile == TargetTile)
            {
                return GetFinalPath(StartTile, TargetTile);
            }

            /*if (CurrentTile.neighborTiles == null)
            {
                 CurrentTile.neighborTiles = TileManager.Instance.GetNeighborTiles(CurrentTile);
            }*/

            List<Tile> neighborTiles = CurrentTile.neighborTiles;

            //Debug.Log("Checking Neighbors");
            for (int i = 0; i < neighborTiles.Count; i++)
            {
                Tile NeighborTile = neighborTiles[i];
                //Debug.Log("Tile: " + neighborTiles[i].cellPosition);
                //Debug.Log("OpenList Count: " + OpenList.Count); 

                if (ClosedList.Contains(NeighborTile))
                {
                    //OpenList.Remove(NeighborTile);
                    continue;
                }
                int MoveCost = CurrentTile.gCost + GetManhattenDistance(CurrentTile, NeighborTile);

                if(MoveCost < NeighborTile.gCost || !OpenList.Contains(NeighborTile))
                {
                    NeighborTile.gCost = MoveCost;
                    NeighborTile.hCost = GetManhattenDistance(NeighborTile, TargetTile);
                    NeighborTile.Parent = CurrentTile;

                    if (!OpenList.Contains(NeighborTile))
                    {
                        OpenList.Add(NeighborTile);
                    }
                }
            }
        }

        return null;
    }

    private List<Tile> GetFinalPath(Tile a_StartingTile, Tile a_EndTile)
    {
        List<Tile> FinalPath = new List<Tile>();
        Tile CurrentTile = a_EndTile;

        while (CurrentTile != a_StartingTile)
        {
            FinalPath.Add(CurrentTile);
            CurrentTile = CurrentTile.Parent;
        }

        FinalPath.Reverse();

        return FinalPath;
    }



    private int GetManhattenDistance(Tile a_tileA, Tile a_tileB)
    {
        /*Debug.Log("a_TileA" + a_tileA == null ? true : false);
        Debug.Log("a_TileB" + a_tileB == null ? true : false);
        Debug.Log("A cellpos" + a_tileA.cellPosition);
        Debug.Log("B cellpos" + a_tileB.cellPosition);*/

        int ix = Mathf.Abs(a_tileA.cellPosition.x - a_tileB.cellPosition.x);
        int iy = Mathf.Abs(a_tileA.cellPosition.y - a_tileB.cellPosition.y);

        return ix + iy;
    }
}

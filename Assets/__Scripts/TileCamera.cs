using System.Collections;
using System.Globalization;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class TileSwap
{
    public int        tileNum;
    public GameObject prefabSwap;
    public GameObject guaranteedItemDrop;
    public int        overrideTileNum = -1;
}

public class TileCamera : MonoBehaviour
{
    static private int      W, H;
    static private int[,]   MAP;
    static public Sprite[]  SPRITES;
    static public Transform TILE_ANCHOR;
    static public Tile[,]   TILES;
    static public string    COLLISIONS;
    

    [Header("Set in Inspector")]
    public TextAsset      mapData;
    public Texture2D      mapTiles;
    public TextAsset      mapCollisions;
    public Tile           prefabTile;
    public int            defaultTileNum;
    public List<TileSwap> tileSwaps;
    
    private Dictionary<int, TileSwap> _tileSwapDict;
    private Transform                 _enemyAnchor;
    private Transform                 _itemAnchor;
    

    private void Awake()
    {
        COLLISIONS = Utils.RemoveLineEndings(mapCollisions.text);
        _enemyAnchor = (new GameObject("Enemy Anchor")).transform;
        _itemAnchor = (new GameObject("Item Anchor")).transform;

        PrepareTileSwapDict();
        LoadMap();
    }

    private void PrepareTileSwapDict()
    {
        _tileSwapDict = new Dictionary<int, TileSwap>();
        foreach (TileSwap ts in tileSwaps)
        {
            _tileSwapDict.Add(ts.tileNum, ts);
        }
    }

    public void LoadMap()
    {
        GameObject go = new GameObject("TILE_ANCHOR");
        TILE_ANCHOR = go.transform;

        SPRITES = Resources.LoadAll<Sprite>(mapTiles.name);

        string[] lines = mapData.text.Split('\n');
        H = lines.Length;
        string[] tileNums = lines[0].Split(' ');
        W = tileNums.Length;

        MAP = new int[W, H];
        for (int j = 0; j < H; j++)
        {
            tileNums = lines[j].Split(' ');
            for (int i = 0; i < W; i++)
            {
                tileNums[i] = tileNums[i].Replace("\r", string.Empty);

                if (tileNums[i] == "..")
                {
                    MAP[i, j] = 0;
                }
                else
                {
                    MAP[i, j] = int.Parse(tileNums[i], NumberStyles.HexNumber);
                }

                CheckTileSwaps(i, j);
            }
        }

        ShowMap();
    }

    private void CheckTileSwaps(int i, int j)
    {
        int tNum = GET_MAP(i, j);
        if (!_tileSwapDict.ContainsKey(tNum)) return;

        TileSwap ts = _tileSwapDict[tNum];
        if (ts.prefabSwap != null)
        {
            GameObject go = Instantiate<GameObject>(ts.prefabSwap);
            Enemy e = go.GetComponent<Enemy>();

            if (e != null)
            {
                go.transform.SetParent(_enemyAnchor);
            }
            else
            {
                go.transform.SetParent(_itemAnchor);
            }

            go.transform.position = new Vector3(i, j, 0);
            if (ts.guaranteedItemDrop != null)
            {
                if (e != null) e.guaranteedItemDrop = ts.guaranteedItemDrop;
            }
        }

        if (ts.overrideTileNum == -1)
        {
            SET_MAP(i, j, defaultTileNum);
        }
        else
        {
            SET_MAP(i, j, ts.overrideTileNum);
        }
    }

    private void ShowMap()
    {
        TILES = new Tile[W, H];

        for (int j = 0; j < H; j++)
        {
            for (int i = 0; i < W; i++)
            {
                if (MAP[i, j] != 0)
                {
                    Tile tile = Instantiate<Tile>(prefabTile);
                    tile.transform.SetParent(TILE_ANCHOR);
                    tile.SetTile(i, j);
                    TILES[i, j] = tile;
                }
            }
        }
    }

    static public int GET_MAP(int x, int y)
    {   
        // Avoid indexOutOfRangeExceptions
        if (x < 0 || x >= W || y < 0 || y >= H)
        {
            return -1;
        }

        return MAP[x, y];
    }

    static public int GET_MAP(float x, float y)
    {
        int tX = Mathf.RoundToInt(x);
        int tY = Mathf.RoundToInt(y - 0.25f); 


        return GET_MAP(tX, tY);
    }

    static public void SET_MAP(int x, int y, int tNum)
    {
        // Avoid indexOutOfRangeExceptions
        if (x < 0 || x >= W || y < 0 || y >= H)
        {
            return;
        }

        MAP[x, y] = tNum;
    }

}

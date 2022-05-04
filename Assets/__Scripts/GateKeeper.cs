using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateKeeper : MonoBehaviour
{
    const int lockedR = 95;
    const int lockedDR = 100;
    const int lockedUR = 81;
    const int lockedL = 102;
    const int lockedDL = 101;
    const int lockedUL = 80;

    const int openR = 48;
    const int openUR = 93;
    const int openDR = 27;
    const int openL = 51;
    const int openUL = 92;
    const int openDL = 26;

    private IKeyMaster _keys;
    
    private void Awake()
    {
        _keys = GetComponent<IKeyMaster>();
    }

    private void OnCollisionStay(Collision other)
    {
        if (_keys.keyCount < 1) return;

        Tile tile = other.gameObject.GetComponent<Tile>();
        if (tile == null) return;

        int facing = _keys.GetFacing();
        Tile tile2;
        switch (tile.tileNum)
        {
            case lockedR:
                if(facing != 0) return;
                tile.SetTile(tile.x, tile.y, openR);

                break;

            case lockedUR:
                if(facing != 1) return;
                tile.SetTile(tile.x, tile.y, openUR);

                tile2 = TileCamera.TILES[tile.x - 1, tile.y];
                tile2.SetTile(tile2.x, tile2.y, openUL);

                break;

            case lockedUL:
                if(facing != 1) return;
                tile.SetTile(tile.x, tile.y, openUL);

                tile2 = TileCamera.TILES[tile.x + 1, tile.y];
                tile2.SetTile(tile2.x, tile2.y, openUR);

                break;
            
            case lockedL:
                if(facing != 2) return;
                tile.SetTile(tile.x, tile.y, openL);

                break;
            
            case lockedDL:
                if(facing != 3) return;
                tile.SetTile(tile.x, tile.y, openDL);

                tile2 = TileCamera.TILES[tile.x + 1, tile.y];
                tile2.SetTile(tile2.x, tile2.y, openDR);

                break;
            
            case lockedDR:
                if(facing != 3) return;
                tile.SetTile(tile.x, tile.y, openDR);

                tile2 = TileCamera.TILES[tile.x + 1, tile.y];
                tile2.SetTile(tile2.x, tile2.y, openDL);

                break;

            default:
                return;
        }

        _keys.keyCount--;
    }
}

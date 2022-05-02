using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFacingMover
{
    public int GetFacing();
    public float GetSpeed();
    public Vector2 GetRoomPosOnGrid(float mult = -1);

    public bool moving { get; }
    public float gridMult { get; }
    public Vector2 roomPos{ get; set; }
    public Vector2 roomNum{ get; set; }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeletos : Enemy, IFacingMover
{
    [Header("Set in Inspector: Skeletos")]
    public int speed = 2;
    public float timeThinkMin = 1f;
    public float timeThinkMax = 4f;

    [Header("Set Dynamically: Skeletos")]
    public int facing = 0;
    public float timeNextDecision = 0;

    private InRoom _inRoom;
    

    override protected void Awake()
    {
        base.Awake();

        _inRoom = GetComponent<InRoom>();
    }
    
    
    override protected void Update()
    {
        base.Update();
        if (knockback) return;
        
        if (Time.time >= timeNextDecision) DecideDirection();

        rigid.velocity = directions[facing] * speed;
    }

    private void DecideDirection()
    {
        facing = Random.Range(0, 4);
        timeNextDecision = Time.time + Random.Range(timeThinkMin, timeThinkMax);
    }

    public int GetFacing()
    {
        return facing;
    }

    public float GetSpeed()
    {
        return speed;
    }

    public Vector2 GetRoomPosOnGrid(float mult = -1)
    {
        return _inRoom.GetRoomPosOnGrid(mult);
    }
    

    public bool moving
    {
        get
        {
            return true;
        }
    }

    public float gridMult
    {
        get
        {
            return _inRoom.gridMult;
        }
    }

    public Vector2 roomPos
    {
        get
        {
            return _inRoom.roomPos;
        }
        set
        {
            _inRoom.roomPos = value;
        }
    }
    
    public Vector2 roomNum
    {
        get
        {
            return _inRoom.roomNum;
        }
        set
        {
            _inRoom.roomNum = value;
        }
    }
}

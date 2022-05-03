using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowDray : MonoBehaviour
{
    static public bool TRANSITIONING = false;
    
    [Header("Set in Inspector")]
    public InRoom drayInRoom;
    public float  transitionTime = 0.5f;
    
    private Vector3 _p0, _p1;
    private InRoom  _inRoom;
    private float   _transitionStart;

    private void Awake()
    {
        _inRoom = GetComponent<InRoom>();
    }

    private void Update()
    {
        if (TRANSITIONING)
        {
            float u = (Time.time - _transitionStart) / transitionTime;
            if (u >= 1)
            {
                u = 1;
                TRANSITIONING = false;
            }

            transform.position = (1 - u) * _p0 + u * _p1;
        }
        else
        {
            if (drayInRoom.roomNum != _inRoom.roomNum) 
                TransitionTo(drayInRoom.roomNum);
        }
    }

    private void TransitionTo(Vector2 rm)
    {
        _p0 = transform.position;
        _inRoom.roomNum = rm;
        _p1 = transform.position + (Vector3.back * 10);
        transform.position = _p1;

        _transitionStart = Time.time;
        TRANSITIONING = false;
    }
}

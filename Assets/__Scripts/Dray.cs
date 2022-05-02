using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Dray : MonoBehaviour
{
    public enum eMode
    {
        idle,
        move,
        attack,
        transition
    }
    
    [Header("Set in Inspector")]
    public float speed = 5;
    public float attackDuration = 0.25f;
    public float attackDelay = 0.5f;

    [Header("Set Dynamically")]
    public int   dirHeld = -1;
    public int   facing = 1;
    public eMode mode = eMode.idle;

    private Rigidbody _rigid;
    private Animator  _anim;

    // Mode related fields
    private float _timeAtkDone = 0;
    private float _timeAtkNext = 0;    
    private KeyCode[] _keys = new KeyCode[]{ KeyCode.RightArrow,KeyCode.UpArrow, KeyCode.LeftArrow, KeyCode.DownArrow};
    private Vector3[] _directions = new Vector3[]{ Vector3.right,Vector3.up, Vector3.left, Vector3.down};
    

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
    }   

    private void Update()
    {
        dirHeld = -1;
        
        // Handle movement input
        for (int i = 0; i < 4; i++)
        {
             if (Input.GetKey(_keys[i])) dirHeld = i;
        }

        // Handle attack input
        if (Input.GetKeyDown(KeyCode.Z) && Time.time >= _timeAtkNext)
        {
            mode = eMode.attack;
            _timeAtkDone = Time.time + attackDuration;
            _timeAtkNext = Time.time + attackDelay;
        }

        // Finish attack
        if (Time.time >= _timeAtkDone) mode = eMode.idle;

        // Handle mode management when not attacking
        if (mode != eMode.attack)
        {
            if (dirHeld == -1)
            {
                mode = eMode.idle;
            }
            else
            {
                facing = dirHeld;
                mode = eMode.move;
            }
        }
        
        // Current mode actions
        Vector3 vel = Vector3.zero;

        switch (mode)
        {
            case eMode.attack:
                _anim.CrossFade("Dray_Attack_" + facing, 0);
                _anim.speed = 0;
                break;
            
            case eMode.idle:
                _anim.CrossFade("Dray_Walk_" + facing, 0);
                _anim.speed = 0;
                break;
            
            case eMode.move:
                vel = _directions[dirHeld];
                _anim.CrossFade("Dray_Walk_" + facing, 0);
                _anim.speed = 1;
                break;
        }

        _rigid.velocity = vel * speed;
    }
}

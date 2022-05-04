using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{
    public enum eMode
    {
        none,
        gOut,
        gInMiss,
        gInHit
    }

    [Header("Set in Inspector")]
    public float grappleSpd = 10;
    public float grappleLength = 7;
    public float grappleInLength = 0.5f;
    public int   unsafeTileHealthPenalty = 2;
    public TextAsset mapGrappleable;
    
    [Header("Set Dynamically")]
    public eMode     mode = eMode.none;
    public List<int> grappleTiles;
    public List<int> unsafeTiles;
    
    private Dray         _dray;
    private Rigidbody    _rigid;
    private Animator     _anim;
    private Collider     _drayColl;
    private GameObject   _grapHead;
    private LineRenderer _grapLine;
    private Vector3      _p0, _p1;
    private int          _facing;
    
    private Vector3[] _directions = new Vector3[]{
        Vector3.right, Vector3.up, Vector3.left, Vector3.down
    };
    
    
    private void Awake()
    {
        string gTiles = mapGrappleable.text;
        gTiles = Utils.RemoveLineEndings(gTiles);

        grappleTiles = new List<int>();
        unsafeTiles = new List<int>();
        for (int i = 0; i < gTiles.Length; i++)
        {
            switch (gTiles[i])
            {
                case 'S':
                    grappleTiles.Add(i);
                    break;

                case 'X':
                    unsafeTiles.Add(i);
                    break;
            }
        }

        _dray = GetComponent<Dray>();
        _anim = GetComponent<Animator>();
        _rigid = GetComponent<Rigidbody>();
        _drayColl = GetComponent<Collider>();

        Transform trans = transform.Find("Grappler");
        _grapHead = trans.gameObject;
        _grapLine = _grapHead.GetComponent<LineRenderer>();
        _grapHead.SetActive(false);
    }   

    private void Update()
    {
        if (!_dray.hasGrappler) return;
        
        switch (mode)
        {
            case eMode.none:
                if (Input.GetKeyDown(KeyCode.X)) StartGrapple();
                break;
        }
    }

    private void FixedUpdate()
    {
        switch (mode)
        {
            case eMode.gOut:
                _p1 += _directions[_facing] * grappleSpd * Time.fixedDeltaTime;
                _grapHead.transform.position = _p1;
                _grapLine.SetPosition(1, _p1);

                int tileNum = TileCamera.GET_MAP(_p1.x, _p1.y);
                if (grappleTiles.IndexOf(tileNum) != -1)
                {
                    mode = eMode.gInHit; 
                    break;
                }

                if ((_p1 - _p0).magnitude >= grappleLength) mode = eMode.gInMiss;
                
                break;

            case eMode.gInMiss:
                float previousDotProduct = Vector3.Dot((_p0 - _p1), _directions[_facing]);

                _p1 -= _directions[_facing] * 2 * grappleSpd * Time.fixedDeltaTime;
                float dotProduct = Vector3.Dot((_p0 - _p1), _directions[_facing]);

                if(dotProduct > previousDotProduct) StopGrapple();
                
                if (dotProduct > 0)
                {
                    _grapHead.transform.position = _p1;
                    _grapLine.SetPosition(1, _p1);
                }
                else
                {
                    StopGrapple();
                }          

                break;

            case eMode.gInHit:
                float dist = grappleInLength + grappleSpd * Time.fixedDeltaTime;
                if (dist > (_p1 - _p0).magnitude)
                {
                    _p0 = _p1 - (_directions[_facing] * grappleInLength);
                    transform.position = _p0;
                    StopGrapple();
                    
                    break;
                }

                _p0 += _directions[_facing] * grappleSpd * Time.fixedDeltaTime;
                transform.position = _p0;
                _grapLine.SetPosition(0, _p0);
                _grapHead.transform.position = _p1;

                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Enemy e = other.GetComponent<Enemy>();
        if (e == null) return;

        mode = eMode.gInMiss;
    }

    private void StartGrapple()
    {
        _dray.enabled = false;
        _facing = _dray.GetFacing();
        _anim.CrossFade("Dray_Attack_" + _facing, 0);

        _drayColl.enabled = false;
        _rigid.velocity = Vector3.zero;

        _p0 = transform.position + (_directions[_facing] * 0.5f);
        _p1 = _p0;

        _grapHead.SetActive(true);
        _grapHead.transform.position = _p1;
        _grapHead.transform.rotation = Quaternion.Euler(0, 0, 90 * _facing);

        _grapLine.positionCount = 2;
        _grapLine.SetPosition(0, _p0);
        _grapLine.SetPosition(1, _p1);
        mode = eMode.gOut;
    }
    
    private void StopGrapple()
    {
        _dray.enabled = true;
        _drayColl.enabled = true;

        int tileNum = TileCamera.GET_MAP(_p0.x, _p0.y);
        if (mode == eMode.gInHit && unsafeTiles.IndexOf(tileNum) != -1)
        {
            _dray.ResetInRoom(unsafeTileHealthPenalty);
        };

        _grapHead.SetActive(false);
        mode = eMode.none;
    }

}

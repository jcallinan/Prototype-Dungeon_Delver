using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public enum eType
    {
        key,
        health,
        grappler
    }

    static public float COLLIDER_DELAY = 0.5f;
    
    [Header("Set in Inspector")]
    public eType itemType;
    
    private void Awake()
    {
        GetComponent<Collider>().enabled = false;
        Invoke("Activate", COLLIDER_DELAY);
    }

    private void Activate()
    {
        GetComponent<Collider>().enabled = true;
    }
}

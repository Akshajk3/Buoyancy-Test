using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BuoyancyObject : MonoBehaviour
{
    public Transform[] floaters;
    public float underWaterDrag = 3f;
    public float underWaterAngularDrag = 1f;

    public float airDrag = 0f;
    public float airAngularDrag = 0.05f;

    public float floatPower = 15f;

    public float waterHeight = 0f;

    Rigidbody rb;

    int floatersUnderWater;

    bool underwater;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    
    void FixedUpdate()
    {
        floatersUnderWater = 0;
        for(int i = 0; i < floaters.Length; i++)
        {
            float depth = floaters[i].position.y - waterHeight;

            if(depth < 0)
            {
                rb.AddForceAtPosition(Vector3.up * floatPower * Mathf.Abs(depth), floaters[i].position, ForceMode.Force);
                floatersUnderWater += 1;
                if(!underwater)
                {
                    underwater = true;
                    SwitchState(true);
                }
            }
        }
        
        if(underwater && floatersUnderWater == 0)
        {
            underwater = false;
            SwitchState(false);
        }
    }

    void SwitchState(bool isUnderwater)
    {
        if(isUnderwater)
        {
            rb.drag = underWaterDrag;
            rb.angularDrag = underWaterAngularDrag;
        }
        else
        {
            rb.drag = airDrag;
            rb.angularDrag = airAngularDrag;
        }
    }
}

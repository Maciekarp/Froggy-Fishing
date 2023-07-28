using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobberMovement : MonoBehaviour
{
    
    [SerializeField] private GameObject rodTip;
    [SerializeField] private Rigidbody bobberRb;
    [SerializeField] private float orthStrength = 0.1f;
    [SerializeField] private float throwForce = 1f;
    [SerializeField] private float reelSpeed = 1f;
    [SerializeField] private float castDelay = 0.5f;

    private Vector3 prevPos;
    private Vector3 push;
    public string bobberState = "reeled";

    // Used by Animator to throw the bobber
    public void StartThrow() {
        Invoke("ThrowBobber", castDelay);
    }

    private void ThrowBobber() {
        bobberState = "casting";
        bobberRb.useGravity = true;

        Vector3 throwDir = (new Vector3(-1,0,0)).normalized;
        Vector3 randomDir = Vector3.ProjectOnPlane( Random.insideUnitSphere, throwDir ).normalized; // random vector orthag
        push = (throwDir + (randomDir * orthStrength)).normalized * throwForce;
        
    }


    public void ReelIn() {
        bobberState = "reeling";
        bobberRb.useGravity = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        bobberState = "reeled";
    }

    // Update is called once per frame
    void Update()   
    {
        if(bobberState == "reeling") {
            Vector3 toTip = (rodTip.transform.position - transform.position);
            if(toTip.magnitude < 0.1f) {
                bobberState = "reeled";
            } else {
                transform.position = transform.position + (toTip.normalized * Time.deltaTime * reelSpeed);
            }
        }
        if(bobberState == "reeled") {
            if(prevPos != transform.position) {prevPos = transform.position;}
            transform.position = rodTip.transform.position;
        }
        if(bobberState == "casting") {
            bobberRb.AddForce(push* 100);
            Debug.Log(push);
            bobberState = "floating";
        }
    }
}

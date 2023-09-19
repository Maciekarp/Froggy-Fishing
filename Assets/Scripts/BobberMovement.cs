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
    [SerializeField] private float floatHeight = 10f;
    [SerializeField] private AnimationCurve floatCurve;
    [SerializeField] private AnimationCurve eatCurve;
    [SerializeField] private float biteDepth = 1f;
    [SerializeField] private float biteLength = 0.5f;
    [SerializeField] private float biteHold = 0.5f;
    private float startBite = -10f;
    private bool isTease = true;
    [SerializeField] private float loopTime = 3f;
    [SerializeField] private float floatStrength = 1f;
    private Vector3 prevPos;
    private Vector3 push;
    private float startFloatTime;
    public string bobberState = "reeled";

    [SerializeField] private GameObject debugObject;


    private bool canCatch = false;
    private bool isReeling = false;

    public bool GetCanCatch() {
        
        isReeling = true;
        if(canCatch) {
            startBite = -10f;
            
            return true;
        }
        return canCatch;
    }

    // Used by Animator to throw the bobber
    public void StartThrow() {
        Invoke("ThrowBobber", castDelay);
        isReeling = false;
    }

    public void ThrowBobber() {
        bobberState = "casting";
        bobberRb.useGravity = true;

        Vector3 throwDir = (new Vector3(-1,0,0)).normalized;
        Vector3 randomDir = Vector3.ProjectOnPlane( Random.insideUnitSphere, throwDir ).normalized; // random vector orthag
        push = (throwDir + (randomDir * orthStrength)).normalized * throwForce;
        
    }


    public void ReelIn() {
        bobberState = "reeling";
        bobberRb.drag = 0.17f;
        bobberRb.useGravity = false;
        isReeling = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        bobberState = "reeled";
    }

    // Update is called once per frame
    void Update()   
    {
        

        debugObject.SetActive(canCatch);    
        canCatch = false;
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
            //Debug.Log(push);
            bobberState = "floating";
        }
        if(bobberState == "floating") {
            // set bite time if its not in the future
            if(!isReeling && Time.time > startBite + biteLength + 1f) {
                isTease = Random.Range(0f, 10f) < 7f;
                startBite = Time.time + Random.Range(5f, 30f);
                
            }
            float biteDelta;
            if(!isTease) {
                
                if(Time.time - startBite < biteLength / 2) {
                    if(Time.time > startBite)
                    canCatch = true;
                    biteDelta = (biteDepth * eatCurve.Evaluate((Time.time - startBite ) / biteLength));
                } else if(isReeling || Time.time - startBite - biteLength / 2 < biteHold) {
                    biteDelta = -1f;
                    canCatch = true;
                } else{
                    canCatch = false;
                    biteDelta = (biteDepth * eatCurve.Evaluate((Time.time - startBite - biteHold) / biteLength));
                }
            } else {
                biteDelta = (biteDepth * eatCurve.Evaluate((Time.time - startBite ) / biteLength));
            }
            biteDelta = isTease ? 0.5f * biteDelta : biteDelta;
            if(transform.position.y < floatHeight && bobberRb.useGravity) {
                bobberRb.useGravity = false;
                bobberRb.drag = 5f;
                bobberRb.velocity = new Vector3(bobberRb.velocity.x, 0, bobberRb.velocity.z);
                startFloatTime = Time.time;
            }
            if(!bobberRb.useGravity) {
                transform.position = Vector3.Lerp(transform.position, new Vector3(
                    transform.position.x, 
                    biteDelta + floatHeight + 1 + (floatCurve.Evaluate((Time.time - startFloatTime) / loopTime) * floatStrength),
                    transform.position.z), 0.1f);
            }
        }
    }
}

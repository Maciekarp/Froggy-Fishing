using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugController : MonoBehaviour
{
    [SerializeField] private Animator anim;

    [SerializeField] private Rigidbody flyRig;
    [SerializeField] private float pullForce = 1f;

    [SerializeField] private string bugType;
    [SerializeField] private GameObject bugParent;
    [SerializeField] private GameObject bugTarget;
    [SerializeField] private float rotateSpeed = 10;
    

    // Chase target follows and rotates towards the target gameobject
    private void ChaseTarget() {
        Vector3 targetDir = (bugTarget.transform.position - transform.position).normalized;
        
        
        if((bugTarget.transform.position - transform.position).magnitude > 0.1f) {
            
            flyRig.AddForce(targetDir * pullForce);
            
            //transform.position = transform.position + targetDir * Time.deltaTime * speed;
            
            // Sets the look dir of the fly
            if(flyRig.velocity != Vector3.zero) {
                Quaternion lookRotation = Quaternion.LookRotation(flyRig.velocity, Vector3.up); 
                transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * rotateSpeed);
            }
        }
    }

    // Update is called once per frame
    void Update() {
        ChaseTarget();
    }

    public string getType() {
        return bugType;
    }

    public void killBug() {
        Destroy(bugParent);
        Destroy(this.gameObject);
    }
    public void freezeAnim() {
        anim.enabled = false;
    }
}

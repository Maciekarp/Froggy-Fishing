using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugController : MonoBehaviour
{
    [SerializeField] private Animator anim;

    [SerializeField] private string bugType;
    [SerializeField] private GameObject bugParent;
    [SerializeField] private GameObject bugTarget;
    [SerializeField] private float speed = 10;
    [SerializeField] private float rotateSpeed = 10;
    

    // Chase target follows and rotates towards the target gameobject
    private void ChaseTarget() {
        Vector3 targetDir = (bugTarget.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(targetDir, Vector3.up);
        if((bugTarget.transform.position - transform.position).magnitude > 0.1f) {
            transform.position = transform.position + targetDir * Time.deltaTime * speed;
                transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * rotateSpeed);
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

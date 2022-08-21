using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugController : MonoBehaviour
{
    [SerializeField] private Animator anim;

    [SerializeField] private string bugType;

    // Update is called once per frame
    void Update() {
        
    }

    public string getType() {
        return bugType;
    }

    public void killBug() {
        Destroy(this.gameObject);
    }
    public void freezeAnim() {
        anim.enabled = false;
    }
}

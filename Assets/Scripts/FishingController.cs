using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingController : MonoBehaviour {
    [SerializeField] private Animator frogAnim;
    [SerializeField] private KeyCode fishKey;
    [SerializeField] private KeyCode resetKey;
    [SerializeField] private float reelTime;    // Time to reel in seconds

    private string state = "idle";
    private float reelStart;
    private bool caught = true;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        //print(state);
        if(state == "idle") {
            if(Input.GetKeyDown(fishKey)) {
                frogAnim.SetBool("Casting", true);
                state = "casting";
            }
        } else if(state == "casting") {
            if(Input.GetKeyDown(resetKey)) {
                frogAnim.SetBool("Casting", false);
                state = "idle";
            }
            if(Input.GetKeyUp(fishKey)) {
                frogAnim.SetTrigger("Cast");
                state = "fishing";
            }

        } else if(state == "fishing") {
            
            if(Input.GetKeyDown(fishKey)) {
                frogAnim.SetTrigger("Reel In");
                state = "reeling";
                reelStart = Time.time;
            }
        } else if(state == "reeling") {
            if(caught) {
                frogAnim.SetBool("Caught", true);
                if(Time.time - reelStart >= reelTime) {
                    frogAnim.SetTrigger("Pull");
                    state = "celebrating";
                }
            } else {
                frogAnim.SetBool("Casting", false);
                frogAnim.SetBool("Caught", false);
                frogAnim.SetTrigger("Pull");
                state = "idle";
            }
        } else if (state == "celebrating") {
            if(Input.anyKey) {
                frogAnim.SetBool("Casting", false);
                frogAnim.SetTrigger("Continue");
                state = "idle";
            }
        }
    }
}

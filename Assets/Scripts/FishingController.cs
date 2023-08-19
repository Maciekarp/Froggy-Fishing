using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
public class FishingController : MonoBehaviour {
    [SerializeField] private Animator frogAnim;
    [SerializeField] private KeyCode fishKey;
    [SerializeField] private KeyCode resetKey;
    [SerializeField] private float reelTime;    // Time to reel in seconds
    
    [SerializeField] private BobberMovement bobber;

    [SerializeField] private Rig headRig;
    [SerializeField] private float transitionTime = 0.5f;

    [SerializeField] private AimController aimController;
    
    [SerializeField] private CameraMovement camMov;

    private string state = "idle";
    private float reelStart;
    private bool caught = true;

    private float transitionStart = -10f;


    void Update() {

        // transitions the rig that controls aim head between on and off
        // and allows for eating            
        if(state == "reeling" || state == "celebrating") {
            headRig.weight = (Time.time - transitionStart)>= transitionTime ? 0f : 1f - (Time.time - transitionStart) / transitionTime;
        aimController.canEat = false;
        }
        if(state == "idle") {
            headRig.weight = (Time.time - transitionStart)>= transitionTime ? 1f : ((Time.time - transitionStart) / transitionTime);
            aimController.canEat = true;
        }

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
                bobber.StartThrow();
                state = "fishing";
            }

        } else if(state == "fishing") {
            
            if(Input.GetKeyDown(fishKey)) {
                frogAnim.SetTrigger("Reel In");
                state = "reeling";
                reelStart = Time.time;
                transitionStart = Time.time;
            }
        } else if(state == "reeling") {
            if(caught) {
                frogAnim.SetBool("Caught", true);
                if(Time.time - reelStart >= reelTime) {
                    frogAnim.SetTrigger("Pull");
                    frogAnim.SetTrigger("PullHead");
                    state = "celebrating";
                    camMov.StartCelebrate();
                }
            } else {
                frogAnim.SetBool("Casting", false);
                frogAnim.SetBool("Caught", false);
                frogAnim.SetTrigger("Pull");
                state = "idle";
                transitionStart = Time.time;
            }
        } else if (state == "celebrating") {
            if(Input.anyKey) {
                frogAnim.SetBool("Casting", false);
                frogAnim.SetTrigger("Continue");
                frogAnim.SetTrigger("ContinueHead");
                state = "idle";
                transitionStart = Time.time;
                camMov.StopCelebrate();
            }
        }
    }
}

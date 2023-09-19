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

    [SerializeField] private GameObject[] fishPrefabs;
    
    [SerializeField] private StringMaster strings;
    [SerializeField] private GameObject fishOnHook;

    [SerializeField] private float KeyDelay = 0.2f;
    private string state = "idle";
    private float reelStart;
    private bool caught = false;

    private float transitionStart = -10f;
    
    private float keyPressTime = -10f;

    void Update() {

        // transitions the rig that controls aim head between on and off
        // and allows for eating            
        if(state == "reeling" || state == "celebrating") {
            headRig.weight = (Time.time - transitionStart)>= transitionTime ? 0f : 1f - (Time.time - transitionStart) / transitionTime;
        aimController.canEat = false;
        }
        if(state == "idle") {
            headRig.weight = (Time.time - transitionStart - 1f)>= transitionTime ? 1f : ((Time.time - transitionStart) / transitionTime - 1);
            aimController.canEat = true;
        }

        if(state == "idle") {
            if(Input.GetKeyDown(fishKey)) {
                frogAnim.SetBool("Casting", true);
                state = "casting";
                strings.sag = 0f;
            }
        } else if(state == "casting") {
            if(Input.GetKeyDown(resetKey)) {
                frogAnim.SetBool("Casting", false);
                strings.sag = 0.2f;
                state = "idle";
            }
            if(Input.GetKeyUp(fishKey)) {
                frogAnim.SetTrigger("Cast");
                //bobber.StartThrow();
                strings.sag = 0.2f;
                state = "fishing";
            }

        } else if(state == "fishing") {
            
            if(Input.GetKeyDown(fishKey)) {
                //Debug.Log("pressed reelin");
                frogAnim.SetTrigger("Reel In");
                strings.sag = 0f;
                state = "reeling";
                reelStart = Time.time;
                transitionStart = Time.time;
                caught = bobber.GetCanCatch();
            }
        } else if(state == "reeling") {
            if(caught) {
                frogAnim.SetBool("Caught", true);
                fishOnHook.SetActive(true);
                if(Time.time - reelStart >= reelTime) {
                    frogAnim.SetTrigger("Pull");
                    bobber.ReelIn();
                    strings.sag = 0.2f;
                    frogAnim.SetTrigger("PullHead");
                    state = "celebrating";
                    camMov.StartCelebrate(fishPrefabs[0]);
                }
            } else if(Time.time - reelStart > 0.5f){
                frogAnim.SetBool("Casting", false);
                frogAnim.SetBool("Caught", false);
                frogAnim.SetTrigger("Pull");
                
                strings.sag = 0.2f;
                state = "idle";
                transitionStart = Time.time;
            } else {
                bobber.ReelIn();
            }
        } else if (state == "celebrating") {
            if(Input.anyKey) {
                fishOnHook.SetActive(false);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimController : MonoBehaviour {
    [SerializeField] private Animator frogAnim;
    [SerializeField] private Transform aimTransform;

    [SerializeField] private Transform tongueBase;
    [SerializeField] private Transform tongueEnd;
    
    [Tooltip("0 to 100 percent look speed")]
    [Range(0f, 1f)]
    [SerializeField] private float lookSpeed = 1f;

    [SerializeField] private float tongueDelay = 0.1f;
    [SerializeField] private float tongueTime = 0.3f;

    [SerializeField] private AnimationCurve tongueCurve;


    [SerializeField] private ClockController clock;

    private bool eating = false;
    private bool eaten = false;
    private GameObject bugCaught = null;
    private BugController bugCont = null;
    private float startTime;

    public bool canEat = false;


    // Update is called once per frame
    void Update() { 
        // Raycasts from the camera to the location the mouse is
        // intersecting position is where aimTransform is placed
        Ray pointRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(!eating && canEat) {
            RaycastHit[] hits;
            hits = Physics.RaycastAll(pointRay);
            if(hits.Length >0 ){
                
                RaycastHit bestHit = hits[0];
                // loops throught all the collider hits to find the most optimal one
                foreach (RaycastHit hit in hits) {
                    if (bestHit.transform.gameObject.GetComponent<BugController>() != null){
                        if(hit.transform.gameObject.GetComponent<BugController>() != null) {
                            bestHit = (hit.distance < bestHit.distance ? hit : bestHit);
                        }
                    } else {
                        if(hit.transform.gameObject.GetComponent<BugController>() != null) {
                            bestHit = hit;
                        } else {
                            bestHit = (hit.distance < bestHit.distance ? hit : bestHit);
                        }
                    }
                }

                // Has the frog look at the hit object
                Vector3 lookLocation = (
                    bestHit.transform.gameObject.GetComponent<BugController>() != null ? bestHit.transform.position : bestHit.point
                );
                // Moves the aim to the look position
                aimTransform.position = lookSpeed * (lookLocation - aimTransform.position) + aimTransform.position;


                
                if(Input.GetMouseButtonDown(0)) {
                    frogAnim.SetTrigger("Eating");
                    eating = true;
                    bugCont = bestHit.transform.gameObject.GetComponent<BugController>();
                    if(bugCont != null) {
                        bugCaught = bestHit.transform.gameObject;
                    } else {
                        bugCaught = null;
                    }
                    startTime = Time.time;
                }
            }
        } else {
            // if eating do the tongue shoot out animation
            tongueEnd.position = 
                ((bugCaught != null ? bugCaught.transform.position : aimTransform.position) - tongueBase.position ) * 
                tongueCurve.Evaluate((Time.time - startTime - tongueDelay) / tongueTime) + 
                tongueBase.position
            ;
            if(!eaten && Time.time > startTime + (tongueDelay + tongueTime) / 2) {
                eaten = true;
                if (bugCaught != null && bugCont != null) {
                    bugCont.freezeAnim();
                    bugCaught.transform.SetParent(tongueEnd.transform, true);
                }

            }
            if(Time.time > startTime + tongueDelay + tongueTime) {
                eating = false;
                eaten = false;
                if(bugCaught != null && bugCont != null) {
                    clock.AddTime();
                    bugCont.killBug();
                }
                tongueEnd.position = tongueBase.position;
                //tongueEnd.position = (bugCaught != null ? bugCaught.transform.position : aimTransform.position);
            }
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimController : MonoBehaviour {
    [SerializeField] private Animator frogAnim;
    [SerializeField] private Transform aimTransform;
    
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        // Raycasts from the camera to the location the mouse is
        // intersecting position is where aimTransform is placed
        Ray pointRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(pointRay, out RaycastHit hit, 100)) {
            aimTransform.position = hit.point;
            //print(hit.transform.name);

        }
        //aimTransform.position = 
        if(Input.GetMouseButtonDown(0)) {
            frogAnim.SetTrigger("Eating");
        }
    }
}

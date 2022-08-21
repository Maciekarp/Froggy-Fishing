using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeConnect : MonoBehaviour {
    public GameObject nodeL;
    public GameObject nodeR;
    public float thickness = 1f;
    
    

    [SerializeField] private bool stretchX = false;
    [SerializeField] private bool stretchY = true;
    [SerializeField] private bool stretchZ = false;

    [SerializeField] private float defaultScale = 1f;

    [SerializeField] private Vector3 rotate;

    [SerializeField] private float sizeRatio = 0.5f;

    [Tooltip("If false will use nodeR location")]
    [SerializeField] private bool doMidPoint = true;

    //public float length = 1f;
    
    // Start is called before the first frame update
    void Start() {
        if(nodeL == null || nodeR == null) {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update() {
        if(nodeL == null || nodeR == null) {
            Destroy(this.gameObject);
        }

        float length = (nodeL.transform.position - nodeR.transform.position).magnitude * sizeRatio;
        transform.localScale = new Vector3(
            (stretchX ? length : thickness), 
            (stretchY ? length : thickness), 
            (stretchZ ? length : thickness)
        ) * defaultScale;
        // Sets location to either midpoint or node 2
        transform.position = 
            (doMidPoint ? 
                (nodeL.transform.position + nodeR.transform.position) / 2 : 
                nodeR.transform.position
            );

        // Rotates to look at target with designated rotation
        if(nodeL.transform.position - nodeR.transform.position != Vector3.zero) {
            transform.rotation = 
                Quaternion.LookRotation(nodeL.transform.position - nodeR.transform.position) *
                Quaternion.Euler(rotate.x, rotate.y, rotate.z);

        }
        
    }
}

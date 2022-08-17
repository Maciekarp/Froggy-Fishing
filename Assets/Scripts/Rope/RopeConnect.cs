using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeConnect : MonoBehaviour {
    public GameObject nodeL;
    public GameObject nodeR;
    public float thickness = 1f;
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

        float length = (nodeL.transform.position - nodeR.transform.position).magnitude / 2;
        transform.localScale = new Vector3(thickness, length, thickness);
        transform.position = (nodeL.transform.position + nodeR.transform.position) / 2;
        transform.rotation = 
            Quaternion.LookRotation(nodeL.transform.position - nodeR.transform.position) *
            Quaternion.Euler(90, 0, 0);

    }
}

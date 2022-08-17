using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeNode : MonoBehaviour {
    [Tooltip("If true will update to follow object")]
    [SerializeField] private bool isEnd = false;
    [Tooltip("Object rope end will follow")]
    [SerializeField] private GameObject followObj = null;

    [Tooltip("Offset from follow object")]
    [SerializeField] private Vector3 offset; 

    public float thickness = 0.1f;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        if(isEnd) {
            transform.position = followObj.transform.position + offset;
        }
        transform.localScale = new Vector3(thickness, thickness, thickness);
    }
}

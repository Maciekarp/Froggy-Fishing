using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringMaster : MonoBehaviour
{
    [Tooltip("Does not do anything at runtime")]
    [SerializeField][Range(1, 20)] private int numSegments = 7;

    [SerializeField][Range(0, 10)] public float sag = 1f;
    [SerializeField][Range(0, 1)] public float thickness = 0.1f;
    
    [SerializeField] private List<RopeController> strings;

    [SerializeField] private RopeController bobberString;

    [Tooltip("uses default sag if negative")]
    public float bobberStringSag = -1;

    void Start() {
        foreach(RopeController rc in strings) {
            rc.numSegments = numSegments;
        }
    }


    // Update is called once per frame
    void Update() {
        foreach(RopeController rc in strings) {
            rc.sag = sag;
            rc.thickness = thickness;
        }
        if(bobberStringSag < 0) {
            bobberString.sag = sag;
        } else {
            bobberString.sag = bobberStringSag;
        }
        bobberString.thickness = thickness;
    }
}

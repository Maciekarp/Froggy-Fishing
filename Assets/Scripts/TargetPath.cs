using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPath : MonoBehaviour {
    /*
    private Vector3 initalPos;

    [SerializeField] private float scaleX = 10;
    [SerializeField] private float scaleY = 5;
    [SerializeField] private float scaleZ = 10;
    

    [SerializeField] private float loopTime = 4;

    // Start is called before the first frame update
    void Start() {
        initalPos = transform.position;
    }

    // Update is called once per frame
    void Update() {
        transform.position = new Vector3(
            initalPos.x + scaleX * (Mathf.Sin(Time.time/loopTime * 3)),
            initalPos.y + scaleY * (Mathf.Sin(Time.time/loopTime * 7)),
            initalPos.z + scaleZ * (Mathf.Sin(Time.time/loopTime * 11))
        );
    }
    */

    [SerializeField] private Collider box;
    [SerializeField] private Transform bugTransform;
    [SerializeField] private float distance = 1f;


    public static Vector3 RandomPointInBounds(Bounds bounds) {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }

    void Start() {
        transform.position = RandomPointInBounds(box.bounds);
    }

    void Update() {
        if(Vector3.Distance(bugTransform.position, transform.position) < distance) {
            transform.position = RandomPointInBounds(box.bounds);
        }
    }



}

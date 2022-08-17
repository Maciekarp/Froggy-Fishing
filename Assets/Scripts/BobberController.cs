using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobberController : MonoBehaviour
{
    [SerializeField] private GameObject rodTip;
    [SerializeField] private GameObject bobberTop;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private float forceMult = 1;
    [SerializeField] public float stringLength = 1;



    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void FixedUpdate() {
        Vector3 tensionDir = (rodTip.transform.position - bobberTop.transform.position).normalized;
        float distFromTip = (rodTip.transform.position - bobberTop.transform.position).magnitude;
        if(distFromTip >= stringLength) {
            rb.AddForceAtPosition(
                tensionDir * Time.deltaTime * Mathf.Pow(distFromTip - stringLength + 1, 2) * forceMult, 
                bobberTop.transform.position
            );
        }
    }
}

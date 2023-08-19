using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconWiggle : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 1f;
    [SerializeField] private float rotateMag = 15f;

    private Vector3 initialRot;
    // Start is called before the first frame update
    void Start() {
        initialRot = transform.localRotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localRotation = Quaternion.Euler(
            initialRot.x + (rotateMag* Mathf.Sin(rotateSpeed * Time.time)),
            initialRot.y + (rotateMag* Mathf.Cos(rotateSpeed * Time.time)),
            initialRot.z
        );
    }
}

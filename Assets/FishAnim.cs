using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishAnim : MonoBehaviour
{

    [SerializeField] private Transform tailEnd;

    [SerializeField] private Transform mouthBone;

    [SerializeField] private float maxRotate = 45f;
    [SerializeField] private float flapSpeed = 1f;
    [SerializeField] private float cycleTime = 1f;

    [SerializeField] private AnimationCurve mouthCurve;

    [SerializeField] private float mouthSpeed = 1f;
    [SerializeField] private float mouthAngle = 15f;

    private Vector3 initialTailEndAngle;
    private Vector3 initialMouthAngle;

    // Start is called before the first frame update
    void Start() {
        initialTailEndAngle = tailEnd.localEulerAngles;
        initialMouthAngle = mouthBone.localEulerAngles;
    }

    // Update is called once per frame
    void Update(){
        mouthBone.localRotation = Quaternion.Euler(
            initialMouthAngle.x,
            initialMouthAngle.y,
            initialMouthAngle.z + mouthCurve.Evaluate(Time.time /  mouthSpeed) * mouthAngle
        );

        tailEnd.localRotation = Quaternion.Euler(
            initialTailEndAngle.x, 
            initialTailEndAngle.y + (maxRotate * Mathf.Cos(Time.time * cycleTime)) * Mathf.Sin(Time.time * flapSpeed), 
            initialTailEndAngle.z
        );
    }
}

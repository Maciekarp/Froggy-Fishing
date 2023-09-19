using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockController : MonoBehaviour
{
    [SerializeField] private GameObject hand;
    [SerializeField] private float maxTime = 60f;
    private Vector3 initialRot;
    public float timeLeft = 60f;
    // Start is called before the first frame update
    void Start()
    {
        initialRot = hand.transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft = maxTime - Time.time;
        hand.transform.eulerAngles = initialRot - new Vector3((timeLeft/60f) * 360f, 0f, 0f);
    }
}

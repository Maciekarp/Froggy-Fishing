using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockController : MonoBehaviour
{
    [SerializeField] private GameObject hand;
    [SerializeField] private float maxTime = 60f;
    private Vector3 initialRot;

    private bool isOn = false;

    public float timeLeft = 60f;
    // Start is called before the first frame update

    public void AddTime() {
        timeLeft = Mathf.Clamp(timeLeft + 1f, 0, maxTime);
    }

    public void StartClock() {
        isOn = true;
    }

    public void StopClock() {
        isOn = false;
    }

    void Start()
    {
        initialRot = hand.transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        if(isOn) {
            timeLeft -= Time.deltaTime;
            hand.transform.eulerAngles = initialRot - new Vector3((timeLeft/maxTime) * 360f, 0f, 0f);
        }
        
    }
}

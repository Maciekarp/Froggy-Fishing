using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigitController : MonoBehaviour {

    // Segment naming scheme:
    /*
      _       a
    |   |   f   b
      _       g
    |   |   e   c
      _       d
    */
    [SerializeField] private GameObject segmentA, segmentB, segmentC, segmentD, segmentE, segmentF, segmentG;

    //[Tooltip("Digit Being Displayed")]
    private int digit = 0;

    // Sets the digit to be displayed returns false if not 0-9
    public bool SetDigit(int newDigit) {
        if(newDigit < -1 || newDigit > 9) {
            return false;
        } else {
            digit = newDigit;
            switch(digit) {
                case -1:
                    segmentA.SetActive(false);
                    segmentB.SetActive(false);
                    segmentC.SetActive(false);
                    segmentD.SetActive(false);
                    segmentE.SetActive(false);
                    segmentF.SetActive(false);
                    segmentG.SetActive(false);
                    break;
                case 0:
                    segmentA.SetActive(true);
                    segmentB.SetActive(true);
                    segmentC.SetActive(true);
                    segmentD.SetActive(true);
                    segmentE.SetActive(true);
                    segmentF.SetActive(false);
                    segmentG.SetActive(true);
                    break;
                case 1:
                    segmentA.SetActive(false);
                    segmentB.SetActive(true);
                    segmentC.SetActive(true);
                    segmentD.SetActive(false);
                    segmentE.SetActive(false);
                    segmentF.SetActive(false);
                    segmentG.SetActive(false);
                    break;
                case 2:
                    segmentA.SetActive(true);
                    segmentB.SetActive(true);
                    segmentC.SetActive(false);
                    segmentD.SetActive(true);
                    segmentE.SetActive(true);
                    segmentF.SetActive(true);
                    segmentG.SetActive(false);
                    break;
                case 3:
                    segmentA.SetActive(true);
                    segmentB.SetActive(true);
                    segmentC.SetActive(true);
                    segmentD.SetActive(true);
                    segmentE.SetActive(false);
                    segmentF.SetActive(true);
                    segmentG.SetActive(false);
                    break;
                case 4:
                    segmentA.SetActive(false);
                    segmentB.SetActive(true);
                    segmentC.SetActive(true);
                    segmentD.SetActive(false);
                    segmentE.SetActive(false);
                    segmentF.SetActive(true);
                    segmentG.SetActive(true);
                    break;
                case 5:
                    segmentA.SetActive(true);
                    segmentB.SetActive(false);
                    segmentC.SetActive(true);
                    segmentD.SetActive(true);
                    segmentE.SetActive(false);
                    segmentF.SetActive(true);
                    segmentG.SetActive(true);
                    break;
                case 6:
                    segmentA.SetActive(true);
                    segmentB.SetActive(false);
                    segmentC.SetActive(true);
                    segmentD.SetActive(true);
                    segmentE.SetActive(true);
                    segmentF.SetActive(true);
                    segmentG.SetActive(true);
                    break;
                case 7:
                    segmentA.SetActive(true);
                    segmentB.SetActive(true);
                    segmentC.SetActive(true);
                    segmentD.SetActive(false);
                    segmentE.SetActive(false);
                    segmentF.SetActive(false);
                    segmentG.SetActive(false);
                    break;
                case 8:
                    segmentA.SetActive(true);
                    segmentB.SetActive(true);
                    segmentC.SetActive(true);
                    segmentD.SetActive(true);
                    segmentE.SetActive(true);
                    segmentF.SetActive(true);
                    segmentG.SetActive(true);
                    break;
                case 9:
                    segmentA.SetActive(true);
                    segmentB.SetActive(true);
                    segmentC.SetActive(true);
                    segmentD.SetActive(true);
                    segmentE.SetActive(false);
                    segmentF.SetActive(true);
                    segmentG.SetActive(true);
                    break;
            }
            return true;
        }
    }

    // Start is called before the first frame update
    // Sets digit to default
    void Start() {
        SetDigit(digit);
    }

    // Used for testing of digit display
    
    void Update() {
        SetDigit((int)Time.time % 10);
    }
    
}

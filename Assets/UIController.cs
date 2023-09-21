using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private ClockController clockCont;
    [SerializeField] private GameObject buttons;


    public void HideUI() {
        buttons.SetActive(false);
    }

    public void StartClock() {
        clockCont.gameObject.SetActive(true);
        clockCont.timeLeft = 60;
    }


    public void QuitGame() {
        Application.Quit();
    }

}

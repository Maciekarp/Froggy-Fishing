using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform cameraTrans;
    [SerializeField] private Transform defaultPosTrans;
    [SerializeField] private Transform celebratePosTrans;
    
    [SerializeField] private AnimationCurve cameraMovement;
    
    [SerializeField] private GameObject celebratePrefab;
    [SerializeField] private Transform celebrateSpawnPos;


    private Vector3 startPos;
    private Vector3 endPos;
    private Quaternion startRot;
    private Quaternion endRot;
    private float startMoveTime = -50;

    private GameObject currCelebrate;
    private GameObject currCaughtFish;

    // Creates celebration and fish objects and brings the 
    public void StartCelebrate(GameObject caughtFishPrefab) {
        currCelebrate = Instantiate(celebratePrefab, celebrateSpawnPos.transform.position, Quaternion.identity);
        Instantiate(caughtFishPrefab, currCelebrate.transform);
        startMoveTime = Time.time;
        startPos = defaultPosTrans.position;
        endPos = celebratePosTrans.position;
        startRot = defaultPosTrans.rotation;
        endRot = celebratePosTrans.rotation;
    }

    public void StopCelebrate() {
        currCelebrate.GetComponent<IconController>().KillIcon();
        startMoveTime = Time.time;
        startPos = celebratePosTrans.position;
        endPos = defaultPosTrans.position;
        startRot = celebratePosTrans.rotation;
        endRot = defaultPosTrans.rotation;
    }

    // Start is called before the first frame update
    void Start() {
        cameraTrans.position = defaultPosTrans.position;
        cameraTrans.rotation = defaultPosTrans.rotation;
        startMoveTime = -50f;
        startPos = celebratePosTrans.position;
        endPos = defaultPosTrans.position;
        startRot = celebratePosTrans.rotation;
        endRot = defaultPosTrans.rotation;
    }

    // Update is called once per frame
    void Update() {
        // travelling to the celebrate screen
        cameraTrans.position = Vector3.LerpUnclamped(startPos, endPos, cameraMovement.Evaluate(Time.time - startMoveTime));
        cameraTrans.rotation = Quaternion.LerpUnclamped(startRot, endRot, cameraMovement.Evaluate(Time.time - startMoveTime));
    }
}

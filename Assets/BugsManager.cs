using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugsManager : MonoBehaviour
{

    [SerializeField] private Collider[] spawnBoxes;
    [SerializeField] private GameObject flyPrefab;
    [SerializeField] private Collider flyBox;

    private void spawnFly() {
        Collider chosenSpawn = spawnBoxes[Random.Range(0, spawnBoxes.Length)];
        Vector3 spawnPoint = new Vector3(
            Random.Range(chosenSpawn.bounds.min.x, chosenSpawn.bounds.max.x),
            Random.Range(chosenSpawn.bounds.min.y, chosenSpawn.bounds.max.y),
            Random.Range(chosenSpawn.bounds.min.z, chosenSpawn.bounds.max.z)
        );
        GameObject newFly = Instantiate(flyPrefab, spawnPoint, Quaternion.identity); 
        TargetPath targetP = newFly.GetComponentInChildren<TargetPath>();
        targetP.box = flyBox;
    }

    // Update is called once per frame
    void Update() {
        if(Input.GetMouseButtonDown(1)) spawnFly();
    }
}

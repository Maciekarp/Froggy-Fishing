using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Bouyancy : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    [SerializeField] private float waterYLevel = 10f;
    [SerializeField] private float bouyancy = 1f;


    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        if(transform.position.y < waterYLevel) {
            rb.velocity += new Vector3(0, Time.deltaTime * (bouyancy * Mathf.Pow(waterYLevel - transform.position.y, 2)) , 0);
        }    
        
    }
}

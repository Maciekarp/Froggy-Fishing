using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeController : MonoBehaviour {

    [Tooltip("Does not do anything at runtime")]
    [SerializeField][Range(1, 20)] public int numSegments = 3;
    
    [SerializeField][Range(0, 10)] public float sag = 1f;
    [SerializeField][Range(0, 1)] public float thickness = 0.1f;
    

    [SerializeField] private GameObject startNode;
    [SerializeField] private GameObject endNode;
    [SerializeField] private GameObject pullTarget;
    

    [SerializeField] private GameObject nodePrefab;
    [SerializeField] private GameObject segmentPrefab;
    
    private float prevThickness;

    public List<GameObject> nodes;
    private List<GameObject> segments;

    private void updateThickness() {
        foreach(GameObject node in nodes) {
            node.GetComponent<RopeNode>().thickness = thickness;
        }
        foreach(GameObject segment in segments) {
            segment.GetComponent<RopeConnect>().thickness = thickness;
        }
    }

    // Splits string into segments
    void Start() {
        nodes = new List<GameObject>();
        segments = new List<GameObject>();

        Vector3 dir = endNode.transform.position - startNode.transform.position;
        GameObject segment;
        RopeConnect ropeC;
        nodes.Add(startNode);
        for(int i = 1; i < numSegments; i++) {
            nodes.Add(Instantiate(nodePrefab, startNode.transform.position + (dir * i / numSegments), Quaternion.identity));
            nodes[i].transform.SetParent(this.transform, true);
            segment = Instantiate(segmentPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            ropeC = segment.GetComponent<RopeConnect>();
            ropeC.nodeL = nodes[i - 1];
            ropeC.nodeR = nodes[i];
            segment.transform.SetParent(this.transform, true);
            segments.Add(segment);

        }
        nodes.Add(endNode);
        segment = Instantiate(segmentPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        ropeC = segment.GetComponent<RopeConnect>();
        ropeC.nodeL = nodes[numSegments - 1];
        ropeC.nodeR = nodes[numSegments];
        segment.transform.SetParent(this.transform, true);

        segments.Add(segment);


        updateThickness();
        prevThickness = thickness;
    }

    // Update is called once per frame
    void Update() {
        if(thickness != prevThickness) {
            updateThickness();
            prevThickness = thickness;
        }


        // Pull Controller
        Vector3 newPullPos = (startNode.transform.position + endNode.transform.position) / 2;
        newPullPos.y -= (startNode.transform.position - endNode.transform.position).magnitude * sag;
        pullTarget.transform.position = newPullPos;

        // Bezier Calculator
        Vector3 p0 = startNode.transform.position;
        Vector3 p2 = endNode.transform.position;
        Vector3 p1 = pullTarget.transform.position;

        for(int i = 0; i < nodes.Count; i++) {
            float t = (float)i / (nodes.Count - 1);
            Vector3 pos = (1f - t) * (((1f - t) * p0) + (t * p1)) + 
                          t * (((1f - t) * p1) + (t * p2));
            nodes[i].transform.position = pos;
        }
    }
    
}

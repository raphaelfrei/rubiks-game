using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadCube : MonoBehaviour{

    List<GameObject> frontRays = new List<GameObject>(), backRays = new List<GameObject>(), leftRays = new List<GameObject>(), rightRays = new List<GameObject>(), upRays = new List<GameObject>(), downRays = new List<GameObject>();

    public Transform tUp, tDown, tLeft, tRight, tFront, tBack;

    int layerMask = 1 << 9;
    
    CubeState cubeState;
    CubeMap cubeMap;

    public GameObject emptyGO;

    void Start(){
        SetRayTransform();
        ReadState();

        cubeState = FindObjectOfType<CubeState>();
        cubeMap = FindObjectOfType<CubeMap>();

        CubeState.started = true;

    }

    void Update(){
        
    }

    public void ReadState() {
        cubeState = FindObjectOfType<CubeState>();
        cubeMap = FindObjectOfType<CubeMap>();

        cubeState.up = ReadFace(upRays, tUp);
        cubeState.down = ReadFace(downRays, tDown);
        cubeState.left = ReadFace(leftRays, tLeft);
        cubeState.right = ReadFace(rightRays, tRight);
        cubeState.front = ReadFace(frontRays, tFront);
        cubeState.back = ReadFace(backRays, tBack);

        cubeMap.Set();
    }

    void SetRayTransform() {
        upRays = BuildRays(tUp, new Vector3(90, 90, 0));
        downRays = BuildRays(tDown, new Vector3(270, 90, 0));
        leftRays = BuildRays(tLeft, new Vector3(0, 180, 0));
        rightRays = BuildRays(tRight, new Vector3(0, 0, 0));
        frontRays = BuildRays(tFront, new Vector3(0, 90, 0));
        backRays = BuildRays(tBack, new Vector3(0, 270, 0));

    }

    List<GameObject> BuildRays(Transform rayTransform, Vector3 direction) {
        int rayCount = 0;
        List<GameObject> rays = new List<GameObject>();

        for(int y = 1; y > -2; y--) {
            for(int x = -1; x < 2; x++) {
                Vector3 startPos = new Vector3(rayTransform.localPosition.x + x, rayTransform.localPosition.y + y, rayTransform.localPosition.z);

                GameObject rayStart = Instantiate(emptyGO, startPos, Quaternion.identity, rayTransform);
                rayStart.name = rayCount.ToString();
                rays.Add(rayStart);
                rayCount++;
            }
        }
        rayTransform.localRotation = Quaternion.Euler(direction);
        
        return rays;

    }

    public List<GameObject> ReadFace(List<GameObject> rayStarts, Transform rayTransform) {
        List<GameObject> facesHit = new List<GameObject>();

        foreach(GameObject rayStart in rayStarts) {
            Vector3 ray = rayStart.transform.position;
            RaycastHit hit;

            if(Physics.Raycast(ray, rayTransform.forward, out hit, Mathf.Infinity, layerMask)) {
                //Debug.DrawRay(ray, rayTransform.forward * hit.distance, Color.yellow);
                facesHit.Add(hit.collider.gameObject);
                //print(hit.collider.gameObject.name);

            } else {
                //Debug.DrawRay(ray, rayTransform.forward * 1000, Color.green);
            }
        }

        return facesHit;
    }
}

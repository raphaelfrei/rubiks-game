using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeState : MonoBehaviour{

    public List<GameObject> front = new List<GameObject>(), back = new List<GameObject>(), left = new List<GameObject>(), right = new List<GameObject>(), up = new List<GameObject>(), 
        down = new List<GameObject>();

    public static bool autoRotating = false, started = false;

    public void PickUp(List<GameObject> cubeSide) {
        foreach(GameObject face in cubeSide) {
            if(face != cubeSide[4]) {
                face.transform.parent.transform.parent = cubeSide[4].transform.parent;
            }
        }

    }

    string GetSideString(List<GameObject> side) {
        string sideString = "";

        foreach(GameObject face in side) {
            sideString += face.name[0].ToString();
            

        }

        return sideString;
    }

    public string GetStateString() {
        string stateString = "";

        stateString += GetSideString(up);
        stateString += GetSideString(right);
        stateString += GetSideString(front);
        stateString += GetSideString(down);
        stateString += GetSideString(left);
        stateString += GetSideString(back);

        return stateString;

    }

    public void PutDown(List<GameObject> littleCubes, Transform pivot) {
        foreach(GameObject littleCube in littleCubes) {
            if(littleCube != littleCubes[4]) {
                littleCube.transform.parent.transform.parent = pivot;
            }
        }
    }
}

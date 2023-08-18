﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeMap : MonoBehaviour{

    CubeState cubeState;

    public Transform up, down, left, right, front, back;

    void Start(){
        
    }

    void Update(){
        
    }

    public void Set() {
        cubeState = FindObjectOfType<CubeState>();

        UpdateMap(cubeState.front, front);
        UpdateMap(cubeState.back, back);
        UpdateMap(cubeState.left, left);
        UpdateMap(cubeState.right, right);
        UpdateMap(cubeState.up, up);
        UpdateMap(cubeState.down, down);
    }

    void UpdateMap(List<GameObject> face, Transform side) {
        int i = 0;
        foreach(Transform map in side) {
            if(face[i].name[0] == 'F') {
                map.GetComponent<Image>().color = Color.red;
            }
            if(face[i].name[0] == 'B') {
                map.GetComponent<Image>().color = new Color(1, 0.5f, 0, 1);
            }
            if(face[i].name[0] == 'U') {
                map.GetComponent<Image>().color = Color.yellow;
            }
            if(face[i].name[0] == 'D') {
                map.GetComponent<Image>().color = Color.white;
            }
            if(face[i].name[0] == 'L') {
                map.GetComponent<Image>().color = Color.blue;
            }
            if(face[i].name[0] == 'R') {
                map.GetComponent<Image>().color = Color.green;
            }
            i++;
        }
    }

}

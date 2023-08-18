﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotRotation : MonoBehaviour{

    List<GameObject> activeSide;

    Vector3 localForward, mouseRef, rotation;

    float sensitivity = 0.4f, speed = 200.0f;

    bool dragging = false, autoRotating = false;

    Quaternion targetQuaternion;

    ReadCube readCube;
    CubeState cubeState;

    void Start(){
        readCube = FindObjectOfType<ReadCube>();
        cubeState = FindObjectOfType<CubeState>();

    }


    void LateUpdate(){
        if(dragging && !autoRotating) {
            SpinSide(activeSide);
            if(Input.GetMouseButtonUp(0)) {
                dragging = false;
                RotateToRightAngle();

            }
        }

        if(autoRotating) {
            AutoRotate();
        }
    }

    void SpinSide(List<GameObject> side) {
        rotation = Vector3.zero;

        Vector3 mouseOffSet = (Input.mousePosition - mouseRef);

        if(side == cubeState.up) {
            rotation.y = (mouseOffSet.x + mouseOffSet.y) * sensitivity * 1.0f;
        }
        if(side == cubeState.down) {
            rotation.y = (mouseOffSet.x + mouseOffSet.y) * sensitivity * -1.0f;
        }
        if(side == cubeState.left) {
            rotation.z = (mouseOffSet.x + mouseOffSet.y) * sensitivity * 1.0f;
        }
        if(side == cubeState.right) {
            rotation.z = (mouseOffSet.x + mouseOffSet.y) * sensitivity * -1.0f;
        }
        if(side == cubeState.front) {
            rotation.x = (mouseOffSet.x + mouseOffSet.y) * sensitivity * -1.0f;
        }
        if(side == cubeState.back) {
            rotation.x = (mouseOffSet.x + mouseOffSet.y) * sensitivity * 1.0f;
        }

        transform.Rotate(rotation, Space.Self);

        mouseRef = Input.mousePosition;

    }

    public void Rotate(List<GameObject> side) {
        activeSide = side;

        mouseRef = Input.mousePosition;
        dragging = true;

        localForward = Vector3.zero - side[4].transform.parent.transform.localPosition;

    }

    void AutoRotate() {
        dragging = false;
        var step = speed * Time.deltaTime;

        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetQuaternion, step);

        if(Quaternion.Angle(transform.localRotation, targetQuaternion) <= 1) {
            transform.localRotation = targetQuaternion;

            readCube.ReadState();

            cubeState.PutDown(activeSide, transform.parent);

            CubeState.autoRotating = false;

            autoRotating = false;
            dragging = false;

        }

    }

    public void StartAutoRotate(List<GameObject> side, float angle) {
        cubeState.PickUp(side);

        Vector3 localForward = Vector3.zero - side[4].transform.parent.transform.localPosition;
        targetQuaternion = Quaternion.AngleAxis(angle, localForward) * transform.localRotation;

        activeSide = side;
        autoRotating = true;

    }

    public void RotateToRightAngle() {
        Vector3 vec = transform.localEulerAngles;

        vec.x = Mathf.Round(vec.x / 90) * 90;
        vec.y = Mathf.Round(vec.y / 90) * 90;
        vec.z = Mathf.Round(vec.z / 90) * 90;

        targetQuaternion.eulerAngles = vec;
        autoRotating = true;

    }
}

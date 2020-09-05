using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestButtonControllor : MonoBehaviour
{
    public GameObject cubeObject;
    GameObject cube;

    // Start is called before the first frame update
    public void ButtonOnclick() {
        GameObject[] startObject = GameObject.FindGameObjectsWithTag("Field");
        if (startObject == null) {
            Debug.Log("startNotFound");
            return;
        }
        cube = cubeObject;
        Vector3 p = startObject[0].transform.TransformPoint(0, 1.0f, 0);
        cube = GameObject.Instantiate(cube, p, Quaternion.identity);
    }
}

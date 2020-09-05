using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;


public class DrawScript : MonoBehaviour
{
    //線の描写
    public GameObject obj;
    //球の描写
    //public GameObject col;
    GameObject drawObj;
    //GameObject drawCol;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount == 1){
            //だいたいカメラ手前10cmの位置
            //transformpointってなんぞや？
            Vector3 p = Camera.main.transform.TransformPoint(0,0,0.1f);
            //押している間
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                drawObj = GameObject.Instantiate(obj, p, Quaternion.identity);
                //drawCol = GameObject.Instantiate(col, p, Quaternion.identity);
            }
            //押下中、ただし動いてはいない
            else if (Input.GetTouch(0).phase == TouchPhase.Stationary)
            {
                drawObj.transform.position = p;
                //drawCol.transform.position = p;
                //drawCol = GameObject.Instantiate(col, p, Quaternion.identity);
            }
        }
    }
}

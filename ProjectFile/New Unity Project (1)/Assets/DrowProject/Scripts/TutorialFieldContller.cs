using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialFieldContller : MonoBehaviour
{
    GameObject canvas;
    TutorialGameController tc;
    //public GameObject cup;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas");
        tc = canvas.GetComponent<TutorialGameController>();
        tc.isFieldSet = true;
    }
    //segだけゴールをうごかす
    public void GoalSetActotion(float seg) {
        Vector3 k=new Vector3(2.0f - seg, 0.2f, 0);
        //cup.transform.position = k;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}

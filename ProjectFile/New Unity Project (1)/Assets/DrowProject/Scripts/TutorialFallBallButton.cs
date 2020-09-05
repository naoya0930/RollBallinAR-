using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialFallBallButton : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject canvas;
    TutorialGameController tc;
    private bool isDispText = true;
    public void Start()
    {
        tc = canvas.GetComponent<TutorialGameController>();
    }
    // Start is called before the first frame update
    public void ButtonOnclick()
    {
        if (isDispText) {
            isDispText = false;
            //Tutorialのテキスト表示用
            tc.isDrowLined = true;
        }
    }
}

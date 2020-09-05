using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//チュートリアル部分のみ実行
public class TutorialGameController : MonoBehaviour
{
    public Text centerText;
    //public Button bottomButton;
    public Button fallBallButton;
    public Button rotateStageButton;
    public Button okButton;
    public Button skipButton;
    public Button resetLine;
    public Button undoButton;
    public Text bottomText;

    public bool isGetPlane = false;
    public bool isFieldSet = false;
    public bool isDrowLined = false;
    //public bool isBollGoul = false;

    //DrawLineの管理
    public GameObject controller;
    DrawScript drawScript;

    void SetBottomText(string str)
    {
        bottomText.text = str;
        return;
    }
    void SetCenterText(string str)
    {
        centerText.text = str;
        return;
    }
    //falseならstrは空にする
    void interactableEx(Button bt,bool b,string str) {
        bt.interactable = b;
        Text tx=bt.transform.GetChild(0).gameObject.GetComponent<Text>();
        tx.text = str;
    }

    //call from OKButton
    public void ResetUIText()
    {
        SetCenterText("");
        SetBottomText("");
        interactableEx(okButton, false, "");
        isFieldSet = false;
        isDrowLined = false;
        isGetPlane = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        //線を引くためのスクリプト
        drawScript = controller.GetComponent<DrawScript>();
        //初期では引けなくする
        drawScript.enabled = false;

        //buttonの初期設定は全てdisable false
        //bottomButton.interactable=false;
        fallBallButton.interactable = false;
        rotateStageButton.interactable = false;
        interactableEx(okButton, false, "");
        undoButton.interactable = false;
        resetLine.interactable = false;
        skipButton.interactable = true;
        //初期テキスト
        SetCenterText("カメラを\n水平面に向けて下さい");
        SetBottomText("");
    }

    // Update is called once per frame
    void Update()
    {
        if (isGetPlane)
        {
            SetCenterText("");
            isGetPlane = false;
            SetBottomText("平面をタップしてステージを出現させましょう");
        }
        else if (isFieldSet)
        {
            //ここで線を引けるようにする
            drawScript.enabled = true;

            SetCenterText("タップながら画面を動かすと\n線を引くことができます");
            SetBottomText("影に沿って\n線を引いてみましょう");
            interactableEx(okButton, true, "OK");
            fallBallButton.interactable = true;
            rotateStageButton.interactable = true;
        }
        else if (isDrowLined)
        {
            SetCenterText("矢印の上部からボールが\n落ちてきます");
            SetBottomText("カップに入るように\n線を引きましょう");
            interactableEx(okButton, true, "OK");
            resetLine.interactable = true;
            skipButton.interactable = true;
            //undoButton.interactable = true;
            isDrowLined = false;
        }
    }
}


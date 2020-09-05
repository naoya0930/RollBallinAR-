using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManeger : MonoBehaviour
{
    int gameScore = -1;
    float gameTimeSec = 300.0f;
    //落ちてくるオブジェクト
    public GameObject cubeObject;
    public GameObject cubeSecond;
    public GameObject cubeThird;
    GameObject cube;

    //DrawLineの管理
    public GameObject controller;
    DrawScript drawScript;

    public Text centerText;
    public Text scoreText;  //得点表示用
    public Text timeText;   //残り時間表示

    public Button fallBallButton;
    public Button rotateStageButton;
    public Button resetLine;
    public Button undoButton;
    public Button skipButton;
    public Button okButton;

    //public bool isGetPlane = false;
    //public bool isFieldSet = false;
    public bool isDrowLined = false;
    public bool isBallGoal = false;
    public bool isGamestarting = false;
    public bool isGameEnd = false;
    public bool isGameStartConfirmation = false;
    private GameObject obj;
    private List<(GameObject, GameObject)> lineList
        = new List<(GameObject, GameObject)>(); //ラインリセット用


    //球が落ちてくるボタン
    public void OnFallButtonClick()
    {
        GameObject startObject = GameObject.FindGameObjectWithTag("Field");
        if (startObject == null)
        {
            return;
        }
        int v = UnityEngine.Random.Range(0, 3);
        switch (v) {
            case 0:
                cube = cubeObject;
                break;
            case 1:
                cube = cubeSecond;
                break;
            case 2:
                cube = cubeThird;
                break;
            default:
                cube = cubeObject;
                break;
        }
        Vector3 p = startObject.transform.TransformPoint(0, 1.0f, 0);

        cube = GameObject.Instantiate(cube, p, Quaternion.identity);
        //線を引けなくする
        drawScript.enabled = false;

        //ボタンを押せなくする
        undoButton.interactable = false;
        fallBallButton.interactable = false;
        rotateStageButton.interactable = false;
        ////リセットボタンを有効にする
        resetLine.interactable = true;

    }
    //call from BallController
    public void BallGoal(){
        //ゲームの状態を戻す
        drawScript.enabled = true;
        undoButton.interactable = true;
        fallBallButton.interactable = true;
        rotateStageButton.interactable = true;
        resetLine.interactable = true;
        isBallGoal = true;

        //ゴールの距離変更
        if (GameObject.FindGameObjectWithTag("Field"))
        {
            obj = GameObject.FindGameObjectWithTag("Field");
            TutorialFieldContller tutorialFieldContller = obj.GetComponent<TutorialFieldContller>();
            tutorialFieldContller.GoalSetActotion(UnityEngine.Random.Range(0, 1.0f));
        }
        //ラインリセット
        foreach ((GameObject, GameObject) g in lineList)
        {
            Destroy(g.Item1);
            Destroy(g.Item2);
        }
        lineList = new List<(GameObject, GameObject)>();
    }

    //リストにゲームオブジェクトを追加する
    public void AddListLineObject(GameObject renderObj, GameObject meshObj) {
        (GameObject, GameObject) g = (renderObj, meshObj);
        lineList.Add(g);
        //centerText.text = "lineList.Count ";
    }

    //ラインの描写を一つ戻すボタン
    public void OnpushUndoButton() {
        if (lineList.Count != 0)
        {
            Destroy(lineList[lineList.Count - 1].Item1);
            Destroy(lineList[lineList.Count - 1].Item2);
        }
    }

    //ラインとボールを全て削除する
    public void OnPushResetButton() {
        fallBallButton.interactable = true;
        rotateStageButton.interactable = true;
        undoButton.interactable = true;
        drawScript.enabled = true;
        foreach ((GameObject, GameObject) g in lineList) {
            Destroy(g.Item1);
            Destroy(g.Item2);
        }
        lineList = new List<(GameObject, GameObject)>();
        if (GameObject.FindGameObjectWithTag("Sphere"))
        {
            GameObject s = GameObject.FindGameObjectWithTag("Sphere");
            Destroy(s);
        }
    }
    //フィールドを回転する
    public void OnPushRotateButton()
    {
        if (obj == null)
        {
            if (GameObject.FindGameObjectWithTag("Field"))
            {
                obj = GameObject.FindGameObjectWithTag("Field");
            }
        }
        else {
            Quaternion rot = Quaternion.AngleAxis(2, Vector3.up);
            Quaternion q = obj.transform.rotation;
            obj.transform.rotation = q * rot;
        }
    }

    void SetCenterText(string str)
    {
        centerText.text = str;
        return;
    }
    //線の色
    //チュートリアルの終了
    public void StartGame(){
        skipButton.gameObject.SetActive(false);
        gameScore = 0;
        scoreText.text = "Score: " + gameScore;
        isGameStartConfirmation = true;
        centerText.text = "ゲームを開始します";
        okButton.interactable = true;
        okButton.gameObject.GetComponentInChildren<Text>().text = "OK";
        OnPushResetButton();
        fallBallButton.interactable = false;
        rotateStageButton.interactable = false;
        resetLine.interactable = false;
        undoButton.interactable = false;
        //残り時間はUpdateで
    }


    public void EndGame() {
        if (isGameEnd)
        {
            centerText.text = "FINISH!";
            okButton.gameObject.GetComponentInChildren<Text>().text = "Retry";
            //TODO: 各ボタンを制御不能に
            fallBallButton.interactable = false;
            rotateStageButton.interactable = false;
            resetLine.interactable = false;
            undoButton.interactable = false;
            skipButton.interactable = false;
            okButton.interactable = true;
        }
    }
    public void OnPushOKButton() {
        //ゲーム開始の確認
        //call from okButton
        if (isGameStartConfirmation)
        {
            isGamestarting = true;
            isGameStartConfirmation = false;
            okButton.interactable = false;
            okButton.gameObject.GetComponentInChildren<Text>().text = "";
            centerText.text = "";
            //UIを戻す
            fallBallButton.interactable = true;
            rotateStageButton.interactable = true;
            resetLine.interactable = true;
            undoButton.interactable = true;
            drawScript.enabled = true;
        }

        if (isGameEnd)
        {
            //ゲームを再起動する
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text="";
        timeText.text="";
        drawScript = controller.GetComponent<DrawScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isBallGoal) {
            if (gameScore == -1)
            {
                //一番初めだけ
                StartGame();
            }
            else {
                gameScore++;
                scoreText.text = "Score: " + gameScore;
            }
            isBallGoal = false;
        }
        if (isGamestarting) {
            timeText.text = String.Format("Time: {0:00.00}", gameTimeSec);
            gameTimeSec -= Time.deltaTime;
            if (gameTimeSec <= 0.0f) {
                gameTimeSec = 0.0f;
                isGameEnd = true;
                timeText.text = String.Format("Time: {0:00.00}", gameTimeSec);
                EndGame();
                isGamestarting = false;
            }
        }
    }
}
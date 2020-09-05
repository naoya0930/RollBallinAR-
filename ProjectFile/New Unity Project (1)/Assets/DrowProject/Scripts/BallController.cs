using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public GameObject canvas;
    GameManeger gameManeger;

    //球体の速度の制限値
    public float speedMax = 10.0f;
    private Rigidbody _rigid;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas");
        _rigid = this.GetComponent<Rigidbody>();
        gameManeger = canvas.GetComponent<GameManeger>();
    }
    //衝突判定
    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Goal")
        {
            //maneger呼び出し
            gameManeger.BallGoal();
            Destroy(this.gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (_rigid.velocity.magnitude > speedMax)
        {
            _rigid.velocity = _rigid.velocity.normalized * speedMax;

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SphereSpeedControllor : MonoBehaviour
{
    //球体の速度の制限値
    public float speedMax = 10.0f;
    private Rigidbody _rigid;
    // Start is called before the first frame update
    void Start()
    {
        _rigid = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_rigid.velocity.magnitude > speedMax) {
            _rigid.velocity=_rigid.velocity.normalized*speedMax;
            
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RendereScript : MonoBehaviour
{
    public TrailRenderer trail;
    public GameObject originObject;
    GameObject originInstance;
    MeshCollider meshColl;

    public GameObject canvas;
    GameManeger gameManeger;
    // Update is called once per frame
    void Start() {
        canvas = GameObject.Find("Canvas");
        gameManeger = canvas.GetComponent<GameManeger>();
        originInstance = GameObject.Instantiate(originObject, new Vector3(0, 0, 0), Quaternion.identity);

        //消す処理のためにリストに登録
        gameManeger.AddListLineObject(this.gameObject, originInstance);
    }
    void Update()
    {
        
        Mesh mesh=new Mesh();
        trail.BakeMesh(mesh,false);
        meshColl = originInstance.GetComponent<MeshCollider>();
        //meshColl = this.GetComponent<MeshCollider>();
        meshColl.sharedMesh = mesh;
    }
}

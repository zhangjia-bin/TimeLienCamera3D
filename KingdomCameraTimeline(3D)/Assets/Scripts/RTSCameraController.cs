using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSCameraController : MonoBehaviour
{
    public Transform DummyFollow;

    public float Speed = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        //a d
        var hor = Input.GetAxis("Horizontal");
        //w s
        var ver = Input.GetAxis("Vertical");
        //DummyFollow.transform.Rotate(new Vector3(0, hor * 100.0f * Time.deltaTime,0),);
        DummyFollow.position += new Vector3(hor,0,ver)*(Speed*Time.deltaTime);
    }
}

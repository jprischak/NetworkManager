using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    /**
    *  VARIABLES
    * */
    public float speed = 10f;


    //private Rigidbody myRigidbody;
    private NetworkView networkView;



    /**
    *  CLASS FUNCTIONS
    * */
    private void Start()
    {
        //myRigidbody = GetComponent<Rigidbody>();
        networkView = GetComponent<NetworkView>();
    }


    private void Update()
    {
        //if(networkView.isMine)
            InputMovement();
    }


    //private void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
    //{
    //    Vector3 syncPosition = Vector3.zero;

    //    if(stream.isWriting)
    //    {
    //        syncPosition = transform.position;
    //        stream.Serialize(ref syncPosition);
    //    }
    //    else
    //    {
    //        stream.Serialize(ref syncPosition);
    //        transform.position = syncPosition;
    //    }
    //}





    /**
    *  FUNCTIONS
    * */
    private void InputMovement()
    {
        if (Input.GetKey(KeyCode.W))
            transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if (Input.GetKey(KeyCode.S))
            transform.Translate(-Vector3.forward * speed * Time.deltaTime);

        if (Input.GetKey(KeyCode.D))
            transform.Translate(Vector3.right * speed * Time.deltaTime);

        if (Input.GetKey(KeyCode.A))
            transform.Translate(-Vector3.right * speed * Time.deltaTime);
    }
}

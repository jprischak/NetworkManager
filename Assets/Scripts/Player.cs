using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    /**
    *  VARIABLES
    * */
    public float speed = 10f;


   
    private NetworkView networkView;
    private Rigidbody myRigidBody;
    private float lastSynchronizationTime = 0f;
    private float syncDelay = 0f;
    private float syncTime = 0f;
    private Vector3 syncStartPosition = Vector3.zero;
    private Vector3 syncEndPosition = Vector3.zero;





    /**
    *  CLASS FUNCTIONS
    * */
    private void Start()
    {
        networkView = GetComponent<NetworkView>();
        myRigidBody = GetComponent<Rigidbody>();
    }


    private void Update()
    {
        if (networkView.isMine)
            InputMovement();
        else
            SyncMovement();
    }


    private void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
    {
        Vector3 syncPosition = Vector3.zero;
        Vector3 syncVelocity = Vector3.zero;



        if (stream.isWriting)
        {
            // When adding items to the stream the order is important, it will be read in the same order
            syncPosition = transform.position;
            stream.Serialize(ref syncPosition);

            syncVelocity = myRigidBody.velocity;
            stream.Serialize(ref syncVelocity);
        }
        else
        {
            // Read our values back from the stream
            stream.Serialize(ref syncPosition);
            stream.Serialize(ref syncVelocity);


            syncTime = 0f;
            syncDelay = Time.time - lastSynchronizationTime;
            lastSynchronizationTime = Time.time;

            syncStartPosition = transform.position;
            syncEndPosition = syncPosition + syncVelocity * syncDelay;
        }
    }





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


    private void SyncMovement()
    {
        syncTime += Time.deltaTime;
        transform.position = Vector3.Lerp(syncStartPosition, syncEndPosition, syncTime / syncDelay);
    }
}

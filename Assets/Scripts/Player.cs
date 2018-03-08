using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    /**
    *  VARIABLES
    * */
    public float speed = 10f;


    private Rigidbody myRigidbody;




    /**
    *  CLASS FUNCTIONS
    * */
    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }


    private void Update()
    {
        InputMovement();
    }








    /**
    *  FUNCTIONS
    * */
    private void InputMovement()
    {
        if (Input.GetKey(KeyCode.W))
            transform.Translate(transform.position + Vector3.forward * speed * Time.deltaTime);

        if (Input.GetKey(KeyCode.S))
            transform.Translate(transform.position - Vector3.forward * speed * Time.deltaTime);

        if (Input.GetKey(KeyCode.D))
            transform.Translate(transform.position + Vector3.right * speed * Time.deltaTime);

        if (Input.GetKey(KeyCode.A))
            transform.Translate(transform.position - Vector3.right * speed * Time.deltaTime);
    }
}

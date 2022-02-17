using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    

    Quaternion StartingRotation;

    float Ver, Hor, Jump, RotHor, RotVer, senssensitivity = 5;
    public float Speed = 10, JumpSpeed = 200;
    bool isGround;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        StartingRotation = transform.rotation;
    }

    void OnCollisionStay(Collision other)
    {
        if(other.gameObject.tag == "ground")
        {
            isGround = true;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "ground")
        {
            isGround = false;
        }
    }

    void FixedUpdate()
    {
        RotHor += Input.GetAxis("Mouse X") * senssensitivity; 
        RotVer += Input.GetAxis("Mouse Y") * senssensitivity;

        

        if (isGround)
        {
            Ver = Input.GetAxis("Vertical") * Time.deltaTime * Speed;
            Hor = Input.GetAxis("Horizontal") * Time.deltaTime * Speed;
            Jump = Input.GetAxis("Jump") * Time.deltaTime * JumpSpeed;
            GetComponent<Rigidbody>().AddForce(transform.up * Jump, ForceMode.Impulse);
        }

        transform.Translate(new Vector3(Hor, 0, Ver));
    }
}

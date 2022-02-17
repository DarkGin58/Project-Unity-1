using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public GameObject targetObject;
    private float distanceToTarget;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float targetObjectX = targetObject.transform.position.x;

        Vector3 newCameraPosition = transform.position;
        newCameraPosition.x = targetObjectX + distanceToTarget;
        transform.position = newCameraPosition;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{

    private const float YMin = -5.0f;
    private const float YMax = 75.0f;

    public Transform lookAt;

    public Transform Player;
    public RaycastHit hit;
    public float distance = 5.0f;
    private float currentX = 0.0f;
    private float currentY = 0.0f;
    public float sensivity;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;


    }

    // Update is called once per frame
    void Update()
    {

        currentX += Input.GetAxis("Mouse X") * sensivity * 5 * Time.deltaTime;
        currentY += Input.GetAxis("Mouse Y") * sensivity * Time.deltaTime;

        currentY = Mathf.Clamp(currentY, YMin, YMax);

        Vector3 Direction = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        transform.position = lookAt.position + rotation * Direction;

        transform.LookAt(lookAt.position);

     

    }
}
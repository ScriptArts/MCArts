using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour {

    public float movementSpeed = 10f;   // The movement speed
    public float rotateSpeed = 100f;    // The rotate speed

    private Vector3 lastMousePosition;
    private Vector3 newAngle = new Vector3(0, 0, 0);

    // Update is called once per frame
    void Update()
    {
        Movement();
        RotateCamera();
    }
    
    public void Movement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward
                * Time.deltaTime * movementSpeed);
        }
            

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(-Vector3.forward
                * Time.deltaTime * movementSpeed);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left
                * Time.deltaTime * movementSpeed);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right
                * Time.deltaTime * movementSpeed);
        }
    }
    
    public void RotateCamera()
    {
        if (Input.GetMouseButtonDown(1))
        {
            // マウスクリック開始(マウスダウン)時にカメラの角度を保持(Z軸には回転させないため).
            newAngle = transform.localEulerAngles;
            lastMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(1))
        {
            // マウスの移動量分カメラを回転させる.
            newAngle.y += (Input.mousePosition.x - lastMousePosition.x) * 0.1f;
            newAngle.x -= (Input.mousePosition.y - lastMousePosition.y) * 0.1f;
            transform.localEulerAngles = newAngle;

            lastMousePosition = Input.mousePosition;
        }
    }
}

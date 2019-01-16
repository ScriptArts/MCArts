/*
 * This is a simple camera movement script.
 * It allows the camera to rotate around a pivot point and move around.
 * The input is done using the arrows keys.
 * 
 * Author: Corey St-Jacques
 */

using UnityEngine;

public class CameraMovement : MonoBehaviour {
    public float movementSpeed = 10f;   // The movement speed
    public float rotateSpeed = 100f;    // The rotate speed
	
	// Update is called once per frame
	void Update () {
        Movement();
        RotateCamera();
	}

    // Camera movement using up and down arrow keys
    public void Movement()
    {
        if (Input.GetKey(KeyCode.UpArrow))
            transform.Translate(Vector3.forward 
                * Time.deltaTime * movementSpeed);

        if (Input.GetKey(KeyCode.DownArrow))
            transform.Translate(-Vector3.forward 
                * Time.deltaTime * movementSpeed);
    }

    // Camera rotation using left and right arrows keys
    public void RotateCamera()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
            transform.Rotate(Vector3.up 
                * Time.deltaTime * rotateSpeed);

        if (Input.GetKey(KeyCode.RightArrow))
            transform.Rotate(Vector3.up 
                * Time.deltaTime * -rotateSpeed);
    }
}

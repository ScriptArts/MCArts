/* The Input screen class allows users to gain easy access
 * to mouse utilities.
 * Such as the mouse position in world space,
 * the current block to be removed position,
 * and the current block position to be placed.
 * 
 * Author: Corey St-Jacques
 */

using UnityEngine;

public class InputScreen : MonoBehaviour {

    // Get this instance
    public static InputScreen Instance;

    // The mouse world position;
    public static Vector3 MousePosition;        // The current block to place position
    public static Vector3 BelowMousePosition;   // The current scanned block position

	// Use this for initialization
	void Start () {
        Instance = this;    // Get this instance

	}
	
	// Update is called once per frame
	void Update () {
        // Get mouse position is world space
        GetMousePosition();
	}

    // Gets the mouse position is world space
    private void GetMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            MousePosition = (hit.point + (hit.normal * 0.0001f)).Snap();        // Slightly higher to get top block.
            BelowMousePosition = (hit.point - (hit.normal * 0.0001f)).Snap();   // Slightly lower to get bottom block.
        }
    }
}

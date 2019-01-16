/*
 * This class allows us to return a true of false value, true 
 * if the mouse is hovering over this graphical interface.
 * 
 * This class is mainly used to see if the mouse pointer is in the 3d space,
 * or in the UI.
 * 
 * Author: Corey St-Jacques
 */

using UnityEngine;
using UnityEngine.EventSystems;

public class GameScreenControl : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler{
    // Is the mouse in the game screen or not
    public static bool mouseOver = false;

    // On mouse enter game screen
	public void OnPointerEnter(PointerEventData eventData){
		mouseOver = true;
	}

    // On mouse exit game screen
    public void OnPointerExit(PointerEventData eventData){
		mouseOver = false;
	}
	
}
using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickMovement : MonoBehaviour, IPointerUpHandler, IPointerDownHandler {

    private PlayerMoveController playerMove;

    void Awake() {
        playerMove = GameObject.Find("Player").GetComponent<PlayerMoveController>();
    }

    public void OnPointerDown(PointerEventData eventData) {
        if (gameObject.name == "Left Button") {
            playerMove.setMovingDirection(true);
        } else if(gameObject.name == "Right Button") {
            playerMove.setMovingDirection(false);
        }
    }

    public void OnPointerUp(PointerEventData eventData) {
        playerMove.playerStopMoving();
    }
}

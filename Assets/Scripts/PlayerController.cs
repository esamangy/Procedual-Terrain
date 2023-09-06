using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //References----------------------------
    [Header("References")]
    private CharacterController controller;
    [SerializeField] private GameObject body;
    [SerializeField] private Camera cam;
    //--------------------------------------

    //Movement------------------------------
    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    private Vector3 moveVal;
    [SerializeField] private float gravityMultiplier;
    private float gravity = -9.81f;
    private float velocity;
    //--------------------------------------

    void Awake(){
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Start(){
        controller = GetComponent<CharacterController>();
        
    }

    void FixedUpdate(){
        applyGravity();
        applyRotation();
        applyMovement();
    }
    private void applyGravity(){
        if(controller.isGrounded && velocity < 0f){
            velocity = -1f;
            
        }
        else{
            velocity += gravity * gravityMultiplier;
        }
        moveVal.y = velocity;
        
    }
    private void applyRotation(){
        Quaternion orientation = cam.transform.rotation;
        Quaternion target = Quaternion.Euler(0, orientation.eulerAngles.y, 0);
        body.transform.rotation = target;
    }
    private void applyMovement(){
        Vector3 moveDir = body.transform.forward * moveVal.z + body.transform.right * moveVal.x + body.transform.up * moveVal.y;
        controller.Move(moveDir * Time.deltaTime);
        body.transform.position = controller.transform.position;
    }

    private void OnMove(InputValue value){
        moveVal.x = value.Get<Vector2>().x * moveSpeed;
        moveVal.z = value.Get<Vector2>().y * moveSpeed;
    }

}

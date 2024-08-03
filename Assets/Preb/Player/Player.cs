using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Joystick moveStick; //Kết nối tới script của joystick để thực hiện di chuyển nhân vật khi thao tác với joy
    [SerializeField] Joystick aimStick;
    [SerializeField] CharacterController characterController; //Built-in character controller do lười =)))
    [SerializeField] float moveSpeed = 1f; //
    [SerializeField] float turnSpeed = 1f; //

    Animator animator;
    float animationTurnSpeed;
    [SerializeField] float animTurnSpeedToLerp;

    Vector2 moveInput; //biến để chứa giá trị từ event trả về
    Vector2 aimInput;
    CameraController cameraController; //Lấy script cameracontroller về để thực hiện truyền giá trị để quay camera khi di chuyển

    Camera mainCam;

    void Start()
    {
        mainCam = Camera.main; //Lấy camera để thực hiện tính toán di chuyển nhật vật theo hướng của camera
        moveStick.onStickValueUpdate += moveStickUpdate; // Đây không phải phép cộng, đây là thực hiện đăng ký hàm moveStickUpdate vào event onStickValueUpdate của joy move, để mỗi khi
        aimStick.onStickValueUpdate += aimStickUpdate;   // drag sẽ được truyển tham chiếu giá trị mới. tương tự  đăng ký hàm aimStickUpdate vào event onStickValueUpdate của joy aim
        cameraController = FindObjectOfType<CameraController>();
        animator = GetComponent<Animator>();
    }
    void moveStickUpdate(Vector2 inputVal) //Đây là hàm đã được đăng ký, thay đổi giá trị moveInput bằng với giá trị tham số inputVal trả về từ event onStickValueUpdate từ joy move
    {
        moveInput = inputVal;
    }
    void aimStickUpdate(Vector2 inputVal) //Đây là hàm đã được đăng ký, thay đổi giá trị aim bằng với giá trị tham số inputVal trả về từ event onStickValueUpdate từ joy aim
    { 
        aimInput = inputVal;
    }
    Vector3 stickInputToWorldDirection(Vector2 inputVal) //hàm này sử lí các giá trị từ các joystick để chuyển thành hướng/vector để tác động lên nhân vật theo dựa trên hướng của camera
    {
        Vector3 rightDir = mainCam.transform.right; //lấy hướng vector phải của camera ( camera đã bị rotate ) theo world space (local space thì lúc nào cũng là 0,0,-1)
        Vector3 forwardDir = Vector3.Cross(rightDir, Vector3.up); //Lấy vector hướng về trước của camera bằng cách tính tích có hướng (véc tơ vuông góc với 2 véc tơ đầu vào) của vector phải của camera
                                                                  //và vector hướng lên trên(kp của camera) của world space
        return rightDir * inputVal.x + forwardDir * inputVal.y; // Cộng vào vì 2 vector không ảnh hưởng lên nhau, cộng vào để thành vector tổng hợp
    }
    void Update()
    {
        PerformMove(); //Xử lí di chuyển và quay người
        UpdateCamera(); //Xử lí quay camera
    }

    private void PerformMove()
    {
        Vector3 moveDir = stickInputToWorldDirection(moveInput);  //Sau khi xử lí giá trị từ move joystick trả về thành hướng di chuyển của nhân vật theo hướng của camera, gán vào biến moveDir                                                            
        characterController.Move(moveDir * Time.deltaTime * moveSpeed); //di chuyển nhân vật
        UpdateAim(moveDir);
        float forward = Vector3.Dot(moveDir,transform.forward);
        float right = Vector3.Dot(moveDir,transform.right);
        animator.SetFloat("forwardSpeed",forward);
        animator.SetFloat("rightSpeed", right);
    }

    private void UpdateAim(Vector3 moveDir)
    {
        Vector3 aimDir = moveDir; // aimDir được gán với moveDir để nếu không tương tác với joy aim, nhân vật vẫn quay theo hướng di chuyển :>
        if (aimInput.magnitude != 0) //Nếu vector trả về từ aimstick khác không thì thực hiện gán aimDir vào giá trị đã được xử lí thành hướng quay của nhân vật theo hướng của camera - Quay theo joystick
        {
            aimDir = stickInputToWorldDirection(aimInput);
        }
        RotateTowards(aimDir);

    }

    private void UpdateCamera()
    {
        if (moveInput.magnitude != 0 && aimInput.magnitude!=0&& cameraController != null)
        {
            cameraController.AddYawnInput(moveInput.x); //truyền tham số giá trị khi joystick kéo trái kéo phải để làm camera quay theo
        }
    }

    private void RotateTowards(Vector3 aimDir)
    {
        float currentTurnSpeed = 0f;
        if (aimDir.magnitude != 0) //Nêu aimDir khác không, thực hiện quay nhân vật 
        {
            Quaternion preRotate = transform.rotation;
            float rotationSpeedAlpha = turnSpeed * Time.deltaTime; // tính toán tốc độ quay ko phụ thuộc vào framerate
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(aimDir, Vector3.up), rotationSpeedAlpha); // Quay từ từ theo trục y để quay nhân vật xung quanh
            Quaternion currentRotate = transform.rotation;
            float Dir = Vector3.Dot(aimDir,transform.right) >0 ? 1 : -1;
            float rotationDelta = Quaternion.Angle(preRotate, currentRotate) * Dir;
            currentTurnSpeed = rotationDelta / Time.deltaTime;
        }
        animationTurnSpeed = Mathf.Lerp(animationTurnSpeed, currentTurnSpeed, Time.deltaTime * animTurnSpeedToLerp);
        animator.SetFloat("turnSpeed", animationTurnSpeed);
    }
}

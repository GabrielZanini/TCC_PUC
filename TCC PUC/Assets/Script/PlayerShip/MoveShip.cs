using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveShip : MonoBehaviour
{
    public Camera camera;

    public bool canGoBackwards = false;
    public bool canRotate;
    public float spawnHeight = 10f;

    float v;
    float h;

    StatusShip status;
    ShipInput input;
    Animator animator;

    public bool hasTouchInput = false;



    Touch touch;
    int fingerId;
    Vector3 touchPosition;
    Vector3 touchOriginalPosition;
    Vector3 shipOriginalPosition;
    Vector3 shipOffsetPosition;
    Vector3 newShipPosition;

    void Start()
    {
        
        status = GetComponent<StatusShip>();
        input = GetComponent<ShipInput>();
        animator = GetComponent<Animator>();

        if (input == null)
        {
            Destroy(this);
        }

        transform.position = new Vector3(0, 0, spawnHeight - CameraManager.Instance.portraitSize);
        transform.rotation = Quaternion.identity;
    }



    void Update()
    {
        v = input.vertical;
        h = input.horizontal;

        MoveHorizontal();
        MoveVertical();

        MoveTouch();
    }



    private void MoveHorizontal()
    {
        if (!canGoBackwards && v < 0)
        {
            return;
        }

        transform.Translate(Vector3.right * h * status.currentSpeed * Time.deltaTime);
    }

    private void MoveVertical()
    {
        if (canRotate)
        {
            Rotate();
        }
        else
        {
            transform.Translate(Vector3.forward * v * status.currentSpeed * Time.deltaTime);
        }

        animator.SetFloat("Vertical", v);
    }

    private void MoveTouch()
    {
#if UNITY_EDITOR
        TouchEditor();
#elif UNITY_ANDROID
        TouchMobile();
#else
    
#endif
    }

    private void TouchMobile()
    {
        if (Input.touchCount > 0)
        {            
            touch = Input.GetTouch(0);
            touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

            if (touch.fingerId != fingerId)
            {
                fingerId = touch.fingerId;
                hasTouchInput = true;

                touchOriginalPosition = touchPosition;
                shipOriginalPosition = transform.position;
            }

            shipOffsetPosition = touchPosition - touchOriginalPosition;
            newShipPosition = CorrectNewPosition(shipOriginalPosition + shipOffsetPosition);

            transform.position = Vector3.Lerp(transform.position, newShipPosition, 0.5f);
        }
        else
        {
            fingerId = -1;
            hasTouchInput = false;
        }
    }

    private void TouchEditor()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (!hasTouchInput)
            {
                hasTouchInput = true;

                touchOriginalPosition = touchPosition;
                shipOriginalPosition = transform.position;
            }

            shipOffsetPosition = touchPosition - touchOriginalPosition;
            newShipPosition = CorrectNewPosition(shipOriginalPosition + shipOffsetPosition);

            transform.position = Vector3.Lerp(transform.position, newShipPosition, 0.5f);
        }
        else
        {
            hasTouchInput = false;
        }
    }

    private void Rotate()
    {
        if (canGoBackwards && v < 0)
        {
            h *= -1;
        } 

        transform.Rotate(Vector3.up * h * status.angularSpeed * Time.deltaTime);
    }

    Vector3 CorrectNewPosition(Vector3 newPosition)
    {
        newPosition = Vector3.Max(newPosition, new Vector3(0 - CameraManager.Instance.width / 2f + 1, 0, 0 - CameraManager.Instance.height / 2f + 1));
        newPosition = Vector3.Min(newPosition, new Vector3(CameraManager.Instance.width / 2f - 1, 0, CameraManager.Instance.height / 2f - 1));

        return newPosition;
    }
    
}

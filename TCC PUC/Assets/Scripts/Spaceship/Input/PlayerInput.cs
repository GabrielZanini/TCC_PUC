using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : ShipInput
{
    public string shootButtonName;
    public string verticalAxisName;
    public string horizontalAxisName;


    Vector3 touchPosition;
    Vector3 touchOriginalPosition;
    Vector3 shipOriginalPosition;
    Vector3 shipOffsetPosition;

    int fingerId = -1;
    bool hasTouchInput = false;



    public bool HasTouch {
        get { return Input.GetKey(KeyCode.Mouse0) || Input.touchCount > 0; }
    }

    private void Awake()
    {
        shootButton.AddButton(shootButtonName);
        verticalAxis.AddAxis(verticalAxisName);
        horizontalAxis.AddAxis(horizontalAxisName);
    }

    private void OnEnable()
    {
        if (autoShoot)
        {
            shootButton.SetFixValue(true, false, false);
        }
    }

    private void Start()
    {
        AddListeners();

               
    }

    private void Update()
    {
        //autoShoot = GameManager.Instance.Level.IsPlaying && GameManager.Instance.IsMobile;
        instantMovement = HasTouch;

        if (instantMovement)
        {
            MoveTouch();
        }
        else
        {
            ClearTouch();
        }
    }

    private void OnDisable()
    {
        instantMovement = false;
        //shootButton.ClearFixValue();
        shootButton.SetFixValue(false, false, true);
        ClearTouch();
    }

    private void OnDestroy()
    {
        RemoveListeners();
    }



    // Listeners

    void AddListeners()
    {
        GameManager.Instance.Level.OnStart.AddListener(ClearTouch);
        GameManager.Instance.Level.OnPause.AddListener(ClearTouch);
    }

    void RemoveListeners()
    {
        GameManager.Instance.Level.OnStart.RemoveListener(ClearTouch);
        GameManager.Instance.Level.OnPause.RemoveListener(ClearTouch);
    }





    private void MoveTouch()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        TouchEditor();
#elif UNITY_ANDROID
        TouchMobile();
#else
    
#endif
    }

    private void TouchMobile()
    {
        if (HasTouch)
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
            newPosition = shipOriginalPosition + shipOffsetPosition;
        }
        else
        {
            ClearTouch();
        }
    }

    private void TouchEditor()
    {
        if (HasTouch)
        {
            touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (!hasTouchInput)
            {
                hasTouchInput = true;

                touchOriginalPosition = touchPosition;
                shipOriginalPosition = transform.position;
            }

            shipOffsetPosition = touchPosition - touchOriginalPosition;
            newPosition = shipOriginalPosition + shipOffsetPosition;
        }
        else
        {
            ClearTouch();
        }
    }

    private void ClearTouch()
    {
        fingerId = -1;
        hasTouchInput = false;
    }
}





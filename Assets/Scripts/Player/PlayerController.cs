using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform sideMovementRoot;
    [SerializeField] private Transform leftLimit, rightLimit;
    [SerializeField] private float playerSpeed, sideMovementSensitivity, sideMovementLerpSpeed;

    private Vector2 inputDrag;
    private Vector2 previousMousePosition;

    private float LeftLimitX => leftLimit.localPosition.x;
    private float RightLimitX => rightLimit.localPosition.x;
    private float SideMovementTarget;

    private Vector2 MousePositionCM
    {
        get
        {
            Vector2 pixels = Input.mousePosition;
            var inches = pixels / Screen.dpi;
            var centimeters = inches * 2.54f;
            return centimeters;
        }
    }

    void Update()
    {
        HandleForwardMovement();
        HandleInput();
        HandleSideMovement();
    }

    private void HandleForwardMovement()
    {
        if (GameManager.Instance.IsGameStarted() && !GameManager.Instance.IsGameOver())
        {
            MoveForward();
        }
    }

    private void MoveForward()
    {
        transform.position += transform.forward * Time.deltaTime * playerSpeed;
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnMouseButtonDown();
        }

        if (Input.GetMouseButton(0))
        {
            OnMouseButton();
        }
        else
        {
            inputDrag = Vector2.zero;
        }
    }

    private void OnMouseButtonDown()
    {
        previousMousePosition = MousePositionCM;
        GameManager.Instance.OnGameStart();
    }

    private void OnMouseButton()
    {
        var deltaMouse = MousePositionCM - previousMousePosition;
        inputDrag = deltaMouse;
        previousMousePosition = MousePositionCM;
    }

    private void HandleSideMovement()
    {
        UpdateSideMovementTarget();
        ApplySideMovement();
    }

    private void UpdateSideMovementTarget()
    {
        SideMovementTarget += inputDrag.x * sideMovementSensitivity;
        SideMovementTarget = Mathf.Clamp(SideMovementTarget, LeftLimitX, RightLimitX);
    }

    private void ApplySideMovement()
    {
        var localPosition = sideMovementRoot.localPosition;
        localPosition.x = Mathf.Lerp(localPosition.x, SideMovementTarget, Time.deltaTime * sideMovementLerpSpeed);
        sideMovementRoot.localPosition = localPosition;
    }
}

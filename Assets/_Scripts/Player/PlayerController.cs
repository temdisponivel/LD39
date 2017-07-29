using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    public PlayerControllerData Data;
    public Rigidbody2D Body;

    public SpriteRenderer Model;

    private bool _hasPressedJump;
    private float _timeHoldingJumpButton;

    private List<EnergyDevice> _availableDevices;

    public bool Active;

    void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);

        _availableDevices = new List<EnergyDevice>();

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Reset()
    {
        Body = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (!Active)
            return;

        var pressedJump = Input.GetButton(Data.JumpButtonName);
        var horizontal = Input.GetAxis(Data.HorizontalAxisName);

        bool jumpNow = false;
        if (IsOnGround())
        {
            if (pressedJump)
            {
                if (_hasPressedJump)
                {
                    _timeHoldingJumpButton += Time.fixedDeltaTime;

                    if (_timeHoldingJumpButton >= Data.MaxTimeHoldingJumpButton)
                        jumpNow = true;
                }
                else
                {
                    _timeHoldingJumpButton = 0;
                    _hasPressedJump = true;
                }
            }
            else if (_hasPressedJump)
                jumpNow = true;
        }
        else
            horizontal *= Data.OnAirHorizontalForceMultiplier;

        if (jumpNow)
        {
            var delta = _timeHoldingJumpButton / Data.MaxTimeHoldingJumpButton;
            var jumpForce = Mathf.Lerp(Data.MinJumpForce, Data.MaxJumpForce, delta);
            Body.AddForce(Vector2.up * jumpForce, Data.JumpForceMode);

            _hasPressedJump = false;
        }

        // Adjust velocity to less than max velocity
        var force = horizontal * Data.HorizontalForce;
        if (Body.velocity.x > Data.MaxHorizontalVelocity)
            force = -(Body.velocity.x - Data.MaxHorizontalVelocity);
        else if (Body.velocity.x < -Data.MaxHorizontalVelocity)
            force = (Mathf.Abs(Body.velocity.x) - Data.MaxHorizontalVelocity);

        Body.AddForce(Vector2.right * force, Data.HorizontalForceMode);

        if (horizontal < 0 && !Model.flipX)
        {
            Model.transform.localScale = new Vector3(-Model.transform.localScale.x, Model.transform.localScale.y, Model.transform.localScale.z);
            Model.flipX = true;
        }
        else if (horizontal > 0 && Model.flipX)
        {
            Model.transform.localScale = new Vector3(Mathf.Abs(Model.transform.localScale.x), Model.transform.localScale.y, Model.transform.localScale.z);
            Model.flipX = false;
        }

        // If the camera is following us, call the update method
        if (CameraController.Instance.Data.TransformToFollow == transform)
            CameraController.Instance.UpdateCamera();
    }

    private bool IsOnGround()
    {
        if (IsFalling())
            return false;

        if (IsJumping())
            return false;

        return true;
    }

    private bool IsJumping()
    {
        var velocityY = Body.velocity.y;
        return velocityY > .01f;
    }

    private bool IsFalling()
    {
        var velocityY = Body.velocity.y;
        return velocityY < -.01f;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        var root = collider.transform.root;
        if (root.gameObject.CompareTag("EnergyDevices"))
        {
            var energyDevice = root.GetComponentInChildren<EnergyDevice>();
            if (!_availableDevices.Contains(energyDevice))
                _availableDevices.Add(energyDevice);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        var root = collider.transform.root;
        if (root.gameObject.CompareTag("EnergyDevices"))
        {
            var energyDevice = root.GetComponentInChildren<EnergyDevice>();
            _availableDevices.Remove(energyDevice);
        }
    }
}

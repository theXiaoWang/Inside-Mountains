using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private InputAction movement;
    [SerializeField] private float controlSpeed = 10f;
    [SerializeField] private float xRange = 5f;
    [SerializeField] private float yRange = 5f;
    [SerializeField] private float posPitchFactor = -8f;
    [SerializeField] private float posYawFactor = 4.5f;
    [SerializeField] private float controlFactor = -25f;

    private float xThrow, yThrow;
    

    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
    }

    private void ProcessRotation()
    {
        float pitchOfPosition = transform.localPosition.y * posPitchFactor;
        float pitchOfControlThrow = yThrow * controlFactor;
        float pitch = pitchOfPosition + pitchOfControlThrow;
        float yaw = transform.localPosition.x * posYawFactor;
        float row = xThrow * controlFactor;
        transform.localRotation= Quaternion.Euler(pitch,yaw,row);
    }

    private void ProcessTranslation()
    {
        xThrow = movement.ReadValue<Vector2>().x;
        yThrow = movement.ReadValue<Vector2>().y;

        float xOffset = xThrow * Time.deltaTime * controlSpeed;
        var localPosition = transform.localPosition;
        float rawXPos = localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        float yOffset = yThrow * Time.deltaTime * controlSpeed;
        float rawYPos = localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, localPosition.z);
    }

    private void OnEnable()
    {
        movement.Enable(); 
    }

    private void OnDisable()
    {
        movement.Disable();
    }
}

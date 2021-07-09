using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class PlayerControl : MonoBehaviour
{
    [Header("General Setup Settings")]
    [SerializeField] InputAction movement;
    [SerializeField] InputAction fire;
    [Tooltip("How fast player ship moves in any direction")]
    [SerializeField] private float controlSpeed = 10f;
    [Tooltip("How far player ship moves horizontally")]
    [SerializeField] private float xRange = 5f;
    [Tooltip("How far player ship moves vertically")]
    [SerializeField] private float yRange = 5f;
    
    [Header("Laser Gun Array")]
    [Tooltip("Add lasers here")]
    [SerializeField] private GameObject[] lasers;
    
    [Header("Screen position based tuning")]
    [Tooltip("Rotation speed factor of pitch axis on player ship ")]
    [SerializeField] private float posPitchFactor = -8f;
    [Tooltip("Rotation speed factor of yaw axis on player ship ")]
    [SerializeField] private float posYawFactor = 4.5f;
    
    [Header("Player input based tuning")]
    [SerializeField] private float controlFactor = -25f;

    private float xThrow, yThrow;
    

    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
    }

    private void ProcessFiring()
    {
        SetLasersActive(false);
        if (fire.ReadValue<float>() > 0)
            SetLasersActive(true);
    }

    private void SetLasersActive(bool isActive)
    {
        foreach (GameObject laser in lasers)
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
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
        fire.Enable();
    }

    private void OnDisable()
    {
        movement.Disable();
        fire.Disable();
    }
}

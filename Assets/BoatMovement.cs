using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float forwardSpeed = 5f;      // how fast the boat moves forward
    public float turnSpeed = 10f;        // how fast it can turn (keep low for "big boat" feel)
    public float turnSmoothness = 1f;    // how sluggish the steering is

    private float targetTurn = 0f;
    private float currentTurn = 0f;

    void Update()
    {
        // --- Forward motion ---
        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);

        // --- Steering input ---
        if (Input.GetKey(KeyCode.Q)) targetTurn = -1f;
        else if (Input.GetKey(KeyCode.E)) targetTurn = 1f;
        else targetTurn = 0f;

        // Smooth steering (slow drift like a big boat)
        currentTurn = Mathf.Lerp(currentTurn, targetTurn, Time.deltaTime * turnSmoothness);

        // Apply rotation
        transform.Rotate(Vector3.up, currentTurn * turnSpeed * Time.deltaTime);
    }
}

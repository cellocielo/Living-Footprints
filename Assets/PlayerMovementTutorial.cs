using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovementTutorial : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float groundDrag;
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [HideInInspector] public float walkSpeed;
    [HideInInspector] public float sprintSpeed;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    [Header("Footstep Audio")]
    public AudioSource footstepAudioSource;
    public AudioClip[] footstepClips;
    public float stepInterval = 0.5f;
    public float minVelocityForSound = 0.5f;
    [Range(0.5f, 1.5f)]
    public float pitchVariation = 0.2f;
    [Range(0.5f, 1.5f)]
    public float volumeVariation = 0.2f;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;
    
    Vector3 moveDirection;
    Rigidbody rb;
    
    // Footstep timing
    private float stepTimer = 0f;
    private bool wasMovingLastFrame = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;
        
        if (footstepAudioSource == null)
        {
            footstepAudioSource = GetComponent<AudioSource>();
            if (footstepAudioSource == null)
            {
                footstepAudioSource = gameObject.AddComponent<AudioSource>();
            }
        }
    }

    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);

        MyInput();
        SpeedControl();
        HandleFootstepAudio();

        if (grounded)
            rb.linearDamping = groundDrag;
        else
            rb.linearDamping = 0;
            
        if (IsAnyUIActive())
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        else if (!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }
    }

        private void HandleFootstepAudio()
    {
        // Check if actually moving (not just input)
        Vector3 horizontalVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        bool isMoving = horizontalVelocity.magnitude > 0.5f; // Simple threshold
        
        if (grounded && isMoving)
        {
            stepTimer += Time.deltaTime;
            
            // Fixed step timing - no speed variation
            if (stepTimer >= stepInterval)
            {
                PlayFootstepSound();
                stepTimer = 0f;
            }
        }
        else
        {
            stepTimer = 0f; // Stop immediately when not moving
        }
    }

    private void PlayFootstepSound()
    {
        if (footstepClips.Length == 0 || footstepAudioSource == null)
            return;

        AudioClip clipToPlay = footstepClips[Random.Range(0, footstepClips.Length)];
        
        footstepAudioSource.pitch = 1f + Random.Range(-pitchVariation, pitchVariation);
        float volume = 1f + Random.Range(-volumeVariation, volumeVariation);
    
        footstepAudioSource.PlayOneShot(clipToPlay, volume);
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    private bool IsAnyUIActive()
    {
        GameObject[] uiObjects = GameObject.FindGameObjectsWithTag("UIPopUp");
        foreach (GameObject ui in uiObjects)
        {
            if (ui.activeInHierarchy)
                return true;
        }
        return false;
    }
}
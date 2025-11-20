using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movementInput;
    private bool isPlayingFootstep = false;
    public float footstepSteps = 0.5f;
    //private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = movementInput * moveSpeed;
    }

    public void Move(InputAction.CallbackContext context)
    {
        //animator.SetBool("IsWalking", true);

        if (context.canceled)
        {
            //    animator.SetBool("IsWalking", false);
            //    animator.SetFloat("LastInputX", movementInput.x);
            //    animator.SetFloat("LastInputY", movementInput.y);
        }
        movementInput = context.ReadValue<Vector2>();
        //animator.SetFloat("InputX", movementInput.x);
        //animator.SetFloat("InputY", movementInput.y);
    }
    void StartFootSteps()
    {
        isPlayingFootstep = true;
        InvokeRepeating(nameof(PlayFootStep), 0f, footstepSteps);
    }
    void StopFootSteps()
    {
        isPlayingFootstep = false;
        CancelInvoke(nameof(PlayFootStep));
    }
    void PlayFootStep()
    {
        SoundEffectManager.Play("footStep");
    }
}

using System.Collections;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] Vector3 playerVelocity;
    [SerializeField] bool groundedPlayer;
    [SerializeField] float playerSpeed;
    [SerializeField] float gravityValue;
    [SerializeField] GameObject activeChar;
    [SerializeField] float moveHorizontal;
    [SerializeField] float moveVertical;
    [SerializeField] float speed = 5f;
    [SerializeField] float rotateSpeed = 3f;
    [SerializeField] float jumpHeight = 1.2f;
    [SerializeField] bool isJumping;
    public Animator anim;


    void Start()
    {
        speed = 5f;
        rotateSpeed = 3f;
        playerSpeed = speed;
        gravityValue = -20f;

        if (controller == null)
        {
            controller = GetComponent<CharacterController>();
        }

        if (controller != null)
        {
            controller.center = new Vector3(0, 1f, 0);
            controller.height = 2f;
            controller.stepOffset = 0.3f;
        }

        if (anim == null)
        {
            if (activeChar != null)
            {
                anim = activeChar.GetComponent<Animator>();
            }
            if (anim == null)
            {
                anim = GetComponentInChildren<Animator>();
            }
        }

        if (anim != null)
        {
            anim.SetBool("isRunning", false);
        }
    }


    void Update()
    {
        if (controller == null)
        {
            controller = GetComponent<CharacterController>();
        }

        groundedPlayer = controller != null && controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }

        transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        float curSpeed = speed * Input.GetAxis("Vertical");
        if (controller != null)
        {
            controller.SimpleMove(forward * curSpeed);
        }

        Animator currentAnim = anim;
        if (currentAnim == null && activeChar != null)
        {
            currentAnim = activeChar.GetComponent<Animator>();
        }

        if (Input.GetKeyDown(KeyCode.Space) && groundedPlayer)
        {
            isJumping = true;
            if (currentAnim != null)
            {
                currentAnim.Play("Jump");
            }
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravityValue);
            StartCoroutine(ResetJump());
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        if (controller != null)
        {
            controller.Move(playerVelocity * Time.deltaTime);
        }

        bool isMoving = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || 
                        Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || 
                        Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || 
                        Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || 
                        Mathf.Abs(Input.GetAxis("Vertical")) > 0.05f || 
                        Mathf.Abs(Input.GetAxis("Horizontal")) > 0.05f;

        if (isMoving)
        {
            if (controller != null)
            {
                controller.minMoveDistance = 0.001f;
            }

            if (!isJumping)
            {
                if (currentAnim != null)
                {
                    currentAnim.SetBool("isRunning", true);
                    if (currentAnim.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
                    {
                        currentAnim.Play("Standard Run");
                    }
                }
            }
        }
        else
        {
            if (controller != null)
            {
                controller.minMoveDistance = 0.001f;
            }

            if (!isJumping)
            {
                if (currentAnim != null)
                {
                    currentAnim.SetBool("isRunning", false);
                    if (currentAnim.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
                    {
                        currentAnim.Play("Idle");
                    }
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        GemControl gem = other.GetComponent<GemControl>();
        if (gem != null)
        {
            gem.CollectGem();
        }
        else if (other.gameObject.name.Contains("Gem"))
        {
            ScoreControl.totalScore += 100;
            Destroy(other.gameObject);
        }
    }

    IEnumerator ResetJump()
    {
        yield return new WaitForSeconds(0.8f);
        isJumping = false;

        Animator currentAnim = anim;
        if (currentAnim == null && activeChar != null)
        {
            currentAnim = activeChar.GetComponent<Animator>();
        }

        if (currentAnim != null)
        {
            bool isMoving = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || 
                            Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || 
                            Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || 
                            Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || 
                            Mathf.Abs(Input.GetAxis("Vertical")) > 0.05f || 
                            Mathf.Abs(Input.GetAxis("Horizontal")) > 0.05f;

            if (isMoving)
            {
                currentAnim.SetBool("isRunning", true);
                currentAnim.Play("Standard Run");
            }
            else
            {
                currentAnim.SetBool("isRunning", false);
                currentAnim.Play("Idle");
            }
        }
    }
}












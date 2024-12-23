using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    CharacterController character;
    [HideInInspector] public Transform player;

    public float moveSpeed = 10.0f;
    public float gravity = 10.0f;
    public float jumpPower = 10.0f;
    public float riseTime = 1.0f;
    public float knockBack = 10.0f;

    public float rayLength = 0.4f;
    float riseTimeTemp = 0.0f;
    
    Animator anime;

    float hor;
    Vector3 moveDirection;
    public Vector3 gravityDirection;

    [HideInInspector] public bool stageOut;
    bool playPossible;
    public bool jump;
    bool unrivaled;
    bool jumpPossible;
    bool isGrounded;
    bool stepSE;

    bool clear, death;

    void Start()
    {
        character = GetComponent<CharacterController>();

        anime = GetComponent<Animator>();

        player = this.transform;
    }

    private void FixedUpdate()
    {
        InputCheck();
        Rotate();
        Animation();
        if (GameManager.Instance.mainGame)
        {
            KnockBack();
        }

        if (playPossible)
        {          
            Move();                 
        }
    }

    private void Update()
    {
        RayCast();
        Grabity();

        if (playPossible)
        {
            Jump();
        }
    }

    void InputCheck()
    {
        if (GameManager.Instance.mainGame && !GameManager.Instance.state_damage && !stageOut)
        {
            playPossible = true;
        }
        else
        {
            playPossible = false;
        }
    }

    void RayCast()
    {
        var distance = rayLength;

        var groundRay_Center = new Ray(transform.position + Vector3.up * distance, Vector3.down);
        var groundRay_Front = new Ray(transform.position + Vector3.left / 2 + Vector3.up * distance, Vector3.down);
        var groundRay_Rear = new Ray(transform.position + Vector3.right / 2 + Vector3.up * distance, Vector3.down);
        RaycastHit objectStatus_C;
        RaycastHit objectStatus_F;
        RaycastHit objectStatus_R;

        Debug.DrawRay(groundRay_Center.origin, groundRay_Center.direction * distance, Color.red, 100.0f, false);
        Debug.DrawRay(groundRay_Front.origin, groundRay_Center.direction * distance, Color.red, 100.0f, false);
        Debug.DrawRay(groundRay_Rear.origin, groundRay_Center.direction * distance, Color.red, 100.0f, false);

        if (Physics.Raycast(groundRay_Center, out objectStatus_C, distance))
        {
            if (objectStatus_C.collider.tag == "Ground" && !jump)
            {
                isGrounded = true;
                jumpPossible = true;
            }
        }
        else if(Physics.Raycast(groundRay_Front, out objectStatus_F, distance))
        {
            if (objectStatus_F.collider.tag == "Ground" && !jump)
            {
                isGrounded = true;
                jumpPossible = true;
            }
        }
        else if(Physics.Raycast(groundRay_Rear, out objectStatus_R, distance))
        {
            if (objectStatus_R.collider.tag == "Ground" && !jump)
            {
                isGrounded = true;
                jumpPossible = true;
            }
        }
        else
        {
            isGrounded = false;
            stepSE = true;
        }

        if (!jump && !isGrounded)
        {
            jumpPossible = false;
        }

        if(stepSE)
        {
            if (isGrounded)
            {
                SoundManager.Instance.PlaySE_Game(8);
                stepSE = false;
            }
        }
    }

    void Grabity()
    {
        gravityDirection.y -= gravity * Time.deltaTime;
        character.Move(gravityDirection * Time.deltaTime);

        if (isGrounded)
        {
            riseTimeTemp = 0;
            gravityDirection.y = Mathf.Clamp(gravityDirection.y, -0.1f, jumpPower);
        }
        else
        {
        }
    }

    void Jump()
    {
        if (isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                gravityDirection.y = jumpPower;

                SoundManager.Instance.PlaySE_Game(3);
                Instantiate(EffectManager.Instance.playerFX[2], transform.position, Quaternion.identity);

                jump = true;
            }
            //Debug.Log("isGrounded");
        }
        else
        {
            if (jumpPossible && Input.GetButtonUp("Jump") || riseTimeTemp > riseTime)
            {
                jumpPossible = false;
                jump = false;
            }

            if (jumpPossible && Input.GetButton("Jump") && riseTimeTemp <= riseTime)
            {
                riseTimeTemp += Time.deltaTime;
                gravityDirection.y = jumpPower;
            }
        }
    }

    void Move()
    {
        hor = Input.GetAxis("Horizontal");
        //Debug.Log(hor);

        moveDirection.x = hor;

        moveDirection = new Vector3(moveDirection.normalized.x, 0, 0) * moveSpeed;

        character.Move(moveDirection * Time.deltaTime);
    }

    void Rotate()
    {
        if (hor > 0)
        {
            player.rotation = Quaternion.Euler(0, 0, 0);            
        }
        else if (hor < 0)
        {
            player.rotation = Quaternion.Euler(0, 180, 0);           
        }
        else
        {
        }
    }

    public void KnockBack()
    {
        if (anime.GetCurrentAnimatorStateInfo(0).IsName("Damage"))
        {
            if (anime.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.8)
            {
                moveDirection = -transform.right * knockBack;
                character.Move(moveDirection * Time.deltaTime);

                jump = false;
            }
            else
            {
                GameManager.Instance.state_damage = false;
                unrivaled = false;
            }
        }
    }

    void Animation()
    {
        if (GameManager.Instance.gameClear && !clear)
        {
            anime.SetTrigger("Clear");
            clear = true;
        }

        if (GameManager.Instance.gameOver && !death)
        {
            anime.SetTrigger("Death");
            death = true;
        }

        if (GameManager.Instance.state_damage && GameManager.Instance.mainGame && !unrivaled)
        {
            unrivaled = true;
            anime.SetTrigger("Damage");
        }

        if (!isGrounded || jump)
        {
            anime.SetBool("Jump", true);
            return;
        }
         anime.SetBool("Jump", false);

        if (hor != 0 && !stageOut && GameManager.Instance.mainGame)
        {
            anime.SetBool("Run", true);
            return;
        }
         anime.SetBool("Run", false);
    }

    public void WalkAnimation()
    {
        SoundManager.Instance.PlaySE_Game(2);
    }
}

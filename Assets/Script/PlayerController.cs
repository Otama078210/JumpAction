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

    float rayLength = 0.1f;
    float riseTimeTemp = 0.0f;
    
    Animator anime;

    float hor;
    Vector3 moveDirection;
    public Vector3 gravityDirection;

    [HideInInspector] public bool stageOut;
    bool playPossible;
    bool unrivaled;
    bool jump;
    bool isGrounded;

    bool clear, death;

    public GameObject hitPos;

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

        var groundRay = new Ray(transform.position + Vector3.up * distance, Vector3.down);
        RaycastHit objectStatus;

        Debug.DrawRay(groundRay.origin, groundRay.direction * distance, Color.red, 100.0f, false);

        if (Physics.Raycast(groundRay, out objectStatus, distance))
        {
            if (objectStatus.collider.tag == "Ground")
            {
                isGrounded = true;
                jump = true;
            }
        }
        else
        {
            isGrounded = false;
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
            }
            //Debug.Log("isGrounded");
        }
        else
        {
            if (jump && Input.GetButtonUp("Jump") || riseTimeTemp > riseTime)
            {
                jump = false;
            }

            if (jump && Input.GetButton("Jump") && riseTimeTemp <= riseTime)
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
            //アニメーションの再生時間が0.8秒になるまで
            if (anime.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.8)
            {
                moveDirection = -transform.right * knockBack;
                character.Move(moveDirection * Time.deltaTime);
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

        if (!isGrounded)
        {
            anime.SetBool("Jump", true);
            return;
        }
         anime.SetBool("Jump", false);

        if (hor != 0 && !stageOut)
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

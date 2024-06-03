using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerScript : MonoBehaviour
{

    CharacterController con;
    Animator anim;
    Rigidbody rb;
    AudioSource audioSource;
    BossScript bossScript;
    public AudioClip jumpSound;

    float normalSpeed = 3f; // �ʏ펞�̈ړ����x
    float sprintSpeed = 5f; // �_�b�V�����̈ړ����x
    float jump = 8f;        // �W�����v��
    float gravity = 10f;    // �d�͂̑傫��
    bool isJumping = false;
    bool isAlive = true;
    Vector3 moveDirection = Vector3.zero;

    Vector3 startPos;


    Vector3 ZmodifyPos;

    [SerializeField] GameObject parentObj;

    void Start()
    {
        con = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        bossScript = GetComponent<BossScript>();
        startPos = transform.position;

        ZmodifyPos.z = 0;
    }

    void Update()
    {
        if (isAlive == true)
        {
            anim.SetBool("is_Alive", true);
            // �ړ����x���擾
            float speed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : normalSpeed;

            // �J�����̌�������ɂ������ʕ����̃x�N�g��
            Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;


            // �O�㍶�E�̓��́iWASD�L�[�j����A�ړ��̂��߂̃x�N�g�����v�Z
            // Input.GetAxis("Vertical") �͑O��iWS�L�[�j�̓��͒l
            // Input.GetAxis("Horizontal") �͍��E�iAD�L�[�j�̓��͒l
            Vector3 moveZ = cameraForward * Input.GetAxis("Vertical") * speed * 0;  //�@�O��i�J������j�@ 
            Vector3 moveX = Camera.main.transform.right * Input.GetAxis("Horizontal") * speed; // ���E�i�J������j

            // isGrounded �͒n�ʂɂ��邩�ǂ����𔻒肵�܂�
            // �n�ʂɂ���Ƃ��̓W�����v���\��

            // �d�͂���������

            moveDirection.y += Physics.gravity.y * Time.deltaTime;

            if (con.isGrounded && isJumping == false)
            {
                moveDirection.y = -0.5f;

                if (Input.GetButtonDown("Jump"))
                {
                    audioSource.PlayOneShot(jumpSound);
                    moveDirection.y = jump;
                    isJumping = true;
                    anim.SetBool("is_jumping", true);

                }


            }
            else if (con.isGrounded && isJumping == true)
            {
                isJumping = false;
                anim.SetBool("is_jumping", false);

            }



            moveDirection = moveZ + moveX + new Vector3(0, moveDirection.y, 0);


            // �ړ��̃A�j���[�V����
            anim.SetFloat("MoveSpeed", (moveZ + moveX).magnitude);

            // �v���C���[�̌�������͂̌����ɕύX�@
            transform.LookAt(transform.position + moveZ + moveX);

            // Move �͎w�肵���x�N�g�������ړ������閽��
            con.Move(moveDirection * Time.deltaTime);

            if (transform.position.z > 0 || transform.position.z < 0)
            {
                transform.position = ZmodifyPos;
            }
        }
        else if(isAlive == false)
        {
            anim.SetBool("is_Alive", false);
        }
    }

    public void MoveStartPos()
    {
        con.enabled = false;

        moveDirection = Vector3.zero;
        transform.position = startPos + Vector3.up * 3f;
        transform.rotation = Quaternion.identity;

        con.enabled = true;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Floor")
            transform.SetParent(parentObj.transform);

    }

    // �Փ˂������������ɌĂ΂��
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
       if(hit.gameObject.tag == "Enemy")
        {
            if (transform.position.y > hit.gameObject.transform.position.y + 0.4f)
            {
                moveDirection.y = jump;
                Destroy(hit.gameObject);
                anim.SetBool("is_jumping", false);
                anim.SetBool("is_jumping", true);
            }
            else { isAlive = false; }
        }
        if (hit.gameObject.tag == "Boss")
        {
           isAlive = false;
        }
    }


    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.name == "Floor")
            transform.SetParent(null);
    }
}
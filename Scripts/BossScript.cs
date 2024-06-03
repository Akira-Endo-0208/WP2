using DG.Tweening.Core.Easing;
using Unity.VisualScripting;
using UnityEngine;
using static MoveEnemy;

public class BossScript : MonoBehaviour
{

    public enum BossState
    {
        Wait,
        Walk,
    };

    // �G�̏��
    private BossState state;

    private CharacterController enemyController;
    private Animator animator;

    private Transform playerTransform;

    [SerializeField]
    private BoxCollider boxCollider;

    //�@���x
    private Vector3 velocity;
    //�@�ړ�����
    private Vector3 direction;
    //�@�����X�s�[�h
    [SerializeField]
    private float walkSpeed = 5.0f;
    //�@�o�ߎ���
    private float elapsedTime;
    //�@�҂�����
    [SerializeField]
    private float waitTime = 5f;

    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        enemyController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        gameManager = GetComponent<GameManager>();

        velocity = Vector3.zero;

        elapsedTime = 0f;
        SetState(BossState.Wait);
    }

    // Update is called once per frame
    void Update()
    {
        

        if (state == BossState.Walk)
        {
            if (enemyController.isGrounded)
            {
                velocity = Vector3.zero;
                animator.SetFloat("Speed", 2.0f);
                velocity = new Vector3(1, velocity.y, velocity.z) * walkSpeed;
            }
        }

        velocity.y += Physics.gravity.y * Time.deltaTime;
        enemyController.Move(velocity * Time.deltaTime);

    }

    public void SetState(BossState tempState, Transform targetObj = null)
    {
        if (tempState == BossState.Walk)
        {
            
            elapsedTime = 0f;
            state = tempState;
            
        }
        else if (tempState == BossState.Wait)
        {
            elapsedTime = 0f;
            state = tempState;

            velocity = Vector3.zero;
            animator.SetFloat("Speed", 0f);
        }
    }

    public BossState GetState()
    {
        return state;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            BgmManager.Instance.Play("Rage of the Forest");
            SetState(BossState.Walk);
            
        }
    }
}

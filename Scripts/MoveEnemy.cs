
using UnityEngine;

public class MoveEnemy : MonoBehaviour
{

    public enum EnemyState
    {
        Walk,
        Wait,
        Chase
    };

    private CharacterController enemyController;
    private Animator animator;
    //�@�ړI�n
    private Vector3 destination;
    //�@�����X�s�[�h
    [SerializeField]
    private float walkSpeed = 1.0f;
    //�@���x
    private Vector3 velocity;
    //�@�ړ�����
    private Vector3 direction;

    //�@SetPosition�X�N���v�g
    private SetPosition setPosition;
    //�@�҂�����
    [SerializeField]
    private float waitTime = 5f;
    //�@�o�ߎ���
    private float elapsedTime;
    // �G�̏��
    private EnemyState state;
    //�@�v���C���[Transform
    private Transform playerTransform;

    //�@�ǂƂ̐ڐG�𔻒肷�郌�C���΂��ꏊ
    [SerializeField]
    private Transform rayTransform;
    //�@���C���΂�����
    [SerializeField]
    private float rayDistance = 2f;
    //�@�ŏ��ɕǂɏՓ˂��Ă���̌o�ߎ���
    private float elapsedCollisionWall = Mathf.Infinity;
    //�@�ŏ��ɕǂɏՓ˂��Ă��玟�ɔ��肷��܂ł̎���
    [SerializeField]
    private float avoidanceTimeCollisionWall = 3f;
    [SerializeField]
    private LayerMask obstacleLayer;


    // Use this for initialization
    void Start()
    {
        enemyController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        setPosition = GetComponent<SetPosition>();
        setPosition.CreateRandomPosition();
        velocity = Vector3.zero;

        elapsedTime = 0f;
        SetState(EnemyState.Walk);
    }

    // Update is called once per frame
    void Update()
    {
        //�@�����܂��̓L�����N�^�[��ǂ���������
        if (state == EnemyState.Walk || state == EnemyState.Chase)
        {
            //�@�L�����N�^�[��ǂ��������Ԃł���΃L�����N�^�[�̖ړI�n���Đݒ�
            if (state == EnemyState.Chase)
            {
                setPosition.SetDestination(playerTransform.position);

            }
            if (enemyController.isGrounded)
            {
                velocity = Vector3.zero;
                animator.SetFloat("Speed", 2.0f);
                direction = (setPosition.GetDestination() - transform.position).normalized;
                transform.LookAt(new Vector3(setPosition.GetDestination().x, transform.position.y, setPosition.GetDestination().z));
                velocity = direction * walkSpeed;

            }

            //�@�ړI�n�ɓ����������ǂ����̔���
            if (Vector3.Distance(transform.position, setPosition.GetDestination()) < 1.0f)
            {
                SetState(EnemyState.Wait);
                animator.SetFloat("Speed", 0.0f);
            }
            //�@�������Ă������莞�ԑ҂�
        }
        else if (state == EnemyState.Wait)
        {
            elapsedTime += Time.deltaTime;

            //�@�҂����Ԃ��z�����玟�̖ړI�n��ݒ�
            if (elapsedTime > waitTime)
            {
                setPosition.CreateRandomPosition();
                SetState(EnemyState.Walk);
            }
        }


        //�@�ǂɓ˓����Ă��鎞
        if (elapsedCollisionWall >= avoidanceTimeCollisionWall)
        {
            if (Physics.Linecast(rayTransform.position, rayTransform.position + rayTransform.forward * rayDistance, obstacleLayer)
                || Physics.Linecast(rayTransform.position, rayTransform.position + (rayTransform.forward + rayTransform.right).normalized * rayDistance, obstacleLayer)
                || Physics.Linecast(rayTransform.position, rayTransform.position + (rayTransform.forward - rayTransform.right).normalized * rayDistance, obstacleLayer)
                )
            {
                Debug.Log("�ǂƐڐG");
                setPosition.CreateRandomPosition();
                elapsedCollisionWall = 0f;
                SetState(EnemyState.Walk);
            }
        }
        //�@��U�ړI�n���Đݒ肵������̉�����Ԃ�݂���
        elapsedCollisionWall += Time.deltaTime;
        if (elapsedCollisionWall >= avoidanceTimeCollisionWall)
        {
            elapsedCollisionWall = avoidanceTimeCollisionWall;
        }

        velocity.y += Physics.gravity.y * Time.deltaTime;
        enemyController.Move(velocity * Time.deltaTime);

    }

    //�@�G�L�����N�^�[�̏�ԕύX���\�b�h
    public void SetState(EnemyState tempState, Transform targetObj = null)
    {
        if (tempState == EnemyState.Walk)
        {

            elapsedTime = 0f;
            state = tempState;
            setPosition.CreateRandomPosition();
        }
        else if (tempState == EnemyState.Chase)
        {
            state = tempState;
            //�@�ҋ@��Ԃ���ǂ�������ꍇ������̂�Off

            //�@�ǂ�������Ώۂ��Z�b�g
            playerTransform = targetObj;
        }
        else if (tempState == EnemyState.Wait)
        {
            elapsedTime = 0f;
            state = tempState;

            velocity = Vector3.zero;
            animator.SetFloat("Speed", 0f);
        }
    }
    //�@�G�L�����N�^�[�̏�Ԏ擾���\�b�h
    public EnemyState GetState()
    {
        return state;
    }
}

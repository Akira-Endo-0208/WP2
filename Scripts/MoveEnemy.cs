
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
    //　目的地
    private Vector3 destination;
    //　歩くスピード
    [SerializeField]
    private float walkSpeed = 1.0f;
    //　速度
    private Vector3 velocity;
    //　移動方向
    private Vector3 direction;

    //　SetPositionスクリプト
    private SetPosition setPosition;
    //　待ち時間
    [SerializeField]
    private float waitTime = 5f;
    //　経過時間
    private float elapsedTime;
    // 敵の状態
    private EnemyState state;
    //　プレイヤーTransform
    private Transform playerTransform;

    //　壁との接触を判定するレイを飛ばす場所
    [SerializeField]
    private Transform rayTransform;
    //　レイを飛ばす距離
    [SerializeField]
    private float rayDistance = 2f;
    //　最初に壁に衝突してからの経過時間
    private float elapsedCollisionWall = Mathf.Infinity;
    //　最初に壁に衝突してから次に判定するまでの時間
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
        //　見回りまたはキャラクターを追いかける状態
        if (state == EnemyState.Walk || state == EnemyState.Chase)
        {
            //　キャラクターを追いかける状態であればキャラクターの目的地を再設定
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

            //　目的地に到着したかどうかの判定
            if (Vector3.Distance(transform.position, setPosition.GetDestination()) < 1.0f)
            {
                SetState(EnemyState.Wait);
                animator.SetFloat("Speed", 0.0f);
            }
            //　到着していたら一定時間待つ
        }
        else if (state == EnemyState.Wait)
        {
            elapsedTime += Time.deltaTime;

            //　待ち時間を越えたら次の目的地を設定
            if (elapsedTime > waitTime)
            {
                setPosition.CreateRandomPosition();
                SetState(EnemyState.Walk);
            }
        }


        //　壁に突入している時
        if (elapsedCollisionWall >= avoidanceTimeCollisionWall)
        {
            if (Physics.Linecast(rayTransform.position, rayTransform.position + rayTransform.forward * rayDistance, obstacleLayer)
                || Physics.Linecast(rayTransform.position, rayTransform.position + (rayTransform.forward + rayTransform.right).normalized * rayDistance, obstacleLayer)
                || Physics.Linecast(rayTransform.position, rayTransform.position + (rayTransform.forward - rayTransform.right).normalized * rayDistance, obstacleLayer)
                )
            {
                Debug.Log("壁と接触");
                setPosition.CreateRandomPosition();
                elapsedCollisionWall = 0f;
                SetState(EnemyState.Walk);
            }
        }
        //　一旦目的地を再設定したら一定の回避時間を設ける
        elapsedCollisionWall += Time.deltaTime;
        if (elapsedCollisionWall >= avoidanceTimeCollisionWall)
        {
            elapsedCollisionWall = avoidanceTimeCollisionWall;
        }

        velocity.y += Physics.gravity.y * Time.deltaTime;
        enemyController.Move(velocity * Time.deltaTime);

    }

    //　敵キャラクターの状態変更メソッド
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
            //　待機状態から追いかける場合もあるのでOff

            //　追いかける対象をセット
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
    //　敵キャラクターの状態取得メソッド
    public EnemyState GetState()
    {
        return state;
    }
}

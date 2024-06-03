using UnityEngine;
using System.Collections;

public class FallFloor : MonoBehaviour
{

    //　床が落下するまでの時間
    [SerializeField]
    private float timeToFall = 3f;
    //　主人公が床に乗っていたトータル時間
    private float totalTime = 0f;
    private Rigidbody rigid;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        rigid.isKinematic = true;
    }

    void Update()
    {
        //　床が落下する時間を超えたらリジッドボディのisKinematicをfalseに
        //　isKinematicをfalseにしたことで重力が働く
        if (totalTime >= timeToFall)
        {
            rigid.isKinematic = false;
        }
    }

    //　主人公が床に乗っている時に呼び出す
    public void ReceiveForce()
    {
        //　床に乗っている時間を足していく
        totalTime += Time.deltaTime;
    }

    void OnCollisionEnter(Collision col)
    {
        //　床が落下し、衝突したゲームオブジェクトのレイヤーがFieldだった時床を消去
        if (col.gameObject.layer == LayerMask.NameToLayer("Field"))
        {
            Destroy(this.gameObject, 0f);
        }
    }
}
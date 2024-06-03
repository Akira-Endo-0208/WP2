using UnityEngine;
using System.Collections;

public class FallFloor : MonoBehaviour
{

    //�@������������܂ł̎���
    [SerializeField]
    private float timeToFall = 3f;
    //�@��l�������ɏ���Ă����g�[�^������
    private float totalTime = 0f;
    private Rigidbody rigid;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        rigid.isKinematic = true;
    }

    void Update()
    {
        //�@�����������鎞�Ԃ𒴂����烊�W�b�h�{�f�B��isKinematic��false��
        //�@isKinematic��false�ɂ������Ƃŏd�͂�����
        if (totalTime >= timeToFall)
        {
            rigid.isKinematic = false;
        }
    }

    //�@��l�������ɏ���Ă��鎞�ɌĂяo��
    public void ReceiveForce()
    {
        //�@���ɏ���Ă��鎞�Ԃ𑫂��Ă���
        totalTime += Time.deltaTime;
    }

    void OnCollisionEnter(Collision col)
    {
        //�@�����������A�Փ˂����Q�[���I�u�W�F�N�g�̃��C���[��Field����������������
        if (col.gameObject.layer == LayerMask.NameToLayer("Field"))
        {
            Destroy(this.gameObject, 0f);
        }
    }
}
using UnityEngine;
using System.Collections;
 
public class AddForceFloor : MonoBehaviour
{

    void OnControllerColliderHit(ControllerColliderHit col)
    {
        //�@FallFloor���ݒ肳�ꂽ�Q�[���I�u�W�F�N�g�ƐڐG
        if (col.gameObject.layer == LayerMask.NameToLayer("FallFloor"))
        {
            //�@�L�����N�^�[�̉������Ƀ��C���΂�FallFloor�ƐڐG�����珰�ɐڐG���Ă��鎖��m�点��
            Debug.DrawLine(transform.position + Vector3.up * 0.1f, transform.position + Vector3.up * -0.2f, Color.red);
            if (Physics.Linecast(transform.position + Vector3.up * 0.1f, transform.position + Vector3.up * -0.2f, LayerMask.GetMask("FallFloor")))
            {
                col.gameObject.GetComponent<FallFloor>().ReceiveForce();
            }
        }
    }
}

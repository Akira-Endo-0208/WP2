using UnityEngine;
using System.Collections;
 
public class AddForceFloor : MonoBehaviour
{

    void OnControllerColliderHit(ControllerColliderHit col)
    {
        //　FallFloorが設定されたゲームオブジェクトと接触
        if (col.gameObject.layer == LayerMask.NameToLayer("FallFloor"))
        {
            //　キャラクターの下方向にレイを飛ばしFallFloorと接触したら床に接触している事を知らせる
            Debug.DrawLine(transform.position + Vector3.up * 0.1f, transform.position + Vector3.up * -0.2f, Color.red);
            if (Physics.Linecast(transform.position + Vector3.up * 0.1f, transform.position + Vector3.up * -0.2f, LayerMask.GetMask("FallFloor")))
            {
                col.gameObject.GetComponent<FallFloor>().ReceiveForce();
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SearchCharacter : MonoBehaviour {
 
    private MoveEnemy moveEnemy;
    [SerializeField]
    private LayerMask obstacleLayer;
    [SerializeField]
    private SphereCollider searchArea;
    [SerializeField]
    private float searchAngle = 130f;
    
    void Start() {
        moveEnemy = GetComponentInParent<MoveEnemy>();
    }
 
    void OnTriggerStay(Collider col) {
        //　プレイヤーキャラクターを発見
        if (col.tag == "Player") {
            //　敵キャラクターの状態を取得
            MoveEnemy.EnemyState state = moveEnemy.GetState();
 
            //Debug.DrawLine(transform.position + Vector3.up, col.transform.position + Vector3.up, Color.blue);

            var playerDirection = col.transform.position - transform.position;
            //　敵の前方からの主人公の方向
            var angle = Vector3.Angle(transform.forward, playerDirection);
            //　サーチする角度内だったら発見
            if ((angle <= searchAngle) && !Physics.Linecast(transform.position + Vector3.up, col.transform.position + Vector3.up, obstacleLayer))
            {

                moveEnemy.SetState(MoveEnemy.EnemyState.Chase, col.transform);

            }

        }
        
    }
 
    void OnTriggerExit(Collider col) {
        if (col.tag == "Player") {
            moveEnemy.SetState(MoveEnemy.EnemyState.Wait);
        }
    }

    private void OnDrawGizmos()
    {
        Handles.color = new Color(1f, 0f, 0f, 0.1f);
        Handles.DrawSolidArc(transform.position, Vector3.up, Quaternion.Euler(0f, -searchAngle, 0f) * transform.forward, searchAngle * 2f, searchArea.radius);
    }
}
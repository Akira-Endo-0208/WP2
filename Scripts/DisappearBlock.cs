using DG.Tweening;
using UnityEngine;
 
public class DisappearBlock : MonoBehaviour
{

    public float enable_time;
    public float disable_time;
    public float animate_time = 0.5f;

    private Collider boxCollider;
    private Material material;

    void Start()
    {
        boxCollider = this.GetComponent<Collider>();
        material = this.GetComponent<MeshRenderer>().material;

        Sequence sequence = createSequence();
        sequence.Play();
    }

    /// <summary>
    /// ループ処理用シーケンスを作成する
    /// </summary>
    private Sequence createSequence()
    {
        // Sequence生成
        Sequence sequence = DOTween.Sequence();

        // 無限ループにする
        sequence.SetLoops(-1);

        // 判定無効化
        sequence.AppendCallback(
            () => boxCollider.isTrigger = true
        );
        sequence.Append(
            DOTween.ToAlpha(
                () => material.color,
                color => material.color = color,
                0,
                animate_time
            )
        );

        // 消滅時間分だけ待機
        sequence.AppendInterval(disable_time);

        // 判定有効化
        sequence.AppendCallback(
            () => boxCollider.isTrigger = false
        );
        sequence.Append(
            DOTween.ToAlpha(
                () => material.color,
                color => material.color = color,
                1f,
                animate_time
            )
        );

        // 生成時間分だけ待機
        sequence.AppendInterval(enable_time);

        return sequence;
    }
}
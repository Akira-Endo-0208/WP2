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
    /// ���[�v�����p�V�[�P���X���쐬����
    /// </summary>
    private Sequence createSequence()
    {
        // Sequence����
        Sequence sequence = DOTween.Sequence();

        // �������[�v�ɂ���
        sequence.SetLoops(-1);

        // ���薳����
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

        // ���Ŏ��ԕ������ҋ@
        sequence.AppendInterval(disable_time);

        // ����L����
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

        // �������ԕ������ҋ@
        sequence.AppendInterval(enable_time);

        return sequence;
    }
}
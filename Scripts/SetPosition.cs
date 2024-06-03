
using UnityEngine;

public class SetPosition : MonoBehaviour {

	//�����ʒu
	private Vector3 startPosition;
	//�ړI�n
	private Vector3 destination;

    //�@�ړ��͈�
    [SerializeField]
    private float movementRange = 40f;

    void Start () {
		//�@�����ʒu��ݒ�
		startPosition = transform.position;
		SetDestination(transform.position);
	}

	//�@�����_���Ȉʒu�̍쐬
	public void CreateRandomPosition() {
		//�@�����_����Vector2�̒l�𓾂�
		var randDestination = Random.insideUnitCircle * 8;

		//�@���ݒn�Ƀ����_���Ȉʒu�𑫂��ĖړI�n�Ƃ���
		SetDestination(startPosition + new Vector3(randDestination.x, startPosition.y, 0));

        var randomPos = startPosition + Random.insideUnitSphere * movementRange;
        var ray = new Ray(randomPos + Vector3.up * 10f, Vector3.down);
        RaycastHit hit;
        //�@�ړI�n���n�ʂɂȂ�悤�ɍĐݒ�
        if (Physics.Raycast(ray, out hit, 100f, LayerMask.GetMask("Field")))
        {
            destination = hit.point;
        }
    }

	//�@�ړI�n��ݒ肷��
	public void SetDestination(Vector3 position) {
		destination = position;
       
    }

	//�@�ړI�n���擾����
	public Vector3 GetDestination() {
		return destination;
	}
}

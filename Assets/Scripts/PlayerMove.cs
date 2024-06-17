using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    float inputX = 0;//float�^�i�����̃f�[�^�^�j�̕ϐ��B�L�[�{�[�h��D�������ꂽ�Ƃ���1,A�������ꂽ�Ƃ���-1����������悤�ɂȂ��Ă���B
    Vector3 mousePos;
    [SerializeField] float velocity;//
    [SerializeField] float jumpPower = 5;
    [SerializeField] float dashBoost;
    public float dashBoostValue;
    float slowMeter;
    [SerializeField] LayerMask terrainLayerMask;
    [SerializeField] GameObject gunRootObject;
    bool isGounded = false;
    Rigidbody rb;

    

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();//��Ő錾����rb�Ƃ����ϐ��ɂ��̃X�N���v�g���A�^�b�`����Ă���I�u�W�F�N�g��Ridigbody����

    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Input.mousePosition;//�}�E�X�̍��W���擾����mousePos�Ƃ���Vector3�^�̕ϐ��ɑ�����Ă���
        //���E�ړ�
        if (Input.GetKey(KeyCode.D))//if����D��������Ă��邩�ǂ������肵�Ă���
        {
            inputX = 1;//������
        }else if (Input.GetKey(KeyCode.A))//if����A��������Ă��邩�ǂ������肵�Ă���
        {
            inputX = -1;
        }
        else//A��D�ǂ����������Ă��Ȃ��ꍇ��inputX�Ƃ����ϐ��ɂO����
        {
            inputX = 0;
        }
        //�_�b�V�����
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            dashBoost = dashBoostValue;
        }
        dashBoost = Mathf.Lerp(dashBoost, 1f, 10f * Time.deltaTime);

        //�X���[
        //-----�������牺-----------------------------------------


        //-----�����܂�-------------------------------------------
        EmitRay();
        if (Input.GetKeyDown(KeyCode.Space) && isGounded == true)
        {
            Jump();
        }

        Move();
        Aim();

    }



    private void Aim()
    {
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10f));//����
        gunRootObject.transform.rotation = Quaternion.LookRotation(worldMousePos-gunRootObject.transform.position);

    }

    private void Move()
    {
        rb.velocity = new Vector3(inputX * velocity * dashBoost, rb.velocity.y, 0);//���x�𒼐ڂ����邱�ƂŃv���C���[�̈ړ��������BinputX�ŕ����]���i��~���܂ށj�Bdashboost�̓_�b�V����������邱�Ƃňꎞ�I�ɐ��l���㏸
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, jumpPower, 0);
    }

    private void EmitRay()
    {
        if (Physics.Raycast(this.transform.position, Vector3.down, 1.1f, terrainLayerMask))
        {
            isGounded = true;
        }
        else
        {
            isGounded = false;
        }
    }
}

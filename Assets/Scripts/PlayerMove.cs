using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    float inputX = 0;//float型（実数のデータ型）の変数。キーボードのDが押されたときに1,Aが押されたときに-1が代入されるようになっている。
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
        rb = this.GetComponent<Rigidbody>();//上で宣言したrbという変数にこのスクリプトがアタッチされているオブジェクトのRidigbodyを代入

    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Input.mousePosition;//マウスの座標を取得してmousePosというVector3型の変数に代入している
        //左右移動
        if (Input.GetKey(KeyCode.D))//if文でDが押されているかどうか判定している
        {
            inputX = 1;//感じろ
        }else if (Input.GetKey(KeyCode.A))//if文でAが押されているかどうか判定している
        {
            inputX = -1;
        }
        else//AもDどちらも押されていない場合はinputXという変数に０を代入
        {
            inputX = 0;
        }
        //ダッシュ回避
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            dashBoost = dashBoostValue;
        }
        dashBoost = Mathf.Lerp(dashBoost, 1f, 10f * Time.deltaTime);

        //スロー
        //-----ここから下-----------------------------------------


        //-----ここまで-------------------------------------------
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
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10f));//正直
        gunRootObject.transform.rotation = Quaternion.LookRotation(worldMousePos-gunRootObject.transform.position);

    }

    private void Move()
    {
        rb.velocity = new Vector3(inputX * velocity * dashBoost, rb.velocity.y, 0);//速度を直接いじることでプレイヤーの移動を実装。inputXで方向転換（停止も含む）。dashboostはダッシュが押されることで一時的に数値が上昇
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

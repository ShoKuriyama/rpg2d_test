using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // void Start()
    // {
        
    // }

    // Update is called once per frame

    [SerializeField] LayerMask solidObjectsLayer;
    Animator animator;
    bool isMoving = false;

    private void Awake(){
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        if(!isMoving){
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");

            if(x != 0){
                y = 0;
            }

            if(x !=0 || y != 0){

                animator.SetFloat("InputX",x);
                animator.SetFloat("InputY",y);
                // transform.position += new Vector3(x, y);
                StartCoroutine(Move(new Vector2(x, y)));

            }


        }

        animator.SetBool("IsMoving", isMoving);

        
    }

    IEnumerator Move(Vector3 direction)
    {
        isMoving = true;
        Vector3 targetPos = transform.position + direction;

        if(isWalkable(targetPos) ==false){
            isMoving  = false;
            yield break;
        }
        // 現在とターゲットの場所が同じになるまで移動
        while((targetPos-transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, 5f * Time.deltaTime);
            yield return null;
        } 
        transform.position = targetPos;
        isMoving = false;
    }

    // 通れるかどうかを返す
    bool isWalkable(Vector3 targetPos){
        // ターゲットの位置を取得
        return Physics2D.OverlapCircle(targetPos, 0.2f, solidObjectsLayer) == false;
        
    }

}

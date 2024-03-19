using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterController : MonoBehaviour
{
    

   [SerializeField] public float speed=5f;
   [SerializeField] public float jumpForce=10f;
   
   private SpriteRenderer spriteRenderer;
   bool jump=false;
   int jumpCounter=0;
   bool canJump=true;
   private Animator animator;


   void Start(){
    spriteRenderer=GetComponent<SpriteRenderer>();
    animator=GetComponent<Animator>();
    
   }
   
   
   public void Update(){
     CharacterControl();
     JumpControl();
     
    }
    
    void CharacterControl(){
        float horizontalMove=Input.GetAxis("Horizontal");
        Vector3 hareket=new Vector3(horizontalMove,0f,0f);
        transform.position += hareket*speed*Time.deltaTime;
        if(horizontalMove>0f){
            spriteRenderer.flipX=false;
        }
        else if(horizontalMove<0f){
            spriteRenderer.flipX=true;
        }
         if (Mathf.Abs(horizontalMove) > 0f)
        {
        animator.SetFloat("isRunning", Mathf.Abs(horizontalMove));
        animator.SetBool("isRunning", true);
        }
        else
        {
        animator.SetFloat("isRunning", 0f);
        animator.SetBool("isRunning", false);
        }
    }    
    void JumpControl(){
        if(Input.GetKeyDown(KeyCode.Space)&& !jump && canJump){
            jumpCounter=jumpCounter+1;
            animator.SetBool("jump",true);
            GetComponent<Rigidbody2D>().velocity=new Vector2(0f,jumpForce);
            if(jumpCounter>1)
            {
                canJump=false;
                jumpCounter=0;
            }
            
            jump=true;
        }
        else {
            animator.SetBool("jump",false);
            jump=false;
        }
    }
       
   
    public void FixedUpdate(){
        
    }
    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.CompareTag("Ground")){
            jump=false;
            canJump=true;
        }
    }
    
 
}
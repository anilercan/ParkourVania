using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovementScript : MonoBehaviour
{
    Vector2 moveInput;
    Rigidbody2D rb2d;
    Animator myAnimator;
    CapsuleCollider2D myCC;
    BoxCollider2D myBC;
    bool isAlive=true;
    [SerializeField] float velocityMultiplier=6f;
    [SerializeField] float jumpHeight=5f;
    [SerializeField] float climbMultiplier=5f;
    [SerializeField] Vector2 deathKick=new Vector2(0f,15f);
    [SerializeField] AudioClip deathSFX;
    //public AudioSource bgmMusic;
    
    float defaultGravity=6f;
    
    void Start()
    {
        rb2d=GetComponent<Rigidbody2D>();
        myAnimator=GetComponent<Animator>();
        myCC=GetComponent<CapsuleCollider2D>();
        myBC=GetComponent<BoxCollider2D>();
        //bgmMusic=GetComponent<AudioSource>();
        //bgmMusic.Play(0);
    }
    void Update()
    {   
        if (isAlive==false){
            return;
        }
        Run();      
        ClimbLadder();
        FlipSprite(); 
        Die();
        
    }
    void OnMove(InputValue value){
        if (isAlive==false){
            return;
        }
        moveInput=value.Get<Vector2>();
    }
    void OnJump(InputValue value){
        if (isAlive==false&&FindObjectOfType<GameSession>().isGameOver()==false){
            FindObjectOfType<GameSession>().deathText.gameObject.SetActive(false);
            int currentSceneIndex=SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
            return;
        }
        if(myBC.IsTouchingLayers(LayerMask.GetMask("Ground","Ladder"))==true&&FindObjectOfType<GameSession>().isGameOver()==false){
            if(value.isPressed){
                rb2d.velocity=rb2d.velocity+new Vector2(0f,jumpHeight);
            }
        }
        
    }
    void OnFire(InputValue value){    
        if (isAlive==false&&FindObjectOfType<GameSession>().isGameOver()==false){
            FindObjectOfType<GameSession>().deathText.gameObject.SetActive(false);
            int currentSceneIndex=SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
        }
    }
    void Run(){
        Vector2 playerVelocity=new Vector2(moveInput.x*velocityMultiplier,rb2d.velocity.y);
        rb2d.velocity=playerVelocity;
        myAnimator.SetBool("IsRunning",IsMoving());
    }
    void ClimbLadder(){
        if(myBC.IsTouchingLayers(LayerMask.GetMask("Ladder"))==true){
            rb2d.gravityScale=0f;
            Vector2 climbVelocity=new Vector2(rb2d.velocity.x,moveInput.y*climbMultiplier);
            rb2d.velocity=climbVelocity;
            if(rb2d.velocity.y!=0f){
                myAnimator.SetBool("IsClimbing",true);
            }
            else{
                myAnimator.SetBool("IsClimbing",false);
            }
        }
        else{
            rb2d.gravityScale=defaultGravity;
            myAnimator.SetBool("IsClimbing",false);
        }
    }
    void FlipSprite(){
        if (IsMoving()==true){
            transform.localScale=new Vector2(Mathf.Sign(rb2d.velocity.x),1f);
        }
    }
    bool IsMoving(){
        if (isAlive==true){
            return Mathf.Abs(rb2d.velocity.x)>Mathf.Epsilon;
        }
        else{
            return false;
        }
    }
    void Die(){
        if (myCC.IsTouchingLayers(LayerMask.GetMask("Blob","Hazards"))||myBC.IsTouchingLayers(LayerMask.GetMask("Blob","Hazards"))){
            isAlive=false;
            AudioSource.PlayClipAtPoint(deathSFX,Camera.main.transform.position);
            myAnimator.SetBool("IsRunning",false);
            myAnimator.SetBool("IsClimbing",false);
            myAnimator.SetBool("IsDead",true);
            rb2d.velocity=deathKick;
            //bgmMusic.Stop();
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }
}

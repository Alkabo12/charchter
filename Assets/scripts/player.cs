using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class player : MonoBehaviour
{
   private float time=60;
    public Text timertext ;
     private AudioSource run_music;

    private AudioClip music_game;
    public AudioClip lose_game;
    public AudioClip win_game;
    public AudioClip red_pick;
    public AudioClip blue_pick;
   
    public AudioClip jumb_music;
    public Camera pick_camera;

   

    private int score;
    public Text scoretext;
    private  int score_red=100;
    public Text scoretext_red;
    private CharacterController controller;
    private Vector3 mov;
    private float speed = 10;
    private float gravity =30;
    private float jump =8;
    private float rotationspeed =150;
    private float rotation =0;

    private Animator animator;

  

    // Start is called before the first frame update
    void Start()
    {
        controller =this.GetComponent<CharacterController>();
        animator =this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

         run_music = pick_camera.GetComponent<AudioSource>(); 
         
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

            if(controller.isGrounded){
             mov = new Vector3(0,0, moveVertical);
             mov=this.transform.TransformDirection(mov);
               
             if(Input.GetButton("Jump")){
                mov.y=jump;
                animator.SetBool("IsRunBack",false);
             animator.SetBool("IsBackWalk",false);
            animator.SetBool("IsRun",false);
             animator.SetBool("IsWalking",false);                                                  
                  animator.SetBool("IsJump",true);
                   
           }
          else{
              animator.SetBool("IsJump",false);    

        }
            }
            if(moveVertical !=0){  
             if ((Input.GetButtonDown("Vertical") && Input.GetAxisRaw("Vertical") < 0)) {
                      animator.SetBool("IsBackWalk",true);
                       animator.SetBool("IsRunBack",true);
                       
                       }
                else{     
                animator.SetBool("IsWalking",true);
                animator.SetBool("IsRun",true);
               
                     }
               
          }
         
          else{
            animator.SetBool("IsRunBack",false);
             animator.SetBool("IsBackWalk",false);
            animator.SetBool("IsRun",false);
             animator.SetBool("IsWalking",false);
          }

          
           
        rotation+=rotationspeed*moveHorizontal*Time.deltaTime;
        this.transform.eulerAngles=new Vector3(0,rotation,0);
      //  mov.x = moveHorizontal;
      //  mov.z = moveVertical;
        mov.y-=gravity*Time.deltaTime;
        controller.Move(mov*speed*Time.deltaTime);
         if (controller.transform.position.y <= -9)
        {
            run_music.Stop();
            Thread.Sleep(1000);
            run_music.PlayOneShot(lose_game);
            Thread.Sleep(3000);
            SceneManager.LoadScene(2);
        }
         if(time >0)
        {
            time-=Time.deltaTime;
        }
        else {
            time=0;
             run_music.Stop();
            Thread.Sleep(1000);
            run_music.PlayOneShot(lose_game);
            Thread.Sleep(3000);
            SceneManager.LoadScene(2);
        }
       DisplayTime(time);
          
    }
    
      void DisplayTime(float timer){
            if(timer <0){
                    timer=0;
            }
            float min=Mathf.FloorToInt(timer/60);
            float sec=Mathf.FloorToInt(timer%60);
            timertext.text=String.Format("{0:00}:{1:00}",min,sec);

    } 

    
     void OnTriggerEnter(Collider x)
        {
        
         if (x.tag == "pickup")
        {
            x.gameObject.SetActive(false);
            score++;
            scoretext.text = "Score: " + score;

            
             run_music.PlayOneShot(blue_pick);

            if (score == 5)
            {
              run_music.Stop();
             run_music.PlayOneShot(win_game);
             Thread.Sleep(3000);
             SceneManager.LoadScene(5);
            }
           
            
           
        }
        if (x.tag == "packup_red")
        {
            x.gameObject.SetActive(false);
            score_red -= 25;
            scoretext_red.text = "Healthy:" + score_red + "%";
            run_music.PlayOneShot(red_pick);
            if (score_red <= 0)
            {
                run_music.Stop();
                run_music.PlayOneShot(lose_game);
                Thread.Sleep(3000);
                SceneManager.LoadScene(2);
            }
        }
         if (x.tag == "health")
        {
             x.gameObject.SetActive(false);
             score_red += 10;
              run_music.PlayOneShot(blue_pick);
            if (score_red >= 100)
            {
              score_red = 100;
              scoretext_red.text = "Healthy:" + score_red + "%";
            }
            else{
               
                 scoretext_red.text = "Healthy:" + score_red + "%";
               }

        }

        }
}

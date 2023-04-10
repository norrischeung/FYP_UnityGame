using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;

public class RunCode_3 : MonoBehaviour
{
    [SerializeField]
    FirebaseManager firebaseManager;

    [SerializeField]
    Statistic statistic;

    public Animator animator;
    public Dropdown Drop1,Drop2;
    public int opt1,opt2;
    public GameObject plus,minus;

    private int numOfAttempt = 0;
    private string level = "Level 3";

    public AudioSource Click;

    void Start() {
        plus.SetActive (false);
        minus.SetActive (false);
    }

    public void playAnimation() {
        Click.Play();
        numOfAttempt++;

        opt1 = Drop1.value;
        opt2 = Drop2.value;

        //reset scence
        plus.SetActive (false);
        minus.SetActive (false);

        //change symbol
        if(opt2==0) {
            plus.SetActive (true);
        }
        else {
            minus.SetActive(true);
        }

        //>=
        if(opt1==0) {
            //++
            if (opt2==0) {
                animator.ResetTrigger("ani.2");
                animator.SetTrigger("ani.2");
                Debug.Log("(>=)(++)");
            }
            //--
            if(opt2==1) {
                animator.ResetTrigger("ani.1");
                animator.SetTrigger("ani.1");
                Debug.Log("(>=)(--)");
            }
            
        }

        //>
        if(opt1==1) {
            //++
            if (opt2==0) {
                animator.ResetTrigger("ani.2");
                animator.SetTrigger("ani.2");
                Debug.Log("(>)(++)");
            }
            //--
            if(opt2==1) {
                animator.ResetTrigger("ani.0");
                animator.SetTrigger("ani.0");
                Debug.Log("(>)(--)");
                //Save in Database
                firebaseManager.SaveAttempt(level, numOfAttempt);
                firebaseManager.SavePassedLevel(3);
            }
        }

        //Save Statistic
        if (opt1==1)
        {
            //op1 correct
            statistic.saveStat("Conditional Operator", true);
        }
        else
        {
            //opt1 incorrect
            statistic.saveStat("Conditional Operator", false);
        }

        if (opt2 == 1)
        {
            //op2 correct
            statistic.saveStat("Increment", true);
        }
        else
        {
            //op2 incorrect
            statistic.saveStat("Increment", false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;

public class RunCode_4 : MonoBehaviour
{
    public Animator animator;
    public Dropdown Drop1,Drop2,Drop3;
    public int opt1,opt2,opt3;
    public Text errorMsg_length,errorMsg_count;

    [SerializeField]
    FirebaseManager firebaseManager;

    [SerializeField]
    Statistic statistic;

    private int numOfAttempt = 0;
    private string level = "Level 4";

    public AudioSource Click,Error;

    void Start() {
        errorMsg_length.enabled = false;
        errorMsg_count.enabled = false;
    }

    public void playAnimation() {
        Click.Play();
        numOfAttempt++;

        opt1 = Drop1.value;
        opt2 = Drop2.value;
        opt3 = Drop3.value;
        errorMsg_length.enabled = false;
        errorMsg_count.enabled = false;

        if(opt1==0) {
            errorMsg_length.enabled = true;
            Error.Play();
        }
        if(opt1==2) {
            errorMsg_count.enabled = true;
            Error.Play();
        }

        if(opt1==1) {
            if(opt2==0) {
                if(opt3==0) {
                    animator.ResetTrigger("ani.1");
                    animator.SetTrigger("ani.1");
                    Debug.Log("(bug)(na)");
                }
                else {
                    //correct ans
                    animator.ResetTrigger("ani.0");
                    animator.SetTrigger("ani.0");
                    Debug.Log("(bug)(i--)");
                    //Save in Database
                    firebaseManager.SaveAttempt(level, numOfAttempt);
                    firebaseManager.SavePassedLevel(4);
                }
            }
            else {
                if(opt3==0) {
                    animator.ResetTrigger("ani.2");
                    animator.SetTrigger("ani.2");
                    Debug.Log("(human)(na)");
                }
                else {
                    animator.ResetTrigger("ani.3");
                    animator.SetTrigger("ani.3");
                    Debug.Log("(human)(i--)");
                }
            }   
        }

        //Save Statistic
        if (opt1 == 1)
        {
            //op1 correct
            statistic.saveStat("Data Type", true);
        }
        else
        {
            //opt1 incorrect
            statistic.saveStat("Data Type", false);
        }
        if (opt2 == 0)
        {
            //op2 correct
            statistic.saveStat("Conditional Statement", true);
        }
        else
        {
            //opt2 incorrect
            statistic.saveStat("Conditional Statement", false);
        }
        if (opt3 == 1)
        {
            //op3 correct
            statistic.saveStat("Increment", true);
        }
        else
        {
            //opt3 incorrect
            statistic.saveStat("Increment", false);
        }
    }
}

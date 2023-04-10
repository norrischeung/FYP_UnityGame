using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;

public class RunCode_5 : MonoBehaviour
{
    [SerializeField]
    FirebaseManager firebaseManager;

    [SerializeField]
    Statistic statistic;

    public Animator animator;
    public Dropdown Drop1,Drop2,Drop3;
    public int opt1,opt2,opt3;
    public Text errorMsg;

    private int numOfAttempt = 0;
    private string level = "Level 5";

    public AudioSource Click,Error;

    void Start() {
        errorMsg.enabled = false;
    }

    public void playAnimation() {
        Click.Play();
        numOfAttempt++;

        opt1 = Drop1.value;
        opt2 = Drop2.value;
        opt3 = Drop3.value;
        errorMsg.enabled = false;

        if(opt1==0 || opt1==1 || opt1==2) {
            errorMsg.enabled = true;
            Error.Play();
        }

        if(opt1==3) {
            Debug.Log("opt1");
            if(opt2==1) {
                Debug.Log("opt2");
                if(opt3==0) {
                    animator.ResetTrigger("ani.1");
                    animator.SetTrigger("ani.1");
                    Debug.Log("wrong");
                }
                if(opt3==2) {
                    animator.ResetTrigger("ani.0");
                    animator.SetTrigger("ani.0");
                    Debug.Log("corret");
                    //Save in Database
                    firebaseManager.SaveAttempt(level, numOfAttempt);
                    firebaseManager.SavePassedLevel(5);
                }
            }   
        }

        //Save Statistic
        if (opt1 == 3)
        {
            //op1 correct
            statistic.saveStat("Data Type", true);
        }
        else
        {
            //opt1 incorrect
            statistic.saveStat("Data Type", false);
        }
        if (opt2 == 1)
        {
            //op2 correct
            statistic.saveStat("Conditional Statement", true);
        }
        else
        {
            //opt2 incorrect
            statistic.saveStat("Conditional Statement", false);
        }
        if (opt3 == 2)
        {
            //op3 correct
            statistic.saveStat("Swapping", true);
        }
        else
        {
            //opt3 incorrect
            statistic.saveStat("Swapping", false);
        }
    }
}

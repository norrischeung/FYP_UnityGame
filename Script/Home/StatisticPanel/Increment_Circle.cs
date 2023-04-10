using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;

public class Increment_Circle : MonoBehaviour
{
    [SerializeField]
    FirebaseManager firebaseManager;

    [Range(0, 100)]
    public float fillValue = 0;
    public Image circleFillImage;
    public RectTransform handlerEdgeImage;
    public RectTransform fillHandler;

    private int attempt, correct;
    public double correctRate = 0.00;

    public bool done = false;
    public Text percentage;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!done)
        {
            showPercentage();
        }
    }

    public void showPercentage()
    {
        loadStat("Increment");
        done = true;
    }

    void FillCircleValue(float value)
    {
        float fillAmount = (value / 100.0f);
        circleFillImage.fillAmount = fillAmount;
        float angle = fillAmount * 360;
        fillHandler.localEulerAngles = new Vector3(0, 0, -angle);
        handlerEdgeImage.localEulerAngles = new Vector3(0, 0, angle);
        percentage.text = correctRate.ToString("F1") + "%";
    }

    public void loadStat(string type)
    {
        StartCoroutine(loadAttempt(type));
    }

    IEnumerator loadAttempt(string type)
    {
        var task = firebaseManager.GetUserReference().Child("Statistic").Child(type).Child("Attempt").GetValueAsync();
        yield return new WaitUntil(() => task.IsCompleted);
        DataSnapshot snapshot = task.Result;
        if (snapshot != null)
        {
            attempt = int.Parse(snapshot.Value.ToString());
            StartCoroutine(loadCorrect(type));
        }
    }

    IEnumerator loadCorrect(string type)
    {
        var task = firebaseManager.GetUserReference().Child("Statistic").Child(type).Child("Correct").GetValueAsync();
        yield return new WaitUntil(() => task.IsCompleted);
        DataSnapshot snapshot = task.Result;
        if (snapshot != null)
        {
            correct = int.Parse(snapshot.Value.ToString());
            correctRate = calculateStat();
            FillCircleValue((float)correctRate);
        }
    }

    public double calculateStat()
    {
        double rate = 0.00;
        if (attempt == 0)
        {
            rate = 0;
        }
        else
        {
            rate = ((double)correct / (double)attempt) * 100;
        }
        return rate;
    }
}
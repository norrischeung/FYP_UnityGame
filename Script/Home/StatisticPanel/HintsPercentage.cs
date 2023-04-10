using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Database;

public class HintsPercentage : MonoBehaviour
{
    public bool done = false, useHints = false;
    public int hints = 0, wait = 0;
    public double hintsPercentage = 0.0;

    public GameObject[] hintsIcon;
    public Text hintsPercentage_text;
 
    [SerializeField]
    FirebaseManager firebaseManager;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < hintsIcon.Length; i++)
        {
            hintsIcon[i].gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (!done)
        {
            showHintsIcon();
        }
    }

    public void showHintsIcon()
    {
        for(int i = 1; i <= hintsIcon.Length; i++)
        {
            StartCoroutine(checkHints(i));
        }
        done = true;
    }

    IEnumerator checkHints(int level)
    {
        var task = firebaseManager.GetUserReference().Child("Level " + level).Child("Hints").GetValueAsync();
        yield return new WaitUntil(() => task.IsCompleted);
        DataSnapshot snapshot = task.Result;
        if (snapshot != null)
        {
            useHints = (bool)snapshot.Value;
            if (useHints)
            {
                hintsIcon[level-1].gameObject.SetActive(true);
                hints++;
            }
            if (level == hintsIcon.Length)
            {
                hintsPercentage = ((double)hints / (double)hintsIcon.Length) * 100;
                hintsPercentage_text.text = "Hints: " + hintsPercentage.ToString("F1") + "% (" + hints + "/" + hintsIcon.Length + ")";
            }
        }
    }
}

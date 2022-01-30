using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class DisplayHighscores : MonoBehaviour 
{
    public TextMeshProUGUI[] rNames;
    public TextMeshProUGUI[] rScores;
    HighScores myScores;

    void Start() //Fetches the Data at the beginning
    {
        for (int i = 0; i < rNames.Length;i ++)
        {
            rNames[i].text = i + 1 + ". Fetching...";
        }
        myScores = GetComponent<HighScores>();
        StartCoroutine("RefreshHighscores");
    }
    public void SetScoresToMenu(PlayerScore[] highscoreList) //Assigns proper name and score for each text value
    {
        for (int i = 0; i < rNames.Length;i ++)
        {
            rNames[i].text = i + 1 + ". ";
            if (highscoreList.Length > i)
            {
                rScores[i].text = highscoreList[i].score.ToString();
                rNames[i].text = highscoreList[i].username.Replace('+',' ');
            }
        }

        //Debug.Log("Resizing Text Together..");
        ResizeAllTextFonts();
    }

    private void ResizeAllTextFonts()
    {
        AutoResizeTextTogether(rNames.Concat(rScores).ToList());
    }

    IEnumerator RefreshHighscores() //Refreshes the scores every 30 seconds
    {
        while(true)
        {
            myScores.DownloadScores();
            yield return new WaitForSeconds(30);
        }
    }

    //rip that shit
    private void AutoResizeTextTogether(List<TextMeshProUGUI> TextObjects)
    {
        if (TextObjects == null || TextObjects.Count == 0)
            return;

        //Debug.Log("Resizing Text Together.. in functions");
        // Iterate over each of the text objects in the array to find a good test candidate
        // There are different ways to figure out the best candidate
        // Preferred width works fine for single line text objects
        int candidateIndex = 0;
        float maxPreferredWidth = 0;

        for (int i = 0; i < TextObjects.Count; i++)
        {
            float preferredWidth = TextObjects[i].preferredWidth;
            if (preferredWidth > maxPreferredWidth)
            {
                maxPreferredWidth = preferredWidth;
                candidateIndex = i;
            }
        }

        // Force an update of the candidate text object so we can retrieve its optimum point size.
        TextObjects[candidateIndex].enableAutoSizing = true;
        TextObjects[candidateIndex].ForceMeshUpdate();
        float optimumPointSize = TextObjects[candidateIndex].fontSize;

        // Disable auto size on our test candidate
        TextObjects[candidateIndex].enableAutoSizing = false;

        // Iterate over all other text objects to set the point size
        for (int i = 0; i < TextObjects.Count; i++)
            TextObjects[i].fontSize = optimumPointSize;
    }
}

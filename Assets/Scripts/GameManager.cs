using System;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

GameObject bugAmountText;

int numberOfBugs = 0;
[SerializeField] private int howManyBuggsToWin = 5;

public float volume = 1;

[SerializeField] AudioSource musicSource;

[SerializeField] Slider volumeSlider;

// DialogueScript dialogueScript;
SceneLoader sceneLoader;


// Start is called once before the first execution of Update after the MonoBehaviour is created
void Start()
{
    sceneLoader = FindObjectOfType<SceneLoader>();
    
    bugAmountText = GameObject.Find("BugText");
    
    AddBuggs(0);
    
    
}

void Update()
{
    

    
}

public void AddBuggs(int buggs)
{
    numberOfBugs += buggs;
    
    bugAmountText.GetComponent<TextMeshProUGUI>().text = numberOfBugs.ToString(); 

    // if(numberOfBugs == 3)
    // {
    //
    //     dialogueScript = FindObjectOfType<DialogueScript>();
    //
    //     dialogueScript.StartDialogue();
    //
    // }
}

public void CheckIfWin(bool walkedInDoor)
{ 

    if(numberOfBugs == howManyBuggsToWin)
    {
        if (walkedInDoor)
        {
            ChangeScene(2);   
        }
        else
        {
            ChangeScene(4);
        }

    }

}

public void ChangeScene(int whatScene)
{
    
    sceneLoader.ChangeScene(whatScene);
    
}

public void ChangeVolume()
{

     volume = volumeSlider.value;
    
    musicSource.volume = volume;

}

}

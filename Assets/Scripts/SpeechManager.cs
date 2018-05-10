using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpeechManager : MonoBehaviour {

    public Text speechText;
    public Text Objective1;
    public Text Objective2;
    public Text Objective3;
    public Text Objective4;
    public Text factText;

    private List<int> speechNumbers = new List<int>();

    #region Singleton

    public static SpeechManager instance;

    void Awake()
    {
        instance = this;
    }

    #endregion

    void Start()
    {
        Objective1.text = "Escape the underground prison";
    }

    public void FindSpeech(string name)
    {
        switch (name)
        {
            case "SpeechPoint":
                {
                    Speak(1);
                    break;
                }
            case "SpeechPoint (1)":
                {
                    Speak(2);
                    break;
                }
            case "SpeechPoint (2)":
                {
                    Speak(3);
                    break;
                }
            case "SpeechPoint (3)":
                {
                    Speak(4);
                    break;
                }
            case "SpeechPoint (4)":
                {
                    Speak(5);
                    break;
                }
            case "SpeechPoint (5)":
                {
                    Speak(6);
                    break;
                }
            case "SpeechPoint (6)":
                {
                    if (SceneManager.GetActiveScene().name == "Area 1")
                    {
                        DataManager.TempPoints = DataManager.Points;
                        DataManager.SpawnLocation = "PlayerSpawnPoint1";
                        SceneManager.LoadScene("Area 2");                   
                    }
                    else
                        Speak(7);
                    break;
                }
            case "SpeechPoint (7)":
                {
                    Speak(8);
                    break;
                }
            case "SpeechPoint (8)":
                {
                    Speak(9);
                    break;
                }
            case "SpeechPoint (9)":
                {
                    DataManager.TempPoints = DataManager.Points;
                    DataManager.SpawnLocation = "PlayerSpawnPoint2";
                    Speak(10);
                    break;
                }
            case "SpeechPoint (10)":
                {
                    Speak(11);
                    break;
                }
            case "SpeechPoint (11)":
                {
                    Speak(12);
                    break;
                }
            case "SpeechPoint (12)":
                {
                    Speak(13);
                    break;
                }
            case "SpeechPoint (13)":
                {
                    Speak(14);
                    break;
                }
            default:
                speechText.text = null;
                break;
        }
    }

    void Speak(int number)
    {
        switch (number)
        {
            case 1:
                {
                    if (!speechNumbers.Contains(number))
                    {
                        factText.text = null;
                        speechText.text = "Albert: I... I can't believe Thomas is dead... but I need do what he told me. I have to escape... even if it kills me";
                        speechNumbers.Add(number);
                    }
                    break;
                }
            case 2:
                {
                    if (!speechNumbers.Contains(number))
                    {
                        factText.text = null;
                        speechText.text = "Albert: Why am I even here?? The war is over. I don't belong here. I Didn't Desert Damn It!!";
                        speechNumbers.Add(number);
                    }
                    break;
                }
            case 3:
                {
                    if (!speechNumbers.Contains(number))
                    {
                        factText.text = null;
                        speechText.text = "Prisoner: Oi!, how did you get that? Open my door... Deserting Prick!";
                        speechNumbers.Add(number);
                    }
                    break;
                }
            case 4:
                {
                    if (!speechNumbers.Contains(number))
                    {
                        factText.text = null;
                        speechText.text = "Prisoner: When I get out, I'll kill you myself, Coward!!";
                        speechNumbers.Add(number);
                    }
                    break;
                }
            case 5:
                {
                    if (!speechNumbers.Contains(number))
                    {
                        factText.text = null;
                        speechText.text = "Albert: I saw the guards shadow underneath the door earlier... My comrades... I can't and won't harm any of them. They don't deserve it.";
                        speechNumbers.Add(number);
                    }
                    break;
                }
            case 6:
                {
                    if (!speechNumbers.Contains(number))
                    {
                        factText.text = null;
                        if (ItemManager.hasKey[0])
                        {
                            speechText.text = "Albert: A locked door, maybe this is the key to unlock it.";
                            Objective1.text = "Escape the underground prison (completed)";
                            Objective2.text = "Find the Key (completed)";
                            Objective3.text = "Get to the front gate";
                        }
                        else if (!ItemManager.hasKey[0])
                        {
                            speechText.text = "Albert: Another locked door, damn it! I'm going have to find the key.";
                            Objective2.text = "Find the Key";
                        }
                        speechNumbers.Add(number);
                    }
                    break;
                }
            case 7:
                {
                    if (!speechNumbers.Contains(number))
                    {
                        factText.text = null;
                        speechText.text = "Albert: God, what I'd do to be home with my wife and little man, celebrating the end to all this... Instead I'm here... It's Hell.";
                        Objective1.text = "Escape the underground prison (completed)";
                        Objective2.text = "Find the Key (completed)";
                        Objective3.text = "Get to the front gate";
                        speechNumbers.Add(number);
                    }
                    break;
                }
            case 8:
                {
                    if (!speechNumbers.Contains(number))
                    {
                        factText.text = null;
                        speechText.text = "Albert: Damn it... I'm never going to get out of here. What if I get on the roof of that big house. I could try and jump for it... Please be high enough";
                        Objective3.text = "Get to the front gate (completed)";
                        Objective4.text = "Climb up the roof of house left of front gate";
                        speechNumbers.Add(number);
                    }
                    break;
                }
            case 9:
                {
                    if (!speechNumbers.Contains(number))
                    {
                        factText.text = null;
                        speechText.text = "Albert: It's still far too high... this is bullsh.. Wait a minute, what's in there?";
                        Objective1.text = "Get inside of the house";
                        Objective2.text = null;
                        Objective3.text = null;
                        Objective4.text = null;
                        speechNumbers.Add(number);
                    }
                    break;
                }
            case 10:
                {
                    if (!speechNumbers.Contains(number))
                    {
                        factText.text = null;
                        speechText.text = "Albert: Oh thank god! a torch, this will help.";
                        Objective1.text = "Get inside of the house (completed)";
                        speechNumbers.Add(number);
                    }
                    break;
                }
            case 11:
                {
                    if (!speechNumbers.Contains(number))
                    {                        
                        factText.text = null;
                        speechText.text = "Albert: What are these keys for? ";
                        speechNumbers.Add(number);                        
                    }
                    break;
                }
            case 12:
                {
                    if (!speechNumbers.Contains(number))
                    {
                        if (ItemManager.hasKey[1])
                        {
                            factText.text = null;
                            speechText.text = "Albert: There must be another way out, a way around all these guards! Hmmm...maybe over the other side of the base";
                            Objective2.text = "Find way to other side of base";
                            speechNumbers.Add(number);
                        }
                    }
                    break;
                }
            case 13:
                {
                    if (!speechNumbers.Contains(number))
                    {
                        if (ItemManager.hasKey[1])
                        {
                            factText.text = null;
                            speechText.text = "Albert: Oh that key works? This one must have been a spare.";
                            speechNumbers.Add(number);
                        }
                    }
                    break;
                }
            case 14:
                {
                    if (!speechNumbers.Contains(number))
                    {
                        if (!ItemManager.hasKey[1])
                        {
                            factText.text = null;
                            speechText.text = "Albert: ugh!! For goodness sake! It's locked...";
                            speechNumbers.Add(number);
                        }
                    }
                    break;
                }
            default:
                break;
        }
    }
}

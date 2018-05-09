using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    #region public
    [Header("Texts")]
    public Text             scoreText;
    public Text             deathText;
    public Text             factText;

    [Header("Spawn Settings")]
    [Tooltip("Allows you to choose where you would like the player to spawn.")]
    public int              spawnPoint = 1;
    private List<string>    Locations = new List<string>();
    [Tooltip("Changes based on what the next spawn location is."), SerializeField]
    private string           spawnLocation;

    [Header("GameObjects")]
    public GameObject       player;
    public GameObject       GameOver;
    public GameObject       ControlsMenu;
    #endregion

    #region private
    // This will count how many risks the player has taken, achieved and what fact should be displayed.
    private int             score,
                            factNumber,
                            numberOfFacts;

    static public bool playerFound;

    private List<int>       factNumbers = new List<int>();
    private float           restartDelay = 2;

    private bool            gameOver = false,
                            spawnedPlayer = false;
    #endregion

    #region Singleton

    public static GameController instance;

    void Awake()
    {
        instance = this;
    }

    #endregion

    void Start()
    {
        scoreText.text = "Risks taken: " + DataManager.Points;
        playerFound = false;

        for(int i = 0; i < this.transform.Find("PlayerSpawnPoints").childCount; i++)
        {
            Locations.Add(this.transform.Find("PlayerSpawnPoints").GetChild(i).name);
        }

        DataManager.SpawnLocation = Locations[spawnPoint - 1];
    }

    void Update()
    {
        if (!spawnedPlayer)
        {
            player.transform.position = this.transform.Find("PlayerSpawnPoints").Find(DataManager.SpawnLocation).position;
            player.transform.rotation = this.transform.Find("PlayerSpawnPoints").Find(DataManager.SpawnLocation).rotation;
            spawnedPlayer = true;
        }

        spawnLocation = DataManager.SpawnLocation;

        if (playerFound == true)      
            PlayerCaught();       

        if (Input.GetButtonDown("CloseText"))
            factText.text = "";

        if (Input.GetButtonDown("ControlsMenu"))
            if (ControlsMenu.activeInHierarchy)            
                ControlsMenu.SetActive(false);
            else if (!ControlsMenu.activeInHierarchy)
                ControlsMenu.SetActive(true);
    }

    public void PlayerCaught()
    {
        if (gameOver == false)
        {
            deathText.text = "Defeats: " + (DataManager.Deaths + 1);
            gameOver = true;
            GameOver.SetActive(true);
        }

        if (Input.GetButtonDown("Restart"))
            Invoke("Restart", restartDelay);
    }

    void Restart()
    {
        DataManager.Deaths++;
        DataManager.Points = DataManager.TempPoints;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
       }

    public void RiskTaken()
    {
        DataManager.Points++;
        scoreText.text = "Risks taken: " + DataManager.Points;
        FindFact();
    }

    void FindFact()
    {
        numberOfFacts = 25;
        factNumber = Random.Range(1, numberOfFacts);

        for (int i = factNumber; i < numberOfFacts; i++)
        {
            if (factNumbers.Contains(i))
            {
                if (i == numberOfFacts)
                    i = 1;
            }
            else
            {
                factNumber = i;
                factNumbers.Add(i);
                break;
            }
        }

        switch (factNumber)
        {
            case 1:
                factText.text = "World War I Fact:\nWorld War 1 began on July 28, 1914. The conflict lasted four years, three months and 14 days, ending on November 11, 1918.\nPress B to close";
                break;
            case 2:
                factText.text = "World War I Fact:\nThe war began because of the assassination of Archduke Franz Ferdinand of Austria. He was heir to throne of Austria-Hungary and his death was the immediate cause of WW1.\nPress B to close";
                break;
            case 3:
                factText.text = "World War I Fact:\nThere were two sides in the war. The Triple Ententes (also known as The Allies) were Britain, France, Ireland and Russia. The Central Powers were Germany and Austria-Hungary.\nPress B to close";
                break;
            case 4:
                factText.text = "World War I Fact:\nAs the war began Italy sided with the Central Powers. They were part of a Triple Alliance with Germany and Austria-Hungary but they did not enter the way because their alliance was supposed to be defensive, but Germany and Austria-Hungary had took the offensive and declared war.\nPress B to close";
                break;
            case 5:
                factText.text = "World War I Fact:\nLater on in the war, Italy joined WW1 on the side of the Triple Entente. They declared war on Austria-Hungary in May, 1915 and Germany in August, 1916.\nPress B to close";
                break;
            case 6:
                factText.text = "World War I Fact:\nWorld War 1 has many different names. It was called The Great War, the World War, the War to End all Wars, World War 1, WW1, the War of the Nations and more.\nPress B to close";
                break;
            case 7:
                factText.text = "World War I Fact:\nAmerica joined World War I on April 6, 1917.This was because a German submarine had sunk a British passenger ship, Lusitania, that killed 1,195 passengers. 128 of those were American citizens and the people were outraged — putting pressure on the U.S.government to declare war. The President, Woodrow Wilson, wanted a peaceful end but the Germans announced that they would sink any ship that approached Britain.This was when President Wilson entered the war to help restore peace to Europe.\nPress B to close";
                break;
            case 8:
                factText.text = "World War I Fact:\nOver 8 million soldiers died in World War 1, and another 21 million injured. A staggering 65 million soldiers were mobilized during the war.\nPress B to close";
                break;
            case 9:
                factText.text = "World War I Fact:\nChemical weapons were first used in WW1. Using poison gas was considered a war crime, but tear gas wasn’t considered to be a conflict by the troops. The Germans were the firs to use lethal gases when they used a chlorine gas attack. Later they also developed and used the most effective gas of the First World War — mustard gas. The British were shocked at the German use of poison gas, but developed their own gas warfare to retaliate.\nPress B to close";
                break;
            case 10:
                factText.text = "World War I Fact:\nThe U.S. were only in combat for 7 months. During this time, around 116,000 soldiers were killed and 204,000 were injured.\nPress B to close";
                break;
            case 11:
                factText.text = "World War I Fact:\nIn 1918 the German citizens began striking and demonstrating against the war. The people were starving and the economy was collapsing because British navy boats were blocking all the German ports. This led to the people protesting to try and end the war.\nPress B to close";
                break;
            case 12:
                factText.text = "World War I Fact:\nGerman Emperor Kaiser Wilhelm II stepped down on November 9, 1918. The leaders of both sides of the war met at Compiegne, France and the peace armistice was signed on November 11, 1918.\nPress B to close";
                break;
            case 13:
                factText.text = "World War I Fact:\nWW1 officially ended on June 28, 1919. This was exactly five years since the assassination of Franz Ferdinand. The armistice on November 11, 1918 ended the fighting, but it took another six months to negotiate peace before the Treaty of Versailles could be prepared.\nPress B to close";
                break;
            case 14:
                factText.text = "World War I Fact:\nThe Treaty of Versailles had a lot of requirements. Germany had to accept full responsibility for causing World War I. Also, they had to surrender some of it’s territories and colonies, and limit the size of its military.\nPress B to close";
                break;
            case 15:
                factText.text = "World War I Fact:\nA League of Nations was also formed to prevent future wars. It helped Europe to rebuild after WW1 and 53 nations had joined by 1923. The U.S. Senate refused to let the United States join and President Woodrow Wilson, who actually established the league, suffered a nervous breakdown — spending the rest of his term as an invalid.\nPress B to close";
                break;
            case 16:
                factText.text = "World War I Fact:\nIn 1926, Germany also joined the League of Nations. Many Germans were resentful of the harsh conditions of the Treaty of Versailles and Germany and Japan both withdrew from the League of Nations in 1933. Italy also withdrew in 1936.\nPress B to close";
                break;
            case 17:
                factText.text = "World War I Fact:\nThe League of Nations could not stop Germany, Italy and Japan from expanding their power. They took over smaller countries and many believe that World War 1 didn’t really end and World War 2 was an extension of it.\nPress B to close";
                break;
            case 18:
                factText.text = "World War I Fact:\nBritish Army generals were banned from going “over the top”. A stereotype of the war was that people high up in the military were afraid to fight and used the ordinary soldiers instead. Actually, there were so many British generals that wanted to fight they had to be banned. This was because if they were killed, the experience of a general would be lost and make it more difficult to fight the war.\nPress B to close";
                break;
            case 19:
                factText.text = "World War I Fact:\n9 out of 10 British soldiers survived. In World War I most British soldiers moved around the trench system constantly and were not very often in the firing line. Their lives in WW1 would have been quite boring, with lots of routine jobs and duties.\nPress B to close";
                break;
            case 20:
                factText.text = "World War I Fact:\nBlood banks were first used in WW1. Blood transfusions were used routinely to treat injured soldiers — transferring blood from one soldier to another. U.S. Army doctor Captain Oswald Johnson established a blood bank on the Western Front in 1917. Blood was kept on ice for up to 28 days using sodium citrate to prevent it from coagulating and becoming unusable.\nPress B to close";
                break;
            case 21:
                factText.text = "World War I Fact:\nThe youngest British soldier was just 12 years old. A boy called Sidney Lewis lied about his age so that he could join the war and fight for his country. He was one of 250,000 underage soldiers and many of them lied so that they could enlist. Most did it because they love their country, and some to escape their poor lives.\nPress B to close";
                break;
            case 22:
                factText.text = "World War I Fact:\nPlastic surgery was invented because of the First World War. Surgeon Harold Gillies helped shrapnel victims who had terrible facial injuries with one of the earliest examples of plastic surgery. The twisted metal caused many facial injuries that were far worse than a straight-line wound of a bullet. The techniques used by Dr Gillies pioneered the techniques for facial reconstructive surgery.\nPress B to close";
                break;
            case 23:
                factText.text = "World War I Fact:\nOver 12 million letters were delivered to the front line every week. It took only 2 days for a letter to be delivered from Britain to France — even during the war. In Regent’s Park, London, a mail office was built to send letters to the trenches. Over 2 billion letters and 114 million parcels were sent by the time the war ended.\nPress B to close";
                break;
            case 24:
                factText.text = "World War I Fact:\nJournalists risked their lives to report on WW1. Journalists were banned from reporting because the British Government wanted to control the information from the trenches and the War Office actually considered reporting on it as helping the enemy. If caught, a journalist would face the death penalty. Despite this, a handful of journalists did report on the war to show the harsh conditions the soldiers faced.\nPress B to close";
                break;
            case 25:
                factText.text = "World War I Fact:\nA battlefield explosion in France was heard in England. The majority of WW1 was fought in muddy trenches but one group of miners dug underground tunnels to detonate mines behind enemy trenches. One mine, in Messines Ridge in Belgium, detonated 900,000lbs of explosives and completely destroyed the German front line. This explosion was so loud and so powerful that the British Prime Minister, David Lloyd George, heard it all the way back in Downing Street, London — 140 miles away.\nPress B to close";
                break;
            default:
                break;
        }
    }
}
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class ItemManager : MonoBehaviour {

    public static List<bool> hasKey = new List<bool>();
    public static bool hasFlashLight;
    public static string visibleItem;

    private List<GameObject> keys = new List<GameObject>();
    private GameObject flashLight;

    private GameObject gameManager;
    private GameObject childObject;

    [Tooltip("Animates the crosshair when looking at an item.")]
    public Animator crossHairAnimation;
    public Text pickupText;

	void Start ()
    {
        gameManager = this.gameObject;

        for(int i = 0; i < gameManager.transform.GetChild(0).childCount; i++)
        {
            keys.Add(gameManager.transform.GetChild(0).GetChild(i).gameObject);
            hasKey.Add(false);
        }

        flashLight = gameManager.transform.GetChild(1).gameObject;
        childObject = flashLight.transform.Find("GlowEffect").gameObject;

        visibleItem = "";
	}

    void Update()
    {
        if (visibleItem != "")
        {
            crossHairAnimation.SetBool("TargetVisible", true);
            if (visibleItem == "FlashLight")
            {
                pickupText.text = "Press E to pick up the Flashlight";
                FindChildObject(flashLight);
                if (Input.GetButtonDown("Use"))
                {
                    flashLight.SetActive(false);
                    hasFlashLight = true;
                    crossHairAnimation.SetBool("TargetVisible", false);
                }
            }

            for (int i = 0; i < keys.Count; i++)
            {
                if (visibleItem == keys[i].name)
                {
                    pickupText.text = "Press E to pick up the Key";
                    FindChildObject(keys[i]);
                    if (Input.GetButtonDown("Use"))
                    {
                        keys[i].SetActive(false);
                        hasKey[i] = true;
                        crossHairAnimation.SetBool("TargetVisible", false);
                    }
                }
            }
        }
        else
        {
            pickupText.text = null;
            childObject.SetActive(false);
            crossHairAnimation.SetBool("TargetVisible", false);
        }       
    }

    void FindChildObject(GameObject parent)
    {
        childObject = parent.transform.Find("GlowEffect").gameObject;
        childObject.SetActive(true);
    }
}

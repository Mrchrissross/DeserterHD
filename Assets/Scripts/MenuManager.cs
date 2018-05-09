using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public GameObject story;
    public GameObject buttons;

    private float targetTime = 63.0f;
    private bool storyOn = false;

    void Update()
    {
        if (Input.GetButtonDown("CloseText"))
        {
            if (storyOn)
                storyOn = false;
            else
                storyOn = true;
        }

        if (storyOn)
        {
            targetTime -= Time.deltaTime;
            story.SetActive(true);
            buttons.SetActive(false);

            if (targetTime <= 0.0f)
            {
                storyOn = false;
                story.SetActive(false);
                buttons.SetActive(true);
            }
        }
        else
        {
            story.SetActive(false);
            buttons.SetActive(true);
        }
    }

    public void StartGame ()                        // Called upon when player clicks play
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

    public void Story()                      // Called upon when player clicks story
    {
        storyOn = true;
    }   

    public void QuitGame()                          // Called upon when player clicks on quit
    {
        Debug.Log("Application Quit!");             // Console will log message saying application quit
        Application.Quit();                         // The application will now quit
    }
}

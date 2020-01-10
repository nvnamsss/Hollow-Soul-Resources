using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject optMenu, pauseScreen;
    public string mainMenuScreen;
    public AudioSource btnSFX, hoverSFX;
    private bool isPaused;

    public Text loadingText;
    public GameObject loadingScreen, loadingIcon;
    public Slider loadingBar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseUnPause();
        }
    }

    public void pauseUnPause()
    {
        if (!isPaused)
        {
            pauseScreen.SetActive(true);
            isPaused = true;
            Time.timeScale = 0f;
        }
        else
        {
            pauseScreen.SetActive(false);
            isPaused = false;
            Time.timeScale = 1f;
        }
    }

    public void openOptions()
    {
        optMenu.SetActive(true);
    }

    public void closeOptions()
    {
        optMenu.SetActive(false);
    }

    public void toMainMenu()
    {
        //Time.timeScale = 1f;
        //SceneManager.LoadScene(mainMenuScreen);
        StartCoroutine(Loading());
    }

    public void playHover()
    {
       hoverSFX.Play();
    }

    public void playBtn()
    {
        btnSFX.Play();
    }


    public IEnumerator Loading()
    {
        loadingScreen.SetActive(true);
        AsyncOperation async = SceneManager.LoadSceneAsync(mainMenuScreen);
        async.allowSceneActivation = false;

        while (async.isDone == false)
        {
            loadingBar.value = async.progress;
            if (async.progress == 0.9f)
            {
                loadingBar.value = 1f;
                loadingText.text = "Press any key to continue";
                loadingIcon.SetActive(false);
                if (Input.anyKeyDown)
                {
                    async.allowSceneActivation = true;

                    Time.timeScale = 1f;
                }
            }
            yield return null;
        }
    }
}

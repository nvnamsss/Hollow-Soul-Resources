using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public string firstLevel;
    public GameObject options;
    public AudioSource buttonClickSFX, hoverSFX;

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

    }

    public void StartGame(){
        //SceneManager.LoadScene(firstLevel);
        StartCoroutine(Loading());
    }

    public void OpenOptions(){
        options.SetActive(true);
    }

    public void CloseOptions(){
        options.SetActive(false);
    }

    public void Quit(){
        Application.Quit();
    }

    public void playButtonSFX()
    {
        buttonClickSFX.Play();
    }

    public void playHoverSFX()
    {
        hoverSFX.Play();
    }

    public IEnumerator Loading()
    {
        loadingScreen.SetActive(true);
        AsyncOperation async = SceneManager.LoadSceneAsync(firstLevel);
        async.allowSceneActivation = false;

        while(async.isDone == false)
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

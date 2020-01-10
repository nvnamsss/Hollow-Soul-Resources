using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    
    public Toggle fullscrTog, vsyncTog;
    public Resolution[] res;
    [SerializeField]
    private int selectedRes;
    public Text leftRes, rightRes, resLabel;
    public AudioMixer mainMixer;
    public Slider mastSli, musiSli, sfxSli;
    public Text mastText, musiText, sfxText;
    public AudioSource sfxLoop;
    // Start is called before the first frame update
    void Start()
    {
        fullscrTog.isOn = Screen.fullScreen;

        if (QualitySettings.vSyncCount == 1)
        {
            vsyncTog.isOn = true;
        }
        else
        {
            vsyncTog.isOn = false;
        }

        bool foundRes = false;

        for (int i = 0; i < res.Length - 1; i++)
        {
            if(Screen.width == res[i].W && Screen.height == res[i].H)
            {
                foundRes = true;
                selectedRes = i;
                updateRes();
            }
        }

        if (!foundRes)
        {
            resLabel.text = Screen.width.ToString() + " x " + Screen.height.ToString();
            selectedRes = -1;
        }

        if (selectedRes == 0)
        {
            leftRes.color = new Color32(128, 128, 128, 255);
        }
        if (selectedRes == res.Length - 1)
        {
            rightRes.color = new Color32(128, 128, 128, 255);
        }

        if (PlayerPrefs.HasKey("MasterVol")) {
            mainMixer.SetFloat("MasterVol", PlayerPrefs.GetFloat("MasterVol"));
            mastSli.value = PlayerPrefs.GetFloat("MasterVol");
        }

        if (PlayerPrefs.HasKey("MusicVol"))
        {
            mainMixer.SetFloat("MusicVol", PlayerPrefs.GetFloat("MusicVol"));
            musiSli.value = PlayerPrefs.GetFloat("MusicVol");
        }

        if (PlayerPrefs.HasKey("MasterVol"))
        {
            mainMixer.SetFloat("SFXVol", PlayerPrefs.GetFloat("SFXVol"));
            sfxSli.value = PlayerPrefs.GetFloat("SFXVol");
        }

        mastText.text = (mastSli.value + 80).ToString();
        musiText.text = (musiSli.value + 80).ToString();
        sfxText.text = (sfxSli.value + 80).ToString();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void resGoLeft()
    {        
        if (selectedRes > 0)
        {
            selectedRes--;
        }
        if (selectedRes < 0)
        {
            selectedRes = 0;
        }
        if (selectedRes == 0)
        {
            leftRes.color = new Color32(128, 128, 128, 255);
        }
        else
        {
            rightRes.color = new Color32(255, 255, 255, 255);
        }
        updateRes();
    }

    public void resGoRight()
    {
        if (selectedRes < res.Length - 1)
        {
            selectedRes++;
        }
        if (selectedRes == res.Length - 1)
        {
            rightRes.color = new Color32(128, 128, 128, 255);
        }
        if (selectedRes == 0)
        {
            leftRes.color = new Color32(128, 128, 128, 255);
        }
        else
        {
            leftRes.color = new Color32(255, 255, 255, 255);
        }
        updateRes();
    }

    public void updateRes()
    {
        resLabel.text = res[selectedRes].W.ToString() + " x " + res[selectedRes].H.ToString();
    }

    public void ApplyGraphics() {
        Screen.fullScreen = fullscrTog.isOn;

        if (vsyncTog.isOn)
        {
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
        }

        Screen.SetResolution(res[selectedRes].W, res[selectedRes].H, fullscrTog.isOn);
    }

    public void setMaterVol()
    {
        mastText.text = (mastSli.value + 80).ToString();
        mainMixer.SetFloat("MasterVol", mastSli.value);
        PlayerPrefs.SetFloat("MasterVol", mastSli.value);
    }

    public void setMusicVol()
    {
        musiText.text = (musiSli.value + 80).ToString();
        mainMixer.SetFloat("MusicVol", musiSli.value);
        PlayerPrefs.SetFloat("MusicVol", musiSli.value);
    }

    public void setSFXVol()
    {
        sfxText.text = (sfxSli.value + 80).ToString();
        mainMixer.SetFloat("SFXVol", sfxSli.value);
        PlayerPrefs.SetFloat("SFXVol", sfxSli.value);
    }

    public void playSFX()
    {
        sfxLoop.Play();
    }

    public void stopSFX()
    {
        sfxLoop.Stop();
    }
}

[System.Serializable]
public class Resolution
{
    public int W, H;
}
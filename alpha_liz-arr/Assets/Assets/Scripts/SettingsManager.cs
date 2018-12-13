using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SettingsManager : MonoBehaviour
{
    public Dropdown resolutionDropwdown;
    public Dropdown textureDropdown;
    public Button applyButton;

    public GameSettings gamesettings;



    public Resolution[] resolutions;

    void OnEnable()
    {
        gamesettings = new GameSettings();
        textureDropdown.onValueChanged.AddListener(delegate { OnTextureQualityChange(); });
        resolutionDropwdown.onValueChanged.AddListener(delegate { OnresolutionChange(); });
        applyButton.onClick.AddListener(delegate { OnApplyButtonClick(); });

        resolutions = Screen.resolutions;
        foreach(Resolution resolution in resolutions)
        {
            resolutionDropwdown.options.Add(new Dropdown.OptionData(resolution.ToString()));
        }
        
    }

    public void OnresolutionChange()
    {
        Screen.SetResolution(resolutions[resolutionDropwdown.value].width, resolutions[resolutionDropwdown.value].height, Screen.fullScreen);
    }

    public void OnTextureQualityChange()
    {
        QualitySettings.masterTextureLimit = gamesettings.textureQuality = textureDropdown.value;
        

    }

    public void OnApplyButtonClick()
    {
        SaveSettings();
    }

    public void SaveSettings()
    {
        string jsonData = JsonUtility.ToJson(gamesettings, true);
        File.WriteAllText(Application.persistentDataPath + "/gamesettings.json", jsonData);

    }

    public void LoadSettings()
    {
        


    }




}

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenuManager : MonoBehaviour
{
    [Header("Scene")]
    public string gameSceneName = "SampleScene";
    [Header("Settings Panel")]
    public GameObject settingsPanel;
    [Header("Settings Sliders")]
    public Slider mouseSensitivitySlider;
    public Slider volumeSlider;
    private const string SensitivityKey = "MouseSensitivity";
    private const string VolumeKey = "GameVolume";
    void Start()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        if (settingsPanel != null)
            settingsPanel.SetActive(false);
        if (mouseSensitivitySlider != null)
        {
            mouseSensitivitySlider.minValue = 0.05f;
            mouseSensitivitySlider.maxValue = 0.5f;
            mouseSensitivitySlider.value =
                PlayerPrefs.GetFloat(SensitivityKey, 0.15f);
            mouseSensitivitySlider.onValueChanged.AddListener(
                SetMouseSensitivity
            );
        }
        if (volumeSlider != null)
        {
            volumeSlider.minValue = 0f;
            volumeSlider.maxValue = 1f;
            volumeSlider.value =
                PlayerPrefs.GetFloat(VolumeKey, 1f);
            volumeSlider.onValueChanged.AddListener(
                SetVolume
            );
            SetVolume(volumeSlider.value);
        }
    }
    public void PlayGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(gameSceneName);
    }
    public void OpenSettings()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(true);
    }
    public void CloseSettings()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(false);
    }
    public void SetMouseSensitivity(float value)
    {
        PlayerPrefs.SetFloat(SensitivityKey, value);
        PlayerPrefs.Save();
    }
    public void SetVolume(float value)
    {
        AudioListener.volume = value;
        PlayerPrefs.SetFloat(VolumeKey, value);
        PlayerPrefs.Save();
    }
    public void QuitGame()
    {
        Debug.Log("Quitting The Void Follows...");
        Application.Quit();
    }
}
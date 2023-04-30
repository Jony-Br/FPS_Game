using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _creditsButton;
    [SerializeField] private Button _quitButton;
    [SerializeField] private Button _returnBackButton;

    [SerializeField] private GameObject _play;
    [SerializeField] private GameObject _settings;
    [SerializeField] private GameObject _credits;
    [SerializeField] private GameObject _quit;

    private GameObject _currentActivatedTab;

    private void Awake()
    {
        _playButton.onClick.AddListener(OnPlayButtonPressed);
        _settingsButton.onClick.AddListener(OnSettingsButtonPressed);
        _creditsButton.onClick.AddListener(OnCreditsButtonPressed);
        _quitButton.onClick.AddListener(OnQuitButtonPressed);
        _returnBackButton.onClick.AddListener(OnReturnBackPressed);
    }
    private void OnPlayButtonPressed()
    {
        _play.SetActive(true);
        _returnBackButton.gameObject.SetActive(true);
        gameObject.SetActive(false);
        _currentActivatedTab = _play;
    }
    private void OnSettingsButtonPressed()
    {
        _settings.SetActive(true);
        _returnBackButton.gameObject.SetActive(true);
        gameObject.SetActive(false);
        _currentActivatedTab = _settings;
    }
    private void OnCreditsButtonPressed()
    {
        _credits.SetActive(true);
        _returnBackButton.gameObject.SetActive(true);
        gameObject.SetActive(false);
        _currentActivatedTab = _credits;
    }
    private void OnQuitButtonPressed()
    {
        gameObject.SetActive(false);
        _quit.SetActive(true);
        _currentActivatedTab = _quit;
        _returnBackButton.gameObject.SetActive(false);
    }
    private void OnReturnBackPressed()
    {
        gameObject.SetActive(true);
        _returnBackButton.gameObject.SetActive(false);
        _currentActivatedTab.SetActive(false);
    }
}

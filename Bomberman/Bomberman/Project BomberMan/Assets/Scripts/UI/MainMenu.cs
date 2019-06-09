using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
public class MainMenu : MonoBehaviour
{
    public GameObject FadeAway;
    public GameObject sFirstObj;
    public GameObject bFirstObj;
    public GameObject cFirstObj;
    public IEnumerator Fade1()
    {
        yield return new WaitForSecondsRealtime(.95f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Start()
    {
        FadeAway = GameObject.Find("FadeAway");
        FadeAway.gameObject.SetActive(false);
    }
    public void PlayGame()
    {

        AudioManager.instance.Play("ButtonClick");
        AudioManager.instance.Play("MenuMusic");

        StartCoroutine(Fade1());
        FadeAway.gameObject.SetActive(true);
    }
    public void QuitGame()
    {

        AudioManager.instance.Play("ButtonClick");
        Debug.Log("QUIT");
        Application.Quit();
    }
    public void SettingsButton()
    {
        AudioManager.instance.Play("ButtonClick");

        GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(bFirstObj, null);
    }
    public void BackButton()
    {
        AudioManager.instance.Play("ButtonClick");

        GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(sFirstObj, null);
    }
    public void CreditsButton()
    {
        AudioManager.instance.Play("ButtonClick");

        GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(cFirstObj, null);
    }
}

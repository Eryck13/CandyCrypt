using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class UI_Manager : MonoBehaviour
{
    // MAKE SINGLETON

    // attach pause menu, respawn, and settings menu to the designated areas in script.
    // This script should be attached to a UI_Manager object (or general managers object) in the scene
    
    public GameObject PauseMenu;
    public GameObject Settings;
    public GameObject RespawnMenu;
    public GameObject Player;

    public GameObject Character;
    public GameObject pFirstObj;
    public GameObject sFirstObj;
    public GameObject rFirstObj;

    int localHealth;
    bool bPaused = false;

    void Awake()
    {
        //making sure menus start inactive
        PauseMenu.SetActive(false);
        Settings.SetActive(false);
        RespawnMenu.SetActive(false);
        Time.timeScale = 1;
    }
    
    void Update()
    {
        localHealth = Player.GetComponent<HealthUI>().health;
        // pausing using "p" and it stops time and opens the menu
        if (Input.GetButtonUp("Pause") && bPaused == false)
        {
            Time.timeScale = 0;
            PauseMenu.SetActive(true);

            GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(pFirstObj, null);

            bPaused = true;
        }
        else if (Input.GetButtonUp("Pause") && (bPaused == true)) // unpuases and removes menus that are active
        {
            Time.timeScale = 1;
            PauseMenu.SetActive(false);
            bPaused = false;
        }

        // checking to see if the player is dead
        if (bPaused == false && localHealth == 0)
        {
            Player.SetActive(false);
            RespawnMenu.SetActive(true);
            GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(rFirstObj, null);
            //Time.timeScale = 0;
            bPaused = true;
        }
    }

    // resume button for menu to unpause game.
    public void resume()
    {
        AudioManager.instance.Play("ButtonClick");

        PauseMenu.SetActive(false);
         bPaused = false;
         Time.timeScale = 1;
    }

    // opens the settings menu for more options
    public void SettingsMenu()
    {
        AudioManager.instance.Play("ButtonClick");

        PauseMenu.SetActive(false);
        Settings.SetActive(true);
        GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(sFirstObj, null);
    }

    //leaves the settings menu and goes back to the pause menu
    public void LeaveSettings()
    {
        AudioManager.instance.Play("ButtonClick");

        PauseMenu.SetActive(true);
        Settings.SetActive(false);
        GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(pFirstObj, null);
    }

    //brings the player to the main menu
    public void MainMenu()
    {
        AudioManager.instance.Play("ButtonClick");

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        PauseMenu.SetActive(false);
        bPaused = false;
        Time.timeScale = 1;
    }

    // closses the game
    public void QuitGame()
    {
        AudioManager.instance.Play("ButtonClick");

        Application.Quit();
    }
    
    // respawns the player by re loading the scene
    public void Respawn()
    {
        AudioManager.instance.Play("ButtonClick");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        RespawnMenu.SetActive(false);
        Player.SetActive(true);
        bPaused = false;
        Time.timeScale = 1;
    }
}

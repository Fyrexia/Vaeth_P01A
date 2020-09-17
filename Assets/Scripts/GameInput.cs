using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameInput : MonoBehaviour
{
    [SerializeField] Text TimeText = null;
    
    [SerializeField] Text YouLoseText = null;

    [SerializeField] PlayerShip playerShip = null;

    [SerializeField] float TimeCounter = 0;
    
    private float TimeOnScreen = 0;
    // Update is called once per frame

    public void Awake()
    {
        YouLoseText.enabled = false;
    }


    void Update()
    {
        if (TimeCounter > 0 && playerShip.Won==false)
        {
            TimeCounter -= Time.deltaTime;
            TimeOnScreen = Mathf.Floor(TimeCounter);
            TimeText.text = "Time: " + TimeOnScreen;

            if (playerShip != null && TimeCounter <= 0f)
            {
                playerShip.Kill();
                YouLoseText.enabled = true;
                DelayHelper.DelayAction(this, ReloadLevel, 5.0f);
            }

        }
        else if(TimeCounter <=0)
        {
            TimeText.text = "Time: " + 0;
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            ReloadLevel();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public static void ReloadLevel()
    {
        int activeSceneIndex =
            SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(activeSceneIndex);
    }

   






}

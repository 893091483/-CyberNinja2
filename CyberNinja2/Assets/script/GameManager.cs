using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ExitUI;
    
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape") && ExitUI.activeInHierarchy ==false)
        {
            ExitUI.SetActive(true);
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<PlayerMovement>().enabled = false;
            Time.timeScale = 0;
        }else if (Input.GetKeyDown("escape") && ExitUI.activeInHierarchy == true)
        {
            ExitUI.SetActive(false);
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<PlayerMovement>().enabled = true;
            Time.timeScale = 1;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void OnQuit()
    {
        Application.Quit();
    }
}

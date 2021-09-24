using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu; //[SerializeField] private is the same as public but other code cannot access this.
    private bool isActive = false;
    public GameObject winText;
    public GameObject loseText;
    public GameObject canvas;
    private bool isEnd = false;

    void PauseGame() //Stop time in game when paused.
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true); //SetActive is the invisible tick box in the inspector.
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

	private void Update()
	{
        if (Input.GetKeyDown(KeyCode.Escape) && isEnd == false)
		{
            if(isActive == false)
			{
                PauseGame();
                isActive = true;
			}

            else
			{
                ResumeGame();
                isActive = false;
			}
		}

        CheckWinCondition();
	}

    private void CheckWinCondition()
	{
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            //WIN IF ALL ENEMIES DIE
            isEnd = true;
            winText.SetActive(true);
            canvas.SetActive(true);
            PauseGame();
        }

        else if (GameObject.FindGameObjectsWithTag("Unit").Length == 0)
        {
            //LOSE IF ALL UNITS DIE
            isEnd = true;
            loseText.SetActive(true);
            canvas.SetActive(true);
            PauseGame();
        }
    }
}
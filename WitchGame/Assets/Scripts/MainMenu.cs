using UnityEngine;

public class MainMenu : MonoBehaviour
{
    Canvas canvas;

    private void Start()
    {
        canvas = GetComponent<Canvas>();
    }

    public void StartGame()
    {
        canvas.enabled = false;
        GameManager.instance.StartGame();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenConroller : MonoBehaviour
{
    [SerializeField]
    private ProtagCore protag1;

    [SerializeField]
    private ProtagCore protag2;

    [SerializeField]
    private GameObject endScreenImage;

    private string sceneName = "MainGame";
    private bool gameEnded;

    private void Start()
    {
        endScreenImage.SetActive(false);
        gameEnded = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        bool continuePressed = Input.GetKeyDown(KeyCode.BackQuote) || Input.GetKeyDown(KeyCode.Period);
        if (gameEnded && continuePressed)
        {
            ResetGame();
        }

        if (protag1.playerState == ProtagCore.PlayerState.DEAD && protag2.playerState == ProtagCore.PlayerState.DEAD &&
            !gameEnded)
        {
            endScreenImage.SetActive(true);
            gameEnded = true;
        }
    }

    private void ResetGame()
    {
        SceneManager.LoadScene(sceneName);
    }
}
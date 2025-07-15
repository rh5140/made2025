using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenController : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        bool continuePressed = Input.GetKeyDown(KeyCode.BackQuote) || Input.GetKeyDown(KeyCode.Period) || Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Slash);
        if (continuePressed)
        {
            SceneManager.LoadScene("MainGame");
        }
    }
}

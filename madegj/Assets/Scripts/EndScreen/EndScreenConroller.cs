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

    void Start()
    {
        endScreenImage.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        if (protag1.playerState == ProtagCore.PlayerState.DEAD && protag2.playerState == ProtagCore.PlayerState.DEAD)
        {
            endScreenImage.SetActive(true);
            if (Input.anyKeyDown)
            {
                ResetGame();
            }
        }
    }

    void ResetGame()
    {
        SceneManager.LoadScene(sceneName);
    }
}

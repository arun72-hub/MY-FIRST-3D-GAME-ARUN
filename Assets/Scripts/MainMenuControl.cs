using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuControl : MonoBehaviour
{
    [SerializeField] AudioSource buttonPress;
    [SerializeField] GameObject fadeOut;
    void Start()
    {

    }


    void Update()
    {

    }

    public void StartGame()
    {
        buttonPress.Play();
        fadeOut.SetActive(true);
        LevelMaintain.levelNumber = 4;
        StartCoroutine(PlayTheGame());

    }
    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator PlayTheGame()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(4);
    }
}

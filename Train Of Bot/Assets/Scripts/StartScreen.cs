using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour {

    //Public Variables
    public Button newGameButton;
    public GameObject fade;
    public float timeToWait;

    //Private Variables
    private Animator fadeOut;
    
	void Start () {
        fadeOut = fade.GetComponent<Animator>();
    }

    public void ButtonClicked(string newGame)
    {
        fade.SetActive(true);
        StartCoroutine(SceneTransitionCoRoutine());
    }

    private IEnumerator SceneTransitionCoRoutine()
    {
        fadeOut.Play("Fade");

        yield return new WaitForSeconds(timeToWait);

        SceneManager.LoadScene(1);
    }
}

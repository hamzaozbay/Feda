using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuUI : MonoBehaviour {
    

    public void ContinueButton() {
        GameManager.instance.UnPauseGame();
        this.gameObject.SetActive(false);
    }

    public void QuitButton() {
        GameManager.instance.UnPauseGame();
        GameManager.instance.LoadNewScene(SceneIndexes.MAIN_MENU);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameMenu : MonoBehaviour {


    public void MainMenu() {
        GameManager.instance.LoadNewScene(SceneIndexes.MAIN_MENU);
    }



}

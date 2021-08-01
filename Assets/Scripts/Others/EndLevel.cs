using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel : MonoBehaviour {

    [SerializeField] private SceneIndexes _nextScene;
    [SerializeField] private SacrificeUI _sacrificeUI;
    [SerializeField] private bool _IsEndGame = false;
    [SerializeField] private GameObject _endGameMenu;

    private bool _isPlayerIn = false;



    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag.Equals("Player") && !_IsEndGame) {
            if (_isPlayerIn) return;

            _isPlayerIn = true;
            StartCoroutine(WaitForSacrifice());
        }
        else if (other.gameObject.tag.Equals("Player") && _IsEndGame) {
            _endGameMenu.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag.Equals("Player") && !_IsEndGame) {
            if (!_isPlayerIn) return;

            _isPlayerIn = false;
            StopCoroutine(WaitForSacrifice());
        }
    }


    private IEnumerator WaitForSacrifice() {
        yield return new WaitForSeconds(.75f);
        if (!_isPlayerIn) yield return null;

        _sacrificeUI.gameObject.SetActive(true);
        _sacrificeUI.SetUI();
        _sacrificeUI.SetEndLevel(this);
        GameManager.instance.PauseGame();
    }


    public SceneIndexes NextScene { get { return _nextScene; } }

}

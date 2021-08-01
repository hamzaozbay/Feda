using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour {

    [SerializeField] private Transform _playerStartPos;
    [SerializeField] private int _neededSoulValue = 0;
    [SerializeField] private GameObject _sceneTransition;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private SkillsUI _skillsUI;
    [SerializeField] private GameObject _diedMenu;
    [SerializeField] private Canvas _worldSpaceCanvas;
    [SerializeField] private GameObject _endLevel;

    [Space(20)]
    [SerializeField] private List<EnemyStats> _enemies;

    private int _aliveEnemies = 10;


    private void Awake() {
        GameManager.instance.levelManager = this;
        GameManager.instance.SetWorldSpaceCanvas(_worldSpaceCanvas);
    }


    private void Start() {
        _endLevel.SetActive(false);
        StartCoroutine(checkAliveEnemies());
    }

    private IEnumerator checkAliveEnemies() {
        while (_aliveEnemies > 0) {
            yield return new WaitForSeconds(1f);
            _aliveEnemies = 0;

            for (int i = _enemies.Count - 1; i >= 0; i--) {
                if (_enemies[i].IsDead()) {
                    _enemies.Remove(_enemies[i]);
                    continue;
                }

                _aliveEnemies++;
            }
        }

        _endLevel.SetActive(true);
    }




    public void SceneTransition(string openOrClose) {
        _sceneTransition.GetComponent<Animator>().SetTrigger(openOrClose);
    }



    public Transform PlayerStartPos { get { return _playerStartPos; } }
    public int NeededSoulValue { get { return _neededSoulValue; } }
    public GameObject PauseMenu { get { return _pauseMenu; } }
    public GameObject DiedMenu { get { return _diedMenu; } }
    public SkillsUI SkillsUI { get { return _skillsUI; } }

}

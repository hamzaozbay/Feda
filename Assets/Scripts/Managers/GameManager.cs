using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    #region  Singleton
    public static GameManager instance { get; private set; }

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
        }
        else {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    #endregion

    [HideInInspector] public LevelManager levelManager;
    [HideInInspector] public int playerHealth = 100;

    [SerializeField] private GameObject _moveIndicatorPoolPrefab;
    [SerializeField] private CursorSprites _cursorSprites;

    private GameObject _player;
    private ObjectPool _moveIndicatorPool;
    private Canvas _worldSpaceCanvas;
    private bool isGamePaused = false;



    private void Start() {
        InitializeMoveIndicator();
        Cursor.SetCursor(_cursorSprites.CursorNormal, Vector2.zero, CursorMode.Auto);
    }



    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) && !IsGamePaused()) {
            PauseGame();
            levelManager.PauseMenu.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && IsGamePaused()) {
            UnPauseGame();
            levelManager.PauseMenu.SetActive(false);
        }
    }



    public void LoadNewScene(SceneIndexes scene) {
        GetPlayerHealth();
        levelManager.SceneTransition("Close");
        StartCoroutine(waitForSceneTransition(scene));
    }
    private IEnumerator waitForSceneTransition(SceneIndexes scene) {
        yield return new WaitForSeconds(1f);
        Loader.Load(scene);
    }



    public void PauseGame() {
        isGamePaused = true;
        Time.timeScale = 0f;
    }
    public void UnPauseGame() {
        isGamePaused = false;
        Time.timeScale = 1f;
    }


    public void ReloadScene() {
        UnPauseGame();
        EquipmentManager.instance.UnequipAll();
        Inventory.instance.ClearInventory();
        playerHealth = 100;
        Loader.Load((SceneIndexes)SceneManager.GetActiveScene().buildIndex);
    }


    public void PlayerDied() {
        PauseGame();
        levelManager.DiedMenu.SetActive(true);
    }


    private void InitializeMoveIndicator() {
        GameObject pool = Instantiate(_moveIndicatorPoolPrefab, this.transform);
        _moveIndicatorPool = pool.GetComponent<ObjectPool>();
    }






    public bool IsGamePaused() {
        return isGamePaused;
    }
    public void GetPlayerHealth() {
        playerHealth = Player.GetComponent<PlayerStats>().CurrentHealth;
    }

    public void SetWorldSpaceCanvas(Canvas c) {
        _worldSpaceCanvas = c;
    }

    public void SetPlayer(GameObject g) {
        _player = g;
    }


    public Canvas WorldSpaceCanvas { get { return _worldSpaceCanvas; } }
    public ObjectPool MoveIndicatorPool { get { return _moveIndicatorPool; } }
    public GameObject Player { get { return _player; } }
    public CursorSprites CursorSprite { get { return _cursorSprites; } }





    [Serializable]
    public struct CursorSprites {
        [SerializeField] private Texture2D _cursorNormal, _cursorSelect, _cursorAttack;

        public Texture2D CursorNormal { get { return _cursorNormal; } }
        public Texture2D CursorSelect { get { return _cursorSelect; } }
        public Texture2D CursorAttack { get { return _cursorAttack; } }
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader {

    private class LoadingMonoBehaviour : MonoBehaviour { }



    private static Action onLoaderCallback;
    private static AsyncOperation loadingAsyncOperation;



    public static void Load(SceneIndexes scene) {
        onLoaderCallback = () => {
            GameObject loadingGameObject = new GameObject("Loading Game Object");
            loadingGameObject.AddComponent<LoadingMonoBehaviour>().StartCoroutine(LoadSceneAsync(scene));
        };
        
        SceneManager.LoadScene((int)SceneIndexes.LOADING);
    }

    private static IEnumerator LoadSceneAsync(SceneIndexes scene) {
        yield return null;

        loadingAsyncOperation = SceneManager.LoadSceneAsync((int)scene);

        while(!loadingAsyncOperation.isDone) {
            yield return null;
        }
    }

    public static float GetLoadingProgress() {
        if(loadingAsyncOperation != null) {
            return loadingAsyncOperation.progress;
        }
        else {
            return 1f;
        }
    }

    public static void LoaderCallback() {
        if(onLoaderCallback != null) {
            onLoaderCallback();
            onLoaderCallback = null;
        }
    }

}

public enum SceneIndexes {
        MAIN_MENU = 0,
        LOADING = 1,
        LEVEL1 = 2,
        LEVEL2 = 3,
        LEVEL3 = 4,
        LEVEL4 = 5,
        LEVEL5 = 6,
        LEVEL6 = 7,
        LEVEL7 = 8,
        LEVEL8 = 9,
        LEVEL9 = 10,
        LEVEL10 = 11,
        CINEMATIC = 12
}

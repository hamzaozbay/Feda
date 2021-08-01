using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicController : MonoBehaviour {

    [SerializeField] private GameObject _text;



    public void EnableText() {
        _text.SetActive(true);
    }


    public void LoadLevel1() {
        Loader.Load(SceneIndexes.LEVEL1);
    }



}

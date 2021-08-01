using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillsUI : MonoBehaviour {

    [SerializeField] private SkillSlot[] _skillSlots = new SkillSlot[3];


    public void StartCountDown(int index, int time) {
        _skillSlots[index].SetCountDownTime(time);
        _skillSlots[index].StartCountDown();
    }


}

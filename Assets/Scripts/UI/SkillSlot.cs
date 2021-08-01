using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillSlot : MonoBehaviour {

    [SerializeField] private string skillName;
    [SerializeField] private string skillDescription;
    [SerializeField] private GameObject countDownObject;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private int countDownTime = 10;
    [SerializeField] private TextMeshProUGUI descTitle;
    [SerializeField] private TextMeshProUGUI descDescription;

    private WaitForSeconds waitFor;
    private int time = 0;



    private void Start() {
        waitFor = new WaitForSeconds(1);

        descTitle.text = skillName;
        descDescription.text = skillDescription;
    }


    public void StartCountDown() {
        countDownObject.SetActive(true);
        StartCoroutine(waitForASecond());
    }
    private IEnumerator waitForASecond() {
        while (time > 0) {
            yield return waitFor;
            time -= 1;

            timeText.text = time.ToString();
        }

        if (time <= 0)
            countDownObject.SetActive(false);
    }

    public void SetCountDownTime(int t) {
        countDownTime = t;
        time = countDownTime;
        timeText.text = time.ToString();
    }


}

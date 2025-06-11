using UnityEngine;
using UnityEngine.UI;

public class GameInfoUI : MonoBehaviour
{
    [SerializeField] private Text stageTxt;
    [SerializeField] private Text waveTxt;
    [SerializeField] private Text goldTxt;

    private void Update()
    {
        SetStageText(GameManager.Instance.Stage);
        SetWaveText(GameManager.Instance.Wave);
        SetGoldText(GameManager.Instance.Gold);
    }

    private void SetStageText(int stage)
    {
        stageTxt.text = $"{stage}";
    }

    private void SetWaveText(int wave)
    {
        waveTxt.text = $"{wave}";
    }

    private void SetGoldText(int gold)
    {
        goldTxt.text = $"{gold} G";
    }
}

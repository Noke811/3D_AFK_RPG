using UnityEngine;
using UnityEngine.UI;

public class GameInfoUI : MonoBehaviour
{
    [SerializeField] private Text stageTxt;
    [SerializeField] private Text levelTxt;
    [SerializeField] private Text goldTxt;

    private void Update()
    {
        SetStageText(GameManager.Instance.Stage, GameManager.Instance.Wave);
        SetLevelText(GameManager.Instance.Player.Level);
        SetGoldText(GameManager.Instance.Gold);
    }

    private void SetStageText(float stage, float wave)
    {
        stageTxt.text = $"{stage} - {wave}";
    }

    private void SetLevelText(float level)
    {
        levelTxt.text = $"Lv. {level}";
    }

    private void SetGoldText(float gold)
    {
        goldTxt.text = $"{gold} G";
    }
}

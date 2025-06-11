using UnityEngine;
using UnityEngine.UI;

public class StatUI : MonoBehaviour
{
    [SerializeField] private Text levelTxt;
    [SerializeField] private Text atkTxt;
    [SerializeField] private Text defTxt;

    private void Update()
    {
        SetLevelText(GameManager.Instance.Player.Level);
        SetAtkText(GameManager.Instance.Player.Stat.Attack);
        SetDefText(GameManager.Instance.Player.Stat.Defense);
    }

    private void SetLevelText(int level)
    {
        levelTxt.text = $"Lv. {level}";
    }

    private void SetAtkText(float atk)
    {
        atkTxt.text = $"{atk}";
    }

    private void SetDefText(float def)
    {
        defTxt.text = $"{def}";
    }
}

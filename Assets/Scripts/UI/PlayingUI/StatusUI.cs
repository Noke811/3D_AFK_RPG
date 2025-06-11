using UnityEngine;
using UnityEngine.UI;

public class StatusUI : MonoBehaviour
{
    [SerializeField] private Slider hp;
    [SerializeField] private Slider mp;
    [SerializeField] private Slider exp;

    private void Update()
    {
        hp.value = GameManager.Instance.Player.Stat.CurHp / GameManager.Instance.Player.Stat.MaxHp;
        mp.value = GameManager.Instance.Player.Stat.CurMp / GameManager.Instance.Player.Stat.MaxMp;
        exp.value = GameManager.Instance.Player.CurExp / GameManager.Instance.Player.NextExp;
    }
}

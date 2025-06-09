using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator Animator { get; private set; }
    public CharacterController Controller { get; private set; }
    public StatHandler Stat { get; private set; }

    private void Awake()
    {
        Animator = GetComponentInChildren<Animator>();
        Controller = GetComponent<CharacterController>();
        Stat = GetComponent<StatHandler>();

        Stat.Init();
    }
}

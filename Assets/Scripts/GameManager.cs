using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public const string TAG_PLAYER = "Player";

    [field: Header("External Objects")]
    [field: SerializeField] public UIManager UIManager { get; private set; }
    [field: SerializeField] public Player Player { get; private set; }

    public int Stage { get; private set; }
    public int Wave { get; private set; }

    public int Gold { get; private set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Stage = 1;
        Wave = 1;
        Gold = 1000;
    }

    private void Update()
    {
        // test
        if(Input.GetKeyDown(KeyCode.C)) NextWave();

        if (Input.GetKeyDown(KeyCode.D)) Player.Stat.TakeDamage(10f);

        if (Input.GetKeyDown(KeyCode.L)) Player.GetExp(3f);
    }

    // 다음 스테이지 실행
    public void NextStage()
    {
        if(Stage == 5)
        {
            Debug.Log("게임 클리어!!");
            return;
        }

        Wave = 1;
        Stage++;
    }

    // 다음 웨이브 실행
    public void NextWave()
    {
        if(Wave == 5)
        {
            NextStage();
            return;
        }

        Wave++;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VersusGameManager : MonoBehaviour
{
    public VersusCameraController MainCamera;
    
    [Header("Prefabs")]
    [SerializeField] private CreateArea[] createAreas;
    [SerializeField] private VersusBullet clone;
    public GameObject bulletNode;
    public VersusPlayer player1;
    public VersusPlayer player2;
    public PropSelectPanelController propPanel1;
    public PropSelectPanelController propPanel2;
    
    [Header("Game Config")]
    [SerializeField] private Color[] colors = new Color[2];
    public CardController[] AvailableCards;
    public int Hp_max;
    public int Energy_max;
    public float PlayerSpeed;
    public float BulletSpeed;
    
    public static string winner = null;
    public static bool isPlayer1Computer = false;
    public static bool isPlayer2Computer = true;

    [Header("Data Management")]
    [SerializeField] private DataManager dataManager;
    public Dictionary<string, int> PropUsage = new Dictionary<string, int>();
    
    private int bulletCounter = 0;
    
    void Start()
    {
        // Init PropUsage dictionary
        foreach (CardController card in AvailableCards)
        {
            PropUsage.Add(card.name, 0);
        }
        player1.IsComputer = isPlayer1Computer;
        player2.IsComputer = isPlayer2Computer;
    }
    
    public void SendForm()
    {
        string prop1 = PropUsage["InvincibleCard"].ToString();
        string prop2 = PropUsage["HealCard"].ToString();
        string prop3 = PropUsage["BulletChangeColorCard"].ToString();
        dataManager.SendVersus(Time.timeSinceLevelLoad.ToString("0.0"), winner, prop1, prop2, prop3);
    }
    
    public VersusBullet CreateBullet(Vector3 position)
    {
        VersusBullet bullet = Instantiate(clone, position, Quaternion.identity, bulletNode.transform);
        bullet.name = bulletCounter.ToString();
        bulletCounter++;
        return bullet;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            propPanel1.gameObject.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.U))
        {
            propPanel2.gameObject.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            MainCamera.StartTransition(3.0f, player1.transform.position);
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            MainCamera.Reset();
        }
        
        
        foreach (CreateArea ca in createAreas)
        {
            if (ca.CheckTime())
            {
                Vector3 pos = ca.GetRandomPos();
                VersusBullet bullet = CreateBullet(pos);
                bullet.Color = colors[Random.Range(0, colors.Length)];
                bullet.TargetPlayer = (Random.Range(0, 2) == 1) ? player1 : player2;
                ca.NextTime();
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public Player Player;
    public ClawGamePhysics Claw;
    public bool isGameOver = false;

    protected override void Awake()
    {
        base.Awake();
        //Player = Instantiate(ResourceManager.Instance.LoadAsset<Player>(), new Vector3(-5.85f, -3.5f, 0f), Quaternion.identity); // 임시
        Claw = FindAnyObjectByType<ClawGamePhysics>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver()
    {
        UIManager.Hide<UITop>();
        UIManager.Show<UIGameOver>();
        Invoke("LoadStartScene", 3f);
        isGameOver = true;
    }

    void LoadStartScene()
    {
        SceneManager.LoadScene("StartScene");
    }
}

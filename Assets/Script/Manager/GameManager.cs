using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Player Player;
    public ClawGamePhysics Claw;

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
}

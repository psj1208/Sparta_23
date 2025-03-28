using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Player Player;

    protected override void Awake()
    {
        base.Awake();
        Player = Instantiate(ResourceManager.Instance.LoadAsset<Player>());
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    #region Enemy AnimationData
    private string attackParameterName = "Attack";
    private string damageParameterName = "Damage";
    private string dieParameterName = "Die";
    public int AttackAnimHash { get; private set; }
    public int DamageAnimHash { get; private set; }
    public int DieAnimHash { get; private set; }
    #endregion

    private BattleStageController _battleStageController;
    public BattleStageController BattleStageController { get { return _battleStageController; } set { _battleStageController = value; } }
    
    
    protected override void Awake()
    {
        base.Awake();
        Animator = GetComponent<Animator>();
        ResourceController = GetComponent<ResourceController>();
        StatHandler = GetComponent<StatHandler>();

        AttackAnimHash = Animator.StringToHash(attackParameterName);
        DamageAnimHash = Animator.StringToHash(damageParameterName);
        DieAnimHash = Animator.StringToHash(dieParameterName);
    }

    protected override void Start()
    {
        base.Start();
        ResourceController.OnDamageAction += Damaged;
        ResourceController.OnDieAction += Die;

        TurnManager.Instance.OnEnemyTurnStart -= AttackOnce;
        TurnManager.Instance.OnEnemyTurnStart += AttackOnce;
        TurnManager.Instance.OnEnemyTurnStart += AttackOnce;
    }

#if DEBUG
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            ResourceController.ChangeHealth(-5);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            ResourceController.ChangeHealth(-10000);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            AttackOnce(GameManager.Instance.Player);
        }
    }
#endif

    /// <summary>
    /// Enemy의 공격 함수
    /// </summary>
    public void AttackOnce(Player player)
    {
        TriggerAnimation(AttackAnimHash);
        player.ResourceController.ChangeHealth(-StatHandler.GetStat(EStatType.Attack));
    }
    
    /// <summary>
    /// Enemy 피격
    /// </summary>
    public void Damaged()
    {
        TriggerAnimation(DamageAnimHash);
    }
    /// <summary>
    /// Enemy 사망
    /// </summary>
    public void Die()
    {
        TriggerAnimation(DieAnimHash);
        StartCoroutine(WaitForDieAnimation());
    }
    
    private IEnumerator WaitForDieAnimation()
    {
        // 현재 실행 중인 애니메이션 상태 정보 가져오기
        AnimatorStateInfo stateInfo = Animator.GetCurrentAnimatorStateInfo(0);

        // 'Die' 애니메이션이 실제로 재생될 때까지 대기
        while (!stateInfo.IsName(dieParameterName))
        {
            yield return null;
            stateInfo = Animator.GetCurrentAnimatorStateInfo(0);
        }

        // 애니메이션 길이만큼 대기
        yield return new WaitForSeconds(stateInfo.length);

        // 배틀 스테이지 컨트롤러에서 자신을 제거
        BattleStageController.RemoveEnemy(this);

        // 게임 오브젝트 제거
        Destroy(gameObject);
    }
    

    void TriggerAnimation(int animationHash)
    {
        Animator.SetTrigger(animationHash);
    }
}

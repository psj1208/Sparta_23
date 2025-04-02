using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEditor.VersionControl;

public class NotificationManager : MonoBehaviour
{
    public Canvas poolingCanvas;
    public static NotificationManager Instance;
    public Transform notificationParent;

    void Awake()    
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
            Instance = this;
    }

    public void ShowDamageIndicator(float amount, Transform enemyTransform)
    {
        if (enemyTransform == null) return;
        
        GameObject damageIndicator = NotificationPool.Instance.GetNotification();
        if (damageIndicator == null) return;
        damageIndicator.SetActive(true);

        TextMeshProUGUI text = damageIndicator.GetComponentInChildren<TextMeshProUGUI>();
        if (amount > 0)
        {
            text.color = Color.green;
            text.text = amount.ToString();
        }
        else
        {
            text.text = amount.ToString();
        }

        RectTransform rectTransform = damageIndicator.GetComponent<RectTransform>();
        if (rectTransform == null) return;
        
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(new Vector3(enemyTransform.position.x, enemyTransform.position.y + 1, 0));
        
        RectTransformUtility.ScreenPointToLocalPointInRectangle(poolingCanvas.GetComponent<RectTransform>(),
            screenPosition, null, out Vector2 localPos);
        
        rectTransform.anchoredPosition = localPos + new Vector2(Random.Range(-30f, 30f), Random.Range(-30f, 30f));
        rectTransform.DOAnchorPosY(rectTransform.anchoredPosition.y + 50, 0.5f).SetEase(Ease.OutBack);

        CanvasGroup canvasGroup = damageIndicator.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1f;
        DOVirtual.DelayedCall(1f, () =>
        {
            if (canvasGroup != null)
            {
                canvasGroup.DOFade(0, 0.5f).OnComplete(() =>
                {
                    damageIndicator.SetActive(false);
                    NotificationPool.Instance.ReturnNotification(damageIndicator);
                });
            }
        });
    }
    
    private Vector2 WorldToCanvasInOverlay(Vector2 world)
    {
        Vector2 screen = Camera.main.WorldToScreenPoint(world);

        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(poolingCanvas.GetComponent<RectTransform>(),screen, null,out Vector2 localPos))
        {
            return localPos;
        }
        return Vector2.zero;
    }
    
}
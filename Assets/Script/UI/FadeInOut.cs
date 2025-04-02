using System.Collections;
using UnityEngine;

public class FadeInOut : MonoBehaviour
{
    [SerializeField]
    private GameObject obj;

    public void FadeIn(float fadeTime)
    {
        obj.SetActive(true);
        StartCoroutine(FadeInCoroutine(fadeTime));
    }

    public void FadeOut(float fadeTime)
    {
        obj.SetActive(true);
        StartCoroutine(FadeOutCoroutine(fadeTime));
    }

    IEnumerator FadeInCoroutine(float fadeTime)
    {
        float time = 0f;
        if (obj.TryGetComponent<CanvasRenderer>(out CanvasRenderer renderer))
        {
            renderer.SetAlpha(0.5f);
            while (time <= fadeTime)
            {
                renderer.SetAlpha(Mathf.Lerp(0f, 1f, time / fadeTime));

                time += Time.deltaTime;
                yield return null;
            }
        }

        yield break;
    }

    IEnumerator FadeOutCoroutine(float fadeTime)
    {
        float time = 0f;
        if (obj.TryGetComponent<CanvasRenderer>(out CanvasRenderer renderer))
        {
            renderer.SetAlpha(.5f);
            while (time <= fadeTime)
            {
                renderer.SetAlpha(Mathf.Lerp(1f, 0f, time / fadeTime));

                time += Time.deltaTime;
                yield return null;
            }

        }

        yield break;
    }
}

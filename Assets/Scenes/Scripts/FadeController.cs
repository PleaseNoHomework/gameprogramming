using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class FadeController : MonoBehaviour
{
    float times = 0f;
    public static FadeController instance;
    public Image panelImage;
    public TMP_Text texts;
    public float fadeDuration = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null) instance = this;
        Color color = panelImage.color;
        color.a = 0f;
    }

    private void Update()
    {
        times += (Time.deltaTime - times) * 0.1f;
        float fps = 1.0f / times;
        texts.text = $"FPS : {fps:0.}";
    }

    public void FadeIn(int flag) //0이면 어두워짐 1이면 밝아짐
    {
        if (flag == 0) StartCoroutine(fadeIn());
        else StartCoroutine(fadeOut());
    }

    public IEnumerator fadeIn() //어두워짐
    {
        float elapsedTime = 0f;
        Color color = panelImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration); // 알파 값 증가
            panelImage.color = color;
            yield return null;
        }
        //여기서 컷신으로 변경해주기
    }

    public IEnumerator fadeOut() //밝아짐
    {
        float elapsedTime = 0f;
        Color color = panelImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(1 - (elapsedTime / fadeDuration)); // 알파 값 증가
            panelImage.color = color;
            yield return null;
        }
    }
}

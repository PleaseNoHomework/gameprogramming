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

    public void FadeIn(int flag) //0�̸� ��ο��� 1�̸� �����
    {
        if (flag == 0) StartCoroutine(fadeIn());
        else StartCoroutine(fadeOut());
    }

    public IEnumerator fadeIn() //��ο���
    {
        float elapsedTime = 0f;
        Color color = panelImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration); // ���� �� ����
            panelImage.color = color;
            yield return null;
        }
        //���⼭ �ƽ����� �������ֱ�
    }

    public IEnumerator fadeOut() //�����
    {
        float elapsedTime = 0f;
        Color color = panelImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(1 - (elapsedTime / fadeDuration)); // ���� �� ����
            panelImage.color = color;
            yield return null;
        }
    }
}

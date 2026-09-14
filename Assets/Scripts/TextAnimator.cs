using System.Collections;
using TMPro;
using UnityEngine;

public class TextAnimator : MonoBehaviour
{
    [SerializeField] AnimationType animationType;

    TMP_Text text;
    enum AnimationType
    {
        None,
        Blinking
    }

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
        if (animationType == AnimationType.Blinking)
        {
            StartCoroutine(Blinking());
        }

    }
    IEnumerator Blinking()
    {
        while (true)
        {
            text.enabled = !text.enabled;
            yield return new WaitForSeconds(0.5f);
        }
    }
}

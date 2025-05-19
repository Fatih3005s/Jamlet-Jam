using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Text : MonoBehaviour
{
    public TextMeshProUGUI textUI;

    public float speed = 20f;        // Karakter/saniye (normal hız)
    private float fastSpeed;         // Hızlanmış hız

    public string[] fullTexts;

    void Start()
    {
        fastSpeed = speed * 10f;     // 10 kat hızlı
        StartCoroutine(PlayTextsEffect());
    }

    IEnumerator PlayTextsEffect()
    {
        foreach (string fullText in fullTexts)
        {
            int charIndex = 0;
            float charsAccumulated = 0f;

            // Yazma aşaması
            while (charIndex < fullText.Length)
            {
                float currentSpeed = Input.GetMouseButton(0) ? fastSpeed : speed;
                charsAccumulated += currentSpeed * Time.deltaTime;

                int charsToShow = Mathf.FloorToInt(charsAccumulated);
                if (charsToShow > 0)
                {
                    charsAccumulated -= charsToShow;
                    charIndex += charsToShow;
                    if (charIndex > fullText.Length)
                        charIndex = fullText.Length;

                    textUI.text = fullText.Substring(0, charIndex);
                }

                yield return null; // Bir sonraki frame'e geç
            }

            // Yazı tamamlandıktan sonra 2 saniye bekle, tıklanırsa beklemeyi bitir
            float waitTime = 2f;
            float elapsed = 0f;
            while (elapsed < waitTime)
            {
                if (Input.GetMouseButtonDown(0))
                    break;

                elapsed += Time.deltaTime;
                yield return null;
            }

            // Silme aşaması
            charIndex = fullText.Length;
            charsAccumulated = 0f;

            while (charIndex > 0)
            {
                float currentSpeed = Input.GetMouseButton(0) ? fastSpeed : speed;
                charsAccumulated += currentSpeed * Time.deltaTime;

                int charsToRemove = Mathf.FloorToInt(charsAccumulated);
                if (charsToRemove > 0)
                {
                    charsAccumulated -= charsToRemove;
                    charIndex -= charsToRemove;
                    if (charIndex < 0)
                        charIndex = 0;

                    textUI.text = fullText.Substring(0, charIndex);
                }

                yield return null;
            }

            textUI.text = "";
            // Yazılar arasında ufak boşluk, bu sefer WaitForSeconds kullanmayalım
            float gapTime = 0.5f;
            elapsed = 0f;
            while (elapsed < gapTime)
            {
                elapsed += Time.deltaTime;
                yield return null;
            }
        }
        SceneManager.LoadScene(2);
    }
}

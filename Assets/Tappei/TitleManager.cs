using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// タイトルシーンを管理するマネージャー
/// </summary>
public class TitleManager : MonoBehaviour
{
    [SerializeField] TitleAnim _titleAnim;

    IEnumerator Start()
    {
        yield return _titleAnim.Play();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SoundManager._instance.FadeOutBGM();
        }
    }

    /// <summary>仮のシーン遷移のメソッド、GameManager側に完成したら消してそっちを使う</summary>
    public void TransionScene()
    {
        Debug.Log("仮のシーン遷移メソッドを使用します。");
        UnityEngine.SceneManagement.SceneManager.LoadScene("InGame");
        SoundManager._instance.FadeOutBGM();
    }
}

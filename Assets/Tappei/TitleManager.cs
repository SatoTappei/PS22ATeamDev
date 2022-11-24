using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �^�C�g���V�[�����Ǘ�����}�l�[�W���[
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

    /// <summary>���̃V�[���J�ڂ̃��\�b�h�AGameManager���Ɋ�������������Ă��������g��</summary>
    public void TransionScene()
    {
        Debug.Log("���̃V�[���J�ڃ��\�b�h���g�p���܂��B");
        UnityEngine.SceneManagement.SceneManager.LoadScene("InGame");
        SoundManager._instance.FadeOutBGM();
    }
}

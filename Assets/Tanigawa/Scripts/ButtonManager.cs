using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    void OnRestartButton()
    { 
        SceneManager.LoadScene("");//�^�C�g���܂���main�V�[���̖��O������
    }

    //�Q�[�����I�����邽�߂̊֐�
    void QuitButton()
    {
        Application.Quit(); //�Q�[�����I������
    }
}

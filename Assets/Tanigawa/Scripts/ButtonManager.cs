using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void OnRestartButton()
    { 
        SceneManager.LoadScene("");//�^�C�g���܂���main�V�[���̖��O������
    }

    //�Q�[�����I�����邽�߂̊֐�
    public void OnQuitButton()
    {
        Application.Quit(); //�Q�[�����I������
    }
}

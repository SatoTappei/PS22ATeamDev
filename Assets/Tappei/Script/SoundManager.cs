using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Linq;

/// <summary>
/// �Q�[�����̃T�E���h���Ǘ�����
/// </summary>
public class SoundManager : MonoBehaviour
{
    public static SoundManager _instance;

    /// <summary>�Đ�����T�E���h�̃f�[�^</summary>
    [Serializable]
    public struct SoundData
    {
        public string key;
        public AudioClip clip;
        float lastTime;
        public float volume;

        public float LastTime { get => lastTime; set => lastTime = value; }
    }

    /// <summary>�Đ�����T�E���h�̃f�[�^���C���X�y�N�^�[����ݒ肷��</summary>
    [SerializeField] SoundData[] _soundDatas;
    /// <summary>��������A�����Ė点��܂ł̊Ԋu</summary>
    [SerializeField] float _distance;
    /// <summary>���������ɉ����Đ��ł���悤�ɂ���������Ă���</summary>
    AudioSource[] _audioSources = new AudioSource[15];
    Dictionary<string, SoundData> _dic = new Dictionary<string, SoundData>();


    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // �������SoundData���w��ł���悤�ɂ���
        _soundDatas.ToList().ForEach(s => _dic.Add(s.key, s));
        // AudioSource����������t����
        for (int i = 0; i < _audioSources.Length; i++)
            _audioSources[i] = gameObject.AddComponent<AudioSource>();
    }

    void Start()
    {

    }

    void Update()
    {

    }

    /// <summary>Key���w�肵�ĉ����Đ�</summary>
    public void Play(string key)
    {
        if (_dic.TryGetValue(key, out SoundData data))
        {
            if (Time.realtimeSinceStartup - data.LastTime < _distance) return;
            data.LastTime = Time.realtimeSinceStartup;

            if (key.IndexOf("BGM_") >= 0)
                PlayBGM(data);
            else
                PlaySE(data);
        }
        else
        {
            Debug.LogWarning("�T�E���h���o�^����Ă��܂���:" + key);
        }
    }

    /// <summary>SE���Đ�����</summary>
    void PlaySE(SoundData data)
    {
        AudioSource source = GetAudioSource();
        if (source == null)
        {
            Debug.LogWarning("����点�܂���ł����BAudioSource������܂���");
            return;
        }
        source.clip = data.clip;
        source.volume = data.volume;
        source.Play();
    }

    /// <summary>BGM���Đ�����</summary>
    void PlayBGM(SoundData data)
    {
        AudioSource source = _audioSources[_audioSources.Length - 1];
        source.clip = data.clip;
        source.volume = data.volume;
        source.loop = true;
        source.Play();
    }

    /// <summary>�Đ����̌��ʉ���S�Ď~�߂�</summary>
    public void StopSEAll()
    {
        foreach (AudioSource source in _audioSources)
            if (source.isPlaying)
                source.Stop();
    }

    /// <summary>�Đ�����BGM���~�߂�</summary>
    public void StopBGM() => _audioSources[_audioSources.Length - 1].Stop();

    /// <summary>BGM���t�F�[�h�A�E�g������</summary>
    public void FadeOutBGM(float duration = 0.5f) => _audioSources[_audioSources.Length - 1].DOFade(0, duration);

    /// <summary>SE��炷���߂�AudioSource���擾</summary>
    AudioSource GetAudioSource()
    {
        // ��Ԍ���AudioSource��BGM�Đ��p�Ɏ���Ă���
        for (int i = 0; i < _audioSources.Length - 1; i++)
            if (!_audioSources[i].isPlaying)
                return _audioSources[i];

        return null;
    }
}
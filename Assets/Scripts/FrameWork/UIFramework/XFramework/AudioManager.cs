
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Frag
{
    /// <summary>
    /// 单个音频
    /// </summary>
    [System.Serializable]
    public class Sound
    {
        /// <summary>
        /// 音频片段
        /// </summary>
        public AudioClip clip;
        /// <summary>
        /// 分组
        /// </summary>
        public AudioMixerGroup group;
        /// <summary>
        /// 音频音量
        /// </summary>
        [Range(0,1)]
        public float volume = 1;
        /// <summary>
        /// 是否循环播放
        /// </summary>
        public bool loop;
        /// <summary>
        /// 是否开局播放
        /// </summary>
        public bool playOnAwake;
    }

    /// <summary>
    /// 音频管理类 简易音频管理器 存在于框架（待重写）
    /// </summary>
    public class AudioManager : MonoSingleton<AudioManager>
    {

        //我将instance换成了Instance不知道会不会报错

        /// <summary>
        /// 存储所有音频信息
        /// </summary>
        public List<Sound> sounds;
        /// <summary>
        /// 存储所有已经创建好的AudioSource组件
        /// </summary>
        private Dictionary<string, AudioSource> dictAudios= new Dictionary<string, AudioSource>();

        /// <summary>
        /// 存储所有音频信息
        /// </summary>
        /// <param name="newSounds"></param>

        // Start is called before the first frame update
        void Start()
        {
            GameObject obj = new GameObject("AudioManager");
            obj.transform.SetParent(transform);
            foreach (var sound in sounds)
            {
                AudioSource audio = obj.AddComponent<AudioSource>();
                audio.clip = sound.clip;
                audio.outputAudioMixerGroup = sound.group;
                audio.loop = sound.loop;
                audio.volume = sound.volume;
                audio.playOnAwake = sound.playOnAwake;

                if (sound.playOnAwake) 
                    audio.Play();

                dictAudios.Add(sound.clip.name, audio);
            }
        }

        /// <summary>
        /// 播放一个音频
        /// </summary>
        /// <param name="audioName">音频名称</param>
        /// <param name="wait">是否等待音频播放完毕</param>
        public static void PlayAudio(string audioName, bool wait = false)
        {
            if (!Instance.dictAudios.ContainsKey(audioName))
            {
                Debug.LogWarning($"名为{audioName}的音频不存在");
                return;
            }
            if (wait)
            {
                if (!Instance.dictAudios[audioName].isPlaying)
                    Instance.dictAudios[audioName].Play();
            }
            else
                Instance.dictAudios[audioName].Play();
        }

        /// <summary>
        /// 停止某一音频的播放
        /// </summary>
        /// <param name="audioName">音频名称</param>
        public static void StopAudio(string audioName)
        {
            if (!Instance.dictAudios.ContainsKey(audioName))
            {
                Debug.LogWarning($"名为{audioName}的音频不存在");
                return;
            }
            Instance.dictAudios[audioName].Stop();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TJ
{
    /// <summary>
    /// ��ʾ������࣬����ΪScriptableObject����
    /// </summary>
    [CreateAssetMenu]
    public class RelicTj : ScriptableObject
    {
        public string relicName;           // ��������
        public string relicDescription;    // ��������
        public Sprite relicIcon;           // ����ͼ��
    }
}

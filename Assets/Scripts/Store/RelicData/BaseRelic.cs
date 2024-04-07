using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Frag
{
    /// <summary>
    /// ��ʾ������࣬����ΪScriptableObject����
    /// </summary>
    [CreateAssetMenu(fileName = "Relic", menuName = "ScriptableObject/Relic")]
    public class BaseRelic : ScriptableObject
    {
        public string relicName;           // ��������
        public string relicDescription;    // ��������
        public Sprite relicIcon;           // ����ͼ��
    }

}
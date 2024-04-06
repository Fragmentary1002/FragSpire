using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Frag
{

    [CreateAssetMenu(fileName = "BaseBuff", menuName = "ScriptableObject/BaseBuff/BaseBuff ")]
    public class BaseBuff : ScriptableObject
    {
        protected Fighter owner;
        
        public Fighter Owner
        {
            set { owner = value; }
        }


        #region �ֶ�ʵ��

        [Header("����ʵ��")]
        [InspectorName("���ȼ�")]
        [Tooltip("buff�����ȼ����ѵ�������")]
        public int maxLevel = 1;

        [InspectorName("��������")]
        [Tooltip("buff�ǲ��������Եģ����������Գ���ʱ��")]
        public bool isPermanent = true;


        [InspectorName("��ʱ������")]
        [Tooltip("��buff����ʱ�䵽0ʱҪ���͵ĵȼ����������Ϊ0�������0��")]
        public int demotion = 1;


        [InspectorName("�Զ�ˢ��")]
        [Tooltip("��buff�����ȼ������ȼ����������ȼ��Ƿ��Զ�ˢ�³���ʱ��")]
        public bool autoRefresh = true;

        [InspectorName("��ͻ����")]
        [Tooltip("��������ͬ��Դ����ͬһ����λʩ��ͬһ��buffʱ�ĳ�ͻ����\r\n��Ϊ����")]
        public ConflictResolution conflictResolution;
        /// <summary>
        /// ��������ͬ��Դ����ͬһ����λʩ��ͬһ��buffʱ�ĳ�ͻ����
        /// </summary>
        public enum ConflictResolution
        {
            combine,//�ϲ���һ��
            append,//��ӵȼ�
            cover,//����
            independent//����

        }

        [InspectorName("buffӦ��ʱ��")]
        [Tooltip("����buffӦ��ʱ��Ĵ�����������")]
        public ApplyTime applyTime;


        [Header("���ڷ��ദ��")]
        [InspectorName("buff����")]
        [Tooltip("���������ڶ�buff���з��ദ��")]
        public BuffType buffType;
        public enum BuffType           // Buff����ö��
        {
            strength,       // ����
            Agility,        // ����
            vulnerable,     // ����
            weak,           // ����
            ritual,         // ��ʽ
            enrage          // ��ŭ
        }

        [InspectorName("ǿ��")]
        [Tooltip("���������ڶ�buff���з��ദ��")]
        public int intensity = 0;

        [InspectorName("tag")]
        [Tooltip("���������ڶ�buff���з��ദ��")]
        public string[] tags = null;

        [Header("UI")]
        public string buffName = null;

        public Sprite buffIcon;     // Buffͼ��

        [Range(0, 999)]
        public int buffValue;       // Buffֵ

        // public BuffCell buffGO;       // Buff UI����
        #endregion

        #region ����
        /// <summary>
        /// ����Ӻ�,һ��������¼�
        /// </summary>
        public virtual void AfterBeAdded()
        {
            // ʵ����Ӻ���߼�
            Tool.Log("ʵ����Ӻ���߼� BaseBuff");
            EventCenter.GetInstance().AddEventListener(this.applyTime.ToString(), Apply);
            return;
        }
        /// <summary>
        /// ����
        /// </summary>
        public virtual void OnUpdate()
        {
            // ʵ�ָ��µ��߼�
        }
        /// <summary>
        /// ���Ƴ���,һ�����Ƴ��¼�
        /// </summary>
        public virtual void AfterBeRemoved()
        {
            // ʵ���Ƴ�����߼�
            EventCenter.GetInstance().RemoveEventListener(this.applyTime.ToString(), Apply);
        }

        /// <summary>
        /// ʵ��
        /// </summary>
        public virtual void Apply()
        {
            // ʵ��Ӧ�õ��߼�
        }

        #endregion

    }
}
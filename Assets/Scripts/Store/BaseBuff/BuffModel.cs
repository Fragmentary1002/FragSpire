using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Frag
{
    [CreateAssetMenu(fileName = "BuffParent", menuName = "ScriptableObject/BuffModel/BuffParent ")]
    public class BuffModel : ScriptableObject
    {
        [Header("������Ϣ")]
        public int id=0;

        public string buffName="New Buff";

        public string description="This is New Buff";

        public Sprite icon;


        [Tooltip("buff���ȼ�")]
        public int priority;

        [Tooltip("buff���ѵ�����")]
        public int maxStack=1;

        public string[] tags;

        [Header("ʱ����Ϣ")]
        [Tooltip("buff�Ƿ�����")]
        public bool isForever=false;

        [Tooltip("buff����ʱ��")]
        public float duration=1;

        [Tooltip("buff tickʱ�� expÿ�������X�˺�")]
        public float tickTime=0;

        [Header("���·�ʽ")]
        [Tooltip("buff����ʱ��ö�� Add Replace Keep")]
        public BuffUpdateTimeEnum buffUpdateTime= BuffUpdateTimeEnum.Add;

        [Tooltip("buff ����������ʽö�� Clear Reduce")]
        public BuffRemoveStackUpdateEnum buffRemoveStackUpdate= BuffRemoveStackUpdateEnum.Reduce;




        //�ص���ʵ��
        //[Header("�����ص���")]
        //public BaseBuffModule OnCreate;
        //public BaseBuffModule OnRemove;
        //public BaseBuffModule OnTick;

        //[Header("�˺��ص���")]
        //public BaseBuffModule OnHit;
        //public BaseBuffModule OnBehurt;
        //public BaseBuffModule OnKill;
        //public BaseBuffModule OnBeKill;


        //�ص��㺯��ʵ��,���ʹ�ú������¼�

        public void OnAdd()
        {
            EventCenter.GetInstance().AddEventListener<BuffInfo>($"{CallBackPoint.OnCreate}", OnCreate);
            EventCenter.GetInstance().AddEventListener<BuffInfo>($"{CallBackPoint.OnRemoved}", OnRemoved);
            EventCenter.GetInstance().AddEventListener<BuffInfo>($"{CallBackPoint.OnTick}", OnTick);

            EventCenter.GetInstance().AddEventListener<DamageInfo>($"{CallBackPoint.OnHit}", OnHit);
            EventCenter.GetInstance().AddEventListener<DamageInfo>($"{CallBackPoint.OnBeHurt}", OnBeHurt);
            EventCenter.GetInstance().AddEventListener<DamageInfo>($"{CallBackPoint.OnKill}", OnKill);
            EventCenter.GetInstance().AddEventListener<DamageInfo>($"{CallBackPoint.OnBeKill}", OnBeKill);

        }

        public void OnRemove()
        {

            EventCenter.GetInstance().RemoveEventListener<BuffInfo>($"{CallBackPoint.OnCreate}", OnCreate);
            EventCenter.GetInstance().RemoveEventListener<BuffInfo>($"{CallBackPoint.OnRemoved}", OnRemoved);
            EventCenter.GetInstance().RemoveEventListener<BuffInfo>($"{CallBackPoint.OnTick}", OnTick);

            EventCenter.GetInstance().RemoveEventListener<DamageInfo>($"{CallBackPoint.OnHit}", OnHit);
            EventCenter.GetInstance().RemoveEventListener<DamageInfo>($"{CallBackPoint.OnBeHurt}", OnBeHurt);
            EventCenter.GetInstance().RemoveEventListener<DamageInfo>($"{CallBackPoint.OnKill}", OnKill);
            EventCenter.GetInstance().RemoveEventListener<DamageInfo>($"{CallBackPoint.OnBeKill}", OnBeKill);

        }

        //�����ص���
        public virtual void OnCreate(BuffInfo buff) { }

        public virtual void OnRemoved(BuffInfo buff) { }

        public virtual void OnTick(BuffInfo buff) { }

        //�˺��ص���
        public virtual void OnHit(DamageInfo damageInfo) { }

        public virtual void OnBeHurt(DamageInfo damageInfo) { }

        public virtual void OnKill(DamageInfo damageInfo) { }

        public virtual void OnBeKill(DamageInfo damageInfo) { }
    }

    /// <summary>
    /// ���ʹ������ģʽ�Ļ�����ʹ��ScriptableObject 
    /// �����Ҹ�ҵ����ܲ��ô���
    /// </summary>
    //public abstract class BaseBuffModule : ScriptableObject
    //{
    //    public abstract void Apply(BuffInfo buffInfo, DamageInfo damageInfo = null);
    //}


}
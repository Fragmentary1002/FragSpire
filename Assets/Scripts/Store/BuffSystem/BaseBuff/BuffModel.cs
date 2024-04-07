using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Frag
{
    [CreateAssetMenu(fileName = "BuffParent", menuName = "ScriptableObject/BuffModel/BuffParent ")]
    public class BuffModel : ScriptableObject
    {
        [Header("基本信息")]
        public int id=0;

        public string buffName="New Buff";

        public string description="This is New Buff";

        public Sprite icon;


        [Tooltip("buff优先级")]
        public int priority;

        [Tooltip("buff最大堆叠层数")]
        public int maxStack=1;

        public string[] tags;

        [Header("时间信息")]
        [Tooltip("buff是否永久")]
        public bool isForever=false;

        [Tooltip("buff持续时间")]
        public float duration=1;

        [Tooltip("buff tick时间 exp每三秒造成X伤害")]
        public float tickTime=0;

        [Header("更新方式")]
        [Tooltip("buff更新时间枚举 Add Replace Keep")]
        public BuffUpdateTimeEnum buffUpdateTime= BuffUpdateTimeEnum.Add;

        [Tooltip("buff 层数消除方式枚举 Clear Reduce")]
        public BuffRemoveStackUpdateEnum buffRemoveStackUpdate= BuffRemoveStackUpdateEnum.Reduce;




        //回调点实现
        //[Header("基础回调点")]
        //public BaseBuffModule OnCreate;
        //public BaseBuffModule OnRemove;
        //public BaseBuffModule OnTick;

        //[Header("伤害回调点")]
        //public BaseBuffModule OnHit;
        //public BaseBuffModule OnBehurt;
        //public BaseBuffModule OnKill;
        //public BaseBuffModule OnBeKill;


        //回调点函数实现,这边使用函数加事件

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

        //基础回调点
        public virtual void OnCreate(BuffInfo buff) { }

        public virtual void OnRemoved(BuffInfo buff) { }

        public virtual void OnTick(BuffInfo buff) { }

        //伤害回调点
        public virtual void OnHit(DamageInfo damageInfo) { }

        public virtual void OnBeHurt(DamageInfo damageInfo) { }

        public virtual void OnKill(DamageInfo damageInfo) { }

        public virtual void OnBeKill(DamageInfo damageInfo) { }
    }

    /// <summary>
    /// 如果使用命令模式的话可以使用ScriptableObject 
    /// 但是我该业务可能不好处理
    /// </summary>
    //public abstract class BaseBuffModule : ScriptableObject
    //{
    //    public abstract void Apply(BuffInfo buffInfo, DamageInfo damageInfo = null);
    //}


}
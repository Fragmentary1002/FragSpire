
using UnityEngine;

namespace Frag
{
    [CreateAssetMenu(fileName = "BuffAgility", menuName = "ScriptableObject/BaseBuff/BuffAgility ")]
    public class BuffAgility : BaseBuff
    {
        public override void AfterBeAdded()
        {
            // 实现添加后的逻辑
            Tool.Log($"实现添加后的逻辑 {this.GetType()}");
            EventCenter.GetInstance().AddEventListener(this.applyTime.ToString(), Apply);
            return;
        }

        public override void OnUpdate()
        {
            // 实现更新的逻辑
        }

        public override void AfterBeRemoved()
        {
            // 实现移除后的逻辑
            EventCenter.GetInstance().RemoveEventListener(this.applyTime.ToString(), Apply);
        }

        public override void Apply()
        {
            // 实现应用的逻辑
            Tool.Log($"实现应用的逻辑 {this.GetType()}");


            owner.DamageFication = new Fication(this.buffValue);
        }
    }
}
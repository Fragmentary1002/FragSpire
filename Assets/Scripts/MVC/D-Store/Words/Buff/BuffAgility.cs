
using UnityEngine;

namespace Frag
{
    [CreateAssetMenu(fileName = "BuffAgility", menuName = "ScriptableObject/BaseBuff/BuffAgility ")]
    public class BuffAgility : BaseBuff
    {
        public override void AfterBeAdded()
        {
            // ʵ����Ӻ���߼�
            Tool.Log($"ʵ����Ӻ���߼� {this.GetType()}");
            EventCenter.GetInstance().AddEventListener(this.applyTime.ToString(), Apply);
            return;
        }

        public override void OnUpdate()
        {
            // ʵ�ָ��µ��߼�
        }

        public override void AfterBeRemoved()
        {
            // ʵ���Ƴ�����߼�
            EventCenter.GetInstance().RemoveEventListener(this.applyTime.ToString(), Apply);
        }

        public override void Apply()
        {
            // ʵ��Ӧ�õ��߼�
            Tool.Log($"ʵ��Ӧ�õ��߼� {this.GetType()}");


            owner.DamageFication = new Fication(this.buffValue);
        }
    }
}
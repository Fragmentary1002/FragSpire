// ����System.Collections�����ռ䣬�������ռ�����˼�����Ļ����ӿں��࣬��IEnumerable, IEnumerator�ȡ�  
using System.Collections;

// ����System.Collections.Generic�����ռ䣬��������ռ��ṩ�˸���ļ����࣬��List<T>, Dictionary<TKey, TValue>�ȡ�  
using System.Collections.Generic;

// ����UnityEngine�����ռ䣬����Unity��Ϸ����ĺ��������ռ䣬������Unity�Ĵ󲿷�API��  
using UnityEngine;

// ����һ����ΪTJ�������ռ䣬������֯�͹�����ص����ṹ��  
namespace TJ
{
    // ����һ�������л��Ĺ�����EnemyAction�����л��������ת��Ϊ�ֽ������Ӷ��������ɵش洢��ͨ�����緢�͡�  
    [System.Serializable]
    public class EnemyAction
    {
        // ����һ��������ö��IntentType����ö�ٶ�����EnemyAction�Ĳ�ͬ��ͼ���͡�  
        public enum IntentType
        {
            // ������ͼ  
            Attack,
            // �赲��ͼ  
            Block,
            // ս��������ͼ  
            StrategicBuff,
            // ս�Լ�����ͼ  
            StrategicDebuff,
            // ����������ͼ  
            AttackDebuff
        }

        // ����һ��������IntentType���͵ı���intentType����ʾEnemyAction����ͼ���͡�  
        public IntentType intentType;

        // ����һ����������������amount���������ڱ�ʾ����������/�������ֵ��  
        public int amount;

        // ����һ����������������debuffAmount������ר�����ڱ�ʾ�������ֵ��  
        public int debuffAmount;

        // ����һ��������Buff.Type���͵ı���buffType���������ڱ�ʾ��������͡�  
        // ע�⣺Buff.Type�������������ط��������һ�����ö�����ͣ�����û�и�������Ķ��塣  
        public Buff.Type buffType;

        // ����һ����������������chance���������ڱ�ʾĳ����ͼ���������ļ��ʡ�  
        public int chance;

        // ����һ��������Sprite���͵ı���icon��Sprite��Unity�����ڱ�ʾ2Dͼ��򶯻����ࡣ  
        // ���icon����������UI����Ϸ����ʾ��EnemyAction��ͼ�ꡣ  
        public Sprite icon;
    }
}
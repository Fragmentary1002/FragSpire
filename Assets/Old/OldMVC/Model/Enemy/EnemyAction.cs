// 引入System.Collections命名空间，该命名空间包含了集合类的基础接口和类，如IEnumerable, IEnumerator等。  
using System.Collections;

// 引入System.Collections.Generic命名空间，这个命名空间提供了更多的集合类，如List<T>, Dictionary<TKey, TValue>等。  
using System.Collections.Generic;

// 引入UnityEngine命名空间，这是Unity游戏引擎的核心命名空间，包含了Unity的大部分API。  
using UnityEngine;

// 声明一个名为TJ的命名空间，用于组织和管理相关的类或结构。  
namespace TJ
{
    // 声明一个可序列化的公共类EnemyAction。序列化允许对象被转换为字节流，从而可以轻松地存储或通过网络发送。  
    [System.Serializable]
    public class EnemyAction
    {
        // 声明一个公共的枚举IntentType，该枚举定义了EnemyAction的不同意图类型。  
        public enum IntentType
        {
            // 攻击意图  
            Attack,
            // 阻挡意图  
            Block,
            // 战略增益意图  
            StrategicBuff,
            // 战略减益意图  
            StrategicDebuff,
            // 攻击减益意图  
            AttackDebuff
        }

        // 声明一个公共的IntentType类型的变量intentType，表示EnemyAction的意图类型。  
        public IntentType intentType;

        // 声明一个公共的整数变量amount，可能用于表示攻击或增益/减益的数值。  
        public int amount;

        // 声明一个公共的整数变量debuffAmount，可能专门用于表示减益的数值。  
        public int debuffAmount;

        // 声明一个公共的Buff.Type类型的变量buffType，可能用于表示增益的类型。  
        // 注意：Buff.Type可能是在其他地方定义的另一个类或枚举类型，这里没有给出具体的定义。  
        public Buff.Type buffType;

        // 声明一个公共的整数变量chance，可能用于表示某个意图或动作触发的几率。  
        public int chance;

        // 声明一个公共的Sprite类型的变量icon，Sprite是Unity中用于表示2D图像或动画的类。  
        // 这个icon可能用于在UI或游戏中显示该EnemyAction的图标。  
        public Sprite icon;
    }
}
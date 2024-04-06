using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TJ
{
    public class Enemy : MonoBehaviour
    {
        // 敌人的行动列表  
        public List<EnemyAction> enemyActions;
        // 当前回合敌人的行动列表  
        public List<EnemyAction> turns = new List<EnemyAction>();
        // 当前回合数  
        public int turnNumber;
        // 是否需要随机打乱敌人的行动  
        public bool shuffleActions;
        // 敌人的Fighter组件  
        public Fighter thisEnemy;

        // UI相关  
        [Header("UI")]
        public Image intentIcon;
        public TMP_Text intentAmount;
        public BuffUI intentUI;

        // 敌人的特殊属性  
        [Header("Specifics")]
        public int goldDrop; // 敌人掉落金币数量  
        public bool bird; // 是否是鸟类敌人  
        public bool nob; // 是否是nob类敌人  
        public bool wiggler; // 是否是wiggler类敌人  
        public GameObject wigglerBuff; // wiggler类敌人的Buff效果对象  
        public GameObject nobBuff; // nob类敌人的Buff效果对象  
        // 战斗场景管理器  
        BattleSceneManager battleSceneManager;
        // 玩家Fighter组件  
        Fighter player;
        // 敌人的Animator组件  
        Animator animator;
        // 是否处于回合中间状态  
        public bool midTurn;

        // 游戏开始时调用  
        private void Start()
        {
            // 查找并获取BattleSceneManager对象  
            battleSceneManager = FindObjectOfType<BattleSceneManager>();
            // 获取玩家的Fighter组件  
            player = battleSceneManager.player;
            // 获取当前敌人的Fighter组件  
            thisEnemy = GetComponent<Fighter>();
            // 获取当前敌人的Animator组件  
            animator = GetComponent<Animator>();

            // 如果需要打乱行动，则生成打乱后的回合列表  
            if (shuffleActions)
                GenerateTurns();
        }

        // 加载敌人的方法  
        private void LoadEnemy()
        {
            // 与Start方法类似，重新获取BattleSceneManager对象和玩家的Fighter组件  
            battleSceneManager = FindObjectOfType<BattleSceneManager>();
            player = battleSceneManager.player;
            thisEnemy = GetComponent<Fighter>();

            // 如果需要打乱行动，则生成打乱后的回合列表  
            if (shuffleActions)
                GenerateTurns();
        }

        // 执行敌人回合的函数  
        public void TakeTurn()
        {
            // 播放敌人意图淡出的动画  
            intentUI.animator.Play("IntentFade");

            // 根据敌人当前回合的意图类型进行不同的操作  
            switch (turns[turnNumber].intentType)
            {
                case EnemyAction.IntentType.Attack:
                    // 攻击玩家的协程  
                    StartCoroutine(AttackPlayer());
                    break;
                case EnemyAction.IntentType.Block:
                    // 执行阻挡动作  
                    PerformBlock();
                    // 应用增益效果的协程  
                    StartCoroutine(ApplyBuff());
                    break;
                case EnemyAction.IntentType.StrategicBuff:
                    // 对自己应用增益效果  
                    ApplyBuffToSelf(turns[turnNumber].buffType);
                    // 应用增益效果的协程  
                    StartCoroutine(ApplyBuff());
                    break;
                case EnemyAction.IntentType.StrategicDebuff:
                    // 对玩家应用减益效果  
                    ApplyDebuffToPlayer(turns[turnNumber].buffType);
                    // 应用增益效果的协程（此处可能是逻辑错误，应该有一个ApplyDebuff的协程）  
                    StartCoroutine(ApplyBuff());
                    break;
                case EnemyAction.IntentType.AttackDebuff:
                    // 对玩家应用减益效果  
                    ApplyDebuffToPlayer(turns[turnNumber].buffType);
                    // 攻击玩家的协程  
                    StartCoroutine(AttackPlayer());
                    break;
                default:
                    // 默认情况，输出调试信息  
                    Debug.Log("你怎么把这个搞砸了");
                    break;
            }
        }

        // 生成敌人回合的函数  
        public void GenerateTurns()
        {
            // 遍历敌人的所有行动  
            foreach (EnemyAction eA in enemyActions)
            {
                // 根据行动的出现几率添加相应次数的行动到回合列表中  
                for (int i = 0; i < eA.chance; i++)
                {
                    turns.Add(eA);
                }
            }
            // 打乱回合列表的顺序  
            turns.Shuffle();
        }

        // 攻击玩家的协程函数  
        private IEnumerator AttackPlayer()
        {
            // 播放攻击动画  
            animator.Play("Attack");
            if (bird)
                // 如果bird为真，则播放鸟图标的攻击动画  
                battleSceneManager.birdIcon.GetComponent<Animator>().Play("Attack");

            // 计算总伤害值，包括基础伤害值和力量增益效果  
            int totalDamage = turns[turnNumber].amount + thisEnemy.strength.buffValue;
            if (player.vulnerable.buffValue > 0)
            {
                // 如果玩家处于易伤状态，则增加伤害值  
                float a = totalDamage * 1.5f;
                // Debug.Log("伤害值从 "+totalDamage+" 增加到 "+(int)a); // 注释掉的调试信息  
                totalDamage = (int)a;
            }
            // 等待0.5秒  
            yield return new WaitForSeconds(0.5f);
            // 对玩家造成伤害  
            player.TakeDamage(totalDamage);
            // 再等待0.5秒  
            yield return new WaitForSeconds(0.5f);
            // 结束当前回合  
            WrapUpTurn();
        }
        // 定义一个协程方法，用于在一段时间后应用增益效果  
        private IEnumerator ApplyBuff()
        {
            // 等待1秒  
            yield return new WaitForSeconds(1f);
            // 结束回合处理  
            WrapUpTurn();
        }

        // 结束回合的处理方法  
        private void WrapUpTurn()
        {
            // 回合数增加  
            turnNumber++;
            // 如果回合数等于总回合数，则重置为0  
            if (turnNumber == turns.Count)
                turnNumber = 0;

            // 如果bird为真，则回合数设置为1  
            if (bird)
                turnNumber = 1;

            // 如果nob为真且回合数为0，则回合数设置为1  
            if (nob && turnNumber == 0)
                turnNumber = 1;

            // 评估回合结束时的增益效果  
            thisEnemy.EvaluateBuffsAtTurnEnd();
            // 设置midTurn为false，表示当前不是回合中  
            midTurn = false;
        }

        // 给自身应用增益效果  
        private void ApplyBuffToSelf(Buff.Type t)
        {
            // 给敌人添加指定类型和数量的增益效果  
            thisEnemy.AddBuff(t, turns[turnNumber].amount);
        }

        // 给玩家施加减益效果  
        private void ApplyDebuffToPlayer(Buff.Type t)
        {
            // 如果玩家为空，则加载敌人（可能是为了初始化玩家对象）  
            if (player == null)
                LoadEnemy();

            // 给玩家添加指定类型和数量的减益效果  
            player.AddBuff(t, turns[turnNumber].debuffAmount);
        }

        // 执行阻挡动作  
        private void PerformBlock()
        {
            // 给敌人增加指定数量的阻挡值  
            thisEnemy.AddBlock(turns[turnNumber].amount);
        }

        // 显示敌人的意图  
        public void DisplayIntent()
        {
            // 如果回合列表为空，则加载敌人  
            if (turns.Count == 0)
                LoadEnemy();

            // 设置意图图标  
            intentIcon.sprite = turns[turnNumber].icon;

            // 判断当前回合意图是否是攻击  
            if (turns[turnNumber].intentType == EnemyAction.IntentType.Attack)
            {
                // 攻击值增加敌人的力量增益效果  
                int totalDamage = turns[turnNumber].amount + thisEnemy.strength.buffValue;
                // 如果玩家有易损性增益效果，则攻击值增加50%  
                if (player.vulnerable.buffValue > 0)
                {
                    totalDamage = (int)(totalDamage * 1.5f);
                }
                // 显示总伤害值  
                intentAmount.text = totalDamage.ToString();
            }
            else
            {
                // 否则显示意图的数值  
                intentAmount.text = turns[turnNumber].amount.ToString();
            }

            // 播放意图出现的动画  
            intentUI.animator.Play("IntentSpawn");
        }

        // 敌人卷曲动作  
        public void CurlUP()
        {
            // 禁用wigglerBuff（可能是某个游戏对象或UI元素）  
            wigglerBuff.SetActive(false);
            // 给敌人增加5点的阻挡值  
            thisEnemy.AddBlock(5);
        }

        // 类的结束括号  
    }
    // 命名空间的结束括号  
}
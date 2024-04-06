using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TJ
{
    // 战斗者类，继承自 MonoBehaviour
    public class Fighter : MonoBehaviour
    {
        // 当前生命值
        public int currentHealth;
        // 最大生命值
        public int maxHealth;
        // 当前阻挡值，默认为0
        public int currentBlock = 0;
        // 战斗者生命条对象
        public FighterHealthBar fighterHealthBar;

        // 增益效果
        [Header("Buffs")]
        public Buff vulnerable;  // 脆弱
        public Buff weak;        // 虚弱
        public Buff strength;    // 强化
        public Buff ritual;      // 仪式
        public Buff enrage;      // 激怒
        public GameObject buffPrefab;  // 增益效果预制体
        public Transform buffParent;    // 增益效果的父对象
        public bool isPlayer;           // 是否为玩家
        Enemy enemy;                    // 敌人对象
        BattleSceneManager battleSceneManager;  // 战斗场景管理器
        GameManager gameManager;                // 游戏管理器
        public GameObject damageIndicator;       // 伤害指示器

        // 在对象被唤醒时调用
        private void Awake()
        {
            // 获取敌人和战斗场景管理器的引用
            enemy = GetComponent<Enemy>();
            battleSceneManager = FindObjectOfType<BattleSceneManager>();
            gameManager = FindObjectOfType<GameManager>();

            // 设置初始生命值
            currentHealth = maxHealth;
            // 设置生命条的最大值和当前值
            fighterHealthBar.healthSlider.maxValue = maxHealth;
            fighterHealthBar.DisplayHealth(currentHealth);
            // 如果是玩家，则在游戏管理器中显示当前生命值和最大生命值
            if (isPlayer)
                gameManager.DisplayHealth(currentHealth, currentHealth);
        }

        // 受到伤害的方法
        public void TakeDamage(int amount)
        {
            // 如果有阻挡值，减少伤害
            if (currentBlock > 0)
                amount = BlockDamage(amount);

            // 如果是敌人且处于最大生命值时，执行敌人的动画
            if (enemy != null && enemy.wiggler && currentHealth == maxHealth)
                enemy.CurlUP();

            // 打印造成的伤害值
            Debug.Log($"造成 {amount} 点伤害");

            // 实例化伤害指示器，并在一段时间后销毁
            DamageIndicator di = Instantiate(damageIndicator, this.transform).GetComponent<DamageIndicator>();
            di.DisplayDamage(amount);
            Destroy(di, 2f);

            // 减少当前生命值，并更新生命值UI
            currentHealth -= amount;
            UpdateHealthUI(currentHealth);

            // 如果生命值小于等于0，结束战斗
            if (currentHealth <= 0)
            {
                if (enemy != null)
                    battleSceneManager.EndFight(true);
                else
                    battleSceneManager.EndFight(false);

                Destroy(gameObject);
            }
        }

        // 更新生命值UI的方法
        public void UpdateHealthUI(int newAmount)
        {
            currentHealth = newAmount;
            // 更新生命值UI
            fighterHealthBar.DisplayHealth(newAmount);

            // 如果是玩家，则更新游戏管理器中的生命值显示
            if (isPlayer)
                gameManager.DisplayHealth(newAmount, maxHealth);
        }

        // 增加阻挡值的方法
        public void AddBlock(int amount)
        {
            // 增加阻挡值，并更新生命条的阻挡值显示
            currentBlock += amount;
            fighterHealthBar.DisplayBlock(currentBlock);
        }

        // 死亡方法
        private void Die()
        {
            // 禁用当前游戏对象
            this.gameObject.SetActive(false);
        }

        // 减少伤害的方法
        private int BlockDamage(int amount)
        {
            if (currentBlock >= amount)
            {
                // 全部阻挡
                currentBlock -= amount;
                amount = 0;
            }
            else
            {
                // 无法全部阻挡
                amount -= currentBlock;
                currentBlock = 0;
            }

            // 更新阻挡值UI
            fighterHealthBar.DisplayBlock(currentBlock);
            return amount;
        }

        // 添加增益效果的方法
        public void AddBuff(Buff.Type type, int amount)
        {
            if (type == Buff.Type.vulnerable)
            {
                if (vulnerable.buffValue <= 0)
                {
                    // 创建新的增益效果对象
                    vulnerable.buffGO = Instantiate(buffPrefab, buffParent).GetComponent<BuffUI>();
                }
                // 增加脆弱效果的值，并更新增益效果UI
                vulnerable.buffValue += amount;
                vulnerable.buffGO.DisplayBuff(vulnerable);
            }
            else if (type == Buff.Type.weak)
            {
                if (weak.buffValue <= 0)
                {
                    // 创建新的增益效果对象
                    weak.buffGO = Instantiate(buffPrefab, buffParent).GetComponent<BuffUI>();
                }
                // 增加虚弱效果的值，并更新增益效果UI
                weak.buffValue += amount;
                weak.buffGO.DisplayBuff(weak);
            }
            else if (type == Buff.Type.strength)
            {
                if (strength.buffValue <= 0)
                {
                    // 创建新的增益效果对象
                    strength.buffGO = Instantiate(buffPrefab, buffParent).GetComponent<BuffUI>();
                }
                // 增加强化效果的值，并更新增益效果UI
                strength.buffValue += amount;
                strength.buffGO.DisplayBuff(strength);
            }
            else if (type == Buff.Type.ritual)
            {
                if (ritual.buffValue <= 0)
                {
                    // 创建新的增益效果对象
                    ritual.buffGO = Instantiate(buffPrefab, buffParent).GetComponent<BuffUI>();
                }
                // 增加仪式效果的值，并更新增益效果UI
                ritual.buffValue += amount;
                ritual.buffGO.DisplayBuff(ritual);
            }
            else if (type == Buff.Type.enrage)
            {
                Debug.Log("添加激怒效果");
                if (enrage.buffValue <= 0)
                {
                    // 创建新的增益效果对象
                    enrage.buffGO = Instantiate(buffPrefab, buffParent).GetComponent<BuffUI>();
                }
                // 增加激怒效果的值，并更新增益效果UI
                enrage.buffValue += amount;
                enrage.buffGO.DisplayBuff(enrage);
            }
        }

        // 回合结束时评估增益效果的方法
        public void EvaluateBuffsAtTurnEnd()
        {
            // 如果脆弱效果大于0
            if (vulnerable.buffValue > 0)
            {
                // 减少脆弱效果的值，并更新增益效果UI
                vulnerable.buffValue -= 1;
                vulnerable.buffGO.DisplayBuff(vulnerable);

                // 如果脆弱效果值小于等于0，销毁增益效果对象
                if (vulnerable.buffValue <= 0)
                    Destroy(vulnerable.buffGO.gameObject);
            }
            // 如果虚弱效果大于0
            else if (weak.buffValue > 0)
            {
                // 减少虚弱效果的值，并更新增益效果UI
                weak.buffValue -= 1;
                weak.buffGO.DisplayBuff(weak);

                // 如果虚弱效果值小于等于0，销毁增益效果对象
                if (weak.buffValue <= 0)
                    Destroy(weak.buffGO.gameObject);
            }
            // 如果仪式效果大于0
            else if (ritual.buffValue > 0)
            {
                // 添加强化效果，值为仪式效果的值
                AddBuff(Buff.Type.strength, ritual.buffValue);
            }
        }

        // 重置增益效果的方法
        public void ResetBuffs()
        {
            // 如果脆弱效果大于0
            if (vulnerable.buffValue > 0)
            {
                // 重置脆弱效果的值，并销毁增益效果对象
                vulnerable.buffValue = 0;
                Destroy(vulnerable.buffGO.gameObject);
            }
            // 如果虚弱效果大于0
            else if (weak.buffValue > 0)
            {
                // 重置虚弱效果的值，并销毁增益效果对象
                weak.buffValue = 0;
                Destroy(weak.buffGO.gameObject);
            }
            // 如果强化效果大于0
            else if (strength.buffValue > 0)
            {
                // 重置强化效果的值，并销毁增益效果对象
                strength.buffValue = 0;
                Destroy(strength.buffGO.gameObject);
            }

            // 重置阻挡值为0，并更新生命条的阻挡值显示
            currentBlock = 0;
            fighterHealthBar.DisplayBlock(0);
        }
    }
}

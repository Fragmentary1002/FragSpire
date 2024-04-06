using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TJ
{
    /// <summary>
    /// 处理休息场景的类，用于增加玩家生命值并显示相应信息
    /// </summary>
    public class Rest : MonoBehaviour
    {
        private EndScreen endScreen;                // 结束场景
        private GameManager gameManager;            // 游戏管理器
        private PlayerStatsUI playerStatsUI;        // 玩家状态界面
        private BattleSceneManager battleSceneManager; // 战斗场景管理器
        public GameObject continueButton;          // 继续按钮

        private void Awake()
        {
            // 查找并引用场景中的对象
            endScreen = FindObjectOfType<EndScreen>();
            gameManager = FindObjectOfType<GameManager>();
            playerStatsUI = FindObjectOfType<PlayerStatsUI>();
            battleSceneManager = FindObjectOfType<BattleSceneManager>();
        }

        private void OnEnable()
        {
            // 当对象启用时，将继续按钮禁用
            continueButton.SetActive(false);
        }

        /// <summary>
        /// 处理结束场景，增加玩家生命值并更新UI
        /// </summary>
        public void HandleEndScreen()
        {
            // 增加玩家生命值
            battleSceneManager.player.currentHealth += (int)(battleSceneManager.player.currentHealth * 0.3f);
            // 如果生命值超过最大值，则设置为最大值
            if (battleSceneManager.player.currentHealth > battleSceneManager.player.maxHealth)
                battleSceneManager.player.currentHealth = battleSceneManager.player.maxHealth;

            // 更新玩家生命值UI
            battleSceneManager.player.UpdateHealthUI(battleSceneManager.player.currentHealth);

            // 显示生命值信息
            gameManager.DisplayHealth(battleSceneManager.player.currentHealth, battleSceneManager.player.maxHealth);
            // 激活继续按钮
            continueButton.SetActive(true);
        }
    }
}

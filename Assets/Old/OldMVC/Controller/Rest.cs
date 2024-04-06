using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TJ
{
    /// <summary>
    /// ������Ϣ�������࣬���������������ֵ����ʾ��Ӧ��Ϣ
    /// </summary>
    public class Rest : MonoBehaviour
    {
        private EndScreen endScreen;                // ��������
        private GameManager gameManager;            // ��Ϸ������
        private PlayerStatsUI playerStatsUI;        // ���״̬����
        private BattleSceneManager battleSceneManager; // ս������������
        public GameObject continueButton;          // ������ť

        private void Awake()
        {
            // ���Ҳ����ó����еĶ���
            endScreen = FindObjectOfType<EndScreen>();
            gameManager = FindObjectOfType<GameManager>();
            playerStatsUI = FindObjectOfType<PlayerStatsUI>();
            battleSceneManager = FindObjectOfType<BattleSceneManager>();
        }

        private void OnEnable()
        {
            // ����������ʱ����������ť����
            continueButton.SetActive(false);
        }

        /// <summary>
        /// ������������������������ֵ������UI
        /// </summary>
        public void HandleEndScreen()
        {
            // �����������ֵ
            battleSceneManager.player.currentHealth += (int)(battleSceneManager.player.currentHealth * 0.3f);
            // �������ֵ�������ֵ��������Ϊ���ֵ
            if (battleSceneManager.player.currentHealth > battleSceneManager.player.maxHealth)
                battleSceneManager.player.currentHealth = battleSceneManager.player.maxHealth;

            // �����������ֵUI
            battleSceneManager.player.UpdateHealthUI(battleSceneManager.player.currentHealth);

            // ��ʾ����ֵ��Ϣ
            gameManager.DisplayHealth(battleSceneManager.player.currentHealth, battleSceneManager.player.maxHealth);
            // ���������ť
            continueButton.SetActive(true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TJ
{
    /// <summary>
    /// 玩家状态UI管理类
    /// </summary>
    public class PlayerStatsUI : MonoBehaviour
    {
        public TMP_Text healthDisplayText;  // 显示生命值的文本对象
        public TMP_Text moneyAmountText;    // 显示金币数量的文本对象
        public TMP_Text floorText;          // 显示楼层的文本对象
        public Transform relicParent;       // 遗物父级对象
        public GameObject relicPrefab;      // 遗物预制体对象
        public GameObject playerStatsUIObject;  // 玩家状态UI对象
        GameManager gameManager;           // 游戏管理器对象

        private void Awake()
        {
            gameManager = FindObjectOfType<GameManager>();  // 获取游戏管理器对象
        }

        /// <summary>
        /// 显示玩家拥有的遗物
        /// </summary>
        public void DisplayRelics()
        {
            // 清除遗物父级对象下的所有子对象
            foreach (Transform c in relicParent)
                Destroy(c.gameObject);

            // 遍历玩家拥有的遗物列表，并实例化遗物预制体对象进行显示
            foreach (RelicTj r in gameManager.relics)
                Instantiate(relicPrefab, relicParent).GetComponent<Image>().sprite = r.relicIcon;
        }
    }
}

using QFramework;
using System.Collections;
using System.Collections.Generic;
using TJ;
using UnityEngine;

namespace Frag
{
    //战斗失败
    public class Fight_Win : FightUnit
    {
        public override void Init()
        {
            this.SendCommand(new ApplyTimeCommand(ApplyTime.BattleEnd));
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
        }




        // EndFight方法用于结束战斗，并根据战斗结果执行相应操作
        private void EndFight(bool win)
        {
            //// 如果玩家拥有名为"BurningBlood"的遗物  
            //if (gameManager.PlayerHasRelic("BurningBlood"))
            //{
            //    // 增加玩家的当前生命值，根据遗物的效果可能有所不同 
            //    player.currentHealth += 6;
            //    // 如果增加后的生命值超过了最大生命值，则将当前生命值设置为最大生命值  
            //    if (player.currentHealth > player.maxHealth)
            //        player.currentHealth = player.maxHealth;
            //    // 更新玩家的生命值UI显示，确保与当前生命值同步  
            //    player.UpdateHealthUI(player.currentHealth);
            //}
            //// 重置玩家的增益效果，通常在战斗结束后进行清理操作
            //player.ResetBuffs();
            //// 调用HandleEndScreen方法来处理战斗结束后的界面显示  
            //HandleEndScreen();

            //// 更新关卡信息，可能是为了在UI上显示当前关卡号或更新玩家的关卡进度等  
            //gameManager.UpdateFloorNumber();
            //// 更新金币数量，根据敌人掉落的金币或其他奖励调整金币数量  
            //gameManager.UpdateGoldNumber(enemies[0].goldDrop);

            //// 根据敌人属性决定是否激活鸟图标，可能是某种特殊效果或增益的标志  
            //if (enemies[0].bird)
            //    birdIcon.SetActive(false); // 关闭kaka
        }


        // 调用HandleEndScreen方法来处理战斗结束后的界面显示  
        private void HandleEndScreen()
        {
            //// 激活结束屏幕的游戏对象，包括黄金奖励、卡牌奖励按钮等
            ////gold
            //endScreen.gameObject.SetActive(true);
            //endScreen.goldReward.gameObject.SetActive(true);
            //endScreen.cardRewardButton.gameObject.SetActive(true);

            //// 设置黄金奖励的文本内容，显示获得的黄金数量  
            //endScreen.goldReward.relicName.text = enemies[0].goldDrop.ToString() + " Gold";

            //// 更新游戏管理器中的黄金数量，增加刚刚获得的黄金数量  
            //gameManager.UpdateGoldNumber(gameManager.goldAmount += enemies[0].goldDrop);

            //// 判断敌人是否为nob类型  
            ////relics
            //if (enemies[0].nob)
            //{
            //    // 如果是，则打乱奖励的遗物库  
            //    gameManager.relicLibrary.Shuffle();

            //    // 激活奖励遗物的游戏对象，并显示第一个遗物  
            //    endScreen.relicReward.gameObject.SetActive(true);
            //    endScreen.relicReward.DisplayRelic(gameManager.relicLibrary[0]);

            //    // 将获得的第一个遗物添加到玩家的遗物列表中  
            //    gameManager.relics.Add(gameManager.relicLibrary[0]);

            //    // 从遗物库中移除已获得的遗物  
            //    gameManager.relicLibrary.Remove(gameManager.relicLibrary[0]);

            //    // 更新玩家的遗物数量显示 
            //    playerStatsUI.DisplayRelics();
            //}
            //else
            //{
            //    // 如果不是nob类型，则隐藏奖励遗物的游戏对象  
            //    endScreen.relicReward.gameObject.SetActive(false);
            //}

        }
    }
}
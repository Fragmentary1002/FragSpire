using QFramework;
using System.Collections;
using System.Collections.Generic;
using TJ;
using UnityEngine;

namespace Frag
{
    //ս��ʧ��
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




        // EndFight�������ڽ���ս����������ս�����ִ����Ӧ����
        private void EndFight(bool win)
        {
            //// ������ӵ����Ϊ"BurningBlood"������  
            //if (gameManager.PlayerHasRelic("BurningBlood"))
            //{
            //    // ������ҵĵ�ǰ����ֵ�����������Ч������������ͬ 
            //    player.currentHealth += 6;
            //    // ������Ӻ������ֵ�������������ֵ���򽫵�ǰ����ֵ����Ϊ�������ֵ  
            //    if (player.currentHealth > player.maxHealth)
            //        player.currentHealth = player.maxHealth;
            //    // ������ҵ�����ֵUI��ʾ��ȷ���뵱ǰ����ֵͬ��  
            //    player.UpdateHealthUI(player.currentHealth);
            //}
            //// ������ҵ�����Ч����ͨ����ս������������������
            //player.ResetBuffs();
            //// ����HandleEndScreen����������ս��������Ľ�����ʾ  
            //HandleEndScreen();

            //// ���¹ؿ���Ϣ��������Ϊ����UI����ʾ��ǰ�ؿ��Ż������ҵĹؿ����ȵ�  
            //gameManager.UpdateFloorNumber();
            //// ���½�����������ݵ��˵���Ľ�һ��������������������  
            //gameManager.UpdateGoldNumber(enemies[0].goldDrop);

            //// ���ݵ������Ծ����Ƿ񼤻���ͼ�꣬������ĳ������Ч��������ı�־  
            //if (enemies[0].bird)
            //    birdIcon.SetActive(false); // �ر�kaka
        }


        // ����HandleEndScreen����������ս��������Ľ�����ʾ  
        private void HandleEndScreen()
        {
            //// ���������Ļ����Ϸ���󣬰����ƽ��������ƽ�����ť��
            ////gold
            //endScreen.gameObject.SetActive(true);
            //endScreen.goldReward.gameObject.SetActive(true);
            //endScreen.cardRewardButton.gameObject.SetActive(true);

            //// ���ûƽ������ı����ݣ���ʾ��õĻƽ�����  
            //endScreen.goldReward.relicName.text = enemies[0].goldDrop.ToString() + " Gold";

            //// ������Ϸ�������еĻƽ����������Ӹոջ�õĻƽ�����  
            //gameManager.UpdateGoldNumber(gameManager.goldAmount += enemies[0].goldDrop);

            //// �жϵ����Ƿ�Ϊnob����  
            ////relics
            //if (enemies[0].nob)
            //{
            //    // ����ǣ�����ҽ����������  
            //    gameManager.relicLibrary.Shuffle();

            //    // ������������Ϸ���󣬲���ʾ��һ������  
            //    endScreen.relicReward.gameObject.SetActive(true);
            //    endScreen.relicReward.DisplayRelic(gameManager.relicLibrary[0]);

            //    // ����õĵ�һ��������ӵ���ҵ������б���  
            //    gameManager.relics.Add(gameManager.relicLibrary[0]);

            //    // ����������Ƴ��ѻ�õ�����  
            //    gameManager.relicLibrary.Remove(gameManager.relicLibrary[0]);

            //    // ������ҵ�����������ʾ 
            //    playerStatsUI.DisplayRelics();
            //}
            //else
            //{
            //    // �������nob���ͣ������ؽ����������Ϸ����  
            //    endScreen.relicReward.gameObject.SetActive(false);
            //}

        }
    }
}
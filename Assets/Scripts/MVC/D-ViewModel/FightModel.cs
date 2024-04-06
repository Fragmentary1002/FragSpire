using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Frag
{
    public class FightModel : AbstractModel, ICanGetModel
    {
        //数据内容
        public int maxEneryCount { get; private set; }

        public int enegryCount { get; private set; }

        public int drawPileCount { get; private set; }

        public int discardPileCount { get; private set; }

        public int cemeteryCount { get; private set; }

        //FighterHealthBarCell


        public Player player;


        public string playerPrefabId { get; private set; }


        public int maxHealthCount { get; private set; }

        public int blockNumberCount { get; private set; }



        public Enemy enemy;

        public string enemyPrefabId { get; private set; }

        public int healthCount { get; private set; }

        public int maxHealthCountEnemy { get; private set; }
        public int blockNumberCountEnemy { get; private set; }
        public int healthCountEnemy { get; private set; }


        public BattleInfo battleInfo;




        /// <summary>
        /// 初始化
        /// </summary>
        protected override void OnInit()
        {
            LoadData();

            //playerPrefabId = PlayerActionManager.Instance.GetFighterId();

            //enemyPrefabId = EnemyActionSystem.Instance.GetFighterId();

            player = this.GetModel<Player>();

            enemy = this.GetModel<Enemy>();
            
            battleInfo = this.GetModel<BattleInfo>();

            EventCenter.GetInstance().AddEventListener("FightUpdate", UpdateData);

        }

        /// <summary>
        /// 更新
        /// </summary>
        public void UpdateData()
        {

            GetModelDataUpdate();

            UpdateInfo();
        }

        //utility
        /// <summary>
        /// 保存
        /// </summary>
        public void SaveData()
        {
            //set
            //PlayerPrefs.SetInt("enegryCount", enegryCount);
            //PlayerPrefs.SetInt("drawPileCount", drawPileCount);
            //PlayerPrefs.SetInt("discardPileCount", discardPileCount);
            //PlayerPrefs.SetInt("cemeteryCount", cemeteryCount);
            //PlayerPrefs.SetInt("blockNumberCount", blockNumberCount);
            //PlayerPrefs.SetInt("healthCount", healthCount);
            //PlayerPrefs.SetInt("maxHealthCount", maxHealthCount);

        }
        public void LoadData()
        {
            //get
            enegryCount = PlayerPrefs.GetInt("enegryCount", 3);
            drawPileCount = PlayerPrefs.GetInt("drawPileCount", 7);
            discardPileCount = PlayerPrefs.GetInt("discardPileCount", 0);
            cemeteryCount = PlayerPrefs.GetInt("cemeteryCount");
            blockNumberCount = PlayerPrefs.GetInt("blockNumberCount");
            healthCount = PlayerPrefs.GetInt("healthCount", 100);
            maxHealthCount = PlayerPrefs.GetInt("maxHealthCount", 100);
        }

        /// <summary>
        /// 通知view层更新
        /// </summary>
        public void UpdateInfo()
        {
            EventCenter.GetInstance().EventTrigger<FightModel>("FightData", this);
        }

        public void OnDestroy()
        {
            EventCenter.GetInstance().RemoveEventListener("FightUpdate", UpdateData);
        }


        /// <summary>
        /// 获取控制层数据
        /// </summary>
        private void GetModelDataUpdate()
        {

            maxEneryCount = battleInfo.enegry.max;

            enegryCount = battleInfo.enegry.cur;

            drawPileCount = battleInfo.drawPile.Count;

            discardPileCount = battleInfo.discardPile.Count;



            //player = PlayerActionManager.Instance.GetFighter();


            maxHealthCount = player.hp.max;

            healthCount = player.hp.cur;

            blockNumberCount = player.currentBlock;


            // enemy = EnemyActionSystem.Instance.GetFighter();

            maxHealthCountEnemy = enemy.hp.max;

            healthCountEnemy = enemy.hp.cur;

            blockNumberCountEnemy = enemy.currentBlock;

        }
    }

}
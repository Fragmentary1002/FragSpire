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

        public BattleInfo battleInfo;




        /// <summary>
        /// 初始化
        /// </summary>
        protected override void OnInit()
        {
            LoadData();

            //playerPrefabId = PlayerActionManager.Instance.GetFighterId();

            //enemyPrefabId = EnemyActionSystem.Instance.GetFighterId();

         
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

        }
    }

}
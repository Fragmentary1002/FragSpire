using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using XFramework;
using XFramework.Extend;
using System.Threading.Tasks;
using Unity.VisualScripting;

namespace Frag
{

    public enum CreatGoType
    {
        player,
        enemy
    }

    /// <summary>
    /// 开始面板
    /// </summary>
    public class FightPanel : BasePanel
    {
        //寻找控件，逻辑处理，数据更新
        /// <summary>
        /// 路径
        /// </summary>
        static readonly string path = "Prefabs/UI/Panel/FightPanel";

        //view
        FighterHealthBarCell healthBarPlayer;
        FighterHealthBarCell healthBarEnemy;

        Transform playerParent;
        Transform enemyParent;


        Transform buffParentPlayer;
        Transform buffParentEnemy;

        private TMP_Text energyText;// 能量值文本显示组件
        private TMP_Text drawPileText;
        private TMP_Text discardPileText;
        private TMP_Text cemeteryText; //等待开发
        private TMP_Text BannerText;

        private Image playerIcon;

        private Button endTurn;
        public Animator banner; // UI横幅动画器，用于展示胜利或失败信息。  

        //model

        /// <summary>
        /// 开始面板
        /// </summary>
        public FightPanel() : base(new UIType(path))
        {

        }

        protected override void InitEvent()
        {
            InitE();

            InitUIName();

            InitClick();

        }
        private void InitE()
        {
            EventCenter.GetInstance().AddEventListener<FightModel>("FightData", UpdateInfo);

            EventCenter.GetInstance().AddEventListener<FightModel>("FightData", InitPrefabs);

            EventCenter.GetInstance().AddEventListener("Unit", BannerOut);

        }

        private void InitUIName()
        {
            energyText = ActivePanel.GetOrAddComponentInChildren<TMP_Text>("Energy/EnergyText");
            drawPileText = ActivePanel.GetOrAddComponentInChildren<TMP_Text>("DrawPile/DrawPileText");
            discardPileText = ActivePanel.GetOrAddComponentInChildren<TMP_Text>("DiscardPile/DiscardPileText");
            BannerText = ActivePanel.GetOrAddComponentInChildren<TMP_Text>("Banner/BannerText");

            banner = ActivePanel.GetOrAddComponentInChildren<Animator>("Banner");

            endTurn = ActivePanel.GetOrAddComponentInChildren<Button>("EndTurn");
        }
        private void InitClick()
        {
            //调用Player回合的结束事件

            endTurn.GetComponent<Button>().onClick.AddListener(EndTurnClick);

        }

        private void InitPrefabs(FightModel fightModel)
        {
            playerParent = ActivePanel.GetOrAddComponentInChildren<Transform>("PlayerParent");
            enemyParent = ActivePanel.GetOrAddComponentInChildren<Transform>("EnemyParent");
            try
            {
                ResMgr.GetInstance().LoadAsync<GameObject>("Prefabs/Fighter/BasePlayer", (go) =>
                {
                    if (playerParent != null)
                    {
                        go.transform.SetParent(playerParent);
                        healthBarPlayer = go.transform.GetOrAddComponentInChildren<FighterHealthBarCell>("FighterHealthBarCell");
                        buffParentPlayer = go.transform.GetOrAddComponentInChildren<Transform>("FighterHealthBarCell/BuffParent");
                    }
                }
                );
                ResMgr.GetInstance().LoadAsync<GameObject>("Prefabs/Fighter/BaseEnemy", (go) =>
                {
                    if (enemyParent != null)
                    {
                        go.transform.SetParent(enemyParent);
                        healthBarEnemy = go.transform.GetOrAddComponentInChildren<FighterHealthBarCell>("FighterHealthBarCell");

                        buffParentEnemy = go.transform.GetOrAddComponentInChildren<Transform>("FighterHealthBarCell/BuffParent");
                    }
                }
                );
            }
            catch
            {
                return;
            }

            EventCenter.GetInstance().RemoveEventListener<FightModel>("FightData", InitPrefabs);
        }


        private void EndTurnClick()
        {
            Debug.Log("EndTurnClick");
            if (FightTurnController.Instance.fightUnit == null) return;

            FightTurnController.Instance.fightUnit.OnDestroy();

            BannerOut();
        }

        private void BannerOut()
        {
            switch (FightTurnController.Instance.fightUnit)
            {
                case Fight_PlayerTurn:
                    BannerText.text = "Player's Turn";
                    //endTurn.enabled = false;
                    break;
                case Fight_EnemyTurn:
                    BannerText.text = "Enemy's Turn";
                    //endTurn.enabled = true;
                    break;
                default:
                    break;
            }

            banner.Play("bannerOut");
        }



        public void UpdateInfo(FightModel fightModel)
        {
            if (fightModel == null)
            {
                Debug.Log("fightModel不存在");
                return;
            }

            try
            {
                //更新血条显示

                healthBarPlayer?.DisplayHealth(fightModel.healthCount, fightModel.maxHealthCount);

                healthBarEnemy?.DisplayHealth(fightModel.healthCountEnemy, fightModel.maxHealthCountEnemy);

                //更新防御显示
                healthBarPlayer?.DisplayBlock(fightModel.blockNumberCount);

                healthBarEnemy?.DisplayHealth(fightModel.healthCountEnemy, fightModel.maxHealthCountEnemy);


                ////更新能量
                energyText.text = fightModel.enegryCount.ToString();

                //更新抽牌堆
                drawPileText.text = fightModel.drawPileCount.ToString();

                //更新弃牌堆

                discardPileText.text = fightModel.discardPileCount.ToString();

                ////更新墓地
                //cemeteryText.text = fightModel.cemeteryCount.ToString();
            }
            catch
            {
                Debug.LogWarning("Model更新错误");
            }

        }


        public override void OnDestroyOrSetActive(bool isDestroy = false)
        {
            base.OnDestroyOrSetActive();
            EventCenter.GetInstance().RemoveEventListener<FightModel>("FightData", UpdateInfo);

        }

    }
}
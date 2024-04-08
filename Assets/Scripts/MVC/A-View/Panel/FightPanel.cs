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
using QFramework;

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
    public class FightPanel : BasePanel,IController
    {
        //寻找控件，逻辑处理，数据更新
        /// <summary>
        /// 路径
        /// </summary>
        static readonly string path = "Prefabs/UI/Panel/FightPanel";

        //view   
        public Transform playerParent;
        public Transform enemyParent;



        public TMP_Text energyText;// 能量值文本显示组件
        public TMP_Text drawPileText;
        public TMP_Text discardPileText;
        public TMP_Text cemeteryText; //等待开发
        public TMP_Text BannerText;


        public Button endTurn;
        public Animator banner; // UI横幅动画器，用于展示胜利或失败信息。  

        //model
        public BattleInfo battleInfo;
        /// <summary>
        /// 开始面板
        /// </summary>
        public FightPanel() : base(new UIType(path))
        {

        }

        protected override void InitEvent()
        {
            battleInfo=this.GetModel<BattleInfo>();

            InitE();

            InitUIName();

            InitClick();

        }
        private void InitE()
        {
            EventCenter.GetInstance().AddEventListener("Battle", UpdateInfo);

            EventCenter.GetInstance().AddEventListener("Battle", InitPrefabs);

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


            playerParent = ActivePanel.GetOrAddComponentInChildren<Transform>("PlayerParent");
            enemyParent = ActivePanel.GetOrAddComponentInChildren<Transform>("EnemyParent");
        }
        private void InitClick()
        {
            //调用Player回合的结束事件

            endTurn.GetComponent<Button>().onClick.AddListener(EndTurnClick);

        }

        private void InitPrefabs()
        {
         
            try
            {
                ResMgr.GetInstance().LoadAsync<GameObject>("Prefabs/Fighter/BasePlayer", (go) =>
                {
                    if (playerParent != null)
                    {
                        go.transform.SetParent(playerParent);
                        FightCardManager.Instance.player = go.GetComponent<PlayerOwner>().owner;
                    }
                }
                );
                ResMgr.GetInstance().LoadAsync<GameObject>("Prefabs/Fighter/BaseEnemy", (go) =>
                {
                    if (enemyParent != null)
                    {
                        go.transform.SetParent(enemyParent);

                    }
                }
                );
            }
            catch
            {
                return;
            }

            EventCenter.GetInstance().RemoveEventListener("Battle", InitPrefabs);

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



        public void UpdateInfo()
        {
            if (battleInfo == null)
            {
                Debug.Log("fightModel不存在");
                return;
            }

            try
            {

                ////更新能量
                energyText.text = battleInfo.enegry.cur.ToString();

                //更新抽牌堆
                drawPileText.text = battleInfo.drawPile.Count.ToString();

                //更新弃牌堆

                discardPileText.text = battleInfo.discardPile.Count.ToString();

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
            EventCenter.GetInstance().RemoveEventListener("Battle", UpdateInfo);

        }
        //指定架构
        public IArchitecture GetArchitecture()
        {
            return CounterApp.Interface;
        }
    }
}
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
    /// ��ʼ���
    /// </summary>
    public class FightPanel : BasePanel,IController
    {
        //Ѱ�ҿؼ����߼��������ݸ���
        /// <summary>
        /// ·��
        /// </summary>
        static readonly string path = "Prefabs/UI/Panel/FightPanel";

        //view   
        public Transform playerParent;
        public Transform enemyParent;



        public TMP_Text energyText;// ����ֵ�ı���ʾ���
        public TMP_Text drawPileText;
        public TMP_Text discardPileText;
        public TMP_Text cemeteryText; //�ȴ�����
        public TMP_Text BannerText;


        public Button endTurn;
        public Animator banner; // UI���������������չʾʤ����ʧ����Ϣ��  

        //model
        public BattleInfo battleInfo;
        /// <summary>
        /// ��ʼ���
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
            //����Player�غϵĽ����¼�

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
                Debug.Log("fightModel������");
                return;
            }

            try
            {

                ////��������
                energyText.text = battleInfo.enegry.cur.ToString();

                //���³��ƶ�
                drawPileText.text = battleInfo.drawPile.Count.ToString();

                //�������ƶ�

                discardPileText.text = battleInfo.discardPile.Count.ToString();

                ////����Ĺ��
                //cemeteryText.text = fightModel.cemeteryCount.ToString();
            }
            catch
            {
                Debug.LogWarning("Model���´���");
            }

        }


        public override void OnDestroyOrSetActive(bool isDestroy = false)
        {
            base.OnDestroyOrSetActive();
            EventCenter.GetInstance().RemoveEventListener("Battle", UpdateInfo);

        }
        //ָ���ܹ�
        public IArchitecture GetArchitecture()
        {
            return CounterApp.Interface;
        }
    }
}
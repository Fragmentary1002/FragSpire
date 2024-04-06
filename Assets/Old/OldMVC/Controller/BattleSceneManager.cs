using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
namespace TJ
{
    public class BattleSceneManager : MonoBehaviour
    {
        // 定义一个列表来存储卡片，这是游戏中的主要元素。  
        [Header("Cards")]
        public List<CardTj> deck;
        public List<CardTj> drawPile = new List<CardTj>(); // 抽卡堆  
        public List<CardTj> cardsInHand = new List<CardTj>();  // 手牌  
        public List<CardTj> discardPile = new List<CardTj>(); // 弃牌堆  
        public CardUI selectedCard;  // 选中的卡片UI组件  
        public List<CardUI> cardsInHandGameObjects = new List<CardUI>(); // 手牌对应的UI组件列表  

        // 定义与战斗相关的统计信息。 
        [Header("Stats")]
        public Fighter cardTarget; // 目标战斗员  
        public Fighter player;// 玩家战斗员  
        public int maxEnergy; // 最大能量值  
        public int energy;// 当前能量值  
        public int drawAmount = 5; // 抽卡数量，默认为5张。
        public Turn turn;       // 回合，表示是玩家还是敌人的回合。
        public enum Turn { Player, Enemy };  // 定义回合枚举。 

        // UI组件的引用，用于控制和显示战斗界面。  
        [Header("UI")]
        public Button endTurnButton; // 结束回合按钮  
        public TMP_Text drawPileCountText;// 抽卡堆数量文本显示组件  
        public TMP_Text discardPileCountText;// 弃牌堆数量文本显示组件  
        public TMP_Text energyText;// 能量值文本显示组件  
        public Transform topParent;  // UI顶层的父对象
        public Transform enemyParent;// 敌人对象的父对象  
        public EndScreen endScreen;// 游戏结束屏幕的组件

        // 定义敌人相关的数据。 
        [Header("Enemies")]
        public List<Enemy> enemies = new List<Enemy>(); // 所有敌人的列表  
        List<Fighter> enemyFighters = new List<Fighter>(); // 所有敌方战斗员的列表  
        public GameObject[] possibleEnemies; // 可能出现的敌人列表  
        public GameObject[] possibleElites; // 可能出现的精英敌人列表  
        bool eliteFight; // 是否是精英战斗的标志位。 
        public GameObject birdIcon;// 小鸟图标对象（可能是某种标记或特效）  
        CardActions cardActions; // 卡片动作的组件，可能包含各种卡片效果。  
        GameManager gameManager;  // 游戏管理器，用于控制游戏流程。  
        PlayerStatsUI playerStatsUI; // 玩家状态UI组件，显示玩家信息。  
        public Animator banner; // UI横幅动画器，可能是用于展示胜利或失败信息。  
        public TMP_Text turnText; // 回合文字显示组件
        public GameObject gameover;// 游戏结束的标志对象。 

        private void Awake()
        {
            gameManager = FindObjectOfType<GameManager>();// 获取游戏管理器组件。
            cardActions = GetComponent<CardActions>(); // 获取卡片动作组件。  
            playerStatsUI = FindObjectOfType<PlayerStatsUI>(); // 获取玩家状态UI组件。  
            endScreen = FindObjectOfType<EndScreen>();  // 获取游戏结束屏幕组件，但目前被注释掉了。 
        }
        // 当开始走廊战斗时调用此方法。可能走廊战斗比较简单或者敌人数量有限制。  
        public void StartHallwayFight()
        {
            BeginBattle(possibleEnemies); // 调用BeginBattle方法，传入可能的敌人列表。  
        }
        // 当开始精英战斗时调用此方法。可能精英战斗难度更高或者敌人能力更强。  
        public void StartEliteFight()
        {
            eliteFight = true;          // 设置eliteFight为true，表示是精英战斗。
            BeginBattle(possibleElites); // 调用BeginBattle方法，传入可能的精英敌人列表。  
        }

        //end
        public void BeginBattle(GameObject[] prefabsArray)
        {
            // 设置玩家回合的文本  
            turnText.text = "Player's Turn";
            // 播放开场动画  
            banner.Play("bannerOut");

            //playerIcon.SetActive(true);

            // 实例化一个随机的敌人预制体  
            GameObject newEnemy = Instantiate(prefabsArray[Random.Range(0, prefabsArray.Length)], enemyParent);
            //endScreen = FindObjectOfType<EndScreen>();

            // 如果存在结束屏幕，就将其设为非活动状态（这里也被注释掉了）
            if (endScreen != null)
                endScreen.gameObject.SetActive(false);

            // 找到所有的敌人对象  
            Enemy[] eArr = FindObjectsOfType<Enemy>();
            enemies = new List<Enemy>();

            // 弃牌区域  
            #region discard hand
            // 遍历手中的每一张卡牌，并将其置入弃牌堆  
            foreach (CardTj card in cardsInHand)
            {
                DiscardCard(card);
            }
            // 遍历手中的每一张卡UI，并将其设为非活动状态（这里也被注释掉了）  
            foreach (CardUI cardUI in cardsInHandGameObjects)
            {
                cardUI.gameObject.SetActive(false);
                //cardsInHand.Remove(cardUI.card);
            }
            #endregion // 弃牌区域结束 

            // 初始化新的弃牌堆、抽牌堆和手中的卡牌列表  
            discardPile = new List<CardTj>();
            drawPile = new List<CardTj>();
            cardsInHand = new List<CardTj>();

            // 将找到的所有敌人添加到敌人列表和对应的组件列表中  
            foreach (Enemy e in eArr) { enemies.Add(e); }
            foreach (Enemy e in eArr) { enemyFighters.Add(e.GetComponent<Fighter>()); }
            // 显示敌人的意图（可能是某种状态或能力）  
            foreach (Enemy e in enemies) e.DisplayIntent();

            // 将玩家的牌堆加入弃牌堆，并洗牌，然后抽一定数量的牌加入手中的卡牌列表中  
            discardPile.AddRange(gameManager.playerDeck);
            ShuffleCards();
            // 初始化能量值为最大值，并显示在UI上  
            DrawCards(drawAmount);
            energy = maxEnergy;
            energyText.text = energy.ToString();

            // 遗物检查区域 
            #region relic checks 

            // 如果玩家拥有名为"PreservedInsect"的遗物，并且精英敌人存在，则减少其生命值四分之一（这里也被注释掉了）  
            if (gameManager.PlayerHasRelic("PreservedInsect") && eliteFight)
                enemyFighters[0].currentHealth = (int)(enemyFighters[0].currentHealth * 0.25);

            // 如果玩家拥有名为"Anchor"的遗物，则增加玩家的格挡值10点（这里也被注释掉了）  
            if (gameManager.PlayerHasRelic("Anchor"))
                player.AddBlock(10);

            // 如果玩家拥有名为"Lantern"的遗物，则增加能量1点（这里也被注释掉了）  
            if (gameManager.PlayerHasRelic("Lantern"))
                energy += 1;

            // 如果玩家拥有名为"Marbles"的遗物，则给精英敌人添加一个弱点Buff（这里也被注释掉了）  
            if (gameManager.PlayerHasRelic("Marbles"))
                enemyFighters[0].AddBuff(Buff.Type.vulnerable, 1);

            //背包遗物，多给俩张
            if (gameManager.PlayerHasRelic("Bag"))
                DrawCards(2);
            //给玩家一个力量buff jax
            if (gameManager.PlayerHasRelic("Varja"))
                player.AddBuff(Buff.Type.strength, 1);

            #endregion
            // 检查是否存在第一个敌人的鸟属性，如果存在则激活鸟图标 
            if (enemies[0].bird)
                birdIcon.SetActive(true); // 激活鸟图标，kaka！
        }

        //end
        public void ShuffleCards()
        {
            discardPile.Shuffle(); // 洗牌操作，将弃牌堆中的卡片随机排序  
            drawPile = discardPile; // 将洗好的弃牌堆赋值给抽牌堆，准备下一次抽牌  
            discardPile = new List<CardTj>();  // 重置弃牌堆，为下一次洗牌做准备
            discardPileCountText.text = discardPile.Count.ToString(); // 更新弃牌堆的数量显示文字  
        }

        //end
        public void DrawCards(int amountToDraw)
        {
            int cardsDrawn = 0;// 初始化抽取的卡片数量为0 
            // 当需要抽取的卡片数量还未满足，并且手牌数量不超过10张时继续循环
            while (cardsDrawn < amountToDraw && cardsInHand.Count <= 10)
            {
                if (drawPile.Count < 1)  // 如果抽牌堆中没有卡片可供抽取  
                    ShuffleCards(); // 则执行洗牌操作，重新填充抽牌堆  

                cardsInHand.Add(drawPile[0]); // 将抽牌堆中的第一张卡片加入到手牌中  
                DisplayCardInHand(drawPile[0]); // 显示抽取到的卡片信息到界面上  
                drawPile.Remove(drawPile[0]);  // 从抽牌堆中移除已抽取的卡片，为下次抽取做准备  
                drawPileCountText.text = drawPile.Count.ToString(); // 更新抽牌堆的数量显示文字  
                cardsDrawn++;   // 已抽取的卡片数量加1  
            }
        }
        // 定义一个方法，用于在玩家手中展示一张卡片。 
        public void DisplayCardInHand(CardTj card)
        {
            // 从游戏对象数组中获取与手牌关联的UI组件。 
            CardUI cardUI = cardsInHandGameObjects[cardsInHand.Count - 1];
            // 加载并显示卡片信息到UI上。  
            cardUI.LoadCard(card);
            // 激活该卡片对应的UI组件，使其可见。  
            cardUI.gameObject.SetActive(true);
        }
        // 定义一个方法，用于玩家出一张卡片。
       //ing

        public void PlayCard(CardUI cardUI)
        {
            //Debug.Log("played card");
            //GoblinNob is enraged
            if (cardUI.card.cardType != CardTj.CardType.Attack && enemies[0].GetComponent<Fighter>().enrage.buffValue > 0)
                enemies[0].GetComponent<Fighter>().AddBuff(Buff.Type.strength, enemies[0].GetComponent<Fighter>().enrage.buffValue);

            // 执行卡片的动作。  
            cardActions.PerformAction(cardUI.card, cardTarget);

            // 扣除相应的能量值。
            energy -= cardUI.card.GetCardCostAmount();
            // 更新显示的能量值。  
            energyText.text = energy.ToString();
            // 在卡片位置实例化一个丢弃效果。
            Instantiate(cardUI.discardEffect, cardUI.transform.position, Quaternion.identity, topParent);
            // 清除已选择的卡片。 
            selectedCard = null;
            // 禁用该卡片的UI组件。  
            cardUI.gameObject.SetActive(false);
            // 从手牌列表中移除该卡片。
            cardsInHand.Remove(cardUI.card);
            // 调用 DiscardCard 方法将卡片放入弃牌堆。  
            DiscardCard(cardUI.card);
        }

        //ing

        // 定义一个方法，用于将一张卡片放入弃牌堆。
        public void DiscardCard(CardTj card)
        {
            // 将卡片添加到弃牌堆中。 
            discardPile.Add(card);
            // 更新弃牌堆的数量显示。
            discardPileCountText.text = discardPile.Count.ToString();
        }
        // 定义一个方法，用于切换回合
        public void ChangeTurn()
        {
            // 如果当前回合是玩家的回合。  
            if (turn == Turn.Player)
            {
                // 切换到敌人的回合。
                turn = Turn.Enemy;
                // 禁用结束回合按钮。  
                endTurnButton.enabled = false;

                // 玩家回合结束
                #region discard hand
                // 遍历所有手牌并丢弃它们。  
                foreach (CardTj card in cardsInHand)
                {
                    DiscardCard(card);
                }
                // 遍历所有与手牌关联的UI组件。  
                foreach (CardUI cardUI in cardsInHandGameObjects)
                {
                    // 如果该UI组件当前是激活状态。  
                    if (cardUI.gameObject.activeSelf)
                        // 在其位置实例化一个丢弃效果。  
                        Instantiate(cardUI.discardEffect, cardUI.transform.position, Quaternion.identity, topParent);
                    // 禁用该卡片的UI组件。  
                    cardUI.gameObject.SetActive(false);
                    // 从手牌列表中移除对应的卡片。  
                    cardsInHand.Remove(cardUI.card);
                }
                #endregion
                // 遍历所有的敌人，重置他们的阻挡值和显示阻挡的UI。  
                foreach (Enemy e in enemies)
                {
                    if (e.thisEnemy == null)
                        e.thisEnemy = e.GetComponent<Fighter>();
                    // 重置敌人的阻挡值。  
                    //reset block
                    e.thisEnemy.currentBlock = 0;
                    // 更新敌人阻挡值的UI显示。  
                    e.thisEnemy.fighterHealthBar.DisplayBlock(0);
                }
                // 评估玩家在当前回合结束时的增益效果
                player.EvaluateBuffsAtTurnEnd();
                // 开始一个新的协程来处理敌人的回合。
                StartCoroutine(HandleEnemyTurn());
            }
            else// 如果当前是敌人的回合    **
            {
                // 遍历所有的敌人，显示他们的意图。  
                foreach (Enemy e in enemies)
                {
                    // 显示敌人的意图或动作。
                    e.DisplayIntent();
                }
                // 切换回玩家的回合。
                turn = Turn.Player;
                // 重置玩家的阻挡值和显示阻挡的UI。 
                //reset block
                player.currentBlock = 0;
                player.fighterHealthBar.DisplayBlock(0);
                // 重置玩家的能量值。  
                energy = maxEnergy;
                // 更新能量值的UI显示。
                energyText.text = energy.ToString();
                // 启用结束回合按钮。  
                endTurnButton.enabled = true;
                // 启用结束回合按钮。  
                DrawCards(drawAmount);

                turnText.text = "Player's Turn";  // 显示"玩家回合"

                // 播放一个横幅动画来表示回合切换。                            
                banner.Play("bannerOut"); // 开始一个横幅动画，可能是显示"轮到玩家了"。  
            }
        }
        // HandleEnemyTurn方法用于处理敌人的回合 
        private IEnumerator HandleEnemyTurn()
        {
            // 将UI文本“turnText”设置为“Enemy's Turn”，表示现在是敌人的回合  
            turnText.text = "Enemy's Turn";
            // 播放横幅动画，通常用于表示回合切换  
            banner.Play("bannerIn");
            // 等待1.5秒，确保横幅动画播放完毕  
            yield return new WaitForSeconds(1.5f);
            // 遍历敌人列表  
            foreach (Enemy enemy in enemies)
            {
                // 设置当前敌人的midTurn属性为true，表示该敌人正在进行回合  
                enemy.midTurn = true;
                // 调用敌人的TakeTurn方法，让敌人进行回合操作  
                enemy.TakeTurn();
                // 等待直到敌人完成其回合，通过WaitForEndOfFrame使协程在帧结束时继续执行  
                while (enemy.midTurn)
                    yield return new WaitForEndOfFrame();
            }
            // 输出调试信息“Turn Over”，表示回合已切换  
            Debug.Log("Turn Over");
            // 调用ChangeTurn方法来更改当前回合的玩家  
            ChangeTurn();
        }
        
       
        // EndFight方法用于结束战斗，并根据战斗结果执行相应操作
        public void EndFight(bool win)
        {
            // 如果战斗没有胜利（win为false），则激活游戏结束的UI组件  
            if (!win)
                gameover.SetActive(true);

            // 如果玩家拥有名为"BurningBlood"的遗物  
            if (gameManager.PlayerHasRelic("BurningBlood"))
            {
                // 增加玩家的当前生命值，根据遗物的效果可能有所不同 
                player.currentHealth += 6;
                // 如果增加后的生命值超过了最大生命值，则将当前生命值设置为最大生命值  
                if (player.currentHealth > player.maxHealth)
                    player.currentHealth = player.maxHealth;
                // 更新玩家的生命值UI显示，确保与当前生命值同步  
                player.UpdateHealthUI(player.currentHealth);
            }
            // 重置玩家的增益效果，通常在战斗结束后进行清理操作
            player.ResetBuffs();
            // 调用HandleEndScreen方法来处理战斗结束后的界面显示  
            HandleEndScreen();

            // 更新关卡信息，可能是为了在UI上显示当前关卡号或更新玩家的关卡进度等  
            gameManager.UpdateFloorNumber();
            // 更新金币数量，根据敌人掉落的金币或其他奖励调整金币数量  
            gameManager.UpdateGoldNumber(enemies[0].goldDrop);

            // 根据敌人属性决定是否激活鸟图标，可能是某种特殊效果或增益的标志  
            if (enemies[0].bird)
                birdIcon.SetActive(false); // 关闭kaka
        }
        // 调用HandleEndScreen方法来处理战斗结束后的界面显示  
        
     
        public void HandleEndScreen()
        {
            // 激活结束屏幕的游戏对象，包括黄金奖励、卡牌奖励按钮等
            //gold
            endScreen.gameObject.SetActive(true);
            endScreen.goldReward.gameObject.SetActive(true);
            endScreen.cardRewardButton.gameObject.SetActive(true);

            // 设置黄金奖励的文本内容，显示获得的黄金数量  
            endScreen.goldReward.relicName.text = enemies[0].goldDrop.ToString() + " Gold";

            // 更新游戏管理器中的黄金数量，增加刚刚获得的黄金数量  
            gameManager.UpdateGoldNumber(gameManager.goldAmount += enemies[0].goldDrop);

            // 判断敌人是否为nob类型  
            //relics
            if (enemies[0].nob)
            {
                // 如果是，则打乱奖励的遗物库  
                gameManager.relicLibrary.Shuffle();

                // 激活奖励遗物的游戏对象，并显示第一个遗物  
                endScreen.relicReward.gameObject.SetActive(true);
                endScreen.relicReward.DisplayRelic(gameManager.relicLibrary[0]);

                // 将获得的第一个遗物添加到玩家的遗物列表中  
                gameManager.relics.Add(gameManager.relicLibrary[0]);

                // 从遗物库中移除已获得的遗物  
                gameManager.relicLibrary.Remove(gameManager.relicLibrary[0]);

                // 更新玩家的遗物数量显示 
                playerStatsUI.DisplayRelics();
            }
            else
            {
                // 如果不是nob类型，则隐藏奖励遗物的游戏对象  
                endScreen.relicReward.gameObject.SetActive(false);
            }

        }

    }
}

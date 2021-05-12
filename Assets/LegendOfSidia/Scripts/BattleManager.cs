using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LegendOfSidia
{
    public class BattleManager : MonoBehaviour
    {
        [System.Serializable]
        public struct BattlePlayer
        {
            public Renderer _renderer;
            public GameObject _gameObject;
        }

        public BattlePlayer battlePlayer1;
        public BattlePlayer battlePlayer2;
        public ParticleSystem attackParticles;

        public DiceTableManager diceTableManager;

        public delegate void OnBattleEnd();
        public OnBattleEnd onBattleEnd;

        private Player currentPlayer1;
        private Player currentPlayer2;

        private void OnEnable()
        {
            diceTableManager.onFinishDiceRoll += GetBattleResults;
        }

        private void OnDisable()
        {
            diceTableManager.onFinishDiceRoll -= GetBattleResults;
        }

        public void StartBattle (Player turnOwnerPlayer, Player adversaryPlayer)
        {
            // Play Music
            GameplayUIStatsContainer.Instace.blurPanel.SetActive(true);
            currentPlayer1 = turnOwnerPlayer;
            currentPlayer2 = adversaryPlayer;
            StartCoroutine(IEStartBattle(turnOwnerPlayer, adversaryPlayer));
        }

        private IEnumerator IEStartBattle(Player turnOwnerPlayer, Player adversaryPlayer)
        {
            battlePlayer1._renderer.material.color = turnOwnerPlayer.color;
            battlePlayer2._renderer.material.color = adversaryPlayer.color;

            battlePlayer1._gameObject.SetActive(true);
            yield return new WaitForSeconds(1f);
            battlePlayer2._gameObject.SetActive(true);
            diceTableManager.ResetTable();

            yield return new WaitForSeconds(2f);
            diceTableManager.CreateDices(turnOwnerPlayer.dices, turnOwnerPlayer.color);
            yield return new WaitForSeconds(1f);
            diceTableManager.CreateDices(adversaryPlayer.dices, adversaryPlayer.color);

            yield return new WaitForSeconds(1f);
            diceTableManager.RollDices();
            
        }

        public void GetBattleResults(List<List<int>> battleScores)
        {
            StartCoroutine(IEDisplayBattleResults(battleScores));
        }

        public IEnumerator IEDisplayBattleResults(List<List<int>> battleScores)
        {
            foreach (List<int> scores in battleScores)
            {
                scores.Sort();
                scores.Reverse();
            }

            int turns = 3;
            int p1Score = 0;
            int p2Score = 0;

            for (int i = 0; i < turns; i++)
            {
                int p1TurnDice = battleScores[0][i];
                int p2TurnDice = battleScores[1][i];

                Debug.Log($"Turn {i}. p1Dice: {p1TurnDice} - p2Dice: {p2TurnDice}.");

                if (p1TurnDice > p2TurnDice) p1Score++;
                else if (p1TurnDice < p2TurnDice) p2Score++;
                else p1Score++;
            }

            if (p1Score > p2Score)
            {
                currentPlayer2.health -= (currentPlayer1.attack + currentPlayer1.turnBonusAttack);
            }
            else
            {
                currentPlayer1.health -= (currentPlayer2.attack + currentPlayer2.turnBonusAttack);
            }
            if (attackParticles) attackParticles.Play();
            // play attack sfx
            
            yield return new WaitForSeconds(5f);

            if (onBattleEnd != null)
            {
                GameplayUIStatsContainer.Instace.blurPanel.SetActive(false);
                battlePlayer1._gameObject.SetActive(false);
                battlePlayer2._gameObject.SetActive(false);
                //Stop battle ost
                onBattleEnd();
                currentPlayer1.UpdateUI();
                currentPlayer2.UpdateUI();
            }
        }
    }
}
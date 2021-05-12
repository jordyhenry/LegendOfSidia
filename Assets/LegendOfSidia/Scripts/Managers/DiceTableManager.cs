using System.Collections.Generic;
using UnityEngine;

namespace LegendOfSidia
{
    public class DiceTableManager : MonoBehaviour
    {
        public GameObject dicePrefab;
        public GameObject table;
        public Vector2 throwForceMinMax;
        public LayerMask diceFacesLayer;

        private List<List<Rigidbody>> diceGroups = new List<List<Rigidbody>>();
        private bool isRolling = false;

        public delegate void OnFinishDiceRoll(List<List<int>> scores);
        public OnFinishDiceRoll onFinishDiceRoll;

        private void Update()
        {
            if (!isRolling) return;
            CountFinishedDices();
        }

        public void ResetTable()
        {
            foreach (List<Rigidbody> diceGroup in diceGroups)
            {
                foreach (Rigidbody dice in diceGroup)
                {
                    Destroy(dice.gameObject);
                }
            }
            diceGroups.Clear();
            table.SetActive(true);
        }

        public void CreateDices(int numberOfDices, Color diceColor)
        {
            List<Rigidbody> dices = new List<Rigidbody>();

            for (int i = 0; i < numberOfDices; i++)
            {
                Vector3 position = GenerateNewDicePosition();
                Vector3 rotation = new Vector3(Random.value * 360f, Random.value * 360f, Random.value * 360f);
                Rigidbody dice = Instantiate(dicePrefab, position, Quaternion.Euler(rotation), table.transform).GetComponent<Rigidbody>();

                dice.GetComponent<Renderer>().material.color = diceColor;
                dices.Add(dice);
            }

            diceGroups.Add(dices);
        }
        private Vector3 GenerateNewDicePosition ()
        {
            float x = Random.Range(-table.transform.localScale.x / 2f, table.transform.localScale.x / 2f) + table.transform.position.x;
            float y = table.transform.position.y + (table.transform.localScale.y / 2f);
            float z = Random.Range(-table.transform.localScale.z / 2f, table.transform.localScale.z / 2f) + table.transform.position.z;

            return new Vector3(x, y, z);
        }

        public void RollDices()
        {
            foreach (List<Rigidbody> group in diceGroups)
            {
                foreach (Rigidbody dice in group)
                {
                    float x = Random.Range(throwForceMinMax.x, throwForceMinMax.y);
                    float y = Random.Range(throwForceMinMax.x, throwForceMinMax.y);
                    float z = Random.Range(throwForceMinMax.x, throwForceMinMax.y);
                    dice.AddForce(new Vector3(x, y, z));
                }
            }

            isRolling = true;
        }
        private void CountFinishedDices()
        {
            int finishedGroupsCount = 0;
            foreach (List<Rigidbody> group in diceGroups)
            {
                int finishedDicesCount = 0;
                foreach (Rigidbody dice in group)
                {
                    if (dice.velocity == Vector3.zero)
                        finishedDicesCount++;
                    else if (Vector3.Distance(dice.position, table.transform.position) > 10f)
                        dice.transform.position = table.transform.position + Vector3.up;
                }

                if (finishedDicesCount >= group.Count)
                    finishedGroupsCount++;
            }

            if (finishedGroupsCount >= diceGroups.Count)
            {
                CalculateScores();
            }
        }
        private void CalculateScores()
        {
            isRolling = false;
            List<List<int>> scoreGroups = new List<List<int>>();
            foreach (List<Rigidbody> group in diceGroups)
            {
                List<int> scores = new List<int>();
                foreach (Rigidbody dice in group)
                {
                    scores.Add(GetDiceScore(dice.transform));
                }

                scoreGroups.Add(scores);
            }

            if (onFinishDiceRoll != null)
            {
                onFinishDiceRoll(scoreGroups);
                table.SetActive(false);
            }
        }
        private int GetDiceScore(Transform diceTransform)
        {
            Vector3 rayPos = diceTransform.position + Vector3.up;
            Ray ray = new Ray(rayPos, Vector3.down);
            RaycastHit hit = new RaycastHit();

            //Debug.DrawRay(rayPos, ray.direction, Color.red);
            int currentScore = 0;
            if (Physics.Raycast(ray, out hit, 1f, diceFacesLayer))
            {
                int.TryParse(hit.transform.name, out currentScore);
            }

            return currentScore;
        }
    }
}
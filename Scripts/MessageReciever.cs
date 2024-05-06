using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using System.Threading;

public class MessageReciever : MonoBehaviour
{
    public TMP_InputField playerInput;
    public TextMeshProUGUI aMSG;
    public TextMeshProUGUI playerScore;
    public TextMeshProUGUI adversaryScore;
    public TextMeshProUGUI playerCredits;
    public TextMeshProUGUI adversaryCredits;
    public GPTSender adversarySender;
    public GPTSender observerSender;
    public ScoreBarManager scorebar;
    public bool playerTurn = true;

    private bool ready = false;

    private void Start()
    {
        playerInput.interactable = false;
        adversarySender.SendToChatGPT("Create a context for the game! Explain the current tensions between our nations, and explain the game briefly to the player. The context is that we are government agencies that focus on social media warfare. 2 Paragraphs or less please");
    }

    public void RecieveObserverJudgement(string judgement)
    {
        if (!ready)
        {
            ready = true;
            playerInput.interactable = true;
            return;
        }

        Debug.Log("The observer sent this judgement: \n" + judgement);

        string[] values = judgement.Split("\n");
        string agent = values[0];

        int pscore = int.Parse(playerScore.text);
        int ascore = int.Parse(adversaryScore.text);
        int pcreds = int.Parse(playerCredits.text);
        int acreds = int.Parse(adversaryCredits.text);

        if (pcreds == 0 && acreds == 0)
        {
            if (pscore > ascore)
            {
                aMSG.text = "Player Wins!!!";
            }
            else if (ascore > pscore)
            {
                aMSG.text = "Adversary Wins!";
            }
            else
            {
                aMSG.text = "Tie Game!";
            }
            return;
        }

        if (values[1].Equals("Invalid Action"))
        {
            if (agent.Equals("Player") || agent.Equals("[Player]"))
            {
                // The player should go again
                playerInput.interactable = true;
                playerInput.text = "Invalid action, try again!";
            }
            else if (agent.Equals("Adversary") || agent.Equals("[Adversary]")
                )
            {
                adversarySender.SendToChatGPT("Invalid action, try again!");
            }
        }
        else
        {
            if (values.Length < 5)
            {
                Debug.LogError("Invalid observer response: " + judgement);
                adversarySender.SendToChatGPT("Your judgement had an invalid format, please try again. Remember the output format.");
                return;
            }

            int cost = int.Parse(values[1]);
            int newF = int.Parse(values[2]);
            int stolenF = int.Parse(values[3]);
            string message = playerInput.text;

            Debug.Log(agent + " spent " + cost.ToString() + " and gained " + newF.ToString() + " new and " + stolenF.ToString() + " stolen" + "\n(" + pscore.ToString() + ")");

            if (agent.Equals("Player") || agent.Equals("[Player]"))
            {
                AdversaryTurn();
                pcreds = pcreds - cost < 0 ? 0 : pcreds - cost;
                pscore = pscore + newF < 0 ? 0 : pscore + newF;
                
                if (stolenF > 0)
                {
                    ascore = ascore - stolenF < 0 ? 0 : ascore - stolenF;
                }
                else
                {
                    pscore = pscore + stolenF < 0 ? 0 : pscore + stolenF;
                    ascore = ascore - stolenF < 0 ? 0 : ascore - stolenF;
                }

                SetVals(pscore, pcreds, ascore, acreds);

                if (acreds > 0)
                {
                    adversarySender.SendToChatGPT("The player's action was: " + message);
                }
                else
                {
                    PlayerTurn();
                }
            }
            else if (agent.Equals("Adversary") || agent.Equals("[Adversary]"))
            {
                acreds = acreds - cost < 0 ? 0 : acreds - cost;
                ascore = ascore + newF < 0 ? 0 : ascore + newF;

                if (stolenF > 0)
                {
                    pscore = pscore - stolenF < 0 ? 0 : pscore - stolenF;
                }
                else
                {
                    ascore = ascore + stolenF < 0 ? 0 : ascore + stolenF;
                    pscore = pscore - stolenF < 0 ? 0 : pscore - stolenF;
                }

                SetVals(pscore, pcreds, ascore, acreds);

                if (pcreds <= 0)
                {
                    adversarySender.SendToChatGPT("Since the player has run out of budget, you get to go again! choose your action wisely...");
                    playerInput.text = "Since you ran out of credits, your opponent will take turns until the game ends...";
                }
                else
                {
                    PlayerTurn();
                }
            }
            else
            {
                Debug.LogError("An invalid observer response was recieved: " + judgement);
                adversarySender.SendToChatGPT("Your judgement had an invalid format, please try again. Remember the output format.");
                return;
            }
        }
    }

    void PlayerTurn()
    {
        playerTurn = true;
        playerInput.interactable = true;
        playerInput.text = "";
    }

    void AdversaryTurn()
    {
        playerTurn = false;
    }

    void SetVals(int pscore, int pcreds, int ascore, int acreds)
    {
        playerScore.text = pscore.ToString();
        adversaryScore.text = ascore.ToString();
        playerCredits.text = pcreds.ToString();
        adversaryCredits.text = acreds.ToString();
        float scoreRatio = (float)pscore / (float)(ascore + pscore); // Cast pscore to float
        scorebar.UpdateScore(scoreRatio);
    }
}

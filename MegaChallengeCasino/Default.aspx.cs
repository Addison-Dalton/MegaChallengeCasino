using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MegaChallengeCasino
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //add array of all slot machine images to ViewState
                string[] slotMachineImages = new string[] 
                { "Bar.png", "Bell.png", "Cherry.png", "Clover.png", "Diamond.png", "HorseShoe.png", "Lemon.png", "Orange.png", "Plum.png", "Seven.png", "Strawberry.png", "Watermellon.png"};
                ViewState.Add("slotMachineImages", slotMachineImages);

                double playerMoney = 100.00;
                ViewState.Add("PlayerMoney", playerMoney);

                //initially display 3 random slot images
                DisplaySlotImages(GetRandomSlotIndexValues());

                //initially display player money
                DisplayPlayerMoney();
            }
        }

        protected void BetButton_Click(object sender, EventArgs e)
        {
            

            int[] slotIndexValues = GetRandomSlotIndexValues();
            double playerBet = double.Parse(betTextBox.Text);
            double betResult = CalculateBet(slotIndexValues, playerBet);

            //check that the player made a valid bet, if true break out of method. 
            //function will alert player if invalid bet
            if (InvalidBet(playerBet))
            {
                return;
            }

            //display slots
            DisplaySlotImages(slotIndexValues);

            //display results based on player winning or losing money
            if (playerBet == betResult) //player lost money
            {
                UpdatePlayerMoney(-betResult);
                DisplayPlayerMoney();
                resultLabel.Text = String.Format("Sorry, you lost {0:C}. Better luck next time!", betResult);
            }
            else //player won money
            {
                UpdatePlayerMoney(betResult);
                DisplayPlayerMoney();
                resultLabel.Text = String.Format("You bet {0:C} and won {1:C}.", playerBet, betResult);
            }
        }

        //method checks for invalid bet by comparing player's current bet to available money
        private bool InvalidBet(double playerBet)
        {
            double playerMoney = (double)ViewState["PlayerMoney"];
            if(playerBet > playerMoney)
            {
                resultLabel.Text = String.Format("Sorry, your bet is invald. The max you can bet is {0:C}", playerMoney);
                return true;
            }
            else
            {
                return false;
            }
        }

        //gets 3 random number between 0 and length of slot image array, returns the 3 numbers as array.
        private int[] GetRandomSlotIndexValues()
        {
            Random rand = new Random();
            string[] slotMachineImages = (string[])ViewState["slotMachineImages"];
            int[] slotIndexValues = new int[3];

            for (int i = 0; i < slotIndexValues.Length; i++)
            {
                slotIndexValues[i] = rand.Next(0, slotMachineImages.Length);
            }

            return slotIndexValues;
        }

        //method first determines number of cheries, sevens, and BARS in current slot result
        //Based on this, it then calculates the player's 'winnings' (could win or lose money)
        //returns double of player's winnings, either an increased amount or original bet (meaning player lost his bet)
        private double CalculateBet(int[] slotIndexValues, double playerBet)
        {
            int numOfCherries = 0;
            int numOfSevens = 0;
            int numOfBars = 0;
            double playerMoney = (double) ViewState["PlayerMoney"];

            //loop through slots, keep count of slot variables above
            for(int i = 0; i < slotIndexValues.Length; i++)
            {
                if (slotIndexValues[i] == 0) //if BAR
                {
                    numOfBars++;
                }
                else if (slotIndexValues[i] == 2) //if cherry
                {
                    numOfCherries++;
                }
                else if (slotIndexValues[i] == 9) //if seven
                {
                    numOfSevens++;
                }
                else
                {
                    continue; //skip current iteration
                }
            }

            //calculate player winnings or loses
            if(numOfBars > 0)
            {
                return playerBet; //player lost bet money
            }
            else if (numOfSevens == 3)
            {
                return playerBet * 100;
            }
            else if(numOfCherries > 0)
            {
                return playerBet * (numOfCherries + 1); //player wins betMoney times number of cherries + 1.
            }
            else //any other combination of slots results in lose of player bet
            {
                return playerBet;
            }
        }

        
        private void DisplaySlotImages(int[] imageIndex)
        {
            string imageURL = "~/Images/";
            string[] slotMachineImages = (string[]) ViewState["slotMachineImages"];
            casinoLeftImage.ImageUrl = imageURL + slotMachineImages[imageIndex[0]];
            casinoMidImage.ImageUrl = imageURL + slotMachineImages[imageIndex[1]];
            casinoRightImage.ImageUrl = imageURL + slotMachineImages[imageIndex[2]];
        }

        //updates the ViewState for playermoney
        private void UpdatePlayerMoney(double betResult)
        {
            double playerMoney = (double)ViewState["PlayerMoney"];
            playerMoney += betResult;
            ViewState["PlayerMoney"] = playerMoney;
        }

        private void DisplayPlayerMoney()
        {
            playerMoneyLabel.Text = String.Format("Player's Money: {0:C}", (double)ViewState["PlayerMoney"]);
        }
    }
}
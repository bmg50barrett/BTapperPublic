using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace BTApper.Views
{

    public sealed partial class MissileView : Page
    {
        //Create Cluster Hit Dice Objects
        Dice dice1 = new Dice();
        Dice dice2 = new Dice();

        //Create hit location dice
        Dice locationDice1 = new Dice();
        Dice locationDice2 = new Dice();
        private int locationResult = 7;

        //Create facingID variable.
        private int facingID = 0;

        //Create RollID
        private int rollID = 0;

        //Create ClusterHitsTable object
        private ClusterHits MissileClusterHits = new ClusterHits();

        //Create Active Weapon storage
        static Weapon activeWeapon = new Weapon();

        //Create Weapon objects
        //Create filler weapon for large array
        static Weapon extra = new Weapon( 0, 0, 0);

        //SRM
        static Weapon srm2 = new Weapon(2, 2, 2, 2);
        static Weapon srm4 = new Weapon(4, 2, 3, 2);
        static Weapon srm6 = new Weapon(6, 2, 4, 2);

        //SSRM
        static Weapon ssrm2 = new Weapon(2, 2, 2, 2);
        static Weapon ssrm4 = new Weapon(4, 2, 3, 2);
        static Weapon ssrm6 = new Weapon(6, 2, 4, 2);

        //MRM
        static Weapon mrm10 = new Weapon(10, 5, 4, 1);
        static Weapon mrm20 = new Weapon(20, 5, 6, 1);
        static Weapon mrm30 = new Weapon(30, 5, 10, 1);
        static Weapon mrm40 = new Weapon(40, 5, 12, 1);

        //LRM
        static Weapon lrm5 = new Weapon(5, 5, 2, 1);
        static Weapon lrm10 = new Weapon(10, 5, 4, 1);
        static Weapon lrm15 = new Weapon(15, 5, 5, 1);
        static Weapon lrm20 = new Weapon(20, 5, 6, 1);

        //Rocket Launcher
        static Weapon rl10 = new Weapon(10, 3, 1);
        static Weapon rl15 = new Weapon(15, 4, 1);
        static Weapon rl20 = new Weapon(20, 5, 1);

        //MML
        static Weapon mml3 = new Weapon(true, 3, 2, 3, 2, 2, 1);
        static Weapon mml5 = new Weapon(true, 5, 2, 5, 3, 2, 1);
        static Weapon mml7 = new Weapon(true, 7, 2, 5, 4, 2, 1);
        static Weapon mml9 = new Weapon(true, 9, 2, 5, 5, 2, 1);

        //Array of Weapon Objects stores data about weapons in same order as MissileView
        Weapon[,] weaponArray = new Weapon[6, 4] {
            { srm2, srm4, srm6, extra },
            { ssrm2, ssrm4, ssrm6, extra },
            { mrm10, mrm20, mrm30, mrm40 },
            { lrm5, lrm10, lrm15, lrm20 },
            { rl10, rl15, rl20, extra },
            { mml3, mml5, mml7, mml9 } };

        //Create Facing Array. 0 = front, 1 = right, 2 = left, 3 = rear.
        String[,] facingArray = {
            { "1", "1", "1",},
            { "Center Torso [critical]", "Right Torso [critical]", "Left Torso [critical]"},
            { "Right Arm (Right Front Leg)", "Right Leg (Right Rear Leg)", "Left Leg (Left Rear Leg)"},
            { "Right Arm (Right Front Leg)", "Right Arm (Right Front Leg)", "Left Arm (Left Front Leg)"},
            { "Right Leg (Right Rear Leg)", "Right Arm (Right Front Leg)", "Left Arm (Left Front Leg)"},
            { "Right Torso", "Right Leg (Right Rear Leg)", "Left Leg (Left Rear Leg)"},
            { "Center Torso", "Right Torso", "Left Torso"},
            { "Left Torso", "Center Torso", "Center Torso"},
            { "Left Leg (Left Rear Leg)", "Left Torso", "Right Torso"},
            { "Left Arm (Left Front Leg)", "Left Arm (Left Front Leg)", "Right Arm (Right Front Leg)"},
            { "Left Arm (Left Front Leg)", "Left Leg (Left Rear Leg)", "Right Leg (Right Rear Leg)"},
            { "Head", "Head", "Head"}, };

        public MissileView()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Required;
            frontFaceButton.IsChecked = true;
            srm2Button.IsChecked = true;
            ammoSwitchMML.IsOn = false;
            MissileRoll2d6(dice1, dice2);
            
        }

        private void UpdateMissileScreen(String stringInput)
        {
            MissileTextBox.Text = Prepender(MissileTextBox.Text, stringInput);
        }

        private string Prepender(string str, string prepend)
        {
            str = str.Replace(Environment.NewLine, Environment.NewLine + prepend);
            str = prepend + Environment.NewLine + str + Environment.NewLine;
            return str;
        }

        private void MissileRoll2d6(Dice dice1, Dice dice2)
        {
            dice1.RollDice();
            dice2.RollDice();
        }

        private void MissileRoll_Click(object sender, RoutedEventArgs e)
        {
            //Increment RollID to track number of clicks.
            rollID++;

            //ROLL FOR CLUSTER HIT TABLE RESULT.
            MissileRoll2d6(dice1, dice2);
            int sum = dice1.GetValue() + dice2.GetValue();
            int amountRoll = MissileClusterHits.GetClusterHits(sum, activeWeapon.GetMaxShots());

            //FIGURE OUT CLUSTER SHOT SIZING.
            //====================================================================

            //Get group sizing.
            int groupSize;
            int groupMissileDamage;

            if (activeWeapon.GetMML() == true)
            {
                if (ammoSwitchMML.IsOn == true)
                {
                    groupSize = activeWeapon.GetMMLLRMShotsPerGroup();
                    groupMissileDamage = activeWeapon.GetMMLLRMdmg();
                }
                else
                {
                    groupSize = activeWeapon.GetMMLSRMShotsPerGroup();
                    groupMissileDamage = activeWeapon.GetMMLSRMdmg();
                }
            }
            else
            {
                groupSize = activeWeapon.GetGrouping();
                groupMissileDamage = activeWeapon.GetDamage();
            }
             
            //Number of whole groups
            int groupNumber = amountRoll / groupSize;
                        
            //Remainder after groups
            int finalGroup = amountRoll % groupSize;

            //====================================================================


            UpdateMissileScreen("==========");

            int groupIndex;
            if (groupNumber == 1 && finalGroup == 0)
            {
                locationDice1.RollDice();
                locationDice2.RollDice();
                locationResult = locationDice1.GetValue() + locationDice2.GetValue();
                UpdateMissileScreen("  " + groupSize + " missiles hit " + facingArray[locationResult - 1, facingID] + " for " + (groupMissileDamage * groupSize) + " damage!");
            } else if (groupNumber == 0 && finalGroup != 0)
            {
                locationDice1.RollDice();
                locationDice2.RollDice();
                locationResult = locationDice1.GetValue() + locationDice2.GetValue();
                UpdateMissileScreen("  " + finalGroup + " missiles hit " + facingArray[locationResult - 1, facingID] + " for " + (groupMissileDamage * finalGroup) + " damage!");
            }
            else
            {
                for (groupIndex = 1; groupIndex <= (groupNumber); groupIndex++)
                {
                    locationDice1.RollDice();
                    locationDice2.RollDice();
                    locationResult = locationDice1.GetValue() + locationDice2.GetValue();
                    UpdateMissileScreen("  " + "Group #" + groupIndex + "  -  " + groupSize + " missiles hit " + facingArray[locationResult - 1, facingID] + " for " + (groupMissileDamage * groupSize) + " damage!");
                }

                //Print out final group damage result.
                if (finalGroup != 0)
                {
                    locationDice1.RollDice();
                    locationDice2.RollDice();
                    locationResult = locationDice1.GetValue() + locationDice2.GetValue();
                    UpdateMissileScreen("  " + "Group #" + groupIndex + "  -  " + finalGroup + " missiles hit " + facingArray[locationResult - 1, facingID] + " for " + (groupMissileDamage * finalGroup) + " damage!");
                }
            }
            
            UpdateMissileScreen("Roll# " + rollID + " - " + "Hit " + amountRoll + " missiles out of a possible " + activeWeapon.GetMaxShots() + "!");
        }

        //Reduces repeated code in button actions. Every button does the same four methods.
        private void ButtonInternalUpdate(int first, int second)
        {
            activeWeapon = weaponArray[first, second];
            if (activeWeapon.GetMML() == true)
            {
                if (ammoSwitchMML.IsOn == true)
                {
                    MissileDamage.Text = activeWeapon.GetMMLLRMdmg().ToString();
                } else if (ammoSwitchMML.IsOn == false)
                {
                    MissileDamage.Text = activeWeapon.GetMMLSRMdmg().ToString();
                }
            } else
            {
                MissileDamage.Text = activeWeapon.GetDamage().ToString();
            }
            
            MissileHeat.Text = activeWeapon.GetHeat().ToString();
            MissileShots.Text = activeWeapon.GetMaxShots().ToString();
            SpecialNotes.Text = activeWeapon.GetNote();

        }

        //Weapon Button Actions
        private void srm2Button_Checked(object sender, RoutedEventArgs e)
        {
            ButtonInternalUpdate(0, 0);
        }

        private void srm4Button_Checked(object sender, RoutedEventArgs e)
        {
            ButtonInternalUpdate(0, 1);
        }

        private void srm6Button_Checked(object sender, RoutedEventArgs e)
        {
            ButtonInternalUpdate(0, 2);
        }

        private void ssrm2Button_Checked(object sender, RoutedEventArgs e)
        {
            ButtonInternalUpdate(1, 0);
        }

        private void ssrm4Button_Checked(object sender, RoutedEventArgs e)
        {
            ButtonInternalUpdate(1, 1);
        }

        private void ssrm6Button_Checked(object sender, RoutedEventArgs e)
        {
            ButtonInternalUpdate(1, 2);
        }
       
        private void MRM10Button_Checked(object sender, RoutedEventArgs e)
        {
            ButtonInternalUpdate(2, 0);
        }

        private void MRM20Button_Checked(object sender, RoutedEventArgs e)
        {
            ButtonInternalUpdate(2, 1);
        }

        private void MRM30Button_Checked(object sender, RoutedEventArgs e)
        {
            ButtonInternalUpdate(2, 2);
        }

        private void MRM40Button_Checked(object sender, RoutedEventArgs e)
        {
            ButtonInternalUpdate(2, 3);
        }

        private void lrm5Button_Checked(object sender, RoutedEventArgs e)
        {
            ButtonInternalUpdate(3, 0);
        }

        private void lrm10Button_Checked(object sender, RoutedEventArgs e)
        {
            ButtonInternalUpdate(3, 1);
        }

        private void lrm15Button_Checked(object sender, RoutedEventArgs e)
        {
            ButtonInternalUpdate(3, 2);
        }

        private void lrm20Button_Checked(object sender, RoutedEventArgs e)
        {
            ButtonInternalUpdate(3, 3);
        }

        private void RL10Button_Checked(object sender, RoutedEventArgs e)
        {
            ButtonInternalUpdate(4, 0);
        }

        private void RL15Button_Checked(object sender, RoutedEventArgs e)
        {
            ButtonInternalUpdate(4, 1);
        }

        private void RL20Button_Checked(object sender, RoutedEventArgs e)
        {
            ButtonInternalUpdate(4, 2);
        }
        private void MML3Button_Checked(object sender, RoutedEventArgs e)
        {
            ButtonInternalUpdate(5, 0);
        }

        private void MML5Button_Checked(object sender, RoutedEventArgs e)
        {
            ButtonInternalUpdate(5, 1);
        }

        private void MML7Button_Checked(object sender, RoutedEventArgs e)
        {
            ButtonInternalUpdate(5, 2);
        }

        private void MML9Button_Checked(object sender, RoutedEventArgs e)
        {
            ButtonInternalUpdate(5, 3);
        }

        //MML Ammo Type Toggle Switch
        private void ammoSwitchMML_Toggled(object sender, RoutedEventArgs e)
        {
            if (MML3Button?.IsChecked == true || MML5Button?.IsChecked == true || MML7Button?.IsChecked == true || MML9Button?.IsChecked == true)
            {
                if (MML3Button.IsChecked == true)
                {
                    ButtonInternalUpdate(5, 0);
                }
                else if (MML5Button.IsChecked == true)
                {
                    ButtonInternalUpdate(5, 1);
                }
                else if (MML7Button.IsChecked == true)
                {
                    ButtonInternalUpdate(5, 2);
                }
                else
                {
                    ButtonInternalUpdate(5, 3);
                }
            }
        }

        //Facing Button Actions
        private void frontFaceButton_Checked(object sender, RoutedEventArgs e)
        {
            facingID = 0;
        }

        private void rightFaceButton_Checked(object sender, RoutedEventArgs e)
        {
            facingID = 1;
        }

        private void leftFaceButton_Checked(object sender, RoutedEventArgs e)
        {
            facingID = 2;
        }

        //Rear facing has the same location roll as front facing.
        private void rearFaceButton_Checked(object sender, RoutedEventArgs e)
        {
            facingID = 0;
        }

    }
}

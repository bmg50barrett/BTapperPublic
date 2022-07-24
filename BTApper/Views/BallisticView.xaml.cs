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
using Windows.UI.Xaml.Navigation;

namespace BTApper.Views
{

    public sealed partial class BallisticView : Page
    {
        //Create Dice Objects
        Dice dice1 = new Dice();
        Dice dice2 = new Dice();

        //Create facingID variable.
        private int facingID = 0;

        //Create ClusterHitsTable object
        private ClusterHits BallisticClusterHits = new ClusterHits();

        //Create flag variables.
        private bool clusterRoll = false;
        private bool multishotRoll = false;

        //Create Weapon objects
        //Weapon object (Name, shots, heat, damage, cluster?, multishot?, notes)
        //Create filler weapon for large array
        static Weapon extra = new Weapon(0, 0, 0);

        //Basic AC
        static Weapon ac2 = new Weapon(1, 1, 2);
        static Weapon ac5 = new Weapon(1, 1, 5);
        static Weapon ac10 = new Weapon(1, 3, 10);
        static Weapon ac20 = new Weapon(1, 7, 20);

        //Ultra AC
        static Weapon uac2 = new Weapon(2, 1, 2, true, true);
        static Weapon uac5 = new Weapon(2, 1, 5, true, true);
        static Weapon uac10 = new Weapon(2, 4, 10, true, true);
        static Weapon uac20 = new Weapon(2, 8, 20, true, true);

        //Light and Rotary
        static Weapon lac2 = new Weapon(1, 1, 2);
        static Weapon lac5 = new Weapon(1, 1, 5);
        static Weapon rac2 = new Weapon(6, 1, 2, true, true);
        static Weapon rac5 = new Weapon(6, 1, 5, true, true);

        //LBX
        static Weapon lb2 = new Weapon(1, 1, 2, true, false);
        static Weapon lb5 = new Weapon(1, 1, 5, true, false);
        static Weapon lb10 = new Weapon(1, 2, 10, true, false);
        static Weapon lb20 = new Weapon(1, 6, 20, true, false);

        //Gauss
        static Weapon lGauss = new Weapon(1, 1, 8);
        static Weapon Gauss = new Weapon(1, 1, 15);
        static Weapon hGauss = new Weapon( 1, 2, 25, false, false,"*Weapons does 25/20/10 damage at Short/Medium/Long range.");

        //Other
        static Weapon lGun = new Weapon( 1, 0, 1);
        static Weapon mGun = new Weapon( 1, 0, 2);
        static Weapon hGun = new Weapon( 1, 0, 3);
        static Weapon nailGun = new Weapon( 1, 0, 0, false, false, "*Weapon does no damage to armor.");

        //Array of Weapon Objects stores data about weapons in same order as BallisticView
        Weapon[,] weaponArray = new Weapon[6, 4] {
            { ac2, ac5, ac10, ac20 }, 
            { uac2, uac5, uac10, uac20 }, 
            { lac2, lac5, rac2, rac5 }, 
            { lb2, lb5, lb10, lb20 },
            { lGauss, Gauss, hGauss, extra },
            { lGun, mGun, hGun, nailGun } };

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

        public BallisticView()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Required;
            frontFaceButton.IsChecked = true;
            ac2Button.IsChecked = true;
            ShotOne.IsChecked = true;
            BallisticRoll2d6(dice1, dice2);
        }
        private void UpdateBallisticScreen(String s)
        {
            //Single Element implemenation of text box.
            BallisticTextBox.Text = Prepender(BallisticTextBox.Text, s);
        }

        private string Prepender(string str, string prepend)
        {
            str = str.Replace(Environment.NewLine, Environment.NewLine + prepend);
            str = prepend + Environment.NewLine + str + Environment.NewLine;
            return str;
        }

        private void BallisticRoll2d6(Dice dice1, Dice dice2)
        {
            dice1.RollDice();
            dice2.RollDice();
            ballisticDice1block.Text = dice1.GetValue().ToString();
            ballisticDice2block.Text = dice2.GetValue().ToString();
        }

        private void BallisticRoll_Click(object sender, RoutedEventArgs e)
        {
            BallisticRoll2d6(dice1, dice2);
            int sum = dice1.GetValue() + dice2.GetValue();

            if (clusterRoll == true && multishotRoll == true && Int16.Parse(BallisticShots.Text) > 1)
            {
                UpdateBallisticScreen("==========");
                int amountRoll = BallisticClusterHits.GetClusterHits(sum, Int16.Parse(BallisticShots.Text));
                UpdateBallisticScreen("Multi Shot Cluster Weapon! Hits: " + amountRoll + " out of " + BallisticShots.Text + "!");
                for (int i = 1; i <= amountRoll; i++)
                {
                    UpdateBallisticScreen("Hit #" + i + "! You hit " + facingArray[sum - 1, facingID] + " for " + BallisticDamage.Text + " damage!");
                }
                

            } else if (clusterRoll == true && multishotRoll == true && Int16.Parse(BallisticShots.Text) == 1)
            {
                UpdateBallisticScreen("==========");
                UpdateBallisticScreen("You hit " + facingArray[sum - 1, facingID] + " for " + BallisticDamage.Text + " damage!");
                

            } else if (clusterRoll == true && Int16.Parse(BallisticShots.Text) == 1)
            {
                int amountRoll = BallisticClusterHits.GetClusterHits(sum, Int16.Parse(BallisticDamage.Text));
                UpdateBallisticScreen("==========");
                UpdateBallisticScreen("Single Shot Cluster Weapon! Hits: " + amountRoll + "!");
                

            } else
            {
                UpdateBallisticScreen("==========");
                UpdateBallisticScreen("You hit " + facingArray[sum - 1, facingID] + " for " + BallisticDamage.Text + " damage!");
                

            }
            
        }

        //Reduces repeated code in button actions. Every button does the same four methods.
        private void ButtonInternalUpdate(int first, int second)
        {
            BallisticDamage.Text = weaponArray[first, second].GetDamage().ToString();
            BallisticHeat.Text = weaponArray[first, second].GetHeat().ToString();
            SpecialNotes.Text = weaponArray[first, second].GetNote();
            clusterRoll = weaponArray[first, second].GetCluster();
            multishotRoll = weaponArray[first, second].GetMulti();
            foreach(ToggleButton item in ShotPicker.Children)
            {

                if ( Int16.Parse(item.Content.ToString()) <= weaponArray[first,second].GetMaxShots())
                {

                    item.IsEnabled = true;

                } else
                {
                    
                    if (item.IsChecked == true)
                    {

                        ShotOne.IsChecked = true;

                    }

                    item.IsEnabled = false;

                }
            }
        }

        //Weapon Button Actions
        private void ac2Button_Checked(object sender, RoutedEventArgs e)
        {
            ButtonInternalUpdate(0, 0);
        }

        private void ac5Button_Checked(object sender, RoutedEventArgs e)
        {
            ButtonInternalUpdate(0, 1);
        }

        private void ac10Button_Checked(object sender, RoutedEventArgs e)
        {
            ButtonInternalUpdate(0, 2);
        }

        private void ac20Button_Checked(object sender, RoutedEventArgs e)
        {
            ButtonInternalUpdate(0, 3);
        }

        private void uac2Button_Checked(object sender, RoutedEventArgs e)
        {
            ButtonInternalUpdate(1, 0);
        }

        private void uac5Button_Checked(object sender, RoutedEventArgs e)
        {
            ButtonInternalUpdate(1, 1);
        }

        private void uac10Button_Checked(object sender, RoutedEventArgs e)
        {
            ButtonInternalUpdate(1, 2);
        }

        private void uac20Button_Checked(object sender, RoutedEventArgs e)
        {
            ButtonInternalUpdate(1, 3);
        }

        private void lightAC2Button_Checked(object sender, RoutedEventArgs e)
        {
            ButtonInternalUpdate(2, 0);
        }

        private void lightAC5Button_Checked(object sender, RoutedEventArgs e)
        {
            ButtonInternalUpdate(2, 1);
        }

        private void rotaryAC2Button_Checked(object sender, RoutedEventArgs e)
        {
            ButtonInternalUpdate(2, 2);
        }

        private void rotaryAC5Button_Checked(object sender, RoutedEventArgs e)
        {
            ButtonInternalUpdate(2, 3);
        }
        private void lb2Button_Checked(object sender, RoutedEventArgs e)
        {
            ButtonInternalUpdate(3, 0);
        }

        private void lb5Button_Checked(object sender, RoutedEventArgs e)
        {
            ButtonInternalUpdate(3, 1);
        }

        private void lb10Button_Checked(object sender, RoutedEventArgs e)
        {
            ButtonInternalUpdate(3, 2);
        }

        private void lb20Button_Checked(object sender, RoutedEventArgs e)
        {
            ButtonInternalUpdate(3, 3);
        }

        private void lightGaussButton_Checked(object sender, RoutedEventArgs e)
        {
            ButtonInternalUpdate(4, 0);
        }

        private void GaussButton_Checked(object sender, RoutedEventArgs e)
        {
            ButtonInternalUpdate(4, 1);
        }

        private void heavyGaussButton_Checked(object sender, RoutedEventArgs e)
        {
            ButtonInternalUpdate(4, 2);
        }

        private void lgun_Checked(object sender, RoutedEventArgs e)
        {
            ButtonInternalUpdate(5, 0);
        }

        private void gun_Checked(object sender, RoutedEventArgs e)
        {
            ButtonInternalUpdate(5, 1);
        }

        private void hgun_Checked(object sender, RoutedEventArgs e)
        {
            ButtonInternalUpdate(5, 2);
        }

        private void ngun_Checked(object sender, RoutedEventArgs e)
        {
            ButtonInternalUpdate(5, 3);
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

        private void ShotOne_Checked(object sender, RoutedEventArgs e)
        {
            ShotTwo.IsChecked = false;
            ShotThree.IsChecked = false;
            ShotFour.IsChecked = false;
            ShotFive.IsChecked = false;
            ShotSix.IsChecked = false;
            BallisticShots.Text = "1";
        }

        private void ShotTwo_Checked(object sender, RoutedEventArgs e)
        {
            ShotOne.IsChecked = false;
            ShotThree.IsChecked = false;
            ShotFour.IsChecked = false;
            ShotFive.IsChecked = false;
            ShotSix.IsChecked = false;
            BallisticShots.Text = "2";
        }

        private void ShotThree_Checked(object sender, RoutedEventArgs e)
        {
            ShotOne.IsChecked = false;
            ShotTwo.IsChecked = false;
            ShotFour.IsChecked = false;
            ShotFive.IsChecked = false;
            ShotSix.IsChecked = false;
            BallisticShots.Text = "3";
        }

        private void ShotFour_Checked(object sender, RoutedEventArgs e)
        {
            ShotOne.IsChecked = false;
            ShotTwo.IsChecked = false;
            ShotThree.IsChecked = false;
            ShotFive.IsChecked = false;
            ShotSix.IsChecked = false;
            BallisticShots.Text = "4";

        }

        private void ShotFive_Checked(object sender, RoutedEventArgs e)
        {
            ShotOne.IsChecked = false;
            ShotTwo.IsChecked = false;
            ShotThree.IsChecked = false;
            ShotFour.IsChecked = false;
            ShotSix.IsChecked = false;
            BallisticShots.Text = "5";
        }

        private void ShotSix_Checked(object sender, RoutedEventArgs e)
        {
            ShotOne.IsChecked = false;
            ShotTwo.IsChecked = false;
            ShotThree.IsChecked = false;
            ShotFour.IsChecked = false;
            ShotFive.IsChecked = false;
            BallisticShots.Text = "6";
        }
    }
}
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

    public sealed partial class MissileView : Page
    {
        //Create Dice Objects
        Dice dice1 = new Dice();
        Dice dice2 = new Dice();

        //Create facingID variable.
        private int facingID = 0;

        //Create ClusterHitsTable object
        private ClusterHits MissileClusterHits = new ClusterHits();

        //Create flag variables.
        private bool clusterRoll = false;
        private bool multishotRoll = false;

        //Create Weapon objects
        //Weapon object (Name, shots, heat, damage, cluster?, multishot?, notes)
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

        //Other
        static Weapon mml3 = new Weapon(0, 0, 0);
        static Weapon mml5 = new Weapon(0, 0, 0);
        static Weapon mml7 = new Weapon(0, 0, 0);
        static Weapon mml9 = new Weapon(0, 0, 0);

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
            ac2Button.IsChecked = true;
            ShotOne.IsChecked = true;
            MissileRoll2d6(dice1, dice2);
        }

        private void UpdateMissileScreen(String s)
        {
            //Single Element implemenation of text box.
            MissileTextBox.Text = Prepender(MissileTextBox.Text, s);
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
            missileDice1block.Text = dice1.GetValue().ToString();
            missileDice2block.Text = dice2.GetValue().ToString();
        }

        private void MissileRoll_Click(object sender, RoutedEventArgs e)
        {
            MissileRoll2d6(dice1, dice2);
            int sum = dice1.GetValue() + dice2.GetValue();

            if (clusterRoll == true && multishotRoll == true && Int16.Parse(MissileShots.Text) > 1)
            {
                UpdateMissileScreen("==========");
                int amountRoll = MissileClusterHits.GetClusterHits(sum, Int16.Parse(MissileShots.Text));
                UpdateMissileScreen("Multi Shot Cluster Weapon! Hits: " + amountRoll + " out of " + MissileShots.Text + "!");
                for (int i = 1; i <= amountRoll; i++)
                {
                    UpdateMissileScreen("Hit #" + i + "! You hit " + facingArray[sum - 1, facingID] + " for " + MissileDamage.Text + " damage!");
                }


            }
            else if (clusterRoll == true && multishotRoll == true && Int16.Parse(MissileShots.Text) == 1)
            {
                UpdateMissileScreen("==========");
                UpdateMissileScreen("You hit " + facingArray[sum - 1, facingID] + " for " + MissileDamage.Text + " damage!");


            }
            else if (clusterRoll == true && Int16.Parse(MissileShots.Text) == 1)
            {
                int amountRoll = MissileClusterHits.GetClusterHits(sum, Int16.Parse(MissileDamage.Text));
                UpdateMissileScreen("==========");
                UpdateMissileScreen("Single Shot Cluster Weapon! Hits: " + amountRoll + "!");


            }
            else
            {
                UpdateMissileScreen("==========");
                UpdateMissileScreen("You hit " + facingArray[sum - 1, facingID] + " for " + MissileDamage.Text + " damage!");


            }

        }

        //Reduces repeated code in button actions. Every button does the same four methods.
        private void ButtonInternalUpdate(int first, int second)
        {
            MissileDamage.Text = weaponArray[first, second].GetDamage().ToString();
            MissileHeat.Text = weaponArray[first, second].GetHeat().ToString();
            SpecialNotes.Text = weaponArray[first, second].GetNote();
            clusterRoll = weaponArray[first, second].GetCluster();
            multishotRoll = weaponArray[first, second].GetMulti();
            foreach (ToggleButton item in ShotPicker.Children)
            {

                if (Int16.Parse(item.Content.ToString()) <= weaponArray[first, second].GetMaxShots())
                {

                    item.IsEnabled = true;

                }
                else
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
            MissileShots.Text = "1";
        }

        private void ShotTwo_Checked(object sender, RoutedEventArgs e)
        {
            ShotOne.IsChecked = false;
            ShotThree.IsChecked = false;
            ShotFour.IsChecked = false;
            ShotFive.IsChecked = false;
            ShotSix.IsChecked = false;
            MissileShots.Text = "2";
        }

        private void ShotThree_Checked(object sender, RoutedEventArgs e)
        {
            ShotOne.IsChecked = false;
            ShotTwo.IsChecked = false;
            ShotFour.IsChecked = false;
            ShotFive.IsChecked = false;
            ShotSix.IsChecked = false;
            MissileShots.Text = "3";
        }

        private void ShotFour_Checked(object sender, RoutedEventArgs e)
        {
            ShotOne.IsChecked = false;
            ShotTwo.IsChecked = false;
            ShotThree.IsChecked = false;
            ShotFive.IsChecked = false;
            ShotSix.IsChecked = false;
            MissileShots.Text = "4";

        }

        private void ShotFive_Checked(object sender, RoutedEventArgs e)
        {
            ShotOne.IsChecked = false;
            ShotTwo.IsChecked = false;
            ShotThree.IsChecked = false;
            ShotFour.IsChecked = false;
            ShotSix.IsChecked = false;
            MissileShots.Text = "5";
        }

        private void ShotSix_Checked(object sender, RoutedEventArgs e)
        {
            ShotOne.IsChecked = false;
            ShotTwo.IsChecked = false;
            ShotThree.IsChecked = false;
            ShotFour.IsChecked = false;
            ShotFive.IsChecked = false;
            MissileShots.Text = "6";
        }
    }
}

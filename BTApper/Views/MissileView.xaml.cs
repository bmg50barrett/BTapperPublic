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
        Dice de1 = new Dice();
        Dice de2 = new Dice();
        Dice heatDice = new Dice();

        //Create facingID variable.
        private int facingID = 1;

        //Create Weapon objects
        //Weapon object (Name, shots, heat, damage, cluster?, multishot?, notes)
        //Create filler weapon for large array
        static Weapon extra = new Weapon(0, 0, 0);

        //Basic
        static Weapon smallLaser = new Weapon("Small Laser", 1, 1, 1);
        static Weapon mediumLaser = new Weapon("Medium Laser", 1, 3, 5);
        static Weapon largeLaser = new Weapon("Large Laser", 1, 8, 8);

        //Particle Projector
        static Weapon snubPPC = new Weapon("Snub-Nose PPC", 1, 10, 10, false, false, false, "* Snub-Nose PPCs deal 10/8/5 damage at Short/Med/Long Range."); //damage changes based on range
        static Weapon lightPPC = new Weapon(1, 5, 5);
        static Weapon PPC = new Weapon(1, 10, 10);
        static Weapon heavyPPC = new Weapon(1, 15, 15);

        //Extended Range
        static Weapon erSmallLaser = new Weapon(1, 2, 3);
        static Weapon erMediumLaser = new Weapon(1, 5, 5);
        static Weapon erLargeLaser = new Weapon(1, 12, 8);
        static Weapon erPPC = new Weapon(1, 15, 10);

        //Pulse
        static Weapon pulseSmallLaser = new Weapon(1, 2, 3);
        static Weapon pulseMediumLaser = new Weapon(1, 4, 6);
        static Weapon pulseLargeLaser = new Weapon(1, 10, 9);

        //Heat
        static Weapon flamer = new Weapon("Flamer", 1, 3, 2, false, false, true, "* This weapon either does damage OR raises heat.");
        static Weapon plasmaRifle = new Weapon("Plasma Rifle", 1, 10, 10, false, false, true, "* This weapon causes the target to generate 1D6 heat.");

        //Array of Weapon Objects stores data about weapons in same order as EnergyView
        Weapon[,] weaponArray = new Weapon[5, 4] { { smallLaser, mediumLaser, largeLaser, extra }, { snubPPC, lightPPC, PPC, heavyPPC }, { erSmallLaser, erMediumLaser, erLargeLaser, erPPC }, { pulseSmallLaser, pulseMediumLaser, pulseLargeLaser, extra }, { flamer, plasmaRifle, extra, extra } };

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
            smallLaserButton.IsChecked = true;
            EnergyRoll2d6(de1, de2);
        }

        private void UpdateEnergyScreen(String s)
        {
            //Single Element implemenation of text box.
            EnergyTextBox.Text = Prepender(EnergyTextBox.Text, s);
        }

        private string Prepender(string str, string prepend)
        {
            str = str.Replace(Environment.NewLine, Environment.NewLine + prepend);
            str = prepend + Environment.NewLine + str + Environment.NewLine;
            return str;
        }

        private void EnergyRoll2d6(Dice de1, Dice de2)
        {
            de1.RollDice();
            de2.RollDice();
            energyDice1block.Text = de1.GetValue().ToString();
            energyDice2block.Text = de2.GetValue().ToString();
        }

        private void EnergyRoll_Click(object sender, RoutedEventArgs e)
        {
            EnergyRoll2d6(de1, de2);
            int sum = de1.GetValue() + de2.GetValue();
            if (plasmaRifleButton.IsChecked == true)
            {
                heatDice.RollDice();
                UpdateEnergyScreen("You hit " + facingArray[sum - 1, facingID] + " for " + EnergyDamage.Text + " damage and " + heatDice.GetValue() + " heat!");
            }
            else
            {
                UpdateEnergyScreen("You hit " + facingArray[sum - 1, facingID] + " for " + EnergyDamage.Text + " damage !");
            }

        }

        private void smallLaserButton_Checked(object sender, RoutedEventArgs e)
        {
            EnergyDamage.Text = weaponArray[0, 0].GetDamage().ToString();
            EnergyHeat.Text = weaponArray[0, 0].GetHeat().ToString();
            SpecialNotes.Text = weaponArray[0, 0].GetNote();
        }

        private void mediumLaserButton_Checked(object sender, RoutedEventArgs e)
        {
            EnergyDamage.Text = weaponArray[0, 1].GetDamage().ToString();
            EnergyHeat.Text = weaponArray[0, 1].GetHeat().ToString();
            SpecialNotes.Text = weaponArray[0, 1].GetNote();
        }

        private void largeLaserButton_Checked(object sender, RoutedEventArgs e)
        {
            EnergyDamage.Text = weaponArray[0, 2].GetDamage().ToString();
            EnergyHeat.Text = weaponArray[0, 2].GetHeat().ToString();
            SpecialNotes.Text = weaponArray[0, 2].GetNote();
        }

        private void snubPPCButton_Checked(object sender, RoutedEventArgs e)
        {
            EnergyDamage.Text = weaponArray[1, 0].GetDamage().ToString();
            EnergyHeat.Text = weaponArray[1, 0].GetHeat().ToString();
            SpecialNotes.Text = weaponArray[1, 0].GetNote();
        }

        private void lightPPCButton_Checked(object sender, RoutedEventArgs e)
        {
            EnergyDamage.Text = weaponArray[1, 1].GetDamage().ToString();
            EnergyHeat.Text = weaponArray[1, 1].GetHeat().ToString();
            SpecialNotes.Text = weaponArray[1, 1].GetNote();
        }

        private void PPCButton_Checked(object sender, RoutedEventArgs e)
        {
            EnergyDamage.Text = weaponArray[1, 2].GetDamage().ToString();
            EnergyHeat.Text = weaponArray[1, 2].GetHeat().ToString();
            SpecialNotes.Text = weaponArray[1, 2].GetNote();
        }

        private void heavyPPCButton_Checked(object sender, RoutedEventArgs e)
        {
            EnergyDamage.Text = weaponArray[1, 3].GetDamage().ToString();
            EnergyHeat.Text = weaponArray[1, 3].GetHeat().ToString();
            SpecialNotes.Text = weaponArray[1, 3].GetNote();
        }

        private void erSmallButton_Checked(object sender, RoutedEventArgs e)
        {
            EnergyDamage.Text = weaponArray[2, 0].GetDamage().ToString();
            EnergyHeat.Text = weaponArray[2, 0].GetHeat().ToString();
            SpecialNotes.Text = weaponArray[2, 0].GetNote();
        }

        private void erMedButton_Checked(object sender, RoutedEventArgs e)
        {
            EnergyDamage.Text = weaponArray[2, 1].GetDamage().ToString();
            EnergyHeat.Text = weaponArray[2, 1].GetHeat().ToString();
            SpecialNotes.Text = weaponArray[2, 1].GetNote();
        }

        private void erLargeButton_Checked(object sender, RoutedEventArgs e)
        {
            EnergyDamage.Text = weaponArray[2, 2].GetDamage().ToString();
            EnergyHeat.Text = weaponArray[2, 2].GetHeat().ToString();
            SpecialNotes.Text = weaponArray[2, 2].GetNote();
        }

        private void erPPCButton_Checked(object sender, RoutedEventArgs e)
        {
            EnergyDamage.Text = weaponArray[2, 3].GetDamage().ToString();
            EnergyHeat.Text = weaponArray[2, 3].GetHeat().ToString();
            SpecialNotes.Text = weaponArray[2, 3].GetNote();
        }

        private void smallPulseButton_Checked(object sender, RoutedEventArgs e)
        {
            EnergyDamage.Text = weaponArray[3, 0].GetDamage().ToString();
            EnergyHeat.Text = weaponArray[3, 0].GetHeat().ToString();
            SpecialNotes.Text = weaponArray[3, 0].GetNote();
        }

        private void medPulseButton_Checked(object sender, RoutedEventArgs e)
        {
            EnergyDamage.Text = weaponArray[3, 1].GetDamage().ToString();
            EnergyHeat.Text = weaponArray[3, 1].GetHeat().ToString();
            SpecialNotes.Text = weaponArray[3, 1].GetNote();
        }

        private void largePulseButton_Checked(object sender, RoutedEventArgs e)
        {
            EnergyDamage.Text = weaponArray[3, 2].GetDamage().ToString();
            EnergyHeat.Text = weaponArray[3, 2].GetHeat().ToString();
            SpecialNotes.Text = weaponArray[3, 2].GetNote();
        }

        private void flamerButton_Checked(object sender, RoutedEventArgs e)
        {
            EnergyDamage.Text = weaponArray[4, 0].GetDamage().ToString();
            EnergyHeat.Text = weaponArray[4, 0].GetHeat().ToString();
            SpecialNotes.Text = weaponArray[4, 0].GetNote();
        }

        private void plasmaRifleButton_Checked(object sender, RoutedEventArgs e)
        {
            EnergyDamage.Text = weaponArray[4, 1].GetDamage().ToString();
            EnergyHeat.Text = weaponArray[4, 1].GetHeat().ToString();
            SpecialNotes.Text = weaponArray[4, 1].GetNote();
        }

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

        private void rearFaceButton_Checked(object sender, RoutedEventArgs e)
        {
            //Rear facing has the same location roll as front facing.
            facingID = 0;
        }
    }
}

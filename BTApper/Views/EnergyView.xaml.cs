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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace BTApper.Views
{

    public sealed partial class EnergyView : Page
    {
        //Create Dice Objects
        Dice de1 = new Dice();
        Dice de2 = new Dice();

        //Create Weapon objects
        //Weapon object (shots, heat, damage, cluster?, multishot?)

        static Weapon extra = new Weapon(0,0,0);

        //Basic
        static Weapon smallLaser = new Weapon("Small Laser", 1, 1, 1);
        static Weapon mediumLaser = new Weapon("Medium Laser", 1, 3, 5);
        static Weapon largeLaser = new Weapon("Large Laser", 1, 8, 8);

        //Particle Projector
        static Weapon snubPPC = new Weapon(1, 10, 10); //damage changes based on range
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
        static Weapon flamer = new Weapon(1, 2, 3, false, false, true);
        static Weapon plasmaRifle = new Weapon(1, 10, 10, false, false, true);

        //Array of Weapon Objects stores data about weapons in same order as EnergyView
        Weapon[,] weaponArray = new Weapon[5, 4] { { smallLaser, mediumLaser, largeLaser, extra }, { snubPPC, lightPPC, PPC, heavyPPC }, { erSmallLaser, erMediumLaser, erLargeLaser, erPPC }, { pulseSmallLaser, pulseMediumLaser, pulseLargeLaser, extra }, { flamer, plasmaRifle, extra, extra } };

        public EnergyView()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Required;

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
        }

        private void smallLaserButton_Checked(object sender, RoutedEventArgs e)
        {
            EnergyDamage.Text = weaponArray[0, 0].GetDamage().ToString();
            EnergyHeat.Text = weaponArray[0, 0].GetHeat().ToString();
        }

        private void mediumLaserButton_Checked(object sender, RoutedEventArgs e)
        {
            EnergyDamage.Text = "0";
            EnergyHeat.Text = "0";
        }

        private void largeLaserButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void snubPPCButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void lightPPCButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void PPCButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void heavyPPCButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void erSmallButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void erMedButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void erLargeButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void erPPCButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void smallPulseButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void medPulseButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void largePulseButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void flamerButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void plasmaRifleButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void frontFaceButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void rightFaceButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void leftFaceButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void rearFaceButton_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}

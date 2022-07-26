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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace BTApper.Views
{
    /// <summary>
    /// This page calculates physical attack rolls.
    /// </summary>
    public sealed partial class PhysicalView : Page
    {

        //Create Dice Objects
        Dice dice1 = new Dice();
        Dice dice2 = new Dice();

        //Create facingID variable.
        private int facingID = 0;

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

        public PhysicalView()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Required;
            frontFaceButton.IsChecked = true;
        }

        private void UpdatePhysicalScreen(String s)
        {
            //Single Element implemenation of text box.
            PhysicalTextBox.Text = Prepender(PhysicalTextBox.Text, s);
        }

        private string Prepender(string str, string prepend)
        {
            str = str.Replace(Environment.NewLine, Environment.NewLine + prepend);
            str = prepend + Environment.NewLine + str + Environment.NewLine;
            return str;
        }

        private void PhysicalRoll2d6(Dice d1, Dice d2)
        {
            String filename;

            d1.RollDice();
            filename = "/Assets/" + d1.GetValue().ToString() + "die.png";
            PhysicalDice1.Source = new BitmapImage(new Uri(base.BaseUri, @filename));

            d2.RollDice();
            filename = "/Assets/" + d2.GetValue().ToString() + "die.png";
            PhysicalDice2.Source = new BitmapImage(new Uri(base.BaseUri, @filename));
        }

        private void PhysicalRoll_Click(object sender, RoutedEventArgs e)
        {
            PhysicalRoll2d6(dice1, dice2);
            int sum = dice1.GetValue() + dice2.GetValue();
            UpdatePhysicalScreen("Total: " + sum);

        }

        private void PlaceholderButton_Checked(object sender, RoutedEventArgs e)
        {

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

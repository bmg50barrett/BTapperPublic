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
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GatorView : Page
    {
        //Declare.
        Dice dice1;
        Dice dice2;
        Modifier speedMod;
        Modifier gunneryMod;
        Modifier attackerMod;
        Modifier targetMod;
        Modifier otherMod;
        Modifier rangeMod;
        Gator gator;

        Modifier miscMod;
        Modifier heatMod;

        public GatorView()
        {
            //Initialize.
            this.InitializeComponent();
            this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Required;
            dice1 = new Dice();
            dice2 = new Dice();
            speedMod = new Modifier();
            gunneryMod = new Modifier();
            attackerMod = new Modifier();
            targetMod = new Modifier();
            otherMod = new Modifier();
            rangeMod = new Modifier();
            gator = new Gator();

            miscMod = new Modifier();
            heatMod = new Modifier();

            SpeedNormal.IsChecked = true;
            G0.IsChecked = true;
            A0.IsChecked = true;
            T0.IsChecked = true;
            O0.IsChecked = true;
            RS.IsChecked = true;
            Heat0to7.IsChecked = true;
            gator.computeGator(gunneryMod, attackerMod, targetMod, otherMod, rangeMod, miscMod, heatMod);
        }

        private void UpdateScreen(String s)
        {
            //Single Element implemenation of text box.
            TargetTextBox.Text = Prepender(TargetTextBox.Text, s);
        }

        private string Prepender(string str, string prepend)
        {
            str = str.Replace(Environment.NewLine, Environment.NewLine + prepend);
            str = prepend + Environment.NewLine + str + Environment.NewLine;
            return str;
        }

        private void UpdateTargetBlock(int n)
        {
            if (n > 12)
            {
                targetBlock.Text = "Auto Miss";
            }
            else if (n < 3)
            {
                targetBlock.Text = "Auto Hit";
            }
            else
            {
                targetBlock.Text = n.ToString();
            }
        }

        private void Roll2d6(Dice d1, Dice d2)
        {
            d1.RollDice();
            d2.RollDice();
            diceResult1.Text = dice1.GetValue().ToString();
            diceResult2.Text = dice2.GetValue().ToString();
        }

        private void Roll_Click(object sender, RoutedEventArgs e)
        {
            Roll2d6(dice1, dice2);
            int sumdice = dice1.GetValue() + dice2.GetValue();
            if (sumdice < gator.GetValue())
            {
                UpdateScreen("You missed! You rolled a " + sumdice + "!");
            }
            else if (sumdice >= gator.GetValue())
            {
                UpdateScreen("You HIT! You Rolled a " + sumdice + "!");
            }
            else
            {
                UpdateScreen("Something happened...");
            }
        }

        private void SpeedNormal_Checked(object sender, RoutedEventArgs e)
        {
            speedMod.SetValue(0);
            foreach (RadioButton item in RangeButtonsPanel.Children)
            {
                if(item.IsChecked == true)
                {
                    item.IsChecked = false;
                    item.IsChecked = true;
                }
            }
            gator.computeGator(gunneryMod, attackerMod, targetMod, otherMod, rangeMod, miscMod, heatMod);
            UpdateTargetBlock(gator.GetValue());
        }

        private void SpeedFast_Checked(object sender, RoutedEventArgs e)
        {
            speedMod.SetValue(1);
            foreach (RadioButton item in RangeButtonsPanel.Children)
            {
                if (item.IsChecked == true)
                {
                    item.IsChecked = false;
                    item.IsChecked = true;
                }
            }
            gator.computeGator(gunneryMod, attackerMod, targetMod, otherMod, rangeMod, miscMod, heatMod);
            UpdateTargetBlock(gator.GetValue());
        }

        private void SpeedFaster_Checked(object sender, RoutedEventArgs e)
        {
            speedMod.SetValue(2);
                        foreach (RadioButton item in RangeButtonsPanel.Children)
            {
                if(item.IsChecked == true)
                {
                    item.IsChecked = false;
                    item.IsChecked = true;
                }
            }
            gator.computeGator(gunneryMod, attackerMod, targetMod, otherMod, rangeMod, miscMod, heatMod);
            UpdateTargetBlock(gator.GetValue());
        }

        private void G0_Checked(object sender, RoutedEventArgs e)
        {
            gunneryMod.SetValue(0);
            gator.computeGator(gunneryMod, attackerMod, targetMod, otherMod, rangeMod, miscMod, heatMod);
            UpdateTargetBlock(gator.GetValue());
        }

        private void G1_Checked(object sender, RoutedEventArgs e)
        {
            gunneryMod.SetValue(1);
            gator.computeGator(gunneryMod, attackerMod, targetMod, otherMod, rangeMod, miscMod, heatMod);
            UpdateTargetBlock(gator.GetValue());
        }

        private void G2_Checked(object sender, RoutedEventArgs e)
        {
            gunneryMod.SetValue(2);
            gator.computeGator(gunneryMod, attackerMod, targetMod, otherMod, rangeMod, miscMod, heatMod);
            UpdateTargetBlock(gator.GetValue());
        }

        private void G3_Checked(object sender, RoutedEventArgs e)
        {
            gunneryMod.SetValue(3);
            gator.computeGator(gunneryMod, attackerMod, targetMod, otherMod, rangeMod, miscMod, heatMod);
            UpdateTargetBlock(gator.GetValue());
        }

        private void G4_Checked(object sender, RoutedEventArgs e)
        {
            gunneryMod.SetValue(4);
            gator.computeGator(gunneryMod, attackerMod, targetMod, otherMod, rangeMod, miscMod, heatMod);
            UpdateTargetBlock(gator.GetValue());
        }

        private void G5_Checked(object sender, RoutedEventArgs e)
        {
            gunneryMod.SetValue(5);
            gator.computeGator(gunneryMod, attackerMod, targetMod, otherMod, rangeMod, miscMod, heatMod);
            UpdateTargetBlock(gator.GetValue());
        }

        private void G6_Checked(object sender, RoutedEventArgs e)
        {
            gunneryMod.SetValue(6);
            gator.computeGator(gunneryMod, attackerMod, targetMod, otherMod, rangeMod, miscMod, heatMod);
            UpdateTargetBlock(gator.GetValue());
        }

        private void G7_Checked(object sender, RoutedEventArgs e)
        {
            gunneryMod.SetValue(7);
            gator.computeGator(gunneryMod, attackerMod, targetMod, otherMod, rangeMod, miscMod, heatMod);
            UpdateTargetBlock(gator.GetValue());
        }

        private void A0_Checked(object sender, RoutedEventArgs e)
        {
            attackerMod.SetValue(0);
            gator.computeGator(gunneryMod, attackerMod, targetMod, otherMod, rangeMod, miscMod, heatMod);
            UpdateTargetBlock(gator.GetValue());
        }

        private void A1_Checked(object sender, RoutedEventArgs e)
        {
            attackerMod.SetValue(1);
            gator.computeGator(gunneryMod, attackerMod, targetMod, otherMod, rangeMod, miscMod, heatMod);
            UpdateTargetBlock(gator.GetValue());
        }

        private void A2_Checked(object sender, RoutedEventArgs e)
        {
            attackerMod.SetValue(2);
            gator.computeGator(gunneryMod, attackerMod, targetMod, otherMod, rangeMod, miscMod, heatMod);
            UpdateTargetBlock(gator.GetValue());
        }

        private void A3_Checked(object sender, RoutedEventArgs e)
        {
            attackerMod.SetValue(3);
            gator.computeGator(gunneryMod, attackerMod, targetMod, otherMod, rangeMod, miscMod, heatMod);
            UpdateTargetBlock(gator.GetValue());
        }

        private void A4_Checked(object sender, RoutedEventArgs e)
        {
            attackerMod.SetValue(4);
            gator.computeGator(gunneryMod, attackerMod, targetMod, otherMod, rangeMod, miscMod, heatMod);
            UpdateTargetBlock(gator.GetValue());
        }

        private void A5_Checked(object sender, RoutedEventArgs e)
        {
            attackerMod.SetValue(5);
            gator.computeGator(gunneryMod, attackerMod, targetMod, otherMod, rangeMod, miscMod, heatMod);
            UpdateTargetBlock(gator.GetValue());
        }

        private void A6_Checked(object sender, RoutedEventArgs e)
        {
            attackerMod.SetValue(6);
            gator.computeGator(gunneryMod, attackerMod, targetMod, otherMod, rangeMod, miscMod, heatMod);
            UpdateTargetBlock(gator.GetValue());
        }

        private void A7_Checked(object sender, RoutedEventArgs e)
        {
            attackerMod.SetValue(7);
            gator.computeGator(gunneryMod, attackerMod, targetMod, otherMod, rangeMod, miscMod, heatMod);
            UpdateTargetBlock(gator.GetValue());
        }

        private void T0_Checked(object sender, RoutedEventArgs e)
        {
            targetMod.SetValue(0);
            gator.computeGator(gunneryMod, attackerMod, targetMod, otherMod, rangeMod, miscMod, heatMod);
            UpdateTargetBlock(gator.GetValue());
        }

        private void T1_Checked(object sender, RoutedEventArgs e)
        {
            targetMod.SetValue(1);
            gator.computeGator(gunneryMod, attackerMod, targetMod, otherMod, rangeMod, miscMod, heatMod);
            UpdateTargetBlock(gator.GetValue());
        }

        private void T2_Checked(object sender, RoutedEventArgs e)
        {
            targetMod.SetValue(2);
            gator.computeGator(gunneryMod, attackerMod, targetMod, otherMod, rangeMod, miscMod, heatMod);
            UpdateTargetBlock(gator.GetValue());
        }

        private void T3_Checked(object sender, RoutedEventArgs e)
        {
            targetMod.SetValue(3);
            gator.computeGator(gunneryMod, attackerMod, targetMod, otherMod, rangeMod, miscMod, heatMod);
            UpdateTargetBlock(gator.GetValue());
        }

        private void T4_Checked(object sender, RoutedEventArgs e)
        {
            targetMod.SetValue(4);
            gator.computeGator(gunneryMod, attackerMod, targetMod, otherMod, rangeMod, miscMod, heatMod);
            UpdateTargetBlock(gator.GetValue());
        }

        private void T5_Checked(object sender, RoutedEventArgs e)
        {
            targetMod.SetValue(5);
            gator.computeGator(gunneryMod, attackerMod, targetMod, otherMod, rangeMod, miscMod, heatMod);
            UpdateTargetBlock(gator.GetValue());
        }

        private void T6_Checked(object sender, RoutedEventArgs e)
        {
            targetMod.SetValue(6);
            gator.computeGator(gunneryMod, attackerMod, targetMod, otherMod, rangeMod, miscMod, heatMod);
            UpdateTargetBlock(gator.GetValue());
        }

        private void T7_Checked(object sender, RoutedEventArgs e)
        {
            targetMod.SetValue(7);
            gator.computeGator(gunneryMod, attackerMod, targetMod, otherMod, rangeMod, miscMod, heatMod);
            UpdateTargetBlock(gator.GetValue());
        }

        private void O0_Checked(object sender, RoutedEventArgs e)
        {
            otherMod.SetValue(0);
            gator.computeGator(gunneryMod, attackerMod, targetMod, otherMod, rangeMod, miscMod, heatMod);
            targetBlock.Text = gator.GetValue().ToString();
        }

        private void O1_Checked(object sender, RoutedEventArgs e)
        {
            otherMod.SetValue(1);
            gator.computeGator(gunneryMod, attackerMod, targetMod, otherMod, rangeMod, miscMod, heatMod);
            UpdateTargetBlock(gator.GetValue());
        }

        private void O2_Checked(object sender, RoutedEventArgs e)
        {
            otherMod.SetValue(2);
            gator.computeGator(gunneryMod, attackerMod, targetMod, otherMod, rangeMod, miscMod, heatMod);
            UpdateTargetBlock(gator.GetValue());
        }

        private void O3_Checked(object sender, RoutedEventArgs e)
        {
            otherMod.SetValue(3);
            gator.computeGator(gunneryMod, attackerMod, targetMod, otherMod, rangeMod, miscMod, heatMod);
            UpdateTargetBlock(gator.GetValue());
        }

        private void O4_Checked(object sender, RoutedEventArgs e)
        {
            otherMod.SetValue(4);
            gator.computeGator(gunneryMod, attackerMod, targetMod, otherMod, rangeMod, miscMod, heatMod);
            UpdateTargetBlock(gator.GetValue());
        }

        private void O5_Checked(object sender, RoutedEventArgs e)
        {
            otherMod.SetValue(5);
            gator.computeGator(gunneryMod, attackerMod, targetMod, otherMod, rangeMod, miscMod, heatMod);
            UpdateTargetBlock(gator.GetValue());
        }

        private void O6_Checked(object sender, RoutedEventArgs e)
        {
            otherMod.SetValue(6);
            gator.computeGator(gunneryMod, attackerMod, targetMod, otherMod, rangeMod, miscMod, heatMod);
            UpdateTargetBlock(gator.GetValue());
        }

        private void O7_Checked(object sender, RoutedEventArgs e)
        {
            otherMod.SetValue(7);
            gator.computeGator(gunneryMod, attackerMod, targetMod, otherMod, rangeMod, miscMod, heatMod);
            UpdateTargetBlock(gator.GetValue());
        }

        private void RMin_Checked(object sender, RoutedEventArgs e)
        {
            switch (speedMod.GetValue())
            {
                case 0:
                    rangeMod.SetValue(1);
                    break;
                case 1:
                    rangeMod.SetValue(1);
                    break;
                case 2:
                    rangeMod.SetValue(0);
                    break;
                default:
                    rangeMod.SetValue(1);
                    break;
            }
            gator.computeGator(gunneryMod, attackerMod, targetMod, otherMod, rangeMod, miscMod, heatMod);
            UpdateTargetBlock(gator.GetValue());
        }

        private void RS_Checked(object sender, RoutedEventArgs e)
        {
            switch (speedMod.GetValue())
            {
                case 0:
                    rangeMod.SetValue(0);
                    break;
                case 1:
                    rangeMod.SetValue(0);
                    break;
                case 2:
                    rangeMod.SetValue(-2);
                    break;
                default:
                    rangeMod.SetValue(0);
                    break;
            }
            gator.computeGator(gunneryMod, attackerMod, targetMod, otherMod, rangeMod, miscMod, heatMod);
            UpdateTargetBlock(gator.GetValue());
        }

        private void RMed_Checked(object sender, RoutedEventArgs e)
        {
            switch (speedMod.GetValue())
            {
                case 0:
                    rangeMod.SetValue(2);
                    break;
                case 1:
                    rangeMod.SetValue(1);
                    break;
                case 2:
                    rangeMod.SetValue(0);
                    break;
                default:
                    rangeMod.SetValue(2);
                    break;
            }
            gator.computeGator(gunneryMod, attackerMod, targetMod, otherMod, rangeMod, miscMod, heatMod);
            UpdateTargetBlock(gator.GetValue());
        }

        private void RL_Checked(object sender, RoutedEventArgs e)
        {
            switch (speedMod.GetValue())
            {
                case 0:
                    rangeMod.SetValue(4);
                    break;
                case 1:
                    rangeMod.SetValue(3);
                    break;
                case 2:
                    rangeMod.SetValue(2);
                    break;
                default:
                    rangeMod.SetValue(4);
                    break;
            }
            gator.computeGator(gunneryMod, attackerMod, targetMod, otherMod, rangeMod, miscMod, heatMod);
            UpdateTargetBlock(gator.GetValue());
        }

        private void RE_Checked(object sender, RoutedEventArgs e)
        {
            switch (speedMod.GetValue())
            {
                case 0:
                    rangeMod.SetValue(99);
                    break;
                case 1:
                    rangeMod.SetValue(4);
                    break;
                case 2:
                    rangeMod.SetValue(3);
                    break;
                default:
                    rangeMod.SetValue(99);
                    break;
            }
            gator.computeGator(gunneryMod, attackerMod, targetMod, otherMod, rangeMod, miscMod, heatMod);
            UpdateTargetBlock(gator.GetValue());
        }

        private void PulseCheck_Checked(object sender, RoutedEventArgs e)
        {
            miscMod.Decrement();
            miscMod.Decrement();
            gator.computeGator(gunneryMod, attackerMod, targetMod, otherMod, rangeMod, miscMod, heatMod);
            UpdateTargetBlock(gator.GetValue());
        }

        private void PulseCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            miscMod.Increment();
            miscMod.Increment();
            gator.computeGator(gunneryMod, attackerMod, targetMod, otherMod, rangeMod, miscMod, heatMod);
            UpdateTargetBlock(gator.GetValue());
        }

        private void TargetingComputerCheck_Checked(object sender, RoutedEventArgs e)
        {
            miscMod.Decrement();
            gator.computeGator(gunneryMod, attackerMod, targetMod, otherMod, rangeMod, miscMod, heatMod);
            UpdateTargetBlock(gator.GetValue());
        }

        private void TargetingComputerCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            miscMod.Increment();
            gator.computeGator(gunneryMod, attackerMod, targetMod, otherMod, rangeMod, miscMod, heatMod);
            UpdateTargetBlock(gator.GetValue());
        }

        private void ShoulderCheck_Checked(object sender, RoutedEventArgs e)
        {
            miscMod.Increment();
            miscMod.Increment();
            miscMod.Increment();
            miscMod.Increment();
            gator.computeGator(gunneryMod, attackerMod, targetMod, otherMod, rangeMod, miscMod, heatMod);
            UpdateTargetBlock(gator.GetValue());
        }

        private void ShoulderCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            miscMod.Decrement();
            miscMod.Decrement();
            miscMod.Decrement();
            miscMod.Decrement();
            gator.computeGator(gunneryMod, attackerMod, targetMod, otherMod, rangeMod, miscMod, heatMod);
            UpdateTargetBlock(gator.GetValue());
        }

        private void ActuatorCheck_Checked(object sender, RoutedEventArgs e)
        {
            miscMod.Increment();
            gator.computeGator(gunneryMod, attackerMod, targetMod, otherMod, rangeMod, miscMod, heatMod);
            UpdateTargetBlock(gator.GetValue());
        }

        private void ActuatorCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            miscMod.Decrement();
            gator.computeGator(gunneryMod, attackerMod, targetMod, otherMod, rangeMod, miscMod, heatMod);
            UpdateTargetBlock(gator.GetValue());
        }

        private void SensorHit_Checked(object sender, RoutedEventArgs e)
        {
            miscMod.Increment();
            miscMod.Increment();
            gator.computeGator(gunneryMod, attackerMod, targetMod, otherMod, rangeMod, miscMod, heatMod);
            UpdateTargetBlock(gator.GetValue());
        }

        private void SensorHit_Unchecked(object sender, RoutedEventArgs e)
        {
            miscMod.Decrement();
            miscMod.Decrement();
            gator.computeGator(gunneryMod, attackerMod, targetMod, otherMod, rangeMod, miscMod, heatMod);
            UpdateTargetBlock(gator.GetValue());
        }

        private void Indirect_Checked(object sender, RoutedEventArgs e)
        {
            miscMod.Increment();
            gator.computeGator(gunneryMod, attackerMod, targetMod, otherMod, rangeMod, miscMod, heatMod);
            UpdateTargetBlock(gator.GetValue());
        }

        private void Indirect_Unchecked(object sender, RoutedEventArgs e)
        {
            miscMod.Decrement();
            gator.computeGator(gunneryMod, attackerMod, targetMod, otherMod, rangeMod, miscMod, heatMod);
            UpdateTargetBlock(gator.GetValue());
        }

        private void Secondary_Checked(object sender, RoutedEventArgs e)
        {
            miscMod.Increment();
            gator.computeGator(gunneryMod, attackerMod, targetMod, otherMod, rangeMod, miscMod, heatMod);
            UpdateTargetBlock(gator.GetValue());
        }

        private void Secondary_Unchecked(object sender, RoutedEventArgs e)
        {
            miscMod.Decrement();
            gator.computeGator(gunneryMod, attackerMod, targetMod, otherMod, rangeMod, miscMod, heatMod);
            UpdateTargetBlock(gator.GetValue());
        }

        private void UActuatorHit_Checked(object sender, RoutedEventArgs e)
        {
            miscMod.Increment();
            gator.computeGator(gunneryMod, attackerMod, targetMod, otherMod, rangeMod, miscMod, heatMod);
            UpdateTargetBlock(gator.GetValue());
        }

        private void UActuatorHit_Unchecked(object sender, RoutedEventArgs e)
        {
            miscMod.Decrement();
            gator.computeGator(gunneryMod, attackerMod, targetMod, otherMod, rangeMod, miscMod, heatMod);
            UpdateTargetBlock(gator.GetValue());
        }

        private void Heat0to7_Checked(object sender, RoutedEventArgs e)
        {
            heatMod.SetValue(0);
            gator.computeGator(gunneryMod, attackerMod, targetMod, otherMod, rangeMod, miscMod, heatMod);
            UpdateTargetBlock(gator.GetValue());
        }

        private void Heat8to12_Checked(object sender, RoutedEventArgs e)
        {
            heatMod.SetValue(1);
            gator.computeGator(gunneryMod, attackerMod, targetMod, otherMod, rangeMod, miscMod, heatMod);
            UpdateTargetBlock(gator.GetValue());
        }

        private void Heat13to16_Checked(object sender, RoutedEventArgs e)
        {
            heatMod.SetValue(2);
            gator.computeGator(gunneryMod, attackerMod, targetMod, otherMod, rangeMod, miscMod, heatMod);
            UpdateTargetBlock(gator.GetValue());
        }

        private void Heat17to23_Checked(object sender, RoutedEventArgs e)
        {
            heatMod.SetValue(3);
            gator.computeGator(gunneryMod, attackerMod, targetMod, otherMod, rangeMod, miscMod, heatMod);
            UpdateTargetBlock(gator.GetValue());
        }

        private void Heat24Plus_Checked(object sender, RoutedEventArgs e)
        {
            heatMod.SetValue(4);
            gator.computeGator(gunneryMod, attackerMod, targetMod, otherMod, rangeMod, miscMod, heatMod);
            UpdateTargetBlock(gator.GetValue());
        }
    }
}
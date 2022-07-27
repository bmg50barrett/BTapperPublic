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
        Modifier minRange;

        //Rapid Fire Jam Checker flag.
        private int rapidFireTargetRoll = 0;

        //Roll ID
        private int rollID = 0;

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
            minRange = new Modifier();

            SpeedNormal.IsChecked = true;
            G0.IsChecked = true;
            A0.IsChecked = true;
            T0.IsChecked = true;
            O0.IsChecked = true;
            RS.IsChecked = true;
            Heat0to7.IsChecked = true;
            MinRangeSlider.Value = 0;
            gator.computeGator(gunneryMod, attackerMod, targetMod, otherMod, rangeMod, miscMod, heatMod);;
        }

        private void UpdateScreen(String s)
        {
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
            String filename;

            d1.RollDice();
            filename = "/Assets/" + d1.GetValue().ToString() + "die.png";
            Dice1.Source = new BitmapImage(new Uri(base.BaseUri, @filename));

            d2.RollDice();
            filename = "/Assets/" + d2.GetValue().ToString() + "die.png";
            Dice2.Source = new BitmapImage(new Uri(base.BaseUri, @filename));
        }

        private void Roll_Click(object sender, RoutedEventArgs e)
        {
            Roll2d6(dice1, dice2);
            int sumdice = dice1.GetValue() + dice2.GetValue();
            rollID++;

            

            if (sumdice < gator.GetValue())
            {
                UpdateScreen("==========");
                UpdateScreen("  You missed! You rolled a " + sumdice + "!");
                if (rapidFireTargetRoll >= 2)
                {
                    if (sumdice <= rapidFireTargetRoll)
                    {
                        UpdateScreen("  Weapon Jammed! Apply damage to target!");
                    }
                    else
                    {
                        UpdateScreen("  Weapon clear! Apply damage to target!");
                    }
                }
                UpdateScreen("Roll#: " + rollID);
            }
            else if (sumdice >= gator.GetValue())
            {
                UpdateScreen("==========");
                UpdateScreen("  You HIT! You Rolled a " + sumdice + "!");
                if (rapidFireTargetRoll >= 2)
                {
                    if (sumdice <= rapidFireTargetRoll)
                    {
                        UpdateScreen("  Weapon Jammed! Apply damage to target!");
                    }
                    else
                    {
                        UpdateScreen("  Weapon clear! Apply damage to target!");
                    }
                }
                UpdateScreen("Roll#: " + rollID);
            }

        }

        private void UpdateValues()
        {
            if (RMin.IsChecked == true)
            {
                gator.computeGator(gunneryMod, attackerMod, targetMod, otherMod, rangeMod, miscMod, heatMod, minRange);
                UpdateTargetBlock(gator.GetValue());
            }
            else
            {
                gator.computeGator(gunneryMod, attackerMod, targetMod, otherMod, rangeMod, miscMod, heatMod);
                UpdateTargetBlock(gator.GetValue());
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
            gator.computeGator(gunneryMod, attackerMod, targetMod, otherMod, rangeMod, miscMod, heatMod);;
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
            gator.computeGator(gunneryMod, attackerMod, targetMod, otherMod, rangeMod, miscMod, heatMod);;
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
            gator.computeGator(gunneryMod, attackerMod, targetMod, otherMod, rangeMod, miscMod, heatMod);;
            UpdateTargetBlock(gator.GetValue());
        }

        private void G0_Checked(object sender, RoutedEventArgs e)
        {
            gunneryMod.SetValue(0);
            if(RMin.IsChecked == true)
            {
                gator.computeGator(gunneryMod, attackerMod, targetMod, otherMod, rangeMod, miscMod, heatMod, minRange);
                UpdateTargetBlock(gator.GetValue());
            } else
            {
                gator.computeGator(gunneryMod, attackerMod, targetMod, otherMod, rangeMod, miscMod, heatMod);
                UpdateTargetBlock(gator.GetValue());
            }
            
        }

        private void G1_Checked(object sender, RoutedEventArgs e)
        {
            gunneryMod.SetValue(1);
            UpdateValues();
        }

        private void G2_Checked(object sender, RoutedEventArgs e)
        {
            gunneryMod.SetValue(2);
            UpdateValues();
        }

        private void G3_Checked(object sender, RoutedEventArgs e)
        {
            gunneryMod.SetValue(3);
            UpdateValues();
        }

        private void G4_Checked(object sender, RoutedEventArgs e)
        {
            gunneryMod.SetValue(4);
            UpdateValues();
        }

        private void G5_Checked(object sender, RoutedEventArgs e)
        {
            gunneryMod.SetValue(5);
            UpdateValues();
        }

        private void G6_Checked(object sender, RoutedEventArgs e)
        {
            gunneryMod.SetValue(6);
            UpdateValues();
        }

        private void G7_Checked(object sender, RoutedEventArgs e)
        {
            gunneryMod.SetValue(7);
            UpdateValues();
        }

        private void A0_Checked(object sender, RoutedEventArgs e)
        {
            attackerMod.SetValue(0);
            UpdateValues();
        }

        private void A1_Checked(object sender, RoutedEventArgs e)
        {
            attackerMod.SetValue(1);
            UpdateValues();
        }

        private void A2_Checked(object sender, RoutedEventArgs e)
        {
            attackerMod.SetValue(2);
            UpdateValues();
        }

        private void A3_Checked(object sender, RoutedEventArgs e)
        {
            attackerMod.SetValue(3);
            UpdateValues();
        }

        private void A4_Checked(object sender, RoutedEventArgs e)
        {
            attackerMod.SetValue(4);
            UpdateValues();
        }

        private void A5_Checked(object sender, RoutedEventArgs e)
        {
            attackerMod.SetValue(5);
            UpdateValues();
        }

        private void A6_Checked(object sender, RoutedEventArgs e)
        {
            attackerMod.SetValue(6);
            UpdateValues();
        }

        private void A7_Checked(object sender, RoutedEventArgs e)
        {
            attackerMod.SetValue(7);
            UpdateValues();
        }

        private void T0_Checked(object sender, RoutedEventArgs e)
        {
            targetMod.SetValue(0);
            UpdateValues();
        }

        private void T1_Checked(object sender, RoutedEventArgs e)
        {
            targetMod.SetValue(1);
            UpdateValues();
        }

        private void T2_Checked(object sender, RoutedEventArgs e)
        {
            targetMod.SetValue(2);
            UpdateValues();
        }

        private void T3_Checked(object sender, RoutedEventArgs e)
        {
            targetMod.SetValue(3);
            UpdateValues();
        }

        private void T4_Checked(object sender, RoutedEventArgs e)
        {
            targetMod.SetValue(4);
            UpdateValues();
        }

        private void T5_Checked(object sender, RoutedEventArgs e)
        {
            targetMod.SetValue(5);
            UpdateValues();
        }

        private void T6_Checked(object sender, RoutedEventArgs e)
        {
            targetMod.SetValue(6);
            UpdateValues();
        }

        private void T7_Checked(object sender, RoutedEventArgs e)
        {
            targetMod.SetValue(7);
            UpdateValues();
        }

        private void O0_Checked(object sender, RoutedEventArgs e)
        {
            otherMod.SetValue(0);
            UpdateValues();
        }

        private void O1_Checked(object sender, RoutedEventArgs e)
        {
            otherMod.SetValue(1);
            UpdateValues();
        }

        private void O2_Checked(object sender, RoutedEventArgs e)
        {
            otherMod.SetValue(2);
            UpdateValues();
        }

        private void O3_Checked(object sender, RoutedEventArgs e)
        {
            otherMod.SetValue(3);
            UpdateValues();
        }

        private void O4_Checked(object sender, RoutedEventArgs e)
        {
            otherMod.SetValue(4);
            UpdateValues();
        }

        private void O5_Checked(object sender, RoutedEventArgs e)
        {
            otherMod.SetValue(5);
            UpdateValues();
        }

        private void O6_Checked(object sender, RoutedEventArgs e)
        {
            otherMod.SetValue(6);
            UpdateValues();
        }

        private void O7_Checked(object sender, RoutedEventArgs e)
        {
            otherMod.SetValue(7);
            UpdateValues();
        }

        private void MinRangeSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if (MinRangeSlider.Value == 0)
            {
                minRange?.SetValue(1);
            }
            else if (MinRangeSlider.Value == -1)
            {
                minRange?.SetValue(2);
            }
            else if (MinRangeSlider.Value == -2)
            {
                minRange?.SetValue(3);
            }
            else if (MinRangeSlider.Value == -3)
            {
                minRange?.SetValue(4);
            }
            else if (MinRangeSlider.Value == -4)
            {
                minRange?.SetValue(5);
            }
            else if (MinRangeSlider.Value == -5)
            {
                minRange?.SetValue(6);
            }
            else {
                minRange?.SetValue(7);
            }

            if (RMin?.IsChecked == true)
            {
                gator.computeGator(gunneryMod, attackerMod, targetMod, otherMod, rangeMod, miscMod, heatMod, minRange);
                UpdateTargetBlock(gator.GetValue());
            }
        }

        private void RMin_Checked(object sender, RoutedEventArgs e)
        {
            rangeMod.SetValue(0);

            if (MinRangeSlider.Value == 0)
            {
                minRange?.SetValue(1);
            }
            else if (MinRangeSlider.Value == -1)
            {
                minRange?.SetValue(2);
            }
            else if (MinRangeSlider.Value == -2)
            {
                minRange?.SetValue(3);
            }
            else if (MinRangeSlider.Value == -3)
            {
                minRange?.SetValue(4);
            }
            else if (MinRangeSlider.Value == -4)
            {
                minRange?.SetValue(5);
            }
            else if (MinRangeSlider.Value == -5)
            {
                minRange?.SetValue(6);
            }
            else if (MinRangeSlider.Value == -6)
            {
                minRange?.SetValue(7);
            } else
            {
                minRange?.SetValue(0);
            }
            gator.computeGator(gunneryMod, attackerMod, targetMod, otherMod, rangeMod, miscMod, heatMod, minRange);
            UpdateTargetBlock(gator.GetValue());
        }

        private void RMin_Unchecked(object sender, RoutedEventArgs e)
        {
            rangeMod.SetValue(0);
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
            gator.computeGator(gunneryMod, attackerMod, targetMod, otherMod, rangeMod, miscMod, heatMod);;
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
            gator.computeGator(gunneryMod, attackerMod, targetMod, otherMod, rangeMod, miscMod, heatMod);;
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
            gator.computeGator(gunneryMod, attackerMod, targetMod, otherMod, rangeMod, miscMod, heatMod);;
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
            gator.computeGator(gunneryMod, attackerMod, targetMod, otherMod, rangeMod, miscMod, heatMod);;
            UpdateTargetBlock(gator.GetValue());
        }

        private void Heat0to7_Checked(object sender, RoutedEventArgs e)
        {
            heatMod.SetValue(0);
            UpdateValues();
        }

        private void Heat8to12_Checked(object sender, RoutedEventArgs e)
        {
            heatMod.SetValue(1);
            UpdateValues();
        }

        private void Heat13to16_Checked(object sender, RoutedEventArgs e)
        {
            heatMod.SetValue(2);
            UpdateValues();
        }

        private void Heat17to23_Checked(object sender, RoutedEventArgs e)
        {
            heatMod.SetValue(3);
            UpdateValues();
        }

        private void Heat24Plus_Checked(object sender, RoutedEventArgs e)
        {
            heatMod.SetValue(4);
            UpdateValues();
        }

        private void PulseCheck_Checked(object sender, RoutedEventArgs e)
        {
            miscMod.Decrement();
            miscMod.Decrement();
            UpdateValues();
        }

        private void PulseCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            miscMod.Increment();
            miscMod.Increment();
            UpdateValues();
        }

        private void TargetingComputerCheck_Checked(object sender, RoutedEventArgs e)
        {
            miscMod.Decrement();
            UpdateValues();
        }

        private void TargetingComputerCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            miscMod.Increment();
            UpdateValues();
        }

        private void ShoulderCheck_Checked(object sender, RoutedEventArgs e)
        {
            miscMod.Increment();
            miscMod.Increment();
            miscMod.Increment();
            miscMod.Increment();
            UpdateValues();
        }

        private void ShoulderCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            miscMod.Decrement();
            miscMod.Decrement();
            miscMod.Decrement();
            miscMod.Decrement();
            UpdateValues();
        }

        private void LActuatorCheck_Checked(object sender, RoutedEventArgs e)
        {
            miscMod.Increment();
            UpdateValues();
        }

        private void LActuatorCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            miscMod.Decrement();
            UpdateValues();
        }

        private void SensorHit_Checked(object sender, RoutedEventArgs e)
        {
            miscMod.Increment();
            miscMod.Increment();
            UpdateValues();
        }

        private void SensorHit_Unchecked(object sender, RoutedEventArgs e)
        {
            miscMod.Decrement();
            miscMod.Decrement();
            UpdateValues();
        }

        private void Indirect_Checked(object sender, RoutedEventArgs e)
        {
            miscMod.Increment();
            UpdateValues();
        }

        private void Indirect_Unchecked(object sender, RoutedEventArgs e)
        {
            miscMod.Decrement();
            UpdateValues();
        }

        private void Secondary_Checked(object sender, RoutedEventArgs e)
        {
            miscMod.Increment();
            UpdateValues();
        }

        private void Secondary_Unchecked(object sender, RoutedEventArgs e)
        {
            miscMod.Decrement();
            UpdateValues();
        }

        private void UActuatorHit_Checked(object sender, RoutedEventArgs e)
        {
            miscMod.Increment();
            UpdateValues();
        }

        private void UActuatorHit_Unchecked(object sender, RoutedEventArgs e)
        {
            miscMod.Decrement();
            UpdateValues();
        }

        private void LBXCluster_Checked(object sender, RoutedEventArgs e)
        {
            miscMod.Decrement();
            UpdateValues();
        }

        private void LBXCluster_Unchecked(object sender, RoutedEventArgs e)
        {
            miscMod.Increment();
            UpdateValues();
        }

        private void MRMRLCluster_Checked(object sender, RoutedEventArgs e)
        {
            miscMod.Increment();
            UpdateValues();
        }

        private void MRMRLCluster_Unchecked(object sender, RoutedEventArgs e)
        {
            miscMod.Decrement();
            UpdateValues();
        }

        private void ImmobileCheck_Checked(object sender, RoutedEventArgs e)
        {
            miscMod.Decrement();
            miscMod.Decrement();
            miscMod.Decrement();
            miscMod.Decrement();
            UpdateValues();
        }

        private void ImmobileCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            miscMod.Increment();
            miscMod.Increment();
            miscMod.Increment();
            miscMod.Increment();
            UpdateValues();
        }

        private void AimedShotCheck_Checked(object sender, RoutedEventArgs e)
        {
            miscMod.Decrement();
            miscMod.Decrement();
            miscMod.Decrement();
            miscMod.Decrement();
            UpdateValues();
        }

        private void AimedShotCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            miscMod.Increment();
            miscMod.Increment();
            miscMod.Increment();
            miscMod.Increment();
            UpdateValues();
        }

        private void AimedShotHeadCheck_Checked(object sender, RoutedEventArgs e)
        {
            miscMod.Increment();
            miscMod.Increment();
            miscMod.Increment();
            UpdateValues();
        }

        private void AimedShotHeadCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            miscMod.Decrement();
            miscMod.Decrement();
            miscMod.Decrement();
            UpdateValues();
        }

        private void RF2or3_Checked(object sender, RoutedEventArgs e)
        {
            RF4or5.IsChecked = false;
            RF6or7.IsChecked = false;
            RF8or9.IsChecked = false;
            rapidFireTargetRoll = 2;
        }

        private void RF2or3_Unchecked(object sender, RoutedEventArgs e)
        {
            rapidFireTargetRoll = 0;
        }

        private void RF4or5_Checked(object sender, RoutedEventArgs e)
        {
            RF2or3.IsChecked = false;
            RF6or7.IsChecked = false;
            RF8or9.IsChecked = false;
            rapidFireTargetRoll = 3;
        }

        private void RF4or5_Unchecked(object sender, RoutedEventArgs e)
        {
            rapidFireTargetRoll = 0;
        }

        private void RF6or7_Checked(object sender, RoutedEventArgs e)
        {
            RF2or3.IsChecked = false;
            RF4or5.IsChecked = false;
            RF8or9.IsChecked = false;
            rapidFireTargetRoll = 4;
        }

        private void RF6or7_Unchecked(object sender, RoutedEventArgs e)
        {
            rapidFireTargetRoll = 0;
        }

        private void RF8or9_Checked(object sender, RoutedEventArgs e)
        {
            RF2or3.IsChecked = false;
            RF4or5.IsChecked = false;
            RF6or7.IsChecked = false;
            rapidFireTargetRoll = 5;
        }

        private void RF8or9_Unchecked(object sender, RoutedEventArgs e)
        {
            rapidFireTargetRoll = 0;
        }

        private void LargeTarget_Checked(object sender, RoutedEventArgs e)
        {
            miscMod.Decrement();
            miscMod.Decrement();
            UpdateValues();
        }

        private void LargeTarget_Unchecked(object sender, RoutedEventArgs e)
        {
            miscMod.Increment();
            miscMod.Increment();
            UpdateValues();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTApper
{
    public class Dice
    {
        int diceroll;
        Random rand;

        public Dice() 
        { 
            this.rand = new Random();
            this.diceroll = 0;
        }

        public Dice(int n)
        {
            this.rand = new Random();
            this.diceroll = n;
        }

        public int GetValue()
        {
            return this.diceroll;
        }

        public void SetValue(int n) {
            this.diceroll = n;
        }

        public void RollDice()
        {
            this.rand = new Random();
            this.diceroll = rand.Next(1, 7);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTApper
{
    public class Modifier
    {
        private int mod;

        public Modifier()
        {
            this.mod = 0;
        }

        public Modifier(int n)
        {
            this.mod = n;
        }

        public int GetValue()
        {
            return this.mod;
        }

        public void SetValue(int n)
        {
            this.mod = n;
        }

        public void Increment()
        {
            this.mod++;
        }

        public void Decrement()
        {
            this.mod--;
        }

    }
}

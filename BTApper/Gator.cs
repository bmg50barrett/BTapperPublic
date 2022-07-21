using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTApper
{
    public class Gator
    {
        int gatorMod;

        public Gator()
        {
            this.gatorMod = 0;
        }

        public Gator(int n)
        {
            this.gatorMod = n;
        }

        public int GetValue()
        {
            return this.gatorMod;
        }

        public void SetValue(int n)
        {
            this.gatorMod = n;
        }

        public void computeGator(Modifier g, Modifier a, Modifier t, Modifier o, Modifier r, Modifier misc, Modifier heat)
        {
            this.SetValue(g.GetValue() + a.GetValue() + t.GetValue() + o.GetValue() + r.GetValue() + misc.GetValue() + heat.GetValue());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTApper
{
    internal class Weapon
    {
        private int maxShots = 1;
        private int shotGrouping = 1;
        private int damage = 1;
        private int heat = 1;
        private bool isCluster = false;
        private bool isMulti = false;
        private String note = "";
        private int mmlSRMshotGrouping = 1;
        private int mmlLRMshotGrouping = 1;
        private int mmlSRMdmg = 1;
        private int mmlLRMdmg = 2;

        public Weapon(int s, int sg, int h, int d)
        {
            this.maxShots = s;
            this.shotGrouping = sg;
            this.heat = h;
            this.damage = d;
        }

        //Used my MML
        public Weapon(int s, int srmGrouping, int lrmGrouping, int h, int srmdmg, int lrmdmg)
        {
            this.maxShots = s;
            this.mmlSRMshotGrouping = srmGrouping;
            this.mmlLRMshotGrouping = lrmGrouping;
            this.heat = h;
            this.mmlSRMdmg = srmdmg;
            this.mmlLRMdmg = lrmdmg;
        }

        public Weapon (int shots, int heat, int damage)
        {
            this.maxShots = shots;
            this.heat = heat;
            this.damage = damage;
            this.isCluster = false;
            this.isMulti = false;
        }

        public Weapon(int shots, int heat, int damage, bool isCluster, bool isMulti)
        {
            this.maxShots = shots;
            this.heat = heat;
            this.damage = damage;
            this.isCluster = isCluster;
            this.isMulti = isMulti;
        }

        public Weapon( int shots, int heat, int damage, bool isCluster, bool isMulti, String note)
        {
            this.maxShots = shots;
            this.heat = heat;
            this.damage = damage;
            this.isCluster = isCluster;
            this.isMulti = isMulti;
            this.note = note;
        }

        public int GetMaxShots()
        {
            return maxShots;
        }

        public int GetGrouping()
        {
            return this.GetGrouping();
        }

        public int GetHeat()
        {
            return this.heat;
        }

        public int GetDamage()
        {
            return this.damage;
        }

        public bool GetCluster()
        {
            return isCluster;
        }

        public bool GetMulti()
        {
            return isMulti;
        }

        public String GetNote()
        {
            return note;
        }

    }
}

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

        private bool isMML = false;
        private int mmlSRMShotsPerGroup = 0;
        private int mmlLRMShotsPerGroup = 0;
        private int mmlSRMdmg = 0;
        private int mmlLRMdmg = 0;

        private bool isLBX = false;
        private int lbxShotPerGroup = 1;
        private int lbxShotDamage = 1;

        public Weapon() { }

        public Weapon(int maxShots, int shotGroupSize, int heat, int damage)
        {
            this.maxShots = maxShots;
            this.shotGrouping = shotGroupSize;
            this.heat = heat;
            this.damage = damage;
        }

        //Used my MML
        /// <summary>
        /// This method builds a MML weapon.
        /// </summary>
        /// <param name="isMML">Boolean flag to determine if object is a MML.</param>
        /// <param name="maxShots">Number of missiles fired from the weapon in one shot.</param>
        /// <param name="srmGrouping">Grouping size if SRM missiles are used.</param>
        /// <param name="lrmGrouping">Grouping size if LRM missiles are used.</param>
        /// <param name="heat">Amount of heat generated from one round of firing.</param>
        /// <param name="srmdmg">Damage of SRM missiles, per missile.</param>
        /// <param name="lrmdmg">Damage of LRM missiles, per missile.</param>
        public Weapon(bool isMML, int maxShots, int srmGrouping, int lrmGrouping, int heat, int srmdmg, int lrmdmg)
        {
            this.isMML = isMML;
            this.maxShots = maxShots;
            this.mmlSRMShotsPerGroup = srmGrouping;
            this.mmlLRMShotsPerGroup = lrmGrouping;
            this.heat = heat;
            this.mmlSRMdmg = srmdmg;
            this.mmlLRMdmg = lrmdmg;

        }

        //Used my LBX

        public Weapon(bool isLBX, int maxShots, int heat, int dmg)
        {
            this.isLBX = isLBX;
            this.maxShots = maxShots;
            this.heat = heat;
            this.damage = dmg;

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
            return this.maxShots;
        }

        public int GetGrouping()
        {
            return this.shotGrouping;
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
            return this.isCluster;
        }

        public bool GetMulti()
        {
            return this.isMulti;
        }

        public String GetNote()
        {
            return this.note;
        }

        public bool GetMML()
        {
            return this.isMML;
        }

        public int GetMMLSRMShotsPerGroup()
        {
            return this.mmlSRMShotsPerGroup;
        }

        public int GetMMLLRMShotsPerGroup()
        {
            return this.mmlLRMShotsPerGroup;
        }

        public int GetMMLSRMdmg()
        {
            return this.mmlSRMdmg;
        }

        public int GetMMLLRMdmg()
        {
            return this.mmlLRMdmg;
        }

        public bool GetLBX()
        {
            return this.isLBX;
        }

        public int GetLBXShotsPerGroup()
        {
            return this.lbxShotPerGroup;
        }

        public int GetLBXShotDamage()
        {
            return this.lbxShotDamage;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTApper
{
    internal class Weapon
    {
        private int shots = 1;
        private int damage = 1;
        private int heat = 1;
        private bool isCluster = false;
        private bool isMulti = false;
        private bool isHeater = false;
        private String weaponName = "None";
        private String note = "";

        public Weapon() { }

        public Weapon (int shots, int heat, int damage)
        {
            this.shots = shots;
            this.heat = heat;
            this.damage = damage;
            this.isCluster = false;
            this.isMulti = false;
            this.isHeater = false;
        }

        public Weapon(String name, int shots, int heat, int damage)
        {
            this.weaponName = name;
            this.shots = shots;
            this.heat = heat;
            this.damage = damage;
            isCluster = false;
            isMulti = false;
            isHeater = false;
        }

        public Weapon(int shots, int heat, int damage, bool isCluster, bool isMulti)
        {
            this.shots = shots;
            this.heat = heat;
            this.damage = damage;
            this.isCluster = isCluster;
            this.isMulti = isMulti;
        }

        public Weapon(int shots, int heat, int damage, bool isCluster, bool isMulti,bool isHeater)
        {
            this.shots = shots;
            this.heat = heat;
            this.damage = damage;
            this.isCluster = isCluster;
            this.isMulti = isMulti;
            this.isHeater = isHeater;
        }

        public Weapon(String name, int shots, int heat, int damage, bool isCluster, bool isMulti, bool isHeater)
        {
            this.weaponName = name;
            this.shots = shots;
            this.heat = heat;
            this.damage = damage;
            this.isCluster = isCluster;
            this.isMulti = isMulti;
            this.isHeater = isHeater;
        }
        public Weapon(String name, int shots, int heat, int damage, bool isCluster, bool isMulti, bool isHeater, String note)
        {
            this.weaponName = name;
            this.shots = shots;
            this.heat = heat;
            this.damage = damage;
            this.isCluster = isCluster;
            this.isMulti = isMulti;
            this.isHeater = isHeater;
            this.note = note;
        }

        public String GetName()
        {
            return weaponName;
        }

        public int GetShots()
        {
            return shots;
        }

        public void SetShots(int n)
        {
            this.shots = n;
        }

        public void IncrementShots()
        {
            this.shots++;
        }

        public void DecrementShots()
        {
            this.shots--;
        }

        public int GetHeat()
        {
            return this.heat;
        }

        public void SetHeat(int n)
        {
            this.heat = n;
        }

        public void IncrementHeat()
        {
            this.heat++;
        }

        public void DecrementHeat()
        {
            this.heat--;
        }

        public int GetDamage()
        {
            return this.damage;
        }

        public void SetDamage(int n)
        { 
            this.damage = n;
        }

        public void IncrementDamage()
        {
            this.damage++;
        }

        public void DecrementDamage()
        {
            this.damage--;
        }

        public bool GetCluster()
        {
            return isCluster;
        }

        public void SetCluster(bool b)
        {
            isCluster = b;
        }

        public bool GetMulti()
        {
            return isMulti;
        }

        public void SetMulti(bool b)
        {
            isMulti = b;
        }

        public bool GetHeater()
        {
            return isHeater;
        }

        public void SetHeater(bool b)
        {
            isHeater = b;
        }

        public String GetNote()
        {
            return note;
        }

        public void SetNote(String s)
        {
            this.note = s;
        }
    }
}

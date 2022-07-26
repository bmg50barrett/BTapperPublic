﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTApper
{
    internal class ClusterHits
    {
        /*Array with Cluster hits table loaded. To make array indexing easier later, all values have been inclued,
          even values that don't exist on the cluster hits table in the book. This will allow 2D6 rolls and weapon sizes to be
          passed directly into the array as indexes. */

        int[,] clusterArray = { /*Wep Size    0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36,37,38,39,40*/
                                /*Roll 0*/  { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                                /*Roll 1*/  { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                                /*Roll 2*/  { 0,0,1,1,1,1,2,2,3,3,3,4,4,4,5,5,5,5,6,6,6,7,7,7,8,8,9,9,9,10,10,0,0,0,0,0,0,0,0,0,12 },
                                /*Roll 3*/  { 0,0,1,1,2,2,2,2,3,3,3,4,4,4,5,5,5,5,6,6,6,7,7,7,8,8,9,9,9,10,10,0,0,0,0,0,0,0,0,0,12 },
                                /*Roll 4*/  { 0,0,1,1,2,2,3,3,4,4,4,5,5,5,6,6,7,7,8,8,9,9,9,10,10,10,11,11,11,12,12,0,0,0,0,0,0,0,0,0,18 },
                                /*Roll 5*/  { 0,0,1,2,2,3,3,4,4,5,6,7,8,8,9,9,10,10,11,11,12,13,14,15,16,16,17,17,17,18,18,0,0,0,0,0,0,0,0,0,24 },
                                /*Roll 6*/  { 0,0,1,2,2,3,4,4,5,5,6,7,8,8,9,9,10,10,11,11,12,13,14,15,16,16,17,17,17,18,18,0,0,0,0,0,0,0,0,0,24 },
                                /*Roll 7*/  { 0,0,1,2,3,3,4,4,5,5,6,7,8,8,9,9,10,10,11,11,12,13,14,15,16,16,17,17,17,18,18,0,0,0,0,0,0,0,0,0,24 },
                                /*Roll 8*/  { 0,0,2,2,3,3,4,4,5,5,6,7,8,8,9,9,10,10,11,11,12,13,14,15,16,16,17,17,17,18,18,0,0,0,0,0,0,0,0,0,24 },
                                /*Roll 9*/  { 0,0,2,2,3,4,5,6,6,7,8,9,10,11,11,12,13,14,14,15,16,17,18,19,20,21,21,22,23,23,24,0,0,0,0,0,0,0,0,0,32 },
                                /*Roll 10*/ { 0,0,2,3,3,4,5,6,6,7,8,9,10,11,11,12,13,14,14,15,16,17,18,19,20,21,21,22,23,23,24,0,0,0,0,0,0,0,0,0,32 },
                                /*Roll 11*/ { 0,0,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,0,0,0,0,0,0,0,0,0,40 },
                                /*Roll 12*/ { 0,0,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,0,0,0,0,0,0,0,0,0,40 }
                                };

        public ClusterHits() { }

        public int GetClusterHits(int roll, int wepsize)
        {
            return clusterArray[roll, wepsize];
        }
    }
}

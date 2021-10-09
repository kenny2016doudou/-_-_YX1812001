using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZKZDLQ.SystemTest
{
    public class MyPoint
    {

        private int lastBigNum; //后一个比前一个大的个数

        public int LastBigNum
        {
            get { return lastBigNum; }
            set { lastBigNum = value; }
        }
        private int firstBigNum; //前一个比后一个大的个数

        public int FirstBigNum
        {
            get { return firstBigNum; }
            set { firstBigNum = value; }
        }
        private int last2firstNum; //lastBigNum - firstBigNum

        public int Last2firstNum
        {
            get { return last2firstNum; }
            set { last2firstNum = value; }
        }
        private int OneIndex; //当前组的第一个索引

        public int OneIndex1
        {
            get { return OneIndex; }
            set { OneIndex = value; }
        }

        public MyPoint(int lastBigNum, int firstBigNum, int last2firstNum, int OneIndex)
        {
            this.lastBigNum = lastBigNum;
            this.firstBigNum = firstBigNum;
            this.last2firstNum = last2firstNum;
            this.OneIndex = OneIndex;
        }


    }
}

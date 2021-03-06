﻿using PrisonArchitect.Helper;
using PrisonArchitect.PrisonFile.HelperBlocks;

namespace PrisonArchitect.PrisonFile.Blocks
{
    public class Finance : HandledBlock
    {
        #region Variables

        public float Balance
        {
            get { return Variables["Balance"].SafeParse<float>(); }
            set { Variables["Balance"] = value; }
        }

        public int LastDay
        {
            get { return Variables["LastDay"].SafeParse<int>(); }
            set { Variables["LastDay"] = value; }
        }

        public int LastHour
        {
            get { return Variables["LastHour"].SafeParse<int>(); }
            set { Variables["LastHour"] = value; }
        }

        public int SalePrice
        {
            get { return Variables["SalePrice"].SafeParse<int>(); }
            set { Variables["SalePrice"] = value; }
        }

        public int BankLoan
        {
            get { return Variables["BankLoan"].SafeParse<int>(); }
            set { Variables["BankLoan"] = value; }
        }

        public float BankCreditRating
        {
            get { return Variables["BankCreditRating"].SafeParse<float>(); }
            set { Variables["BankCreditRating"] = value; }
        }

        public int Ownership
        {
            get { return Variables["Ownership"].SafeParse<int>(); }
            set { Variables["Ownership"] = value; }
        }

        public int ExportsToday
        {
            get { return Variables["ExportsToday"].SafeParse<int>(); }
            set { Variables["ExportsToday"] = value; }
        }

        public int ExportsYesterday
        {
            get { return Variables["ExportsYesterday"].SafeParse<int>(); }
            set { Variables["ExportsYesterday"] = value; }
        }

        #endregion Variables
    }
}
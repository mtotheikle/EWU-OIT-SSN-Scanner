using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EWUScanner
{
    public class CreditData
    {
        public enum CCCode { None = 0, LOW, HIGH };

        private int count;
        private int retCode;
        private CCCode priority;
        private int visaCount;
        private int mcCount;
        private int amexCount;
        private int disCount;
        private int dinnCount;
        private int jcbCount;

        public CreditData(int count, int retCode, int visaCount, int mcCount, 
            int amexCount, int disCount, int dinnCount, int jcbCount)
        {
            this.count = count;
            this.retCode = retCode;
            this.priority = (CCCode)retCode;
            this.visaCount = visaCount;
            this.mcCount = mcCount;
            this.amexCount = amexCount;
            this.disCount = disCount;
            this.dinnCount = dinnCount;
            this.jcbCount = jcbCount;
        }

        public int Count
        {
            get { return this.count; }
            set { this.count = value; }
        }

        public int RetCode
        {
            get { return this.retCode; }
            set { this.retCode = value; }
        }

        public CCCode Priority
        {
            get {return priority;}
            set { this.priority = value; }
        }

        public int VisaCount
        {
            get { return this.visaCount; }
            set { this.visaCount = value; }
        }

        public int MC_Count
        {
            get { return this.mcCount; }
            set { this.mcCount = value; }
        }

        public int AmexCount
        {
            get { return this.amexCount; }
            set { this.amexCount = value; }
        }

        public int DisCount
        {
            get { return this.disCount; }
            set { this.disCount = value; }
        }

        public int DinnCount
        {
            get { return this.dinnCount; }
            set { this.dinnCount = value; }
        }

        public int JCB_Count
        {
            get { return this.jcbCount; }
            set { this.jcbCount = value; }
        }

    }
}
 
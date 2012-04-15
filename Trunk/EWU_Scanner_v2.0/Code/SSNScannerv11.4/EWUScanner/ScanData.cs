using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EWUScanner
{
    public class ScanData
    {
        public enum Code { NONE = 0, LOW, MEDIUM, HIGH };

        private int count;
        private int retCode;
        private Code priority;
        private int patternD9;
        private int patternD3D2D4;

        public ScanData(int count, int retCode, int d9, int d324)
        {
            this.count = count;
            this.retCode = retCode;
            this.priority = (Code)retCode;
            this.patternD9 = d9;
            this.patternD3D2D4 = d324;
        }

        public int Pattern_D9
        {
            get { return patternD9; }
            set { patternD9 = value; }
        }

        public int Pattern_D3D2D4
        {
            get { return patternD3D2D4; }
            set { patternD3D2D4 = value; }
        }

        public ScanData(): this(0, 0, 0, 0)
        {}
        
        public int Count
        {
            get { return count; }
            set { count = value; }
        }

        public int RetCode
        {
            get { return retCode; }
            set { priority = (Code)(retCode = value); }
        }

        public Code Priority
        {
            get { return priority; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using TMV.Utilities;

namespace TMV.Data.Entities
{
    public class AdminWorkFlowInfo
    {
        private int _workFlowId = Null.NullInteger;
        private int _fromId = Null.NullInteger;
        private int _toId = Null.NullInteger;

        #region Public Properties
        public int WorkFlowID
        {
            get { return _workFlowId; }
            set { _workFlowId = value; }
        }

        public int FromID
        {
            get { return _fromId; }
            set { _fromId = value; }
        }

        public int ToID
        {
            get { return _toId; }
            set { _toId = value; }
        }

        #endregion
    }
}

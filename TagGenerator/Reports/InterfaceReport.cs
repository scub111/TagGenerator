using System;
using System.Collections.Generic;
using System.Reflection;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.Xpo;
using DevExpress.XtraReports.UI;
using RapidInterface;

namespace TagGenerator
{
    public partial class InterfaceReport : DBViewBase
    {
        #region Contstructors

        public InterfaceReport()
        {
            InitializeComponent();
            Reports = new List<XtraReportEx>(10);
        }
        #endregion

        #region Properties
        private List<XtraReportEx> _ListReport;
        /// <summary>
        /// Список объектов XtraReport.
        /// </summary>
        public List<XtraReportEx> Reports
        {
            get { return _ListReport; }
            set { _ListReport = value; }
        }
        #endregion

        #region Metods virtual
        #endregion

        #region Metods overrided
        /*
        public override void RefreshDataBegin(bool bForceReload)
        {
            base.RefreshDataBegin(bForceReload);
            ReloadTables(uowBase, bForceReload);
            for (int i = 0; i < Reports.Count; i++)
                Reports[i].CreateDocument();

        }

        public override void ChangedData()
        {
            base.ChangedData();
            for (int i = 0; i < Reports.Count; i++)
                Reports[i].IsDataChanged = true;
        }
             */        
        #endregion       
 
        #region Metods event handler
        private void InterfaceReport_Load(object sender, EventArgs e)
        {
            Type myType = GetType();
            FieldInfo[] myFieldInfo = myType.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            for (int i = 0; i < myFieldInfo.Length; i++)
                if (myFieldInfo[i].FieldType.BaseType == typeof(XtraReportEx))
                {
                    XtraReportEx repObj = myFieldInfo[i].GetValue(this) as XtraReportEx;
                    Reports.Add(repObj);
                }
            for (int i = 0; i < Reports.Count; i++)
            {
                Type myTypeInt = Reports[i].GetType();
                FieldInfo[] myFieldInfoInt = myTypeInt.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

                for (int j = 0; j < myFieldInfoInt.Length; j++)
                    if (myFieldInfoInt[j].FieldType == typeof(XPCollection))
                    {
                        XPCollection xpcObj = myFieldInfoInt[j].GetValue(Reports[i]) as XPCollection;
                        //xpcObj.Session = uowBase;
                        //xpcObj.DeleteObjectOnRemove = true;
                        //CollectionViewDynamics.Add(new XPChangeCollection(xpcObj));
                    }
            }
        }
        #endregion

        private void rpcBase_MouseMove(object sender, MouseEventArgs e)
        {
            if (Cursor != Cursors.Default)
                Cursor = Cursors.Default;
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            /*
            if (Reports.Count > 0)
                Reports[0].ShowRibbonDesigner();
             */
        }
    }
}

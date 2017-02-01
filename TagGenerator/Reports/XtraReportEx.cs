using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.Parameters;

namespace TagGenerator
{
    public class XtraReportEx : XtraReport
    {
        #region Contructors
        public XtraReportEx()
        {
            this._IsInited = false;
            this._IsDataChanged = false;
            this.parInfo = new ParameterInfo();
            this.ParametersRequestSubmit += new EventHandler<ParametersRequestEventArgs>(MyXtraReport_ParametersRequestSubmit);
            this.BeforePrint += new System.Drawing.Printing.PrintEventHandler(MyXtraReport_BeforePrint);
        }

        #endregion

        #region Classes
        /// <summary>
        /// Класс для работы с параметрами отчета.
        /// </summary>
        public class ParameterInfo
        {
            public ParameterInfo()
            {
                Params = new List<Parameter>(2);
                ObjPasts = new List<object>(2);
            }

            List<Parameter> Params { get; set; }

            List<object> ObjPasts { get; set; }

            /// <summary>
            /// Добавление нового компонента.
            /// </summary>
            /// <param name="Parameter"></param>
            public void Add(Parameter Parameter)
            {
                Params.Add(Parameter);
                ObjPasts.Add(Parameter.Value);
            }

            /// <summary>
            /// Обновление списка.
            /// </summary>
            void RefreshParams()
            {
                for (int i = 0; i < Params.Count; i++)
                    ObjPasts[i] = Params[i].Value;
            }

            /// <summary>
            /// Расчет изменения.
            /// </summary>
            /// <returns></returns>
            public bool CalculateChanges()
            {
                bool result = false;

                for (int i = 0; i < Params.Count; i++)
                    if (ObjPasts[i] != Params[i].Value)
                    {
                        result = true;
                        break;
                    }

                RefreshParams();

                return result;
            }
        }
        #endregion

        #region Properties
        bool _IsInited;
        /// <summary>
        /// Инициализация отчета.
        /// </summary>
        public bool IsInited
        {
            get { return _IsInited; }
            set { _IsInited = value; }
        }

        bool _IsDataChanged;
        private TopMarginBand topMarginBand1;
        private DetailBand detailBand1;
        private BottomMarginBand bottomMarginBand1;
        /// <summary>
        /// Изменение данных.
        /// </summary>
        public bool IsDataChanged
        {
            get { return _IsDataChanged; }
            set { _IsDataChanged = value; }
        }
        #endregion

        #region Variables
        /// <summary>
        /// Объект для отслеживания изменений параметров.
        /// </summary>
        ParameterInfo parInfo;

        #region Variables
        #endregion

        #endregion

        #region Methods virtual
        /// <summary>
        /// Обновление данных.
        /// </summary>
        public virtual void RefreshData()
        {
        }

        /// <summary>
        /// Инициализация данных.
        /// </summary>
        public virtual void Init()
        {
            for (int i = 0; i < Parameters.Count; i++)
                parInfo.Add(Parameters[i]);
        }
        #endregion

        #region Metods event handler
        void MyXtraReport_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {     
            if (!IsInited)
            {
                IsInited = true;
                Init();
            }
            RefreshData();
        }

        void MyXtraReport_ParametersRequestSubmit(object sender, ParametersRequestEventArgs e)
        {
            //RefreshData();
        }
        #endregion

        private void InitializeComponent()
        {
            this.topMarginBand1 = new DevExpress.XtraReports.UI.TopMarginBand();
            this.detailBand1 = new DevExpress.XtraReports.UI.DetailBand();
            this.bottomMarginBand1 = new DevExpress.XtraReports.UI.BottomMarginBand();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // topMarginBand1
            // 
            this.topMarginBand1.Name = "topMarginBand1";
            // 
            // detailBand1
            // 
            this.detailBand1.Name = "detailBand1";
            // 
            // bottomMarginBand1
            // 
            this.bottomMarginBand1.Name = "bottomMarginBand1";
            // 
            // XtraReportEx
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.topMarginBand1,
            this.detailBand1,
            this.bottomMarginBand1});
            this.Version = "13.1";
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
        }
    }
}

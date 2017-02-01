using System;
using System.Collections.Generic;
using System.Linq;
using RapidInterface;
using System.ComponentModel;
using ArchestrA.GRAccess;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Threading;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;

namespace TagGenerator
{
    [DBAttribute(Caption = "Тип объекта", IconFile = "ObjectType.png")]
    public partial class ObjectTypeView : DBViewInterface
    {
        /// <summary>
        /// Типы данных.
        /// </summary>
        public enum DataType
        {
            [Description("Boolean")]
            Boolean = 0,
            [Description("Integer")]
            Integer = 1,
            [Description("Float")]
            Float = 2,
            [Description("Double")]
            Double = 3,
            [Description("String")]
            String = 4,
            [Description("Time")]
            Time = 5
        }

        /// <summary>
        /// Типы новления.
        /// </summary>
        public enum UpdateType
        {
            [Description("Update")]
            Update = 0,
            [Description("Create")]
            Create = 1,
            [Description("Recreate")]
            Recreate = 2
        }

        public ObjectTypeView()
        {
            InitializeComponent();

            this.repDataType.DataSource = EnumHelper.ToExtendedList<int>(typeof(DataType));

            StyleFormatCondition styleFormatCondition1 = new DevExpress.XtraGrid.StyleFormatCondition();
            styleFormatCondition1.Appearance.BackColor = System.Drawing.Color.LightGray;
            styleFormatCondition1.Appearance.Options.UseBackColor = true;
            styleFormatCondition1.Condition = DevExpress.XtraGrid.FormatConditionEnum.Expression;
            styleFormatCondition1.Expression = "[Basic]";
            ObjectTypeAddonCollectionGridView1.FormatConditions.Add(styleFormatCondition1);

            StyleFormatCondition styleFormatCondition2 = new DevExpress.XtraGrid.StyleFormatCondition();
            styleFormatCondition2.Appearance.BackColor = System.Drawing.Color.LightGray;
            styleFormatCondition2.Appearance.Options.UseBackColor = true;
            styleFormatCondition2.Condition = DevExpress.XtraGrid.FormatConditionEnum.Expression;
            styleFormatCondition2.Expression = "[Basic]";
            ObjectTypeAddonCollectionGridView2.FormatConditions.Add(styleFormatCondition2);

            workerImport = new BackgroundWorker();
            workerImport.DoWork += workerImport_DoWork;
            workerImport.WorkerReportsProgress = true;
            workerImport.ProgressChanged += workerImport_ProgressChanged;
        }

        /// <summary>
        /// Фоновый поток импорта информации в галактику.
        /// </summary>
        BackgroundWorker workerImport;

        /// <summary>
        /// Сообщение от фонового потока импорта к интерфейсу программы.
        /// </summary>
        string workerMessage;

        /// <summary>
        /// Процент выполнения импорта.
        /// </summary>
        int progressPossition;

        /// <summary>
        /// Получение типа.
        /// </summary>
        public static MxDataType GetDataType(int type)
        {
            switch (type)
            {
                case 0: return MxDataType.MxBoolean;
                case 1: return MxDataType.MxInteger;
                case 2: return MxDataType.MxFloat;
                case 3: return MxDataType.MxDouble;
                case 4: return MxDataType.MxString;
                case 5: return MxDataType.MxTime;
                default: return MxDataType.MxNoData;
            }
        }

        /// <summary>
        /// Получение типа.
        /// </summary>
        public static void PutMxValue(ref MxValue valueObj, MxDataType type, string value)
        {
            switch (type)
            {
                case MxDataType.MxBoolean:  valueObj.PutBoolean(bool.Parse(value)); break;
                case MxDataType.MxInteger:  valueObj.PutInteger(int.Parse(value)); break;
                case MxDataType.MxFloat:    valueObj.PutFloat(float.Parse(value)); break;
                case MxDataType.MxDouble:   valueObj.PutDouble(double.Parse(value)); break;
                case MxDataType.MxString:   valueObj.PutString(value); break;
                //case MxDataType.MxTime:     valueObj.PutTime(DateTime.Parse(value)); break;
                default:                    valueObj.PutString(value); break;
            }
        }

        private void btnImportToArchestrA_Click(object sender, EventArgs e)
        {
            meResultArchestrA.Text = "";
            workerImport.RunWorkerAsync();
        }

        void Report(string message)
        {
            workerImport.ReportProgress(0);
            workerMessage = message;
        }        

        void workerImport_DoWork(object sender, DoWorkEventArgs e)
        {
            DateTime t0 = DateTime.Now;
            int createSuccessCount = 0;
            int recreateSuccessCount = 0;
            int updateSuccessCount = 0;
            int faultCount = 0;
            int delay = 50;
            UpdateType updateType = UpdateType.Update;
            int index = 0;
            double progress = 0;
            int count = 0;
            try
            {
                //string nodeName = Environment.MachineName;
                string nodeName = Global.Default.varXml.SystemPlatform.NodeName;
                string galaxyName = Global.Default.varXml.SystemPlatform.GalaxyName;

                // create GRAccessAppClass object
                GRAccessApp grAccess = new GRAccessAppClass();

                Report(string.Format("Подключение к Galaxy ({0}/{1})...\r\n", nodeName, galaxyName));

                // try to get galaxy
                IGalaxies gals = grAccess.QueryGalaxies(nodeName);
                if (gals == null || grAccess.CommandResult.Successful == false)
                {
                    Report(string.Format("Error!\r\nCustomMessage - {0}\r\nCommandResult - {1}\r\n", grAccess.CommandResult.CustomMessage, grAccess.CommandResult.Text));
                    return;
                }
                else
                {
                    Report("Подключен.\r\n");
                }
                IGalaxy galaxy = gals[galaxyName];

                string login = Global.Default.varXml.SystemPlatform.Login;
                string password = Global.Default.varXml.SystemPlatform.Password;

                Report(string.Format("Авторизация {0}/{1}...\r\n", login, password));

                // log in
                galaxy.Login(login, password);
                ICommandResult cmd;
                cmd = galaxy.CommandResult;
                if (!cmd.Successful)
                {
                    Report("Ошибка авторизации:" + cmd.Text + " : " + cmd.CustomMessage + "\r\n");
                    return;
                }
                else
                {
                    Report("Авторизован.\r\n");
                }

                XPCollection<ObjectTypeAddonCollection> importFields = new XPCollection<ObjectTypeAddonCollection>();

                XPCollection<ObjectType> objTypes2 = new XPCollection<ObjectType>();
                ObjectType objType = (ObjectType)_dbInterface1.GetCurrentObject();
                ObjectType objTypeThreadSafe = null;

                foreach (ObjectTypeAddonCollection addon in importFields)
                {
                    if (addon.ObjectTypeOwner.Oid == objType.Oid)
                    {
                        objTypeThreadSafe = addon.ObjectTypeOwner;
                        break;
                    }
                }

                if (objTypeThreadSafe != null)
                {
                    string templateName = "";
                    IgObjects queryResult;
                    string[] tagnames = new string[1];

                    // Почему-то не работает стандартная выборка.
                    //importFields.Filter = CriteriaOperator.Parse("[ObjectTypeOwner] == {0}", objTypeThreadSafe);
                    importFields.Criteria = new BinaryOperator("ObjectTypeOwner", objTypeThreadSafe);
                    importFields.Filter = CriteriaOperator.Parse("[ArchestrAImport]");

                    count = importFields.Count;

                    templateName = objType.ArchestrATemplate;
                    tagnames[0] = templateName;

                    queryResult = galaxy.QueryObjectsByName(EgObjectIsTemplateOrInstance.gObjectIsInstance, ref tagnames);

                    // get the $UserDefined template
                    tagnames[0] = objType.ArchestrATemplate;

                    queryResult = galaxy.QueryObjectsByName(
                        EgObjectIsTemplateOrInstance.gObjectIsTemplate,
                        ref tagnames);

                    cmd = galaxy.CommandResult;
                    if (!cmd.Successful)
                        Report("Ошибка инициализации шаблона:" + cmd.Text + " : " + cmd.CustomMessage + "\r\n");

                    ITemplate template = (ITemplate)queryResult[1];

                    Report(string.Format("Обновляем шаблон {0}...\r\n", objType.ArchestrATemplate)); Thread.Sleep(delay);
                    template.CheckOut();

                    foreach (ObjectTypeAddonCollection field in importFields)
                    {
                        index++;
                        try
                        {
                            if (!string.IsNullOrEmpty(field.AttributeAddon))
                            {
                                IAttribute att = template.Attributes[field.AttributeAddon];
                                MxDataType type = GetDataType(field.DataType);
                                if (att == null)
                                {
                                    updateType = UpdateType.Create;
                                    createSuccessCount++;
                                    template.AddUDA(field.AttributeAddon, type, MxAttributeCategory.MxCategoryWriteable_USC_Lockable, MxSecurityClassification.MxSecurityOperate);
                                }
                                else
                                {
                                    if (att.DataType == type)
                                    {
                                        updateType = UpdateType.Update;
                                        updateSuccessCount++;
                                    }
                                    else
                                    {
                                        updateType = UpdateType.Recreate;
                                        recreateSuccessCount++;
                                        template.DeleteUDA(field.AttributeAddon);
                                        template.AddUDA(field.AttributeAddon, type, MxAttributeCategory.MxCategoryWriteable_USC_Lockable, MxSecurityClassification.MxSecurityOperate);
                                    }
                                }

                                att = template.Attributes[field.AttributeAddon];
                                att.Description = field.Comment;

                                string fieldValue = field.ArchestrAValue;
                                if (!string.IsNullOrEmpty(fieldValue))
                                {
                                    MxValue mxValue = new MxValueClass();
                                    PutMxValue(ref mxValue, type, fieldValue);
                                    att.SetValue(mxValue);
                                }        

                                progress = ((double)index / count * 1000);
                                progressPossition = (int)Math.Round(progress);

                                switch (updateType)
                                {
                                    case UpdateType.Update:
                                        Report(string.Format("Поле {0} ({1} / {2}) обновлено удачно.\r\n", field.AttributeAddon, type, fieldValue));
                                        break;
                                    case UpdateType.Create:
                                        Report(string.Format("Поле {0} ({1} / {2}) создано удачно.\r\n", field.AttributeAddon, type, fieldValue));
                                        break;
                                    case UpdateType.Recreate:
                                        Report(string.Format("Поле {0} ({1} / {2}) пересоздано удачно.\r\n", field.AttributeAddon, type, fieldValue));
                                        break;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Report(string.Format("У поля {0} есть проблема создания :( [{1} -> {2}] \r\n", templateName, ex.Message, ex.InnerException));
                            faultCount++;
                        }
                    }

                    template.Save();
                    template.CheckIn("Check in after adding.");
                    Report(string.Format("Обновлен.\r\n"));

                    galaxy.Logout();
                    Thread.Sleep(delay);
                    Report(string.Format("Отключение от галактики выполнено. \r\n"));
                    Thread.Sleep(delay);
                }
            }
            catch (Exception ex)
            {
                meResultArchestrA.Text = ex.Message + "->" + ex.InnerException + "\r\n";
            }
            TimeSpan diff = DateTime.Now - t0;
            Report(string.Format("Было потрачено времени: {0} c.\r\n", diff.ToString())); Thread.Sleep(delay);
            Report(string.Format("Среднее время на один объект: {0:0.#} c.\r\n", diff.TotalSeconds / count)); Thread.Sleep(delay);
            Report(string.Format("Удачно созданных: {0}.\r\n", createSuccessCount)); Thread.Sleep(delay);
            Report(string.Format("Удачно обновленных: {0}.\r\n", updateSuccessCount)); Thread.Sleep(delay);
            Report(string.Format("Удачно пересозданных: {0}.\r\n", recreateSuccessCount)); Thread.Sleep(delay);
            Report(string.Format("Неудачно выполненных команд: {0}. \r\n", faultCount)); Thread.Sleep(delay);
        }

        void workerImport_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            meResultArchestrA.Text += string.Format("{0}: {1}", DateTime.Now.ToLongTimeString(), workerMessage);
            meResultArchestrA.SelectionStart = meResultArchestrA.Text.Length;
            meResultArchestrA.ScrollToCaret();
            prbImport.Position = progressPossition;
        }

        private void ObjectTypeView_Load(object sender, EventArgs e)
        {
            tlHierarchy.ExpandAll();
        }

        private void _dbInterface1_CurrentObjectChanged(object sender, Event.CurrentObjectEventArgs args)
        {
            Global.Default.CurrentObjectType = (ObjectType)args.CurrentObject;
        }

        private void ObjectTypeAddonCollectionGridView1_ShowingEditor(object sender, CancelEventArgs e)
        {
            //e.Cancel = (bool)ObjectTypeAddonCollectionGridView1.GetRowCellValue(ObjectTypeAddonCollectionGridView1.FocusedRowHandle, "Basic");
            //e.Cancel = (bool)ObjectTypeAddonCollectionGridView1.GetRowCellValue(((GridView)sender).FocusedRowHandle, "Basic");

            ObjectTypeAddonCollection addon = (ObjectTypeAddonCollection)ObjectTypeAddonCollectionGridControl1.FocusedView.GetRow(((GridView)sender).FocusedRowHandle);
            e.Cancel = addon.Basic;
        }

        private void ObjectTypeAddonCollectionGridView2_ShowingEditor(object sender, CancelEventArgs e)
        {
            ObjectTypeAddonCollection addon = (ObjectTypeAddonCollection)tableGridControl1.FocusedView.GetRow(((GridView)sender).FocusedRowHandle);
            e.Cancel = addon.Basic;
        }
    }
}

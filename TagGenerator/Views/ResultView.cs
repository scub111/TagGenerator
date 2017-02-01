using System;
using System.Collections.Generic;
using System.Linq;
using RapidInterface;
using DevExpress.Xpo;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraSplashScreen;
using ArchestrA.GRAccess;
using System.ComponentModel;
using System.Threading;
using DevExpress.Data.Filtering;

namespace TagGenerator
{
    [DBAttribute(Caption = "Результат", IconFile = "Result.png")]
    public partial class ResultView : DBViewBase
    {
        public ResultView()
        {
            InitializeComponent();

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

        private void btnCalculate_Click(object sender, EventArgs e)
        {

            SplashScreenManager.ShowForm(typeof(WaitFormEx));
            SplashScreenManager.Default.SetWaitFormDescription("Происходит формирование списка тегов...");

            int count = 0;
            DateTime t0 = DateTime.Now;
            meResultTags.Text = "";
            string result = "";
            XPCollection<Object> objects = new XPCollection<Object>();
            foreach (Object obj in objects)
            {
                if (obj.ObjectTypeID != null)
                    for (int i = 0; i < obj.ObjectTypeID.ObjectTypeAddonCollection.Count; i++)
                    {
                        ObjectTypeAddonCollection addonCol = obj.ObjectTypeID.ObjectTypeAddonCollection[i];
                        if (addonCol.TagExport)
                        {
                            //result += string.Format("\"{0}{1}\",\"{2}{3}\"\r\n", obj.Attribute, addonCol.AttributeAddon, obj.ItemReference, addonCol.ItemReferenceAddon);
                            result += string.Format(obj.ObjectTypeID.RecordFormat + "\r\n", obj.Attribute, addonCol.AttributeAddon, obj.ItemReference, addonCol.ItemReferenceAddon);
                            count++;
                        }
                    }
            }

            // Очистка от послених переносов и вывод результата на элемент интерфейса.
            if (result.Length > 2)
                meResultTags.Text = result.Remove(result.Length - 2, 2);

            TimeSpan diff = DateTime.Now - t0;
            teCalculateTime.EditValue = diff.ToString();
            teCalculateCount.EditValue = count.ToString();
            
            SplashScreenManager.CloseForm();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true; 

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                File.WriteAllText(saveFileDialog.FileName, meResultTags.Text);
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
            int updateSuccessCount = 0;
            int faultCount = 0;
            int delay = 50;
            bool isUpdating = false;
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

                string instanceName = "";
                IgObjects queryResult;
                string[] tagnames = new string[1];
                XPCollection<Object> objects = new XPCollection<Object>();
                objects.Criteria = CriteriaOperator.Parse("[ArchestrAImport]");
                count = objects.Count;
                foreach (Object obj in objects)
                {
                    index++;
                    try
                    {
                        if (obj.ObjectTypeID != null)
                        {
                            if (!string.IsNullOrEmpty(obj.ObjectTypeID.ArchestrATemplate))
                            {
                                instanceName = obj.Attribute;
                                tagnames[0] = instanceName;

                                queryResult = galaxy.QueryObjectsByName(
                                EgObjectIsTemplateOrInstance.gObjectIsInstance,
                                ref tagnames);

                                IInstance instance = null;
                                cmd = galaxy.CommandResult;

                                try
                                {
                                    instance = (IInstance)queryResult[1];
                                    isUpdating = true;
                                }
                                catch
                                {
                                    isUpdating = false;
                                }

                                if (isUpdating)
                                {
                                    updateSuccessCount++;
                                }
                                else
                                {
                                    // get the $UserDefined template
                                    tagnames[0] = obj.ObjectTypeID.ArchestrATemplate;

                                    queryResult = galaxy.QueryObjectsByName(
                                        EgObjectIsTemplateOrInstance.gObjectIsTemplate,
                                        ref tagnames);

                                    cmd = galaxy.CommandResult;
                                    if (!cmd.Successful)
                                        meResultArchestrA.Text = "Ошибка инициализации шаблона:" + cmd.Text + " : " + cmd.CustomMessage + "\r\n";

                                    ITemplate userDefinedTemplate = (ITemplate)queryResult[1];

                                    // create an instance 
                                    instance = userDefinedTemplate.CreateInstance(instanceName, true);
                                    createSuccessCount++;
                                    isUpdating = false;
                                }
                                
                                instance.CheckOut();
                                MxValue mxName = new MxValueClass();
                                mxName.PutString(obj.Chart);
                                instance.Attributes[Global.Default.varXml.SystemPlatform.NameField].SetValue(mxName);

                                MxValue mxDescription = new MxValueClass();
                                mxDescription.PutString(obj.Comment);
                                instance.Attributes[Global.Default.varXml.SystemPlatform.DescriptionField].SetValue(mxDescription);

                                if (obj.ObjectTypeID.IsRealType)
                                {
                                    MxValue mxPointNum = new MxValueClass();
                                    mxPointNum.PutInteger(obj.PointNum);
                                    instance.Attributes[Global.Default.varXml.SystemPlatform.PointNumField].SetValue(mxPointNum);

                                    MxValue mxUnit = new MxValueClass();
                                    mxUnit.PutString(obj.Unit);
                                    instance.Attributes[Global.Default.varXml.SystemPlatform.UnitField].SetValue(mxUnit);
                                }

                                instance.Save();
                                instance.CheckIn("Check in after adding.");

                                progress = ((double)index / objects.Count * 1000);
                                progressPossition = (int)Math.Round(progress);

                                if (!isUpdating)
                                    Report(string.Format("Объект {0} создан удачно.\r\n", instanceName));
                                else
                                    Report(string.Format("Объект {0} обновлен удачно.\r\n", instanceName));
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Report(string.Format("У объекта {0} есть проблема создания :( [{1} -> {2}] \r\n", instanceName, ex.Message, ex.InnerException));
                        faultCount++;
                    }
                }

                galaxy.Logout();
                Thread.Sleep(delay);
                Report(string.Format("Отключение от галактики выполнено. \r\n"));
                Thread.Sleep(delay);
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
            Report(string.Format("Неудачно выполненных команд: {0}. \r\n", faultCount)); Thread.Sleep(delay);
        }

        void workerImport_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            meResultArchestrA.Text += string.Format("{0}: {1}", DateTime.Now.ToLongTimeString(), workerMessage);
            meResultArchestrA.SelectionStart = meResultArchestrA.Text.Length;
            meResultArchestrA.ScrollToCaret();
            prbImport.Position = progressPossition;
        }
    }
}

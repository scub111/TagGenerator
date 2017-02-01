using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace TagGenerator
{
    /// <summary>
    /// Основной класс глобальных переменных, которых можно сохранить в XML-файд
    /// </summary>
    public class VarXml
    {
        /// <summary>
        /// Класс подключения к БД.
        /// </summary>
        public class DBConnectionXml
        {
            /// <summary>
            /// Тип БД.
            /// </summary>
            public int DataBaseType;

            /// <summary>
            /// Имя БД.
            /// </summary>
            public string DataBase;

            /// <summary>
            /// Необходимость окна ввода пользователя и пароля.
            /// </summary>
            public bool LoginFormNeed;

            /// <summary>
            /// Необходимость ввода пароля.
            /// </summary>
            public bool PasswordNeed;

            /// <summary>
            /// Пользователь.
            /// </summary>
            public string User;

            /// <summary>
            /// Пароль.
            /// </summary>
            public string Password;
        }
        /// <summary>
        /// Класс для работы с System Platform.
        /// </summary>
        public class SystemPlatformXml
        {
            /// <summary>
            /// Имя сервера, где установлена Galaxy. 
            /// </summary>
            public string NodeName;

            /// <summary>
            /// Имя Galaxy.
            /// </summary>
            public string GalaxyName;

            /// <summary>
            /// Логин.
            /// </summary>
            public string Login;

            /// <summary>
            /// Пароль.
            /// </summary>
            public string Password;

            /// <summary>
            /// Поле имени.
            /// </summary>
            public string NameField;

            /// <summary>
            /// Поле описания.
            /// </summary>
            public string DescriptionField;
            
            /// <summary>
            /// Поле количество знаков после запятой.
            /// </summary>
            public string PointNumField;
            
            /// <summary>
            /// Поле единицы измерения.
            /// </summary>
            public string UnitField;
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        public VarXml(string strFileXml)
        {
            this.FileXml = strFileXml;
            DBConnection = new DBConnectionXml();
            SystemPlatform = new SystemPlatformXml();
            Init();
        }

        /// <summary>
        /// Конструктор/
        /// </summary>
        public VarXml()
            : this("Config.xml")
        {
        }

        void Init()
        {
            FilePath = Path.GetDirectoryName(Application.ExecutablePath) + "\\" + FileXml;

            DBConnection.DataBaseType = 1;
            DBConnection.DataBase = "DataBase.accdb";
            DBConnection.PasswordNeed = false;
            DBConnection.LoginFormNeed = false;
            DBConnection.User = "admin";
            DBConnection.Password = "123456";

            SystemPlatform.NodeName = "127.0.0.1";
            SystemPlatform.GalaxyName = "Temp";
            SystemPlatform.Login = "Admin";
            SystemPlatform.Password = "password";
            SystemPlatform.NameField = "Name";
            SystemPlatform.DescriptionField = "Description";
            SystemPlatform.PointNumField = "PointNum";
            SystemPlatform.UnitField = "Unit";
        }

        /// <summary>
        /// Название файла, куда будет сохняться данные.
        /// </summary>
        [XmlIgnore]
        string FileXml;

        /// <summary>
        /// Путь к файлу.
        /// </summary>
        [XmlIgnore]
        public string FilePath;

        /// <summary>
        /// Подплючение к БД.
        /// </summary>
        public DBConnectionXml DBConnection;

        /// <summary>
        /// Параметры System Platform.
        /// </summary>
        public SystemPlatformXml SystemPlatform;

        /// <summary>
        /// Сохранить данные в XML-файл.
        /// </summary>
        public void SaveToXML()
        {
            XmlSerializer xmlSer = new XmlSerializer(typeof(VarXml));
            TextWriter textWriter = new StreamWriter(FilePath);
            xmlSer.Serialize(textWriter, this);
            textWriter.Close();
        }

        /// <summary>
        /// Загрузить данные из XML-файла.
        /// </summary>
        public void LoadFromXML()
        {
            if (File.Exists(FilePath))
            {
                XmlSerializer deserializer = new XmlSerializer(typeof(VarXml));
                TextReader textReader = new StreamReader(FilePath);
                VarXml obj = (VarXml)deserializer.Deserialize(textReader);
                textReader.Close();

                DBConnection.DataBaseType = obj.DBConnection.DataBaseType;
                DBConnection.DataBase = obj.DBConnection.DataBase;
                DBConnection.PasswordNeed = obj.DBConnection.PasswordNeed;
                DBConnection.LoginFormNeed = obj.DBConnection.LoginFormNeed;
                DBConnection.User = obj.DBConnection.User;
                DBConnection.Password = obj.DBConnection.Password;

                SystemPlatform.NodeName = obj.SystemPlatform.NodeName;
                SystemPlatform.GalaxyName = obj.SystemPlatform.GalaxyName;
                SystemPlatform.Login = obj.SystemPlatform.Login;
                SystemPlatform.Password = obj.SystemPlatform.Password;
                SystemPlatform.NameField = obj.SystemPlatform.NameField;
                SystemPlatform.DescriptionField = obj.SystemPlatform.DescriptionField;
                SystemPlatform.PointNumField = obj.SystemPlatform.PointNumField;
                SystemPlatform.UnitField = obj.SystemPlatform.UnitField;                
            }
        }
    }

    public class GlobalDefault
    {
        /// <summary>
        /// Версия программы.
        /// </summary>
        public string Version;

        /// <summary>
        /// Переменные из файла настроек.
        /// </summary>
        public VarXml varXml;

        /// <summary>
        /// Текущий тип объекта.
        /// </summary>
        public ObjectType CurrentObjectType;

        public void Init()
        {
            Version = "v1.3.0";

            string fileName = Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetEntryAssembly().Location); 

            varXml = new VarXml(string.Format("{0}.xml", fileName));
            varXml.LoadFromXML();
            //varXml.SaveToXML();
        }
    }

    public class Global
    {
        private static GlobalDefault defaultInstance = new GlobalDefault();
        public static GlobalDefault Default { get { return defaultInstance; } }
    }
}

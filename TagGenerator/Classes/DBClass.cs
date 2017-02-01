using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Xpo;
using RapidInterface;
using DevExpress.Data.Filtering;

namespace TagGenerator
{
    #region Object
    /// <summary>
    /// Таблица "Объект".
    /// </summary>
    [DBAttribute(Caption = "Объект", IconFile = "Object.png")]
    public class Object : XPObjectEx
    {
        public Object() : base(Session.DefaultSession) { }

        public Object(Session session) : base(session) { }

        ObjectType _ObjectTypeID;
        /// <summary>
        /// Тип объекта.
        /// </summary>
        [DisplayName("Тип объекта")]
        public ObjectType ObjectTypeID
        {
            get { return _ObjectTypeID; }
            set { SetPropertyValueEx("ObjectTypeID", ref _ObjectTypeID, value); }
        }

        string _Chart;
        /// <summary>
        /// Контур.
        /// </summary>
        [DisplayName("Контур")]
        [Size(SizeAttribute.Unlimited)]
        public string Chart
        {
            get { return _Chart; }
            set { SetPropertyValueEx("Chart", ref _Chart, value); }
        }

        string _Attribute;
        /// <summary>
        /// Атрибут.
        /// </summary>
        [DisplayName("Атрибут")]
        [Size(SizeAttribute.Unlimited)]
        public string Attribute
        {
            get { return _Attribute; }
            set { SetPropertyValueEx("Attribute", ref _Attribute, value); }
        }

        string _ItemReference;
        /// <summary>
        /// Ссылка.
        /// </summary>
        [DisplayName("Ссылка")]
        [Size(SizeAttribute.Unlimited)]
        public string ItemReference
        {
            get { return _ItemReference; }
            set { SetPropertyValueEx("ItemReference", ref _ItemReference, value); }
        }

        string _Comment;
        /// <summary>
        /// Комментарий.
        /// </summary>
        [DisplayName("Комментарий")]
        [Size(SizeAttribute.Unlimited)]
        public string Comment
        {
            get { return _Comment; }
            set { SetPropertyValueEx("Comment", ref _Comment, value); }
        }        

        int _PointNum;
        /// <summary>
        /// Кол-во знаков после запятой.
        /// </summary>
        [DisplayName("Кол-во знаков")]
        public int PointNum
        {
            get { return _PointNum; }
            set { SetPropertyValueEx("PointNum", ref _PointNum, value); }
        }

        string _Unit;
        /// <summary>
        /// Единица измерения.
        /// </summary>
        [DisplayName("Единица измерения")]
        [Size(SizeAttribute.Unlimited)]
        public string Unit
        {
            get { return _Unit; }
            set { SetPropertyValueEx("Unit", ref _Unit, value); }
        }

        bool _ArchestrAExport;
        /// <summary>
        /// ArchestrA экспорт.
        /// </summary>
        [DisplayName("ArchestrA экспорт")]
        public bool ArchestrAExport
        {
            get { return _ArchestrAExport; }
            set { SetPropertyValueEx("ArchestrAExport", ref _ArchestrAExport, value); }
        }
    }
    #endregion

    #region ObjectType
    /// <summary>
    /// Таблица "Тип объекта".
    /// </summary>
    [DBAttribute(Caption = "Тип объекта", IconFile = "ObjectType.png")]
    public class ObjectType : XPObjectEx
    {
        public ObjectType() : base(Session.DefaultSession) { }

        public ObjectType(Session session) : base(session) { }

        public override string DisplayMember
        {
            get
            {
                return ObjectTypeCaption;
            }
        }

        ObjectType _ObjectTypeBaseID;
        /// <summary>
        /// Базовый тип.
        /// </summary>
        [DisplayName("Базовый тип")]
        public ObjectType ObjectTypeBaseID
        {
            get { return _ObjectTypeBaseID; }
            set { SetPropertyValueEx("ObjectTypeBaseID", ref _ObjectTypeBaseID, value); }
        }

        string _ObjectTypeCaption;
        /// <summary>
        /// Наименование.
        /// </summary>
        [DisplayName("Наименование")]
        [Size(SizeAttribute.Unlimited)]
        public string ObjectTypeCaption
        {
            get { return _ObjectTypeCaption; }
            set { SetPropertyValueEx("ObjectTypeCaption", ref _ObjectTypeCaption, value); }
        }

        string _ArchestrATemplate;
        /// <summary>
        /// ArchestrA Template.
        /// </summary>
        [DisplayName("ArchestrA шаблон")]
        [Size(SizeAttribute.Unlimited)]
        public string ArchestrATemplate
        {
            get { return _ArchestrATemplate; }
            set { SetPropertyValueEx("ArchestrATemplate", ref _ArchestrATemplate, value); }
        }
        
        string _SimaticPCSBlock;
        /// <summary>
        /// Simatic PCS Block.
        /// </summary>
        [DisplayName("Simatic PCS Block")]
        [Size(SizeAttribute.Unlimited)]
        public string SimaticPCSBlock
        {
            get { return _SimaticPCSBlock; }
            set { SetPropertyValueEx("SimaticPCSBlock", ref _SimaticPCSBlock, value); }
        }

        string _Comment;
        /// <summary>
        /// Комментарий.
        /// </summary>
        [DisplayName("Комментарий")]
        [Size(SizeAttribute.Unlimited)]
        public string Comment
        {
            get { return _Comment; }
            set { SetPropertyValueEx("Comment", ref _Comment, value); }
        }

        string _RecordFormat;
        /// <summary>
        /// Формат записи.
        /// </summary>
        [DisplayName("Формат записи")]
        [Size(SizeAttribute.Unlimited)]
        public string RecordFormat
        {
            get { return _RecordFormat; }
            set { SetPropertyValueEx("RecordFormat", ref _RecordFormat, value); }
        }

        bool _IsRealType;
        /// <summary>
        /// Вещественный тип.
        /// </summary>
        [DisplayName("Вещественный тип")]
        public bool IsRealType
        {
            get { return _IsRealType; }
            set { SetPropertyValueEx("IsRealType", ref _IsRealType, value); }
        }

        void GetObjectTypeAddonCollectionCriteria(ref string criteria, ObjectType parent)
        {
            if (parent != null)
            {
                criteria += string.Format(" || [ObjectTypeOwner].[Oid] == {0}", parent.Oid);
                if (parent.ObjectTypeBaseID != null)
                    GetObjectTypeAddonCollectionCriteria(ref criteria, parent.ObjectTypeBaseID);
            }
        }

        public XPCollection<ObjectTypeAddonCollection> GetObjectTypeAddonCollection(/*XPCollection<ObjectTypeAddonCollection> resultAddons, ObjectType parent*/)
        {
            XPCollection<ObjectTypeAddonCollection> addons = new XPCollection<TagGenerator.ObjectTypeAddonCollection>(Session);

            string criteria = string.Format("[ObjectTypeOwner].[Oid] == {0}", Oid);

            GetObjectTypeAddonCollectionCriteria(ref criteria, ObjectTypeBaseID);

            addons.Filter = CriteriaOperator.Parse(criteria);
            if (!IsLoading)
                foreach (ObjectTypeAddonCollection addon in addons)
                {
                    if (addon.ObjectTypeOwner == this)
                        addon.Basic = false;
                    else
                        addon.Basic = true;
                }

            return addons;
        }

        /// <summary>
        /// Таблица-коллекция "Дополнения".
        /// </summary>
        [DisplayName("Дополнения")]
        [Association("ObjectType-ObjectTypeAddonCollection"), Aggregated]
        public XPCollection<ObjectTypeAddonCollection> ObjectTypeAddonCollection
        {
            //get { return GetCollection<ObjectTypeAddonCollection>("ObjectTypeAddonCollection"); }
            get { return GetObjectTypeAddonCollection(); }
        }

        public override void Init()
        {
            base.Init();
            if (!IsLoading)
            {                
                RecordFormat = "\"{0}{1}\",\"{2}{3}\"";
            }
        }
    }
    #endregion

    #region ObjectTypeAddonCollection
    /// <summary>
    /// Таблица-коллекция "Дополнения".
    /// </summary>
    [DBAttribute(Caption = "Дополнения", IconFile = "ObjectTypeAddonCollection.png")]
    public class ObjectTypeAddonCollection : XPObjectEx
    {
        public ObjectTypeAddonCollection() : base(Session.DefaultSession) { }

        public ObjectTypeAddonCollection(Session session) : base(session) { }

        ObjectType _ObjectTypeOwner;
        /// <summary>
        /// Владелец "Тип объекта".
        /// </summary>
        [DisplayName("Владелец \"Тип объекта\"")]
        [Association("ObjectType-ObjectTypeAddonCollection")]
        public ObjectType ObjectTypeOwner
        {
            get { return _ObjectTypeOwner; }
            set { SetPropertyValueEx("ObjectTypeOwner", ref _ObjectTypeOwner, value); }
        }        

        string _AttributeAddon;
        /// <summary>
        /// Дополнение атрибута.
        /// </summary>
        [DisplayName("Дополнение атрибута")]
        [Size(SizeAttribute.Unlimited)]
        public string AttributeAddon
        {
            get { return _AttributeAddon; }
            set { SetPropertyValueEx("AttributeAddon", ref _AttributeAddon, value); }
        }     

        string _ItemReferenceAddon;
        /// <summary>
        /// Дополнение ссылки.
        /// </summary>
        [DisplayName("Дополнение ссылки")]
        [Size(SizeAttribute.Unlimited)]
        public string ItemReferenceAddon
        {
            get { return _ItemReferenceAddon; }
            set { SetPropertyValueEx("ItemReferenceAddon", ref _ItemReferenceAddon, value); }
        }

        string _Comment;
        /// <summary>
        /// Комментарий.
        /// </summary>
        [DisplayName("Комментарий")]
        [Size(SizeAttribute.Unlimited)]
        public string Comment
        {
            get { return _Comment; }
            set { SetPropertyValueEx("Comment", ref _Comment, value); }
        }

        bool _TagExport;
        /// <summary>
        /// Тег экспорт.
        /// </summary>
        [DisplayName("Тег экспорт")]
        public bool TagExport
        {
            get { return _TagExport; }
            set { SetPropertyValueEx("TagExport", ref _TagExport, value); }
        }

        bool _ArchestrAExport;
        /// <summary>
        /// ArchestrA экспорт.
        /// </summary>
        [DisplayName("ArchestrA экспорт")]
        public bool ArchestrAExport
        {
            get { return _ArchestrAExport; }
            set { SetPropertyValueEx("ArchestrAExport", ref _ArchestrAExport, value); }
        }

        string _ArchestrAValue;
        /// <summary>
        /// ArchestrA значение.
        /// </summary>
        [DisplayName("Значение")]
        public string ArchestrAValue
        {
            get { return _ArchestrAValue; }
            set { SetPropertyValueEx("ArchestrAValue", ref _ArchestrAValue, value); }
        }

        int _DataType;
        /// <summary>
        /// Тип.
        /// </summary>
        [DisplayName("Тип")]
        public int DataType
        {
            get { return _DataType; }
            set { SetPropertyValueEx("DataType", ref _DataType, value); }
        }

        /// <summary>
        /// Базовый.
        /// </summary>
        [DisplayName("Базовый")]
        [NonPersistent]
        public bool Basic { get; set; }

        protected override void OnSaving()
        {
            if (this.ObjectTypeOwner == null)
                Delete();
            base.OnSaving();
        }

        public override void Init()
        {
            base.Init();
            if (!IsLoading)
            {
                TagExport = true;
                ArchestrAExport = true;
                ObjectTypeOwner = Global.Default.CurrentObjectType;
            }
        }
    }
    #endregion
}

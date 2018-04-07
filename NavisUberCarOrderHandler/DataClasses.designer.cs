﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NavisUberCarOrderHandler
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="test_device_db")]
	public partial class DataClassesDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertCar_Order(Car_Order instance);
    partial void UpdateCar_Order(Car_Order instance);
    partial void DeleteCar_Order(Car_Order instance);
    partial void InsertLst_LoaiXe(Lst_LoaiXe instance);
    partial void UpdateLst_LoaiXe(Lst_LoaiXe instance);
    partial void DeleteLst_LoaiXe(Lst_LoaiXe instance);
    #endregion
		
		public DataClassesDataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["test_device_dbConnectionString"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public DataClassesDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClassesDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClassesDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClassesDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Car_Order> Car_Orders
		{
			get
			{
				return this.GetTable<Car_Order>();
			}
		}
		
		public System.Data.Linq.Table<Lst_LoaiXe> Lst_LoaiXes
		{
			get
			{
				return this.GetTable<Lst_LoaiXe>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Car_Order")]
	public partial class Car_Order : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _orderID;
		
		private string _origin_place;
		
		private string _destination_place;
		
		private string _car_type;
		
		private System.DateTime _pickup_time;
		
		private string _contact_number;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnorderIDChanging(int value);
    partial void OnorderIDChanged();
    partial void Onorigin_placeChanging(string value);
    partial void Onorigin_placeChanged();
    partial void Ondestination_placeChanging(string value);
    partial void Ondestination_placeChanged();
    partial void Oncar_typeChanging(string value);
    partial void Oncar_typeChanged();
    partial void Onpickup_timeChanging(System.DateTime value);
    partial void Onpickup_timeChanged();
    partial void Oncontact_numberChanging(string value);
    partial void Oncontact_numberChanged();
    #endregion
		
		public Car_Order()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_orderID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int orderID
		{
			get
			{
				return this._orderID;
			}
			set
			{
				if ((this._orderID != value))
				{
					this.OnorderIDChanging(value);
					this.SendPropertyChanging();
					this._orderID = value;
					this.SendPropertyChanged("orderID");
					this.OnorderIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_origin_place", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string origin_place
		{
			get
			{
				return this._origin_place;
			}
			set
			{
				if ((this._origin_place != value))
				{
					this.Onorigin_placeChanging(value);
					this.SendPropertyChanging();
					this._origin_place = value;
					this.SendPropertyChanged("origin_place");
					this.Onorigin_placeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_destination_place", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string destination_place
		{
			get
			{
				return this._destination_place;
			}
			set
			{
				if ((this._destination_place != value))
				{
					this.Ondestination_placeChanging(value);
					this.SendPropertyChanging();
					this._destination_place = value;
					this.SendPropertyChanged("destination_place");
					this.Ondestination_placeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_car_type", DbType="NVarChar(20) NOT NULL", CanBeNull=false)]
		public string car_type
		{
			get
			{
				return this._car_type;
			}
			set
			{
				if ((this._car_type != value))
				{
					this.Oncar_typeChanging(value);
					this.SendPropertyChanging();
					this._car_type = value;
					this.SendPropertyChanged("car_type");
					this.Oncar_typeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_pickup_time", DbType="DateTime NOT NULL")]
		public System.DateTime pickup_time
		{
			get
			{
				return this._pickup_time;
			}
			set
			{
				if ((this._pickup_time != value))
				{
					this.Onpickup_timeChanging(value);
					this.SendPropertyChanging();
					this._pickup_time = value;
					this.SendPropertyChanged("pickup_time");
					this.Onpickup_timeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_contact_number", DbType="NChar(11) NOT NULL", CanBeNull=false)]
		public string contact_number
		{
			get
			{
				return this._contact_number;
			}
			set
			{
				if ((this._contact_number != value))
				{
					this.Oncontact_numberChanging(value);
					this.SendPropertyChanging();
					this._contact_number = value;
					this.SendPropertyChanged("contact_number");
					this.Oncontact_numberChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Lst_LoaiXe")]
	public partial class Lst_LoaiXe : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _id_loai_xe;
		
		private string _ten_loai_xe;
		
		private string _mo_ta;
		
		private System.Nullable<int> _code;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void Onid_loai_xeChanging(int value);
    partial void Onid_loai_xeChanged();
    partial void Onten_loai_xeChanging(string value);
    partial void Onten_loai_xeChanged();
    partial void Onmo_taChanging(string value);
    partial void Onmo_taChanged();
    partial void OncodeChanging(System.Nullable<int> value);
    partial void OncodeChanged();
    #endregion
		
		public Lst_LoaiXe()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_id_loai_xe", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int id_loai_xe
		{
			get
			{
				return this._id_loai_xe;
			}
			set
			{
				if ((this._id_loai_xe != value))
				{
					this.Onid_loai_xeChanging(value);
					this.SendPropertyChanging();
					this._id_loai_xe = value;
					this.SendPropertyChanged("id_loai_xe");
					this.Onid_loai_xeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ten_loai_xe", DbType="NVarChar(100)")]
		public string ten_loai_xe
		{
			get
			{
				return this._ten_loai_xe;
			}
			set
			{
				if ((this._ten_loai_xe != value))
				{
					this.Onten_loai_xeChanging(value);
					this.SendPropertyChanging();
					this._ten_loai_xe = value;
					this.SendPropertyChanged("ten_loai_xe");
					this.Onten_loai_xeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_mo_ta", DbType="NVarChar(200)")]
		public string mo_ta
		{
			get
			{
				return this._mo_ta;
			}
			set
			{
				if ((this._mo_ta != value))
				{
					this.Onmo_taChanging(value);
					this.SendPropertyChanging();
					this._mo_ta = value;
					this.SendPropertyChanged("mo_ta");
					this.Onmo_taChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_code", DbType="Int")]
		public System.Nullable<int> code
		{
			get
			{
				return this._code;
			}
			set
			{
				if ((this._code != value))
				{
					this.OncodeChanging(value);
					this.SendPropertyChanging();
					this._code = value;
					this.SendPropertyChanged("code");
					this.OncodeChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591

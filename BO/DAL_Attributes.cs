using System;
using System.Data;

namespace BO
{
	// het attribuut wordt gebruikt als Property attribuut
	[AttributeUsage(AttributeTargets.Property)]
	public class BaseFieldAttribute : Attribute // ERFBASIS
	{
		string msColName;
		DbType dbType = DbType.String;
		int    miSize   = 0;
		bool mbReadOnly = false;
	
		public BaseFieldAttribute(string sColName, int iSize)
		{
			this.ColumnName = sColName;	
			this.Size = iSize;
		}
		
		public string ColumnName
		{
			get { return msColName;  }
			set { msColName = value; }
		}	

		public DbType Type
		{
			get { return dbType;  }
			set { dbType = value; }
		}
		
		public int Size 
		{
			get { return miSize;  }
			set { miSize = value; }
		}

		public bool ReadOnly
		{
			get{ return mbReadOnly;}
			set { mbReadOnly = value; }
		}
	}

	// het attribuut wordt gebruikt als Property attribuut
	[AttributeUsage(AttributeTargets.Property)]
	public class ReadOnlyFieldAttribute : BaseFieldAttribute
	{
		public ReadOnlyFieldAttribute(string sColName, int iSize): base(sColName, iSize)
		{
			this.ReadOnly = true;
		}
	}

	// het attribuut wordt gebruikt als Property attribuut
	[AttributeUsage(AttributeTargets.Property)]
	public class KeyReadOnlyFieldAttribute : KeyFieldAttribute
	{
		public KeyReadOnlyFieldAttribute(string sColName, int iSize, int sequence):
			                                 base(sColName, iSize, sequence)
	{
		this.ReadOnly = true;
	}

	}
	
	// het attribuut wordt gebruikt als Property attribuut
	[AttributeUsage(AttributeTargets.Property)]
	public class KeyFieldAttribute : BaseFieldAttribute
	{
		int miSequence;
		public KeyFieldAttribute(string sColName, int iSize, int sequence) : base(sColName, iSize)
		{
			miSequence= sequence;
		}
		public int Sequence 
		{
			get { return miSequence;  }
			set { miSequence = value; }
		}

	};
	
	// het attribuut wordt gebruikt als Property attribuut
	[AttributeUsage(AttributeTargets.Property)]
	public class ForeignKeyFieldAttribute : BaseFieldAttribute
	{
		public ForeignKeyFieldAttribute(string sColName, int iSize) : base(sColName, iSize)
		{			
			
		}
		
	};	
	
	// het attribuut wordt gebruikt als Class | Struct attribuut
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
	public class DataTableAttribute : Attribute
	{
		string msTblName;
		string mspUPDATE   = "";
		
		public DataTableAttribute(string sTblName)
		{
			this.TableName = sTblName;
		}
		
		public string TableName
		{
			get { return msTblName;  }
			set { msTblName = value; }
		}		
		
		
		public string spUPDATE
		{
			get { return mspUPDATE;  }
			set { mspUPDATE = value; }
		}
	}

}
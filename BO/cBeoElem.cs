using System;

namespace BO
{
	/// <summary>
	/// Summary description for cBeoElem.
	/// </summary>
	[DataTable("tblBeoScore", spUPDATE="")] 
	public class cBeoElem: cObj_base
	{
		private string msBeoID; // bestaat uit periode + msnr's van TL + beo
		private string msElem; 
		private string msScor="", msToel = "", msDoel ="", msAfsp = "";
		private string msComp = "", msCompOmschr = "", msCompNr_Omschr = "", msElemDescr = "", msIndi = "";

		public cBeoElem()
		{
		}

		[KeyField("BeoID", 24, 1)]
		public string BeoID
		{ 
			get{ return msBeoID; }
			set{ msBeoID = value; }
		}

		[KeyField("Elem", 5, 2)]
		public string Elem
		{ 
			get{ return msElem; }
			set{ msElem = value; }
		}

		[BaseField("Scor", 1)]
		public string Scor
		{ 
			get{ return msScor; }
			set{ msScor= value; }
		}

		[BaseField("Toel", 255)]
		public string Toel
		{ 
			get{ return msToel; }
			set{ msToel= value; }
		}

		[BaseField("Doel", 255)]
		public string Doel
		{ 
			get{ return msDoel; }
			set{ msDoel= value; }
		}

		[BaseField("Afsp", 255)]
		public string Afsp
		{ 
			get{ return msAfsp; }
			set{ msAfsp= value; }
		}

		[ReadOnlyField("Comp", 50)]
		public string Comp
		{ 
			get{ return msComp; }
			set{ msComp = value; }
		}

		[ReadOnlyField("CompOmschr", 50)]
		public string CompOmschr
		{ 
			get{ return msCompOmschr; }
			set{ msCompOmschr = value; }
		}

		// Nr + Omschrijving: nodig in Rapport (ter bespreking)
		public string CompNr_Omschr
		{ 
			get{ return msCompNr_Omschr; }
			set{ msCompNr_Omschr = value; }
		}

		[ReadOnlyField("ElemDescr", 50)]
		public string ElemDescr
		{ 
			get{ return msElemDescr; }
			set{ msElemDescr = value; }
		}

		[ReadOnlyField("Indi", 300)]
		public string Indi_with_ElemDescr
		{ 
			get
			{
				// toevoegen elem. omschr.  aan indicatoren
				return "<b>" + this.ElemDescr + "</b><br>" + msIndi;
			}
			set{ msIndi = value; }
		}

		[ReadOnlyField("Indi", 300)]
		public string Indi
		{ 
			get
			{
				return msIndi;
			}
			set{ msIndi = value; }
		}

	}
}

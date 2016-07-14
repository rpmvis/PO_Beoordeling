using System;
using System.Text;

namespace FUNC
{
	/// <summary>
	/// Summary description for cDate.
	/// </summary>
	public class cDate
	{
		public cDate()
		{
		}

		public static string SQLdate(DateTime dDate)
		{
			// convert date to string with mm/dd/yyyy format
			string s = "";

			s = System.Convert.ToString(dDate.Month) + "/" + 
					System.Convert.ToString(dDate.Day) + "/" + 
					System.Convert.ToString(dDate.Year);
      return "'" + s + "'";
		}

		public static bool Is_dmy()
		{
			string sDate = "1/8/1900";

			DateTime dTest = System.Convert.ToDateTime(sDate);  
			
			return (dTest.Month == 8);
		}

		public static string ToDay_UI()
		{
			string sToDay = System.Convert.ToString(System.DateTime.Today);
			sToDay = sToDay.Replace(" 0:00:00", ""); 
			return sToDay;
		}

		public static string ToDay_SQL()
		{
			string sToDay = SQLdate(System.DateTime.Today);
			return sToDay;
		}

		public static string DutchDate(string SQLdate)
		{
			string sRet=""; 

			if (SQLdate.ToString() == "")
				sRet = SQLdate;
			else
			{
				char[] sep ="/- ".ToCharArray(); // mbv spatie wordt ook de tijd string gesplitst
				string[] sSplit = SQLdate.Split(sep);

				if (sSplit.Length >= 3)
				{
					StringBuilder sb = new StringBuilder();
					sb.Append(sSplit[1]); 
					sb.Append("-");
					sb.Append(sSplit[0]); 
					sb.Append("-");
					sb.Append(sSplit[2]); 

					sRet = sb.ToString(); 
				}
			}

			return sRet;
 		}


		public static string Date_UI(System.DateTime dDate)
		{
			string s = "";

			int iDay = dDate.Day;
			int iMonth = dDate.Month;
			int iYear = dDate.Year; 

			if (iDay == 1 && iMonth == 1 && (iYear == 1 | iYear == 1900) )
				 s = "";
			else
				s = System.Convert.ToString(iDay) + "-" + 
						System.Convert.ToString(iMonth) + "-" + 
						System.Convert.ToString(iYear);
			return s;
		}

		public static System.DateTime Date_BO(string sDate)
		{

			System.DateTime dDate = new System.DateTime(1,1,1);

			if (sDate.ToString() != "")
			{
				try
				{
					// probeer datum van 1/1/1 te krijgen op datum in sDate
					dDate = System.Convert.ToDateTime(sDate);
				}
				catch
				{
					dDate = new System.DateTime(1,1,1);
				}
				finally
				{
					string s = dDate.Day.ToString() + "-" + dDate.Month.ToString()  + "-" + dDate.Year.ToString();    
					Console.WriteLine(s); 
				}
			}
			return dDate;
		}

	}
}

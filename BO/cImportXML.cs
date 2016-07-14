using System;
using System.Data;
using System.Data.OleDb;
using System.Text; 
using System.IO; 
using System.Xml; 

namespace BO
{
	/// <summary>
	/// Summary description for cImportXML.
	/// </summary>
	public class cImportXML
	{
		public cImportXML()
		{
		}

		public void Import(string sFilePath
			, string sXML
			, ref string sMessage)
		{
			XmlNode ndRoot = null;

			// remove from root node all attributes like:
			// xmlns:od="urn:schemas-microsoft-com:officedata" generated="2007-06-09T11:29:35"
			System.Xml.XmlDocument doc = new XmlDocument();
			doc.LoadXml(sXML);
			
			ndRoot = GetRootNode(doc);
			ndRoot.Attributes.RemoveAll(); 

			sXML = doc.OuterXml.ToString(); 

			// get Data Source table
			NameTable nt = new NameTable();
			XmlNamespaceManager nsmgr = new XmlNamespaceManager(nt);
			XmlParserContext context = new XmlParserContext(null, nsmgr, null, XmlSpace.None);
			XmlTextReader tr = new XmlTextReader(sXML, XmlNodeType.Document, context);

			DataSet ds = new DataSet();
			ds.ReadXml(tr);

			DataTable dtSource = ds.Tables[0]; 
			cImport2 oImport2 = new cImport2();
			oImport2.Import2(sFilePath, ref dtSource, ref sMessage);
		}

		private XmlNode GetRootNode(XmlDocument doc)
		{
			// purpose: retrieve root node from XML document
			// technique: retrieve 1st element with NodeType = element
			XmlNode ndRoot = null;

			foreach(XmlNode nd in doc.ChildNodes)
			{
				if(nd.NodeType == XmlNodeType.Element)
				{
					ndRoot = nd;
					break;
				}
			}

			return ndRoot;
		}

	}
}

using Microsoft.Reporting.WebForms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using BusinessEntityL;


namespace ReportingLayer
{
    public class RptSetupClass
    {
        public static LocalReport GetLocalReport(string RptName, Object RptDataSet, Object RptDataSet2, Object UserDataset)
        {
            var assamblyPath = Assembly.GetExecutingAssembly().CodeBase;
            Assembly assembly1 = Assembly.LoadFrom(assamblyPath);
            //Assembly assembly1 = Assembly.LoadFrom("ASITHmsRpt2Inventory.dll");
            Stream stream1 = assembly1.GetManifestResourceStream("ReportingLayer." + RptName + ".rdlc");

            LocalReport Rpt1a = new LocalReport();
            Rpt1a.DisplayName = RptName;
            Rpt1a.LoadReportDefinition(stream1);
            Rpt1a.DataSources.Clear();

            switch (Rpt1a.DisplayName.Trim())
            {
                case "Sales.POSSalesInv01": Rpt1a = SetRptPOSSalesInv01(Rpt1a, RptDataSet, RptDataSet2, UserDataset); break;
            }

            Rpt1a.Refresh();
            return Rpt1a;
        }

        private static LocalReport SetRptPOSSalesInv01(LocalReport rpt1a, object rptDataSet, object rptDataSet2, object userDataset)
        {

            rpt1a.SetParameters(new ReportParameter("txtCompAdd", "ABC Company"));
            rpt1a.SetParameters(new ReportParameter("txtsaledate", "22-Jan-2017"));
            rpt1a.SetParameters(new ReportParameter("txtAdd", "Rupnogar,Mirpur-12, Dhaka"));
            rpt1a.SetParameters(new ReportParameter("txtOrdTime", "22-Jan-2017"));
            rpt1a.SetParameters(new ReportParameter("txtsaleNo", "00001"));
            rpt1a.SetParameters(new ReportParameter("txtamt", "In Word :"));
            rpt1a.SetParameters(new ReportParameter("txtPreBy", "Sabid"));

            rpt1a.DataSources.Add(new ReportDataSource("DataSet1", (List<BusinessEntityL.Sales.SalesInvRpt>)rptDataSet));
            return rpt1a;
        }
    }
}

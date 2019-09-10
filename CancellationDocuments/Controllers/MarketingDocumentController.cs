using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAPbobsCOM;

namespace CancellationDocuments.Controllers
{
    class MarketingDocumentController : IMarketingDocuments
    {
        public bool Cancel(object document)
        {
            Documents marketingDocmunet = (Documents)document;
            if (marketingDocmunet.Cancelled == BoYesNoEnum.tNO)
            {
                Documents cancelationMarketingDocument = marketingDocmunet.CreateCancellationDocument();
                int res = cancelationMarketingDocument.Add();
                if (res == 0)
                {
                    return true;
                }
                string error = DiManager.Company.GetLastErrorDescription();
                throw new Exception($"Document Number {marketingDocmunet.DocEntry}: Error - {error}");
            }
            throw new Exception($"Document Number {marketingDocmunet.DocEntry} Already cancelled");
        }
    }
}

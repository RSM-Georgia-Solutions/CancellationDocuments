using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAPbobsCOM;
using SAPbouiCOM;

namespace CancellationDocuments.Interfaces
{
    class PaymentController : IPayments
    {
        public bool Cancel(object document)
        {
            var payment = (Payments)document;
            var res = payment.Cancel();
            if (res == 0)
            {
                return true;
            }
            string error = DiManager.Company.GetLastErrorDescription();
            throw new Exception($"Payment Number {payment.DocEntry}: Error - {error}");
        }
    }
}


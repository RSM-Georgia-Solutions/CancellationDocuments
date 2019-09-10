using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CancellationDocuments.StaticData
{
    public static class Data
    {
       public static Dictionary<int, string> MatketingTypes = new Dictionary<int, string>
        {
            {13, "A/R Invoice"},
            {15, "Delivery"},
            {16, "Returns"},
            {203, "A/R Down Payment"},
            {14, "A/R Credit Memo"},
            {132, "Correction Invoice"},
            {20, "Goods Receipt"},
            {21, "Goods Return"},
            {204, "A/P Down Payment"},
            {18, "A/P Invoice"},
            {19, "A/P Credit Memo"},
            {17, "Sales Order"},
            {22, "Purchase Order"},
            {23, "Sales Quotation"},
            {67, "Inventory Transfers"},
            {59, "Goods Receipt"},
            {60, "Goods Issue"},
            {163, "A/P Correction Invoice"},
            {164, "A/P Correction Invoice Reversal"},
            {165, "A/R Correction Invoice"},
            {166, "A/R Correction Invoice Reversal"},
            {1250000001, "Inventory Transfer Request"},
            {540000006, "Purchase Quotation"},
            {1470000113, "Purchase Request"},
            {234000031, "Return Request"},
            {234000032, "Goods Return Request"}
        };

        public static Dictionary<int, string> PaymentTypes = new Dictionary<int, string>
        {
            {24, "Incoming Payment"},
            {46,"Outgoing Payments" }
        };
    }
}

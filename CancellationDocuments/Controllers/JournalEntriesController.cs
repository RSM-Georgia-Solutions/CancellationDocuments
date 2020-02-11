using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAPbobsCOM;
using IJournalEntries = CancellationDocuments.Interfaces.IJournalEntries;

namespace CancellationDocuments.Controllers
{
    public class JournalEntriesController : IJournalEntries
    {
        public bool Cancel(object document)
        {
            JournalEntries journalEntry = (JournalEntries)document;
            int res = journalEntry.Cancel();
            if (res == 0)
            {
                return true;
            }
            string error = DiManager.Company.GetLastErrorDescription();
            throw new Exception($"Entry Number {journalEntry.Number}: Error - {error}");
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml;
using CancellationDocuments.Controllers;
using CancellationDocuments.Interfaces;
using CancellationDocuments.StaticData;
using SAPbobsCOM;
using SAPbouiCOM;
using SAPbouiCOM.Framework;
using Application = SAPbouiCOM.Framework.Application;
using ValidValue = SAPbobsCOM.ValidValue;

namespace CancellationDocuments
{
    [FormAttribute("CancellationDocuments.CancellationDocuments", "CancellationDocuments.b1f")]
    class CancellationDocuments : UserFormBase
    {
        public CancellationDocuments()
        {
        }

        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.ComboBox0 = ((SAPbouiCOM.ComboBox)(this.GetItem("Item_1").Specific));
            this.StaticText0 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_2").Specific));
            this.EditText0 = ((SAPbouiCOM.EditText)(this.GetItem("Item_3").Specific));
            this.StaticText1 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_4").Specific));
            this.Button1 = ((SAPbouiCOM.Button)(this.GetItem("Item_5").Specific));
            this.Button1.PressedAfter += new SAPbouiCOM._IButtonEvents_PressedAfterEventHandler(this.Button1_PressedAfter);
            this.Button0 = ((SAPbouiCOM.Button)(this.GetItem("Item_0").Specific));
            this.Button0.PressedAfter += new SAPbouiCOM._IButtonEvents_PressedAfterEventHandler(this.Button0_PressedAfter);
            this.OnCustomInitialize();

        }

        /// <summary>
        /// Initialize form event. Called by framework before form creation.
        /// </summary>
        public override void OnInitializeFormEvents()
        {
        }

        private void OnCustomInitialize()
        {
            FillOBjectType(ComboBox0, Data.PaymentTypes, Data.MatketingTypes);
            ComboBox0.Select(0, BoSearchKey.psk_Index);
            ComboBox0.Item.DisplayDesc = true;
            StaticText0.Item.FontSize = 12;
            StaticText1.Item.FontSize = 12;
            StaticText0.Item.Height = 18;
            StaticText1.Item.Height = 18;
            UIAPIRawForm.Left = 600;
            UIAPIRawForm.Top = 150;
        }

        private SAPbouiCOM.ComboBox ComboBox0;
        private SAPbouiCOM.StaticText StaticText0;
        private SAPbouiCOM.EditText EditText0;
        private SAPbouiCOM.StaticText StaticText1;
        private SAPbouiCOM.Button Button1;

    

        private void FillOBjectType(ComboBox comboBox, params Dictionary<int, string>[] docTypes)
        {
            foreach (var docType in docTypes)
            {
                foreach (var pair in docType)
                    comboBox.ValidValues.Add(pair.Key.ToString(), pair.Value);
            }
        }


        private void Button1_PressedAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            int objectType = int.Parse(ComboBox0.Selected.Value);
            var docNums = EditText0.Value.Split(',');
            int totalDocs = docNums.Length;
            int progress = 0;

            if (Data.PaymentTypes.ContainsKey(objectType))
            {
                PaymentController pController = new PaymentController();
                Payments payment = (Payments)DiManager.Company.GetBusinessObject((BoObjectTypes)objectType);
                foreach (string docNum in docNums)
                {
                    payment.GetByKey(int.Parse(docNum));
                    try
                    {
                        pController.Cancel(payment);
                        progress++;
                        Application.SBO_Application.SetStatusBarMessage($"{progress} Of {totalDocs}",
                            BoMessageTime.bmt_Short, false);
                    }
                    catch (Exception ex)
                    {
                        progress++;
                        Application.SBO_Application.MessageBox(ex.Message);
                    }
                }
            }
            else if (Data.MatketingTypes.ContainsKey(objectType))
            {
                MarketingDocumentController mController = new MarketingDocumentController();
                Documents docmunet = (Documents)DiManager.Company.GetBusinessObject((BoObjectTypes)objectType);
                foreach (string docNum in docNums)
                {
                    docmunet.GetByKey(int.Parse(docNum));
                    try
                    {
                        mController.Cancel(docmunet);
                        progress++;
                        Application.SBO_Application.SetStatusBarMessage($"{progress} Of {totalDocs}",
                            BoMessageTime.bmt_Short, false);
                    }
                    catch (Exception ex)
                    {
                        progress++;
                        Application.SBO_Application.MessageBox(ex.Message);
                    }
                }
            }
        }


        private void Cancel(dynamic docmunet)
        {
            var docNums = EditText0.Value.Split(',');
            int totalDocs = docNums.Length;
            int progress = 0;
            foreach (string docNum in docNums)
            {
                docmunet.GetByKey(int.Parse(docNum));
                var res = docmunet.Cancel();
                if (res == 0)
                {
                    progress++;
                    Application.SBO_Application.SetStatusBarMessage($"{progress} Of {totalDocs}",
                        BoMessageTime.bmt_Short, false);
                }
                else
                {
                    string error = DiManager.Company.GetLastErrorDescription();
                    Application.SBO_Application.MessageBox($"{error} --- At Document {docNum}");
                }
            }
        }

        private Button Button0;

        private void Button0_PressedAfter(object sboObject, SBOItemEventArg pVal)
        {
            Application.SBO_Application.Forms.ActiveForm.Close();
        }
    }
}
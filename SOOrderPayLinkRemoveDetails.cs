using PX.Common;
using PX.Data;
using PX.Objects.AR;
using PX.Objects.CA;
using PX.Objects.Extensions.PayLink;
using PX.Objects.TX;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using V2 = PX.CCProcessingBase.Interfaces.V2;
using PX.Objects.CS;
using PX.Objects.CC.PaymentProcessing.Helpers;
using PX.Objects.SO;
using PX.Objects.SO.GraphExtensions.SOOrderEntryExt;
using PX.CCProcessingBase.Interfaces.V2;

namespace PX.Objects.CC.GraphExtensions
{

    public class SOOrderPayLinkRemoveDetails : PXGraphExtension<SOOrderEntryPayLink, SOOrderEntry>
    {
        public static bool IsActive()
        {
            return PXAccess.FeatureInstalled<FeaturesSet.acumaticaPayments>();
        }

        [PXOverride]
        public virtual void CalculateAndSetLinkAmount(SOOrder doc, V2.PayLinkProcessingParams payLinkParams, Action<SOOrder, V2.PayLinkProcessingParams> baseMethod)
        {
            baseMethod(doc, payLinkParams);
            var total = payLinkParams.Amount;
            foreach (var appliedDoc in payLinkParams.DocumentData.AppliedDocuments)
            {
                total += appliedDoc.Amount;
            }
            payLinkParams.DocumentData.DocumentDetails = new List<DocumentDetailData>() { new DocumentDetailData() { ItemID = "Total", Quantity = 1, Price = total } };
        }
    }

    public class ARInvoiceEntryPayLinkRemoveDetails : PXGraphExtension<ARInvoiceEntryPayLink, ARInvoiceEntry>
    {
        public static bool IsActive()
        {
            return PXAccess.FeatureInstalled<FeaturesSet.acumaticaPayments>();
        }

        [PXOverride]
        public virtual void CalculateAndSetLinkAmount(ARInvoice doc, V2.PayLinkProcessingParams payLinkParams, Action<ARInvoice, V2.PayLinkProcessingParams> baseMethod)
        {
            baseMethod(doc, payLinkParams);
            var total = payLinkParams.Amount;
            foreach (var appliedDoc in payLinkParams.DocumentData.AppliedDocuments)
            {
                total += appliedDoc.Amount;
            }
            payLinkParams.DocumentData.DocumentDetails = new List<DocumentDetailData>() { new DocumentDetailData() { ItemID = "Total", Quantity = 1, Price = total } };
        }
    }
}
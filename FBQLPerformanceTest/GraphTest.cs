using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CR;
using PX.Objects.SO;

namespace FBQLPerformanceTest
{
	public class GraphTest : PXGraph<GraphTest>
	{
		[PXCopyPasteHiddenView()]
		public PXSelectJoin<SOAdjust, InnerJoin<ARPayment, On<ARPayment.docType, Equal<SOAdjust.adjgDocType>, And<ARPayment.refNbr, Equal<SOAdjust.adjgRefNbr>>>>> Adjustments;

		[PXCopyPasteHiddenView()]

		public SelectFrom<SOAdjust>.InnerJoin<ARPayment>.On<SOAdjust.adjgDocType.IsEqual<ARPayment.docType>.
			And<SOAdjust.adjgRefNbr.IsEqual<ARPayment.refNbr>>> Adjustments2;

		public static Vendor FindByID(PXGraph graph, int? bAccountID)
		{
			return PXSelect<Vendor,
					Where2<Where<Vendor.type, Equal<BAccountType.vendorType>,
							Or<Vendor.type, Equal<BAccountType.combinedType>>>,
						And<Vendor.bAccountID, Equal<Required<Vendor.bAccountID>>>>>
				.Select(graph, bAccountID);
		}

		public static Vendor FindByIDFbql(PXGraph graph, int? bAccountID)
		{
			return SelectFrom<Vendor>.Where<Vendor.type.IsEqual<BAccountType.vendorType>.
					Or<Vendor.type.IsEqual<BAccountType.combinedType>.
					And<Vendor.bAccountID.IsEqual<@P.AsInt>>>>.View
				.Select(graph, bAccountID);
		}
	}
}

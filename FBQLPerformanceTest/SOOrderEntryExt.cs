using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CR;
using PX.Objects.SO;
using PX.Olap;

namespace FBQLPerformanceTest
{
	public class SOOrderEntryExt : PXGraphExtension<SOOrderEntry>
	{
		public PXViewOf<SOOrder>.BasedOn<SelectFrom<SOOrder>.
			Where<SOOrder.approved.IsEqual<True>>> ActiveProducts1;

		public SelectFrom<SOOrder>.
			Where<SOOrder.approved.IsEqual<True>>.View ActiveProducts2;

		public PXAction<SOOrder> DifferentTests;

		[PXButton]
		[PXUIField(DisplayName = "Different test")]
		protected virtual IEnumerable differentTests(PXAdapter adapter)
		{
			var mixSelect = SelectFrom<Contact>.Where<Contact.displayName.
				IsEqual<Use<Student1.firstName>.AsString>>.View.Select(Base);
			

			var joinWithoutSingleTable = SelectFrom<SOOrder>.LeftJoin<SOLine>.On<SOOrder.orderNbr.IsEqual<SOLine.orderNbr>.
				And<SOOrder.orderType.IsEqual<SOLine.orderType>>>.
					Where<SOOrder.orderNbr.IsEqual<@P.AsString>>.View.Select(Base, "001558").ToList();

			var joinWithSingleTable = SelectFrom<SOOrder>.LeftJoin<SOLine>.On<SOOrder.orderNbr.IsEqual<SOLine.orderNbr>.
				And<SOOrder.orderType.IsEqual<SOLine.orderType>>>.SingleTableOnly.
					Where<SOOrder.orderNbr.IsEqual<@P.AsString>>.View.Select(Base, "001558").ToList();

			var havingTest = SelectFrom<SOOrder>.LeftJoin<SOLine>.On<SOOrder.orderNbr.IsEqual<SOLine.orderNbr>.
					And<SOOrder.orderType.IsEqual<SOLine.orderType>>>.AggregateTo<Sum<SOOrder.curyOrderTotal>>
				.Having<SOLine.baseOpenQty.Averaged.IsGreater<@P.AsDecimal>>.View.Select(Base, 35.6m);

			return adapter.Get();
		}
	}
}

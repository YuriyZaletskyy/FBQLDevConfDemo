using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PX.Data;
using PX.Data.BQL;

namespace FBQLPerformanceTest
{
	[Serializable]
	public class ProcessingPerformanceDummy : IBqlTable
	{
		#region Selected
		public abstract class selected : PX.Data.BQL.BqlBool.Field<selected> { }
		protected bool? _Selected = false;
		[PXBool()]
		[PXUnboundDefault]
		[PXUIField(DisplayName = "Selected")]
		public virtual bool? Selected
		{
			get
			{
				return _Selected;
			}
			set
			{
				_Selected = value;
			}
		}
		#endregion

		public abstract class caption : BqlString.Field<ProcessingPerformanceDummy.caption> { }

		[PXDBString(IsKey = true)]
		[PXUIField(DisplayName = "Caption")]
		public string Caption { get; set; }
	}

	[Serializable]
	public class ProcessingPerformanceDAC : IBqlTable
	{
		public abstract class results : BqlString.Field<ProcessingPerformanceDAC.results> { }

		[PXString(4000)]
		[PXUIField(DisplayName = "Results")]
		public string Results { get; set; }
	}

	[Serializable]
	public class LogMessage : IBqlTable
	{
		#region Message
		[PXDBString(300, IsKey = true, IsUnicode = true, InputMask = "")]
		[PXUIField(DisplayName = "Message")]
		public virtual string Message { get; set; }
		public abstract class message : BqlString.Field<message> { }
		#endregion

		#region Milliseconds FBQL
		[PXDBInt()]
		[PXUIField(DisplayName = "Milliseconds FBQL")]
		public virtual int? MillisecondsF { get; set; }
		public abstract class millisecondsF : BqlInt.Field<millisecondsF> { }
		#endregion

		#region Milliseconds BQL
		[PXDBInt()]
		[PXUIField(DisplayName = "Milliseconds BQL")]
		public virtual int? MillisecondsB { get; set; }
		public abstract class millisecondsB : BqlInt.Field<millisecondsB> { }
		#endregion

		#region Milliseconds BQL
		[PXDBInt()]
		[PXUIField(DisplayName = "Milliseconds BQL")]
		public virtual int? Iterations { get; set; }
		public abstract class iterations : BqlInt.Field<iterations> { }
		#endregion

		#region AsImportScenario
		[PXDBBool()]
		[PXUIField(DisplayName = "As Import Scenario")]
		public virtual bool? AsImportScenario { get; set; }
		public abstract class asImportScenario : BqlBool.Field<asImportScenario> { }
		#endregion

	}
}

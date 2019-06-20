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
	[PXCacheName("Student new approach")]
	public class Student2 : IBqlTable
	{
		public abstract class studentID : BqlInt.Field<studentID> { }

		[PXDBIdentity]
		public virtual int? StudentID { get; set; }

		public abstract class studentCD : BqlString.Field<studentCD> { }

		[PXDBString(IsKey = true)]
		[PXSelector(typeof(Search<Student2.studentCD>), typeof(Student2.firstName), typeof(Student2.lastName))]
		public virtual string StudentCD { get; set; }

		public abstract class firstName : BqlString.Field<firstName> { }

		[PXDBString]
		[PXUIField(DisplayName = "First name")]
		public virtual string FirstName { get; set; }

		public abstract class lastName : BqlString.Field<lastName> { }

		[PXDBString]
		[PXUIField(DisplayName = "Last Name")]
		public virtual string LastName { get; set; }
	}
}

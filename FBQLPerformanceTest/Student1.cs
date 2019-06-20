using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBQLPerformanceTest
{
	/// <summary>
	/// This is traditional BQL Entity
	/// </summary>
	[Serializable]
	[PXCacheName("Student old approach")]
	public class Student1 : IBqlTable 
	{
		public abstract class studentID : IBqlField { }

		[PXDBIdentity]
		public virtual int? StudentID { get; set; }

		public abstract class studentCD : IBqlField { }

		[PXDBString(50, IsKey = true, IsUnicode = true)]
		[PXSelector(typeof(Search<Student1.studentCD>), typeof(Student1.firstName), typeof(Student1.lastName))]
		public virtual string StudentCD { get; set; }

		public abstract class firstName : IBqlField { }

		[PXDBString(50)]
		[PXUIField(DisplayName = "First name")]
		public virtual string FirstName { get; set; }

		public abstract class lastName : IBqlField { }

		[PXDBString(50)]
		[PXUIField(DisplayName = "Last Name")]
		public virtual string LastName { get; set; }
	}
}

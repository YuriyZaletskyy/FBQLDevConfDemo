using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PX.Data;
using PX.Data.BQL.Fluent;

namespace FBQLPerformanceTest
{
	public class FBQLStudent1 : PXGraph<FBQLStudent1, Student1>
	{
		public PXSelect<Student1> Students; // Classical BQL graph
	}

	public class FBQLStudent2 : PXGraph<FBQLStudent2, Student2>
	{
		public SelectFrom<Student2>.View Students; // FBQL graph
	}
}

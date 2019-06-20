using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PX.BulkInsert.Installer;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.CR;
using PX.Data.BQL.Fluent;

namespace FBQLPerformanceTest
{
	public class ProcessingStudents : PXGraph<ProcessingStudents>
	{
		public PXFilter<ProcessingPerformanceDAC> PerformanceProcessing;

		public PXProcessing<ProcessingPerformanceDummy> ProcessingItems;

		public ProcessingStudents()
		{
			ProcessingItems.SetProcessDelegate(ExecuteCreation);
		}

		public PXAction<ProcessingPerformanceDAC> TestPerformance;

		[PXButton]
		[PXUIField(DisplayName = "Test Performance")]
		protected virtual IEnumerable testPerformance(PXAdapter adapter)
		{
			 PXLongOperation.StartOperation(this, delegate
				{
					ExecuteCreation(null);
				});

			return adapter.Get();
		}

		protected virtual IEnumerable processingItems()
		{
			var dummy = new ProcessingPerformanceDummy();
			var result = new List<ProcessingPerformanceDummy>();
			dummy.Caption = "Test Performance";
			
			result.Add(dummy);

			return result;

		}

		public static void ExecuteCreation(List<ProcessingPerformanceDummy> items)
		{
			RunPerformanceSelect(10);
			RunPerformanceSelect(100);
			RunPerformanceSelect(100);
			RunPerformanceSelect(2000);
			RunPerformanceSelect(4000);
			RunPerformanceSelect(6000);
			RunPerformanceSelect(10000);
			RunPerformanceSelect(15000);
			RunPerformanceSelect(20000);
			//RunPerformanceSelect(40000);
			//RunPerformanceSelect(80000);
			//RunPerformanceSelect(100000);


			RunPerformanceTest(10);
			RunPerformanceTest(100);
			RunPerformanceTest(1000);
			RunPerformanceTest(2000);
			RunPerformanceTest(4000);
			RunPerformanceTest(6000);
			RunPerformanceTest(10000);
			RunPerformanceTest(15000);
			RunPerformanceTest(20000);
			//RunPerformanceTest(40000);
			//RunPerformanceTest(80000);
			//RunPerformanceTest(100000);

			RunPerformanceSelect(10, false);
			RunPerformanceSelect(100, false);
			RunPerformanceSelect(100, false);
			RunPerformanceSelect(2000, false);
			RunPerformanceSelect(4000, false);
			RunPerformanceSelect(6000, false);
			RunPerformanceSelect(10000, false);
			RunPerformanceSelect(15000, false);
			RunPerformanceSelect(20000, false);
			//RunPerformanceSelect(40000, false);
			//RunPerformanceSelect(80000, false);
			//RunPerformanceSelect(100000, false);


			RunPerformanceTest(10, false);
			RunPerformanceTest(100, false);
			RunPerformanceTest(1000, false);
			RunPerformanceTest(2000, false);
			RunPerformanceTest(4000, false);
			RunPerformanceTest(6000, false);
			RunPerformanceTest(10000, false);
			//RunPerformanceTest(15000, false);
			//RunPerformanceTest(20000, false);
			//RunPerformanceTest(40000, false);
			//RunPerformanceTest(80000, false);
			//RunPerformanceTest(100000, false);
		}

		private static void RunPerformanceSelect(int numberOfIterations, bool cachingOfGraph = true)
		{
			StringBuilder sb = new StringBuilder();

			int numberOfNotNull = 872;

			Random rand = new Random();

			var sw = new Stopwatch();
			sw.Start();
			var graph0 = PXGraph.CreateInstance<FBQLStudent1>();
			for (int i = 0; i < numberOfIterations; i++)
			{
				int startIdx = rand.Next(numberOfNotNull);

				if (cachingOfGraph)
				{
					graph0 = PXGraph.CreateInstance<FBQLStudent1>();
				}
				
				var contact = PXSelect<Contact, Where<Contact.displayName, IsNotNull, And<Contact.displayName, Contains<Required<Contact.displayName>>>>>.SelectWindowed(graph0, startIdx, 1, ' ').First();

				var firstName = contact.GetItem<Contact>().DisplayName.Split(' ')[0];
				var secondName = contact.GetItem<Contact>().DisplayName.Split(' ')[1];
			}
			sw.Stop();
			sb.Append($"Classical select took {sw.ElapsedMilliseconds} milliseconds on {numberOfIterations} of iterations");

			var sw1 = new Stopwatch();
			sw1.Start();

			var graph1 = PXGraph.CreateInstance<FBQLStudent2>();
			for (int i = 0; i < numberOfIterations; i++)
			{
				int startIdx = rand.Next(numberOfNotNull);

				if (cachingOfGraph)
				{
					graph1 = PXGraph.CreateInstance<FBQLStudent2>();
				}
				

				//var contact = PXSelect<Contact, Where<Contact.displayName, IsNotNull, And<Contact.displayName, Contains<Required<Contact.displayName>>>>>.SelectWindowed(graph, startIdx, 1, ' ').First();
				var contact = SelectFrom<Contact>.Where<Contact.displayName.IsNotNull.And<Contact.displayName.Contains<@P.AsString>>>.View.SelectWindowed(graph1, startIdx, 1, ' ').First();

				var firstName = contact.GetItem<Contact>().DisplayName.Split(' ')[0];
				var secondName = contact.GetItem<Contact>().DisplayName.Split(' ')[1];
				
			}
			sw1.Stop();
			sb.Append($"Performance Select");
			sb.Append(Guid.NewGuid().ToString());

			PXDatabase.Insert<LogMessage>(
				new PXDataFieldAssign<LogMessage.message>(sb.ToString()),
				new PXDataFieldAssign<LogMessage.millisecondsB>(sw.ElapsedMilliseconds),
				new PXDataFieldAssign<LogMessage.millisecondsF>(sw1.ElapsedMilliseconds),
				new PXDataFieldAssign<LogMessage.iterations>(numberOfIterations),
				new PXDataFieldAssign<LogMessage.asImportScenario>(cachingOfGraph)
			);
		}

		private static void RunPerformanceTest(int numberOfIterations, bool cachingOfGraph = true)
		{
			//Initially create instance of graph with old BQL with usage of BQL
			//Then create instance of graph with F-BQL and with usage of BQL for 

			//Right now in Acumatica sales demo following SQL:
			//select * from Contact where LEN(DisplayName ) > 0
			//gives 872 rows
			//I plan to create some amount of 

			StringBuilder sb = new StringBuilder();
			
			int numberOfNotNull = 872;

			Random rand = new Random();

			var sw = new Stopwatch();
			sw.Start();

			var graph0 = PXGraph.CreateInstance<FBQLStudent1>();

			for (int i = 0; i < numberOfIterations; i++)
			{
				int startIdx = rand.Next(numberOfNotNull);

				if (cachingOfGraph)
				{
					graph0 = PXGraph.CreateInstance<FBQLStudent1>();
				}
				
				var contact = PXSelect<Contact, Where<Contact.displayName, IsNotNull, And<Contact.displayName, Contains<Required<Contact.displayName>>>>>.SelectWindowed(graph0, startIdx, 1, ' ').First();

				var firstName = contact.GetItem<Contact>().DisplayName.Split(' ')[0];
				var secondName = contact.GetItem<Contact>().DisplayName.Split(' ')[1];
				graph0.Clear();

				var student = new Student1();
				student.StudentCD = firstName.ToUpper() + secondName.ToUpper();
				student = graph0.Students.Insert(student);

				student.FirstName = firstName;
				student.LastName = secondName;
				graph0.Students.Update(student);
				graph0.Persist();
			}
			sw.Stop();
			sb.Append($"Classical way took {sw.ElapsedMilliseconds} milliseconds on {numberOfIterations} of iterations");

			var sw1 = new Stopwatch();
			sw1.Start();

			var graph1 = PXGraph.CreateInstance<FBQLStudent2>();

			for (int i = 0; i < numberOfIterations; i++)
			{
				int startIdx = rand.Next(numberOfNotNull);

				if (cachingOfGraph)
				{
					graph1 = PXGraph.CreateInstance<FBQLStudent2>();
				}

				//var contact = PXSelect<Contact, Where<Contact.displayName, IsNotNull, And<Contact.displayName, Contains<Required<Contact.displayName>>>>>.SelectWindowed(graph, startIdx, 1, ' ').First();
				var contact = SelectFrom<Contact>.Where<Contact.displayName.IsNotNull.And<Contact.displayName.Contains<@P.AsString>>>.View.SelectWindowed(graph1, startIdx, 1, ' ').First();


				var firstName = contact.GetItem<Contact>().DisplayName.Split(' ')[0];
				var secondName = contact.GetItem<Contact>().DisplayName.Split(' ')[1];
				graph1.Clear();

				var student = new Student2();
				student.StudentCD = firstName.ToUpper() + secondName.ToUpper();
				student = graph1.Students.Insert(student);

				student.FirstName = firstName;
				student.LastName = secondName;
				graph1.Students.Update(student);
				graph1.Persist();
			}
			sw1.Stop();
			sb.Append($"Performance Persist");
			sb.Append(Guid.NewGuid().ToString());

			PXDatabase.Insert<LogMessage>(
				new PXDataFieldAssign<LogMessage.message>(sb.ToString()),
				new PXDataFieldAssign<LogMessage.millisecondsB>(sw.ElapsedMilliseconds),
				new PXDataFieldAssign<LogMessage.millisecondsF>(sw1.ElapsedMilliseconds),
				new PXDataFieldAssign<LogMessage.iterations>(numberOfIterations),
				new PXDataFieldAssign<LogMessage.asImportScenario>(cachingOfGraph)
			);
		}
	}
}

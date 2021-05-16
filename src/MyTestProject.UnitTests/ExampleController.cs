using System;

namespace MyTestProject {

	public class ExampleController {

		private readonly IDatabaseService DatabaseService;

		private readonly IExecutionService ExecutionService;

		public ExampleController(
			IDatabaseService databaseService,
			IExecutionService executionService
		) {

			this.DatabaseService = databaseService ?? new DatabaseService();
			this.ExecutionService = executionService ?? new ExecutionService();
		}

		public string Execute(int value) {

			return this.ExecutionService.Echo(value);
        }
	}

	public interface IDatabaseService {

    }

	public interface IExecutionService {

		string Echo(int value);
	}

	public class DatabaseService : IDatabaseService {


	}

	public class ExecutionService : IExecutionService {

		public string Echo(int value) {
			return $"Echo back {value}";
        }
	}
}

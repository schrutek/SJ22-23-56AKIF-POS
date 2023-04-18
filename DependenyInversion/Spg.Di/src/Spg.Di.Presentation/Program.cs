using Spg.Di.Application;
using Spg.Di.DomainModel.Interfaces;
using Spg.Di.DomainModel.Model;

ILogger logger = new LoggingService();

MyModelEntity entity = new MyModelEntity(logger);
entity.Name = "irrelevant!";
entity.DoSomething();
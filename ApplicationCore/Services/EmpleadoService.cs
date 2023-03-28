using System;
using ApplicationCore.IServices;
using ApplicationCore.IServices.CRUD;
using Infrastructure.Models;
using Infrastructure.Repositories.Generic;
using Microsoft.Extensions.Logging;

namespace ApplicationCore.Services
{
	public class EmpleadoService : CRUD<Empleado>, IEmpleadoService
	{
		private readonly IRepository _repo;
		private readonly ILogger<EmpleadoService> _logger;

		public EmpleadoService(IRepository repo, ILogger<EmpleadoService> logger) : base(repo, logger)
		{
			_repo = repo;
			_logger = logger;
		}
    }
}


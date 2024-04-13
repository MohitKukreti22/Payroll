using Microsoft.EntityFrameworkCore;
using PayRoll.Contexts;
using PayRoll.Interfaces;


using PayRoll.Exceptions;

using PayRoll.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace PayRoll.Repositories
{

    public class ValidationRepository : IRepository<string, Validation>
    {
        private readonly RequestTarkerContext _context;
        private readonly ILogger<ValidationRepository> _loggerValidationRepository;

        public ValidationRepository(RequestTarkerContext context, ILogger<ValidationRepository> loggerValidationRepository)
        {
            _context = context;
            _loggerValidationRepository = loggerValidationRepository;
        }

        public async Task<Validation> Add(Validation item)
        {
            _context.Validations.Add(item);
            await _context.SaveChangesAsync();
            _loggerValidationRepository.LogInformation($"Added New Validation : {item.Email}");
            return item;
        }

        public async Task<Validation?> Delete(string key)
        {
            var foundedValidation = await Get(key);
            if (foundedValidation == null)
            {
                return null;
            }
            else
            {
                _context.Validations.Remove(foundedValidation);
                await _context.SaveChangesAsync();
                return foundedValidation;
            }
        }

        public async Task<Validation?> Get(string key)
        {
            var foundedValidation = await _context.Validations.FirstOrDefaultAsync(validation => validation.Email == key);
            if (foundedValidation == null)
            {
                return null;
            }
            else
            {
                return foundedValidation;
            }
        }

        public async Task<List<Validation>?> GetAll()
        {
            var allValidations = await _context.Validations.ToListAsync();
            if (allValidations.Count == 0)
            {
                return null;
            }
            else
            {
                return allValidations;
            }
        }

        public async Task<Validation> Update(Validation item)
        {
            _context.Entry<Validation>(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return item;
        }
    }
}


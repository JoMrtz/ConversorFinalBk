﻿using ConversorFinal_BE.Data;
using ConversorFinalBk.Entities;
using Microsoft.AspNetCore.Identity;

namespace ConversorFinalBk.Services
{
    public class ConversionService
    {
        private readonly ConversorContext _conversorContext;
        private readonly SessionService _sessionService;
        public ConversionService(ConversorContext conversorContext, SessionService sessionService)
        {
            _conversorContext = conversorContext;
            _sessionService = sessionService;   
        }
        public List<Conversion> GetAll()
        {
            return _conversorContext.Conversion.ToList();
        }
        public void IncrementCounter()
        {
            var IdUser = _sessionService.getOneById();
            var counter2 = _conversorContext.Conversion.FirstOrDefault(c => c.IdUser == IdUser);
            var user = _conversorContext.User.FirstOrDefault(c => c.Id == IdUser);
            var subs = _conversorContext.Subscription.FirstOrDefault(c => c.Id == user.IdSubscription);
            var Restantes = subs.MaxAttemps - counter2.Attemps;
            if (Restantes >= 0) { 
            counter2.Attemps++;
            }
            else
                throw new Exception("No tene monedita");
                
            _conversorContext.Update(counter2);
            _conversorContext.SaveChanges();
        }        
    }
}
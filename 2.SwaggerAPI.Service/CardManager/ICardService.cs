using SwaggerAPI.ViewModel.AdminViewModel;
using SwaggerAPI.ViewModel.CalendarRequest;
using SwaggerAPI.ViewModel.CardManager;
using SwaggerAPI.ViewModel.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace _2.SwaggerAPI.Service.CardManager
{
   public interface ICardService
    {
        Task<CardReponse> Create(CreateCard createCard);
        Task<CardReponse> TopUp(TopUp topUp);
        Task<CardReponse> ChangeCardStatus(ChangeCardStatusRequest changeCardStatusRequest);
        Task<List<RevenueStatisticsReponse>> RevenueStatisticsReponse();
        Task<PatientCardResponse> GetCardInformation(PatientCardRequest patientCardRequest);
        Task<List<RevenueStatisticsReponse>> GetRevenueStatisticsByAction(string action);
        Task<List<RevenueStatisticsReponse>> GetRevenueStatisticsByDate(DateTime dateTime);
        Task<List<RevenueStatisticsReponse>> GetRevenueStatisticsByActionAndDate(string action, DateTime dateTime);
        Task<List<CardInformationViewModel>> GetALLCard();
    }
}

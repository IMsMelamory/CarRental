using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalDesktop.ViewModel
{
   public class RentViewModel: BaseViewModel
   {
       private string _clientNumderLicense;
       private string _carNmber;
       private DateTime _startRent;
       private DateTime _endRent;
       private int _dayRentCount;
       private double _fine;

       public string ClientNumberLicense
        {
           get => _clientNumderLicense;
           set
           {
               _clientNumderLicense = value;
               OnPropertyChanged();
           }
       }
       public string CarNumber
        {
           get => _carNmber;
           set
           {
               _carNmber = value;
               OnPropertyChanged();
           }
       }
       public DateTime StartRent
        {
           get => _startRent;
           set
           {
               _startRent = value;
               OnPropertyChanged();
           }
       }
       public DateTime EndRent
       {
           get => _endRent;
           set
           {
               _endRent = value;
               OnPropertyChanged();
           }
       }
       public int DayRentCount
        {
           get => _dayRentCount;
           set
           {
               _dayRentCount = value;
               OnPropertyChanged();
           }
       }
       public double Fine
        {
           get => _fine;
           set
           {
               _fine = value;
               OnPropertyChanged();
           }
       }

   }
}

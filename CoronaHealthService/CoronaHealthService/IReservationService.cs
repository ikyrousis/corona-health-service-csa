using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace CoronaHealthService
{
    [ServiceContract]
    public interface IReservationService
    {
        [OperationContract]
        string GetServiceName();

        [OperationContract]
        List<Ggd> GetAvailableTestLocations();

        [OperationContract]
        Boolean MakeReservation(string centerLocation);

        [OperationContract]
        Boolean CancelReservation(string centerLocation);
    }
}

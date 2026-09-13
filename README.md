# Corona Health Service (CSA)

A WCF client–server application for managing GGD corona test locations. The service exposes a reservation interface for citizens and an admin interface with real-time (duplex) updates for GGD staff.

Built for the Fontys ICT CSA (Client-Server Applications) exam, October 2020.

## Structure

| Solution | Project | Description |
| --- | --- | --- |
| `CoronaHealthService` | `CoronaHealthService` | WCF service library, hosted by `WcfSvcHost` |
| `GGDCoronaTestManagement` | `CoronaTestReservationApp` | WinForms client: service name, available test locations, make/cancel reservation |
| `GGDCoronaTestManagement` | `GGDAdminApp` | WinForms client: all GGD info, increase capacity, add a test location, live duplex callbacks |

## Service contracts

- **IReservationService** – `GetServiceName`, `GetAvailableTestLocations`, `MakeReservation`, `CancelReservation`
- **IAdminService** – `GetGgdInfo`, `IncreaseCapacity`, `Connect`/`Disconnect`, `AddAdditionalCoronaTestLocation`
- **IServiceCallBack** – duplex callbacks `OnIncreaseCapacity`, `OnUpdateLocations`

Binding: `wsDualHttpBinding` at `http://localhost:8733/Design_Time_Addresses/CoronaHealthService/Service1/`.

## Requirements

- Windows (WCF + WinForms on .NET Framework)
- Visual Studio 2019 or later (with WCF / .NET desktop development workload)
- .NET Framework 4.7.2

## Running

Run in this order:

1. Make sure no stale host is holding port 8733: `taskkill /IM WcfSvcHost.exe /F` (also stop any previous VS debugging session).
2. Start the service: open `CoronaHealthService/CoronaHealthService.sln` and run it (F5/Ctrl+F5). VS starts `WcfSvcHost` at the address above; verify by opening `http://localhost:8733/Design_Time_Addresses/CoronaHealthService/Service1/?wsdl` in a browser.
3. Start the clients: open `GGDCoronaTestManagement/GGDCoronaTestManagement.sln` and run `CoronaTestReservationApp` and/or `GGDAdminApp`.
   - `GGDAdminApp` must start **after** the service (its constructor calls `Connect()` and crashes otherwise).
   - `CoronaTestReservationApp` opens without the service but all calls fail until it is running.
4. Shut down in reverse: close the clients first (the admin app calls `Disconnect()`), then stop the service.

Service data is in-memory, so restarting the service resets capacities and added locations.

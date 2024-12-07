using FixLife.WebApiDomain.Exceptions;
using FixLife.WebApiInfra.Abstraction.Dashboard;
using MediatR;

namespace FixLife.WebApiQueries.Dashboard.Queries
{
    public class GetDetectionPushHandler : IRequestHandler<GetDetectionPushQuery, GetDetectionPushResponse>
    {
        private readonly IDashboardService _dashboardService;
        public GetDetectionPushHandler(IDashboardService dashboardService) { 
            _dashboardService= dashboardService;
        }

        public async Task<GetDetectionPushResponse?> Handle(GetDetectionPushQuery request, CancellationToken cancellationToken)
        {
            var notificationToSend = await _dashboardService.HandleDetectPush(request.UserId);

            if(notificationToSend != null && notificationToSend is GetDetectionPushResponse)
            {
                return notificationToSend as GetDetectionPushResponse;
            }

            throw new PlanNotFoundException("Notification sender does not found plans to handle notification!");
        }
    }
}

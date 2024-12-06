using MediatR;

namespace FixLife.WebApiQueries.FirstPlan.Commands
{
    public class AddPlanCommand : IRequest<CreatePlanResponse>
    {
        public CreatePlanRequest Request { get; set; }
        public string UserId { get; set; }
        public AddPlanCommand(CreatePlanRequest request, string userId)
        {
            Request = request;
            UserId = userId;
        }
    }
}

using MediatR;

namespace FixLife.WebApiQueries.FirstPlan.Commands
{
    public class EditPlanCommand : IRequest<EditPlanResponse>
    {
        public EditPlanRequest Request { get; set; }
        public string UserId { get; set; }
        public EditPlanCommand(EditPlanRequest request, string userId)
        {
            Request = request;
            UserId = userId;
        }
    }
}

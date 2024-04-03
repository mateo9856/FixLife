using FixLife.ClientApp.Models;

namespace FixLife.ClientApp.Common.Abstraction
{
    public interface IDashboardService
    {
        Task SaveBasicDataToFile();
        AppPlan GetAppPlanData();
    }
}

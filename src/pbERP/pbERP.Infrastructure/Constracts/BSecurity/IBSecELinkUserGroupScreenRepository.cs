using pbERP.Domain.Models.BSecurity;

namespace pbERP.Infrastructure.Constracts.BSecurity;

public interface IBSecELinkUserGroupScreenRepository : IGenericRepository<BSecELinkUserGroupScreen>
{
   Task<int> InsertLinkUserGroupScreen(List<BSecELinkUserGroupScreen> models);
}

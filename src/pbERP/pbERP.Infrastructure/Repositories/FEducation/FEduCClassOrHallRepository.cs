using Microsoft.EntityFrameworkCore;
using pbERP.DataStructure;
using pbERP.Domain.DTOs.FEducation;
using pbERP.Domain.Models.FEducation;
using pbERP.Infrastructure.Constracts.FEducation;

namespace pbERP.Infrastructure.Repositories.FEducation;

public class FEduCClassOrHallRepository : GenericRepository<FEduCClassOrHall>, IFEduCClassOrHallRepository
{
  public FEduCClassOrHallRepository(pbERPContext _context) : base(_context)
  {
  }

  public async Task<IReadOnlyList<FEduCClassOrHallDto>> GetClassOrHallsAsync()
  {
    IReadOnlyList< FEduCClassOrHallDto> classOrHalls = await context.FEduCClassOrHalls.Include(x => x.ClassRoom).Select(x => new FEduCClassOrHallDto 
    { 
      HallSeatId = x.HallSeatId,
      HallSeatNumber = x.HallSeatNumber,
      HallSeatCharge = x.HallSeatCharge,
      ClassRoomId = x.ClassRoomId,
      ClassRoomName = x.ClassRoom.ClassRoomName
    }).OrderByDescending(x => x.HallSeatId).ToListAsync();
    return classOrHalls;
  }
}

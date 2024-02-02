using Contracts.Constants;
using Contracts.GlobalExeptionHandler.Exceptions;
using Contracts.V1.Jogging.Filters;
using Contracts.V1.Jogging.Models;
using Contracts.V1.SharedModels;
using DataAccess.SQL.Entities;
using DataAccess.SQL.UnitOfWork;
using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;

namespace BusinessLogic.JoggingTracker
{
    public interface IJoggingTrackerManager
    {
        public Task<ResourceCollection<JoggingEntity>> GetRecoreds(JoggingTrackerFilter? filter);
        public Task<JoggingEntity> GetRecoredById(int id);
        public Task<JoggingEntity> CreateRecord(JoggingModel model);
        public Task<JoggingEntity> UpdateRecord(int id, JoggingModel model);
        public Task DeleteRecord(int id);
    }

    public class JoggingTrackerManager : IJoggingTrackerManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly string UserName;
        private readonly string UserId;

        public JoggingTrackerManager(IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            UserName = httpContextAccessor.HttpContext.User?.FindFirst("userName")?.Value ?? string.Empty;
            UserId = httpContextAccessor.HttpContext.User?.FindFirst("id")?.Value ?? string.Empty;
        }

        public async Task<ResourceCollection<JoggingEntity>> GetRecoreds(JoggingTrackerFilter? filter)
        {
            var records = (await _unitOfWork.JoggingRepository.GetItemsAsync(GetExpressions(filter), null, filter))
                .ToList();


            return new ResourceCollection<JoggingEntity>(records,
                await _unitOfWork.JoggingRepository.CountAsync(GetExpressions(filter)));
        }

        public async Task<JoggingEntity> GetRecoredById(int id)
        {
            var record = await _unitOfWork.JoggingRepository.FirstOrDefaultAsync(x => x.Id == id);
            ValidateRecordExist(id, record);

            return record!;
        }

        public async Task<JoggingEntity> CreateRecord(JoggingModel model)
        {
            var entity = model.ToEntity().MapEntityTrackableInformation(UserName, DateTimeOffset.Now);
            entity.UserId = UserId;
            _unitOfWork.JoggingRepository.Create(entity);
            await _unitOfWork.SaveChanges();

            return (await _unitOfWork.JoggingRepository.FirstOrDefaultAsync(x => x.Id == entity.Id))!; 
        }

        public async Task<JoggingEntity> UpdateRecord(int id, JoggingModel model)
        {
            var record = await _unitOfWork.JoggingRepository.FirstOrDefaultAsync(x => x.Id == id);
            ValidateRecordExist(id, record);

            record!.Date = model.Date;
            record.Distance = model.Distance;
            record.Time = model.Time;
            record.Location = model.Location;


            _unitOfWork.JoggingRepository.Update(
                record.MapEntityTrackableInformation(record.CreatedBy,record.CreatedOn, UserName));
            await _unitOfWork.SaveChanges();

            return (await _unitOfWork.JoggingRepository.FirstOrDefaultAsync(x => x.Id == record.Id))!;
        }

        public async Task DeleteRecord(int id)
        {
            var record = await _unitOfWork.JoggingRepository.FirstOrDefaultAsync(x => x.Id == id);
            ValidateRecordExist(id, record);

            _unitOfWork.JoggingRepository.Delete(record!);
            await _unitOfWork.SaveChanges();
        }

        private static void ValidateRecordExist(int id, JoggingEntity? record)
        {
            if (record is null)
                throw new ItemNotFoundException(nameof(id), ValidationMessages.RecordIdDoesNotExist(id));
        }

        private static List<Expression<Func<JoggingEntity, bool>>> GetExpressions(JoggingTrackerFilter? filter)
        {
            List<Expression<Func<JoggingEntity, bool>>> experssions = new();
            if (filter is null) return experssions;

            if (filter.Ids.Any())
            {
                experssions.Add(item => filter.Ids.Any(x => x == item.Id));
            }

            return experssions;
        }
    }
}

using Core.Dtos.Workshop;
using Core.Models;

namespace Backend.Mappers
{
    public static class WorkshopMappers
    {
        public static WorkshopDto ToWorkshopDto(this Workshop workshopModel)
        {
            return new WorkshopDto
            {
                Id = workshopModel.Id,
                Hours = workshopModel.Hours,
                Name = workshopModel.Name,
                SubjectId = workshopModel.SubjectId,
                Subject = workshopModel.Subject.ToSubjectDto()
            };
        }

        public static Workshop ToWorkshopFromCreateDto(this CreateWorkshopRequestDto workshopDto)
        {
            return new Workshop
            {
                Hours = workshopDto.Hours,
                Name = workshopDto.Name,
                SubjectId = workshopDto.SubjectId
            };
        }

        public static Workshop ToWorkshopFromUpdateDto(this UpdateWorkshopRequestDto workshopDto)
        {
            return new Workshop
            {
                Hours = workshopDto.Hours,
                Name = workshopDto.Name,
                SubjectId = workshopDto.SubjectId
            };

        }
    }
}

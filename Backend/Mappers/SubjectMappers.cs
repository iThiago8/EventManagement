using Core.Dtos.Subject;
using Core.Models;

namespace Backend.Mappers
{
    public static class SubjectMappers
    {
        public static SubjectDto ToSubjectDto(this Subject subjectModel)
        {
            return new SubjectDto
            {
                Id = subjectModel.Id,
                Name = subjectModel.Name
            };
        }

        public static Subject ToSubjectFromDto(this SubjectDto subjectDto)
        {
            return new Subject
            {
                Id = subjectDto.Id,
                Name = subjectDto.Name
            };
        }

        public static Subject ToSubjectFromCreateDto(this CreateSubjectRequestDto subjectDto)
        {
            return new Subject
            {
                Name = subjectDto.Name
            };
        }
        public static Subject ToSubjectFromUpdateDto(this UpdateSubjectRequestDto subjectDto)
        {
            return new Subject
            {
                Name = subjectDto.Name
            };
        }
    }
}

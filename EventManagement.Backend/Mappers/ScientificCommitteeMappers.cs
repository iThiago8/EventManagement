using Backend.Dtos.ScientificCommittee;
using Backend.Models;

namespace Backend.Mappers
{
    public static class ScientificCommitteeMappers
    {
        public static ScientificCommitteeDto ToScientificCommitteeDto(this ScientificCommittee scientificCommitteeModel)
        {
            return new ScientificCommitteeDto
            {
                Id = scientificCommitteeModel.Id,
                Name = scientificCommitteeModel.Name,
                Subject = scientificCommitteeModel.Subject.ToSubjectDto(),
                SubjectId = scientificCommitteeModel.SubjectId
            };
        }

        public static ScientificCommittee ToScientificCommitteFromCreateDto(this CreateScientificCommitteeRequestDto scientificCommitteeDto)
        {
            return new ScientificCommittee
            {
                Name = scientificCommitteeDto.Name,
                SubjectId = scientificCommitteeDto.SubjectId
            };
        }
        public static ScientificCommittee ToScientificCommitteFromUpdateDto(this UpdateScientificCommitteeRequestDto scientificCommitteeDto)
        {
            return new ScientificCommittee
            {
                Name = scientificCommitteeDto.Name,
                SubjectId = scientificCommitteeDto.SubjectId
            };
        }
    }
}

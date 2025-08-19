using apis.Dtos.ScientificCommittee;
using apis.Models;

namespace apis.Mappers
{
    public static class ScientificCommitteeMappers
    {
        public static ScientificCommitteeDto ToScientificCommitteeDto(this ScientificCommittee scientificCommitteeModel)
        {
            return new ScientificCommitteeDto
            {
                Id = scientificCommitteeModel.Id,
                Name = scientificCommitteeModel.Name,
                Subject = scientificCommitteeModel.Subject,
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

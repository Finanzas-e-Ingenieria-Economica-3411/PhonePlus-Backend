using PhonePlus.Domain.Enums;

namespace PhonePlus.Interface.DTO.Credits;

public record GracePeriodDto(int Period, GraceType Type);
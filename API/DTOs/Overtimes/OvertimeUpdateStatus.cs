using API.Utilities.Enums;

namespace API.DTOs.Overtimes
{
	public class OvertimeUpdateStatus
	{
		public Guid Guid { get; set; }
		public StatusLevel Status { get; set; }
	}
}

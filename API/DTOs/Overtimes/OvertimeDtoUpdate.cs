﻿using API.Models;
using API.Utilities.Enums;

namespace API.DTOs.Overtimes;

public class OvertimeDtoUpdate
{
    public Guid Guid { get; set; }
    public string OvertimeNumber { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string? Remarks { get; set; }
    public StatusLevel Status { get; set; }
    public Guid EmployeeGuid { get; set; }

    public static implicit operator Overtime(OvertimeDtoUpdate getOvertime)
    {
        return new()
        {
            Guid = getOvertime.Guid,
            StartDate = getOvertime.StartDate,
            OvertimeNumber = getOvertime.OvertimeNumber,
            EndDate = getOvertime.EndDate,
            Remarks = getOvertime.Remarks,
            Status = getOvertime.Status,
            EmployeeGuid = getOvertime.EmployeeGuid,
        };
    }

    public static explicit operator OvertimeDtoUpdate(Overtime overtime)
    {
        return new()
        {
            Guid = overtime.Guid,
            StartDate = overtime.StartDate,
            OvertimeNumber = overtime.OvertimeNumber,
            EndDate = overtime.EndDate,
            Remarks = overtime.Remarks,
            Status = overtime.Status,
            EmployeeGuid = overtime.EmployeeGuid,
        };
    }
}

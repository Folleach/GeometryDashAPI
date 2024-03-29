﻿using GeometryDashAPI.Attributes;
using GeometryDashAPI.Server.Dtos;

namespace GeometryDashAPI.Server.Responses
{
    [Sense("||")]
    [AsStruct]
    public class AccountInfoResponse : GameObject
    {
        [GameProperty("0")] public Account Account { get; set; }
    }
}
﻿using MessagePack;

namespace eTickets.DTO
{
    public class ActorDto
    {
        
        public int Id { get; set; }
        public string ProfilePictureURL { get; set; }
        public string FullName { get; set; }
        public string Bio { get; set; }
    }
}

using System;
using WiventoryAPI.Models;
using WiventoryAPI.DTOs;


namespace WiventoryAPI.Services
{
    public class HotelService
    {
        public readonly IStorageService<Hotel> _storage;

        public HotelService(IStorageService<Hotel> storage)
        {
            _storage = storage;
        }

        //public async Task<CreateHotelResponseDto> CreateHotel(CreateHotelRequestDto hotelDto, CreateUserRequestDto userDto)
        //{
		/*
            var hotel = new Hotel
            {
                Name = hotelDto.Name,
                     Email = hotelDto.Email,
                     Subdomain = hotelDto.Subdomain,
                     Website = hotelDto.Website
            };

            var user = new User
            {
                FirstName = userDto.FirstName,
                          LastName = userDto.LastName,
                          Email = userDto.Email,
                          Password = userDto.Password // (make sure to hash this!)
            };

            await _storage.AddAsync(hotel);
            await _storage.AddAsync(user);

            return new CreateHotelResponseDto
            {
                HotelId = hotel.Id,
                        UserId = user.Id,
                        Message = "Hotel and admin user created successfully"

            };
	    */
        //}

    }
}

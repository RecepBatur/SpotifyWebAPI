using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entities.Concrete;
using Entities.Dtos;
using Entities.Dtos.PlaylistDtos;
using Entities.Dtos.SongDtos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class SongManager : ISongService
    {
        private readonly ISongDal _songDal;

        public SongManager(ISongDal songDal)
        {
            _songDal = songDal;
        }

        public IDataResult<bool> Add(SongAddDto song)
        {
            var addedSong = new Song
            {
                Name = song.Name,
                Status = song.Status,
                CreatedDate = song.CreatedDate,
                ModifiedDate = song.ModifiedDate,
            };
            _songDal.Add(addedSong);
            return new SuccessDataResult<bool>(true, Messages.SongAdded);
        }

        public IDataResult<List<SongListDto>> GetList()
        {
            var list = _songDal.GetList();
            var songList = new List<SongListDto>();
            foreach (var song in list)
            {
                songList.Add(new()
                {
                    Id = song.Id,
                    Name = song.Name,
                    Status = song.Status,
                    CreatedDate = song.CreatedDate,
                    ModifiedDate = song.ModifiedDate,
                });
            }
            return new SuccessDataResult<List<SongListDto>>(songList, Messages.SongList);
        }

        public async Task<IDataResult<SongListApiDto.Root>> GetSongListApi(string albumId)
        {
            SongListApiDto.Root songlistDtos = new SongListApiDto.Root();
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://api.spotify.com/v1/albums/{albumId}"),
                Headers =
    {
        { "Accept", "application/json" },
        { "Authorization", "Bearer BQDp1OAfwg5fjojnjY0SDQyd_FX7NONiDIgJBbSurfChcSYegF-X7x7RvBLtgY43imF8Ao_zpa_ERfChZg7rny7rp1gBhkgpVBR1759u9S4hkcFq16V0xLgwsVn-lbAMPbbBpJIwT1bZuiBtR9PbJDYTSjtcESeXvp45178w1eUkIrGQ5iPmEvPzTcX9kI2H_STq" },
    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                if (body != null)
                {
                    SongListApiDto.Root data = JsonConvert.DeserializeObject<SongListApiDto.Root>(body);
                    Console.WriteLine(data);
                    songlistDtos = data;
                }
                Console.WriteLine(body);
                return new SuccessDataResult<SongListApiDto.Root>(songlistDtos, Messages.SongList);
            }
        }

        public async Task<IDataResult<bool>> SongAddApi(string albumId)
        {
            //songDto = new SongAddApiDto.Root();
            SongAddApiDto.Root songlistDtos = new SongAddApiDto.Root();
            var dto = await GetSongListApi(albumId);


            if (dto.Data != null)
            {
                foreach (var item in dto.Data.tracks.items.OrderByDescending(x => x.id).Take(5))
                {
                    Song songAdded = new Song();
                    {
                        songAdded.Name = item.name;
                        songAdded.Status = true;
                    }
                    _songDal.Add(songAdded);

                }

            }
            return new SuccessDataResult<bool>(true, Messages.SongAdded);

        }

        public IDataResult<bool> Update(SongUpdateDto song)
        {
            var updatedSong = new Song
            {
                Id = song.Id,
                Name = song.Name,
                Status = song.Status,
                CreatedDate = song.CreatedDate,
                ModifiedDate = song.ModifiedDate,
            };
            _songDal.Update(updatedSong);
            return new SuccessDataResult<bool>(true, Messages.SongUpdated);
        }

        public IDataResult<bool> UpdateStatus(int songId)
        {
            var song = _songDal.Get(x => x.Id == songId);
            if (song != null)
            {
                song.Status = !song.Status;
                _songDal.Update(song);
                return new SuccessDataResult<bool>(true, Messages.SongStatus);
            }
            else
            {

                return new ErrorDataResult<bool>(false, Messages.SongNotFound);
            }
        }
    }
}

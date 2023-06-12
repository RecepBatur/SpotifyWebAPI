using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos.LibraryDtos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class LibraryAlbumManager : ILibraryAlbumService
    {
        private readonly ILibraryAlbumDal _libraryAlbumDal;
        private readonly IUserDal _userDal;
        private readonly ITokenHelper _tokenHelper;
        public LibraryAlbumManager(ILibraryAlbumDal libraryAlbumDal, IUserDal userDal, ITokenHelper tokenHelper)
        {
            _libraryAlbumDal = libraryAlbumDal;
            _userDal = userDal;
            _tokenHelper = tokenHelper;
        }

        public async Task<IDataResult<bool>> AlbumAddApi(string albumId)
        {
            AlbumsAddApiDto.Root albumaddDtos = new AlbumsAddApiDto.Root();
            var dto = await GetAlbumListApi(albumId);

            if (dto.Data != null)
            {
                LibraryAlbum albumAdded = new LibraryAlbum();
                {
                    albumAdded.AlbumName = dto.Data.name;
                    albumAdded.Status = dto.Data.is_playable;
                    albumAdded.AlbumId = dto.Data.id;

                }
                _libraryAlbumDal.Add(albumAdded);
            }

            return new SuccessDataResult<bool>(true, Messages.AlbumAdded);
        }

        public async Task<IDataResult<LibraryListApiDto.Root>> GetAlbumListApi(string albumId)
        {
            LibraryListApiDto.Root albumlistDtos = new LibraryListApiDto.Root();
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://api.spotify.com/v1/albums/{albumId}"),
                Headers =
    {
        { "Accept", "application/json" },
        { "Authorization", "Bearer BQAhbArY-OEXQHiPTFUkhGP_FZ4mKODLvXZ3ryz_AJMnE-yV7CTEaOvvw__vTuD9zr0xNgC3F98hezkGeh_tJ6MYV2AZ1O3Nq_6q04t3sHQcwyQ_AhMfUvaHoxUXevMf8jK_Wo5rFlF0oXkHKVaJPnv2RxbaXU6JWWURFI63l9uGuB57NNNTvDQsHVrxdtyY1PK-" },
    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                if (body != null)
                {
                    LibraryListApiDto.Root data = JsonConvert.DeserializeObject<LibraryListApiDto.Root>(body);
                    Console.WriteLine(data);
                    albumlistDtos = data;
                }
                Console.WriteLine(body);
                return new SuccessDataResult<LibraryListApiDto.Root>(albumlistDtos, Messages.AlbumList);
            }
        }
    }
}

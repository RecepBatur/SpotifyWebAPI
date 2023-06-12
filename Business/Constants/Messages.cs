using Core.Entities.Concrete;
using Entities.Dtos.AlbumDtos;
using Entities.Dtos.FollowerDtos;
using Entities.Dtos.LibraryDtos;
using Entities.Dtos.SongDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class Messages
    {
        public static string AlbumAdded = "Album başarılı bir şekilde eklendi.";
        public static string AlbumUpdated = "Album başarılı bir şekilde güncellendi.";
        public static string AlbumDeleted = "Album başarılı bir şekilde silindi.";
        public static string AlbumList = "Başarılı";
        public static string AlbumStatus = "Album statusu güncellenmiştir.";
        public static string AlbumStatusError = "Böyle bir album yok!";

        public static string FavoriteAdded = "Şarkı favorilerinize eklendi.";
        public static string FavoriteDeleted = "Şarkı başarılı bir şekilde silinmiştir.";
        public static string FavoriteUpdated = "Favori şarkınız güncellenmiştir.";
        public static string FavoriteNotFound = "Favori şarkı güncellenmedi.";
        public static string FavoriteList = "Başarılı";
        public static string FavoriteError = "Şarkı bulunamadı!";
        public static string FollowerStatusNotFound = "Böyle bir takipçi bulunamadı";

        public static string FollowerAdded = "Takip başarılı.";
        public static string FollowerUpdated = "Takipçi başarılı bir şekilde güncellenmiştir.";
        public static string FollowerDeleted = "Takipçi başarılı bir şekilde silinmiştir.";
        public static string FollowerList = "Başarılı";
        public static string FollowerStatus = "Takipçi statu güncellendi";

        public static string LibraryAdded = "Kütüphane başarılı bir şekilde eklenmiştir.";
        public static string LibraryUpdated = "Kütüphane başarılı bir şekilde güncellendi.";
        public static string LibraryDeleted = "Kütüphane başarılı bir şekilde silinmiştir.";
        public static string LibraryList = "Başarılı";
        public static string LibraryStatus = "Kütüphane statusu güncellenmiştir.";
        public static string LibraryStatusNotFound = "Böyle bir kütüphane yok!";

        public static string PlaylistAdded = "Playlist oluşturuldu.";
        public static string PlaylistDeleted = "Playlist başarılı bir şekilde silinmiştir.";
        public static string PlaylistUpdated = "Playlist başarılı bir şekilde güncellendi.";
        public static string PlaylistList = "Başarılı.";
        public static string PlaylistError = "Şarkı bulunamadığı için playliste eklenemedi";
        public static string PlaylistStatus = "Playlist statusu güncellendi";
        public static string PlaylistStatusNotFound = "Böyle bir playlist yok!";

        public static string SongAdded = "Şarkı başarılı bir şekilde eklendi.";
        public static string SongDeleted = "Şarkı başarılı bir şekilde silindi.";
        public static string SongList = "Başarılı";
        public static string SongUpdated = "Şarkı başarılı bir şekilde güncellendi.";
        public static string SongStatus = "Song statu güncellenmiştir.";
        public static string SongNotFound = "Böyle bir şarkı yok!";

        public static string UserNotFound = "Kullanıcı bulunamadı";
        public static string PasswordError = "Şifre hatalı";
        public static string SuccessDefaultLogin = "Sisteme giriş başarılı";
        public static string UserAlreadyExists = "Böyle bir email sistemde mevcut";
        public static string UserRegistered = "Kullanıcı başarılı bir şekilde kaydedildi.";
        public static string AccessTokenCreated = "Access Token başarılı şekilde oluşturuldu";
        public static string UserAdded = "Kullanıcı başarılı bir şekilde eklendi";
        public static string UserRole = "Kullanıcı rolü eklendi";
        public static string UserStatus = "Kullanıcı status güncellendi";
        public static string FollowerError = "Takipçi bulunamadı";
        public static string FollowError = "Takipçi bulunamadı";
        public static string UserControl = "Bu kullanıcı adı sistemde mevcut";
        public static string AlbumNotFound = "Bu Id'ye sahip bir albüm yok!";
        public static string FavoritesListed = "Kullanıcı favori şarkıları listelendi";
        public static string PlaylistNotFound = "Böyle bir playlist yok";
        public static string UserPlaylistNotFound = "Bu kullanıcıya ait bir playlist yok";
        public static string PlaylistAlreadyFollowed = "Bu playlist takip ediliyor";
        public static string PlaylistFollowed = "Playlist takip edildi";
        public static string PlaylistSongAdd = "Şarkı başarılı bir şekilde playliste eklendi";
        public static string PlaylistListed = "Playlistleriniz başarılı bir şekilde listelenmiştir.";
        public static string UserAlbumList = "Kullanıcı albümleri listelendi";
        public static string UserLibraryNotFound = "Bu kullanıcıya ait bir kütüphane bulunamadı";
    }
}

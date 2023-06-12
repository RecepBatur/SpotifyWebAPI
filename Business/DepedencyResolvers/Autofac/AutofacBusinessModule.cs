using Autofac;
using Business.Abstract;
using Business.Concrete;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using DataAccess.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DepedencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    { 
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AlbumManager>().As<IAlbumService>();
            builder.RegisterType<EfAlbumDal>().As<IAlbumDal>();

            builder.RegisterType<UserAlbumManager>().As<IUserAlbumService>();
            builder.RegisterType<EfUserAlbumDal>().As<IUserAlbumDal>();

            builder.RegisterType<FavoriteManager>().As<IFavoriteService>();
            builder.RegisterType<EfFavoriteDal>().As<IFavoriteDal>();

            builder.RegisterType<FollowerManager>().As<IFollowerService>();
            builder.RegisterType<EfFollowerDal>().As<IFollowerDal>();

            builder.RegisterType<LibraryManager>().As<ILibraryService>();
            builder.RegisterType<EfLibraryDal>().As<ILibraryDal>();

            builder.RegisterType<LibraryPlaylistManager>().As<ILibraryPlaylistService>();
            builder.RegisterType<EfLibraryPlaylistDal>().As<ILibraryPlaylistDal>();

            builder.RegisterType<LibraryAlbumManager>().As<ILibraryAlbumService>();
            builder.RegisterType<EfLibraryAlbumDal>().As<ILibraryAlbumDal>();

            builder.RegisterType<PlaylistManager>().As<IPlaylistService>();
            builder.RegisterType<EfPlaylistDal>().As<IPlaylistDal>();

            builder.RegisterType<PlaylistFollowingManager>().As<IPlaylistFollowingService>();
            builder.RegisterType<EfPlaylistFollowingDal>().As<IPlaylistFollowingDal>();

            builder.RegisterType<PlaylistSongManager>().As<IPlaylistSongService>();
            builder.RegisterType<EfPlaylistSongDal>().As<IPlaylistSongDal>();

            builder.RegisterType<SongManager>().As<ISongService>();
            builder.RegisterType<EfSongDal>().As<ISongDal>();

            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();

            builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();

            builder.RegisterType<UserRoleManager>().As<IUserRoleService>();
            builder.RegisterType<EfUserRoleDal>().As<IUserRoleDal>();
        }
    }
}

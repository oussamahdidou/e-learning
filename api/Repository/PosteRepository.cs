using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Comment;
using api.extensions;
using api.generique;
using api.helpers;
using api.interfaces;
using api.Model;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class PosteRepository : IPosteRepository
    {

        private readonly apiDbContext _context;
        public PosteRepository(apiDbContext context)
        {
            _context = context;
        }
        public async Task<Result<PosteDto>> GetPostById(int id)
        {
            PosteDto? poste = await _context.postes.Include(x => x.AppUser).Include(x => x.Comments).Where(x => x.Id == id)
        .Select(poste => new PosteDto
        {
            Id = poste.Id,
            Author = poste.AppUser.UserName,
            Content = poste.Content,
            Titre = poste.Titre,
            CommentsNumber = poste.Comments.Count(),
            CreatedAt = poste.CreatedAt,
            Image = poste.Image,
            Fichier = poste.Fichier,
            IsAdminPoste = poste.AppUser is Admin,
            AuthorId = poste.AppUser.Id,
        })
        .FirstOrDefaultAsync();
            if (poste == null)
            {
                return Result<PosteDto>.Failure("Poste not found");
            }

            return Result<PosteDto>.Success(poste);
        }
        public async Task<Result<List<PosteDto>>> GetAllPosts(QueryObject queryObject)
        {
            var posteQuery = _context.postes
                .Include(p => p.Comments)
                .Include(x => x.AppUser)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(queryObject.Query))
            {
                posteQuery = posteQuery.Where(x => EF.Functions.Like(x.Titre, $"%{queryObject.Query}%")
                                                || EF.Functions.Like(x.Content, $"%{queryObject.Query}%"));
            }

            if (queryObject.SortBy == "most-responses")
            {
                posteQuery = posteQuery.OrderByDescending(p => p.Comments.Count);
            }

            if (queryObject.SortBy == "recent")
            {
                posteQuery = posteQuery.OrderByDescending(p => p.CreatedAt);
            }

            // Pagination
            int skip = (queryObject.PageNumber - 1) * queryObject.PageSize;
            posteQuery = posteQuery.Skip(skip).Take(queryObject.PageSize);

            // Step 1: Project data from the database excluding IsAdminPoste
            var result = await posteQuery
                .Select(p => new
                {
                    Id = p.Id,
                    Titre = p.Titre,
                    Content = p.Content,
                    CreatedAt = p.CreatedAt,
                    CommentsNumber = p.Comments.Count,
                    Author = p.AppUser.UserName,

                    AppUser = p.AppUser  // Include AppUser to check if it's Admin later
                })
                .ToListAsync();

            // Step 2: Perform in-memory mapping for IsAdminPoste
            var mappedResult = result.Select(p => new PosteDto
            {
                Id = p.Id,
                Titre = p.Titre,
                Content = p.Content,
                CreatedAt = p.CreatedAt,
                CommentsNumber = p.CommentsNumber,
                Author = p.Author,
                AuthorId = p.AppUser.Id,
                IsAdminPoste = p.AppUser is Admin  // Perform the type check in memory
            }).ToList();

            return Result<List<PosteDto>>.Success(mappedResult);
        }

        public async Task<Result<List<PosteDto>>> GetUserPosts(AppUser user, CommentQuery commentQuery)
        {
            var posteQuery = _context.postes.Where(x => x.AppUserId == user.Id).AsQueryable();
            int skip = (commentQuery.Page - 1) * 20;
            posteQuery = posteQuery.Skip(skip).Take(20);

            // Step 1: Project data from the database excluding IsAdminPoste
            var result = await posteQuery
                .Select(p => new
                {
                    Id = p.Id,
                    Titre = p.Titre,
                    Content = p.Content,
                    CreatedAt = p.CreatedAt,
                    CommentsNumber = p.Comments.Count,
                    Author = p.AppUser.UserName,

                    AppUser = p.AppUser  // Include AppUser to check if it's Admin later
                })
                .ToListAsync();

            // Step 2: Perform in-memory mapping for IsAdminPoste
            var mappedResult = result.Select(p => new PosteDto
            {
                Id = p.Id,
                Titre = p.Titre,
                Content = p.Content,
                CreatedAt = p.CreatedAt,
                CommentsNumber = p.CommentsNumber,
                Author = p.Author,
                AuthorId = p.AppUser.Id,
                IsAdminPoste = p.AppUser is Admin  // Perform the type check in memory
            }).ToList();

            return Result<List<PosteDto>>.Success(mappedResult);

        }

        public async Task AddAsync(Poste poste)
        {
            _context.postes.Add(poste);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Poste poste)
        {
            _context.postes.Update(poste);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var poste = await _context.postes.FindAsync(id);
            if (poste != null)
            {
                _context.postes.Remove(poste);
                await _context.SaveChangesAsync();
            }
        }
    }
}
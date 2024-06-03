using DocumentClassificationZonalOcr.Api.MappingExtensions;
using DocumentClassificationZonalOcr.Api.Models;
using DocumentClassificationZonalOcr.Api.Results;
using DocumentClassificationZonalOcr.Shared.Dtos;
using DocumentClassificationZonalOcr.Shared.Results;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DocumentClassificationZonalOcr.Api.Data.Repositories
{
    public class FormRepository : IFormRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FormRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Result<Form>> GetByIdAsync(int id)
        {
            var form = await _context.Set<Form>().FindAsync(id);
            return form != null ? Result.Success(form) : Result.Failure<Form>("Form not found.");
        }

        public async Task<Result<Form>> CreateAsync(Form form)
        {
            _context.Set<Form>().Add(form);
            await _context.SaveChangesAsync();
            return Result.Success(form);
        }

        public async Task<Result<bool>> UpdateAsync(Form form)
        {
            _context.Entry(form).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Result.Success(true);
        }

        public async Task<Result<bool>> DeleteAsync(int id)
        {
            var form = await _context.Set<Form>().FindAsync(id);
            if (form == null)
                return Result.Failure<bool>("Form not found.");

            _context.Set<Form>().Remove(form);
            await _context.SaveChangesAsync();
            return Result.Success(true);
        }
        public async Task<Result<List<Zone>>> GetAllFormZonesAsync(int formId)
        {
            var form = await _context.Set<Form>().Include(f => f.Samples).ThenInclude(fs => fs.Zones).FirstOrDefaultAsync(f => f.Id == formId);

            if (form == null)
                return Result.Failure<List<Zone>>("FormRepository.GetAllFormZonesAsync", "Form not found.");

            var allZones = form.Samples.SelectMany(fs => fs.Zones).ToList();
            return Result.Success(allZones);
        }
        public async Task<Result<CustomList<FormDto>>> GetAllFormsAsync(DataTableOptionsDto options)
        {
            var query = _context.Set<Form>().AsQueryable();

            if (!string.IsNullOrEmpty(options.SearchText))
            {
                query = query.Where(f => f.Name.Contains(options.SearchText));
            }

            //if (!string.IsNullOrEmpty(options.OrderBy))
            //{
            //    query = ApplyOrderBy(query, options.OrderBy);
            //}

            int totalCount = await query.CountAsync();

            int totalPages = totalCount > 0 ? (int)Math.Ceiling((double)totalCount / options.Length) : 0;

            var forms = await query
                .Skip(options.Start)
                .Take(options.Length)
                .ToListAsync();

            var formDtos = forms.Select(f => new FormDto
            {
                Id = f.Id,
                Name = f.Name,
            });

            return Result.Success(formDtos.ToCustomList(totalCount, totalPages));
        }
        public async Task<Result<CustomList<FieldDto>>> GetFormFieldByIdAsync(int formId, DataTableOptionsDto options)
        {
            try
            {
                var query = _context.Set<Field>().Where(f => f.FormId == formId);

                if (!string.IsNullOrEmpty(options.SearchText))
                {
                    query = query.Where(f => f.Name.Contains(options.SearchText));
                }

                int totalCount = await query.CountAsync();
                int totalPages = totalCount > 0 ? (int)Math.Ceiling((double)totalCount / options.Length) : 0;

                var fields = await query
                    .Skip(options.Start)
                    .Take(options.Length)
                    .ToListAsync();

                var fieldDtos = fields.Select(field => field.ToDto());

                return Result.Success(fieldDtos.ToCustomList(totalCount, totalPages));
            }
            catch (Exception ex)
            {
                return Result.Failure<CustomList<FieldDto>>("An error occurred while fetching form fields.", ex.Message);
            }
        }

        public async Task<Result<CustomList<FormSampleDto>>> GetFormSampleByIdAsync(int formId, DataTableOptionsDto options)
        {
            try
            {
                var query = _context.Set<FormSample>().Where(fs => fs.FormId == formId);

                //if (!string.IsNullOrEmpty(options.SearchText))
                //{
                //    query = query.Where(fs => fs.Name.Contains(options.SearchText));
                //}

                int totalCount = await query.CountAsync();

                int totalPages = totalCount > 0 ? (int)Math.Ceiling((double)totalCount / options.Length) : 0;

                var formSamples = await query
                    .Skip(options.Start)
                    .Take(options.Length)
                    .ToListAsync();

                var request = _httpContextAccessor.HttpContext.Request;
                var baseUrl = $"{request.Scheme}://{request.Host.Value}";

                var formSampleDtos = formSamples.Select(formSample => formSample.ToDto(baseUrl));

                return Result.Success(formSampleDtos.ToCustomList(totalCount, totalPages));
            }
            catch (Exception ex)
            {
                return Result.Failure<CustomList<FormSampleDto>>("An error occurred while fetching form samples.", ex.Message);
            }
        }

        private IQueryable<Form> ApplyOrderBy(IQueryable<Form> query, string orderBy)
        {
            var parts = orderBy.Split(' ');
            var field = parts[0];
            var direction = parts.Length > 1 && parts[1].ToUpper() == "DESC" ? "Descending" : "Ascending";

            var parameter = Expression.Parameter(typeof(Form), "x");
            var property = Expression.Property(parameter, field);
            var lambda = Expression.Lambda(property, parameter);

            var methodName = direction == "Descending" ? "OrderByDescending" : "OrderBy";
            var orderByExpression = Expression.Call(
                typeof(Queryable),
                methodName,
                new[] { typeof(Form), property.Type },
                query.Expression,
                lambda
            );

            return query.Provider.CreateQuery<Form>(orderByExpression);
        }

    }
}

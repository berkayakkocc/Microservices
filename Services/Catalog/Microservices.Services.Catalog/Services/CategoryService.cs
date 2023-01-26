using AutoMapper;
using Microservices.Services.Catalog.Dtos;
using Microservices.Services.Catalog.Models;
using Microservices.Services.Catalog.Settings;
using Microservices.Shared.Dtos;
using MongoDB.Driver;

namespace Microservices.Services.Catalog.Services
{
    internal class CategoryService: ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IDatabaseSettings _databaseSettings;

        public CategoryService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            MongoClient client = new MongoClient(databaseSettings.ConnectionString);
            IMongoDatabase database = client.GetDatabase(databaseSettings.ConnectionString);
            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }
        public async Task<Response<List<CategoryDto>>> GetAllAsync()
        {
            Task<List<Category>> categories = _categoryCollection.Find(category => true).ToListAsync();

            return Response<List<CategoryDto>>.Success(_mapper.Map<List<CategoryDto>>(categories), 200);


        }
        public async Task<Response<CategoryDto>> CreateAsync(Category category)
        {
            await _categoryCollection.InsertOneAsync(category);

            return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(category), 200);
        }
        
        public async Task<Response<CategoryDto>> GetByIdAsync(string id)
        {
            Task<Category> categoryById = _categoryCollection.Find<Category>(x => x.Id == id).FirstOrDefaultAsync();

            if (categoryById==null)
            {
                return Response<CategoryDto>.Fail("Category not found", 404);
            }
            return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(categoryById), 200);


        }
    }
}

using Application.Messaging;
using Application.Messaging.Interfaces;
using AutoMapper;
using CoreSharp.Templates.Blazor.Application.Dtos.Products;
using CoreSharp.Templates.Blazor.Domain.Exceptions;
using CoreSharp.Templates.Blazor.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace CoreSharp.Templates.Blazor.Application.UseCases.GetProductById;

internal sealed class GetProductByIdHandler : IQueryHandler<GetProductById, ProductDto>
{
    // Fields
    private readonly IAppUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    // Constructors
    public GetProductByIdHandler(IAppUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    // Methods
    public async Task<IResult<ProductDto>> Handle(GetProductById request, CancellationToken cancellationToken)
    {
        var productRepository = _unitOfWork.ProductRepository;
        var productId = request.ProductId;
        var product = await productRepository.GetAsync(productId, request.Navigation, cancellationToken);
        _ = product ?? throw new ProductNotFoundException(productId);
        var productDto = _mapper.Map<ProductDto>(product);
        return Results.Ok(productDto);
    }
}

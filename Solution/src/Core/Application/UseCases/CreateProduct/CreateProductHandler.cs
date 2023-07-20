using AutoMapper;
using CoreSharp.Exceptions;
using CoreSharp.Templates.Blazor.Application.Dto;
using CoreSharp.Templates.Blazor.Application.Messaging.Interfaces;
using CoreSharp.Templates.Blazor.Application.Messaging.Results;
using CoreSharp.Templates.Blazor.Domain.Entities;
using CoreSharp.Templates.Blazor.Domain.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CoreSharp.Templates.Blazor.Application.UseCases.CreateProduct;

internal sealed class CreateProductHandler : ICommandHandler<CreateProduct, ProductDto>
{
    // Fields
    private readonly IAppUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    // Constructors
    public CreateProductHandler(IAppUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    // Methods
    public async Task<Result<ProductDto>> Handle(CreateProduct request, CancellationToken cancellationToken)
    {
        var productRepository = _unitOfWork.ProductRepository;
        var productdto = request.ProductDto;

        // Check if exists
        if (await productRepository.ExistsAsync(productdto.Id, cancellationToken))
        {
            throw EntityExistsException.Create<Product, Guid>(e => e.Id, productdto.Id);
        }

        // Create 
        var productToCreate = _mapper.Map<Product>(request.ProductDto);
        var createdProduct = await productRepository.AddAsync(productToCreate, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        // Return 
        return _mapper.Map<ProductDto>(createdProduct);
    }
}

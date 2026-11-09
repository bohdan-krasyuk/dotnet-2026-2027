using Domain.Products;
using MediatR;

namespace Application.Products.Commands;

public record UploadProductImageCommand : IRequest
{
    public required Stream FileStream { get; init; }
    public required string FileName { get; init; }
}

public class UploadProductImageCommandHandler : IRequestHandler<UploadProductImageCommand>
{
    public async Task Handle(UploadProductImageCommand request, CancellationToken cancellationToken)
    {
        var image = ProductImage.New(ProductImageId.New(), request.FileName);

        var write = new StreamWriter(image.FilePath);
        await request.FileStream.CopyToAsync(write.BaseStream);
        write.FlushAsync();
    }
}
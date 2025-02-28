
namespace Basket.API.Basket.DeleteBasket
{
    public record DeleteBasketResult(bool isSuccess = true);
    public record DeleteBasketCommand(string UserName) :ICommand<DeleteBasketResult>;
    public class DeleteBasketCommandHandler(IBasketRepository repository)
        : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
    {
        public async Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
        {
            await repository.DeleteBasket(command.UserName, cancellationToken);

            return new DeleteBasketResult();
        }
    }
}

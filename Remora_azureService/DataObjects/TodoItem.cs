using Microsoft.Azure.Mobile.Server;
using Remora_azure.Shared;

namespace Remora_azureService.DataObjects
{
    public class TodoItem : EntityData, ITodoItem
	{
		public string Text { get; set; }

		public bool Complete { get; set; }
	}
}
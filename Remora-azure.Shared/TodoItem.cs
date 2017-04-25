using System;

namespace Remora_azure.Shared
{
	public interface ITodoItem
	{
		string Id { get; set; }
		byte[] Version { get; set; }
		DateTimeOffset? CreatedAt { get; set; }
		DateTimeOffset? UpdatedAt { get; set; }
		bool Deleted { get; set; }
		string Text { get; set; }
		bool Complete { get; set; }
	}
}

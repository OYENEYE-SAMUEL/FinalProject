using Application.DTOs;

namespace WasteSystemApp.Models
{
	public class AssignCollectionViewModel
	{
			public ICollection<WasteCollectionResponseModel> WasteCollections { get; set; }
				= new HashSet<WasteCollectionResponseModel>();
			public ICollection<StaffResponseModel> StaffCollections { get; set; }
				= new HashSet<StaffResponseModel>();
			public Guid StaffId { get;  set; }

			public AssignCollectionViewModel()
			{
				StaffId = WasteCollections?.FirstOrDefault()?.StaffId ?? Guid.Empty;
			}
		
	}
}

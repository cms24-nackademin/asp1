using Domain.Models;

namespace WebApp.ViewModels;

public class MembersViewModel
{
    public IEnumerable<Member> Members { get; set; } = [];
    public AddMemberForm AddMemberForm { get; set; } = new AddMemberForm();
}

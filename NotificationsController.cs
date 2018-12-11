@model IEnumerable<JobPlacementDashboard.Models.JPBulletin>

<script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.3/umd/popper.min.js" integrity="sha384-vFJXuSJphROIrBnz7yo7oB41mKfc8JzQZiCq4NCceLEaO4IHwicKwpJf9c9IpFgh" crossorigin="anonymous"></script>

<div class="bulletin-wrapper">

    <div class="bulletin-board">
        @{
            if (ViewBag.ResultsFound)
            {
                /* Texture generated from https://www.cssmatic.com/noise-texture */
        <div id="yasin01" class="jumbotron border">
            <!-- Dropdown Button -->
            <div>
                <button class="btn-default btn-category dropdown-toggle" type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                    Categories<span class="caret"></span>
                </button>
                <ul class="dropdown-menu CatDropDown" id="CatDropDown">
                    <li class="CatDropDownItem">@Html.ActionLink("Advice", "Index", new { sortType = ViewBag.AdviceSortParm })</li>
                    <li class="CatDropDownItem">@Html.ActionLink("Job Postings", "Index", new { sortType = ViewBag.JobPostingSortParm })</li>
                    <li class="CatDropDownItem">@Html.ActionLink("Events", "Index", new { sortType = ViewBag.EventsSortParm })</li>
                    <li class="CatDropDownItem">@Html.ActionLink("All", "Index", new { sortType = ViewBag.AllSortParm })</li>
                </ul>
            </div>
            <h3 class="text-center">Job Placement Bulletins</h3>

            <div id="yasin02">

                @foreach (var item in Model)
                {
                    <div id="yasin03" class="card">

                        @if (User.IsInRole("Admin"))
                        {
                            <div class="pull-right" id="bulletin-trash">
                                @*@Html.ActionLink(HttpUtility.HtmlDecode("&#x1F5D1;"), "Delete", new { id = item.BulletinId})*@
                                @using (Html.BeginForm()) {
                                    @Html.AntiForgeryToken()
                                    <button class ="remove-card" id="@item.BulletinId" type="button"><i class="fa fa-trash"></i></button>
                                }
                            </div>
                        }

                        <div class="card-body" style="text-align: center">
                            <h5 class="card-title">@Html.Raw(@item.BulletinCategoryEnum)</h5>
                            <h6 class="card-subtitle mb-2 text-muted">@item.BulletinDate.ToString("MM-dd-yyyy")</h6>
                            <p class="card-text">@Html.Raw(@item.BulletinBody)</p>
                        </div>
                    </div>
                }
            </div>
        </div>
            }
            else
            {
                <div>
                    <h2>No @ViewBag.SortMessage to show at this time</h2>
                </div>
            }
        }
    </div>

    <div class="MeetupBulletin">
        @{Html.RenderAction("_MeetUpApi", "JPBulletins");}
    </div>
</div>

@section Scripts {
    @Scripts.Render("~/bundles/jqueryval")
    <script type="text/javascript">
        /* When the user clicks on the button,
toggle between hiding and showing the dropdown content */
        function myFunctionC() {
            document.getElementById("myDropdownCategory").classList.toggle("show");
        }

        // Close the dropdown menu if the user clicks outside of it
        window.onclick = function (event) {
            if (!event.target.matches('.dropbtnC')) {

                var dropdowns = document.getElementsByClassName("dropdownC-content");
                var i;
                for (i = 0; i < dropdowns.length; i++) {
                    var openDropdown = dropdowns[i];
                    if (openDropdown.classList.contains('show')) {
                        openDropdown.classList.remove('show');
                    }
                }
            }
        }

        //Delete Card from db and animate card deletion
        $(".remove-card").click(function () {
            id = this.getAttribute("id");
            link = "/JPBulletins/delete/" + id
            var $target = $(this).parents('#yasin03');
            $.ajax({
                type: "post",
                url: link,                
                success: $target.hide('slow', function () { $target.remove(); })         
            })
        })
    </script>
}
